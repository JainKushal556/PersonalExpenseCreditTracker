# sync_db.ps1
# This script drops all stored procedures in the database and recreates them from MasterStoredProcedures.sql.
# It preserves all existing table data.

$appConfigPath = Join-Path $PSScriptRoot "..\..\WinFormsApp\PersonalExpenseCreditTracker\PersonalExpenseCreditTracker\App.config"
# Find the SQL script file using wildcard to avoid checkmark encoding issues
$spScriptPath = (Get-ChildItem -Path $PSScriptRoot -Filter "*MasterStoredProcedures.sql" | Select-Object -ExpandProperty FullName -First 1)

if (-not (Test-Path $appConfigPath)) {
    Write-Error "Could not find App.config at: $appConfigPath"
    exit
}

if (-not $spScriptPath -or -not (Test-Path $spScriptPath)) {
    Write-Error "Could not find Stored Procedures script."
    exit
}

# 1. Parse connection string from App.config
[xml]$config = Get-Content $appConfigPath
$connectionString = $config.configuration.connectionStrings.add | Where-Object { $_.name -eq "DBCS" } | Select-Object -ExpandProperty connectionString

if (-not $connectionString) {
    Write-Error "DBCS connection string not found in App.config"
    exit
}

Write-Host "Connecting to Database using connection string..." -ForegroundColor Cyan
Write-Host "Connection: $connectionString" -ForegroundColor Yellow

# Load SqlClient
Add-Type -AssemblyName "System.Data"

$connection = New-Object System.Data.SqlClient.SqlConnection($connectionString)

try {
    $connection.Open()
    Write-Host "Connected successfully!" -ForegroundColor Green

    # 2. Drop all existing stored procedures
    Write-Host "Dropping all existing stored procedures to avoid duplicate object conflicts..." -ForegroundColor Cyan
    
    $dropSql = @'
DECLARE @procName VARCHAR(500)
DECLARE cur CURSOR FOR
    SELECT '[' + SCHEMA_NAME(schema_id) + '].[' + name + ']' 
    FROM sys.procedures
OPEN cur
FETCH NEXT FROM cur INTO @procName
WHILE @@FETCH_STATUS = 0
BEGIN
    EXEC('DROP PROCEDURE ' + @procName)
    FETCH NEXT FROM cur INTO @procName
END
CLOSE cur
DEALLOCATE cur
'@

    $cmd = $connection.CreateCommand()
    $cmd.CommandText = $dropSql
    $cmd.ExecuteNonQuery() > $null
    Write-Host "All old stored procedures dropped successfully!" -ForegroundColor Green

    # 3. Read and execute script block by block (separated by GO)
    Write-Host "Applying new stored procedures..." -ForegroundColor Cyan
    $scriptContent = Get-Content $spScriptPath -Raw
    
    # Split the script by GO keyword (case-insensitive, on its own line)
    $blocks = $scriptContent -split "(?mi)^\s*GO\s*$"

    $successCount = 0
    $failCount = 0

    foreach ($block in $blocks) {
        $trimmedBlock = $block.Trim()
        if ($trimmedBlock.Length -eq 0) {
            continue
        }

        try {
            $cmd = $connection.CreateCommand()
            $cmd.CommandText = $trimmedBlock
            $cmd.ExecuteNonQuery() > $null
            $successCount++
        }
        catch {
            Write-Host "Error executing block:" -ForegroundColor Red
            Write-Host $_.Exception.Message -ForegroundColor DarkRed
            $failCount++
        }
    }

    Write-Host "`nSync Complete!" -ForegroundColor Green
    Write-Host "Successfully applied: $successCount procedures" -ForegroundColor Green
    if ($failCount -gt 0) {
        Write-Host "Failed to apply: $failCount procedures" -ForegroundColor Red
    }
}
catch {
    Write-Error "Database connection or execution failed: $_"
}
finally {
    if ($connection.State -eq [System.Data.ConnectionState]::Open) {
        $connection.Close()
    }
}
