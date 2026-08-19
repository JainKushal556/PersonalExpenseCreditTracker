# powershell -ExecutionPolicy Bypass -File "e:\Dekstop Application\PersonalExpenseCreditTracker\Database\Master\sync_db.ps1"


# sync_db.ps1
# This script ensures the complete database environment exists:
# 1. Creates the Database if it doesn't exist.
# 2. Creates the Schema (Tables) if no tables exist (or if -ForceRecreate is passed).
# 3. Inserts Master Seed Data if schema was created.
# 4. Drops and recreates all Stored Procedures from MasterStoredProcedures.sql.

param (
    [switch]$ForceRecreate = $false
)

$appConfigPath = Join-Path $PSScriptRoot "..\..\WinFormsApp\PersonalExpenseCreditTracker\PersonalExpenseCreditTracker\App.config"

# Find script files using wildcards
$schemaScriptPath = (Get-ChildItem -Path $PSScriptRoot -Filter "*MasterSchema.sql" | Select-Object -ExpandProperty FullName -First 1)
$seedScriptPath   = (Get-ChildItem -Path $PSScriptRoot -Filter "*NewMasterSeedData.sql" | Select-Object -ExpandProperty FullName -First 1)
if (-not $seedScriptPath) {
    $seedScriptPath = (Get-ChildItem -Path $PSScriptRoot -Filter "*MasterSeedData.sql" | Select-Object -ExpandProperty FullName -First 1)
}
$spScriptPath     = (Get-ChildItem -Path $PSScriptRoot -Filter "*MasterStoredProcedures.sql" | Select-Object -ExpandProperty FullName -First 1)

if (-not (Test-Path $appConfigPath)) {
    Write-Error "Could not find App.config at: $appConfigPath"
    exit
}

# 1. Parse connection string from App.config
[xml]$config = Get-Content $appConfigPath
$connectionString = $config.configuration.connectionStrings.add | Where-Object { $_.name -eq "DBCS" } | Select-Object -ExpandProperty connectionString

if (-not $connectionString) {
    Write-Error "DBCS connection string not found in App.config"
    exit
}

Add-Type -AssemblyName "System.Data"

$csBuilder = New-Object System.Data.SqlClient.SqlConnectionStringBuilder($connectionString)
$targetDb = if ($csBuilder.ContainsKey("database")) { $csBuilder["database"] } else { $csBuilder.InitialCatalog }
if ([string]::IsNullOrWhiteSpace($targetDb)) {
    $targetDb = "dbPersonalExpenseCreditTracker"
}

Write-Host "Target Database: $targetDb" -ForegroundColor Yellow
Write-Host "Server Instance: $($csBuilder.DataSource)" -ForegroundColor Yellow

# Helper function to execute multi-block SQL scripts
function Invoke-SqlScript {
    param (
        [System.Data.SqlClient.SqlConnection]$Connection,
        [string]$FilePath,
        [string]$StepName
    )

    if (-not $FilePath -or -not (Test-Path $FilePath)) {
        Write-Warning "Script file for '$StepName' not found: $FilePath"
        return
    }

    Write-Host "`n---> Applying $StepName ($([System.IO.Path]::GetFileName($FilePath)))..." -ForegroundColor Cyan
    $scriptContent = Get-Content $FilePath -Raw
    $blocks = $scriptContent -split "(?mi)^\s*GO\s*$"

    $successCount = 0
    $failCount = 0

    foreach ($block in $blocks) {
        $trimmedBlock = $block.Trim()
        if ($trimmedBlock.Length -eq 0) { continue }

        try {
            $cmd = $Connection.CreateCommand()
            $cmd.CommandText = $trimmedBlock
            $cmd.CommandTimeout = 120
            $cmd.ExecuteNonQuery() > $null
            $successCount++
        }
        catch {
            Write-Host "Error executing $StepName block:" -ForegroundColor Red
            Write-Host $_.Exception.Message -ForegroundColor DarkRed
            $failCount++
        }
    }

    if ($failCount -eq 0) {
        Write-Host "$StepName applied successfully! ($successCount blocks executed)" -ForegroundColor Green
    } else {
        Write-Host "$StepName finished with $successCount succeeded, $failCount failed blocks." -ForegroundColor Yellow
    }
}

# 2. Check and Create Database using 'master' catalog connection
$masterBuilder = New-Object System.Data.SqlClient.SqlConnectionStringBuilder($connectionString)
if ($masterBuilder.ContainsKey("database")) {
    $masterBuilder["database"] = "master"
} else {
    $masterBuilder.InitialCatalog = "master"
}
$masterConn = New-Object System.Data.SqlClient.SqlConnection($masterBuilder.ConnectionString)

try {
    Write-Host "`nConnecting to master database to verify '$targetDb' exists..." -ForegroundColor Cyan
    $masterConn.Open()

    $checkDbCmd = $masterConn.CreateCommand()
    $checkDbCmd.CommandText = "SELECT COUNT(*) FROM sys.databases WHERE name = '$targetDb'"
    $dbExists = ($checkDbCmd.ExecuteScalar() -gt 0)

    if (-not $dbExists) {
        Write-Host "Database '$targetDb' does not exist. Creating database now..." -ForegroundColor Yellow
        $createDbCmd = $masterConn.CreateCommand()
        $createDbCmd.CommandText = "CREATE DATABASE [$targetDb]"
        $createDbCmd.ExecuteNonQuery() > $null
        Write-Host "Database '$targetDb' created successfully!" -ForegroundColor Green
    } else {
        Write-Host "Database '$targetDb' already exists." -ForegroundColor Green
    }
}
catch {
    Write-Error "Failed to connect to server master database: $_"
    exit
}
finally {
    if ($masterConn.State -eq [System.Data.ConnectionState]::Open) {
        $masterConn.Close()
    }
}

# 3. Connect to Target Database & Initialize Schema / Seed Data / SPs
$connection = New-Object System.Data.SqlClient.SqlConnection($connectionString)

try {
    Write-Host "`nConnecting to '$targetDb'..." -ForegroundColor Cyan
    $connection.Open()
    Write-Host "Connected successfully to '$targetDb'!" -ForegroundColor Green

    # Check if tables exist
    $checkTablesCmd = $connection.CreateCommand()
    $checkTablesCmd.CommandText = "SELECT COUNT(*) FROM sys.tables"
    $tableCount = $checkTablesCmd.ExecuteScalar()

    $shouldBuildSchema = ($tableCount -eq 0) -or $ForceRecreate

    if ($shouldBuildSchema) {
        if ($tableCount -eq 0) {
            Write-Host "`nNo tables found in '$targetDb'. Building full schema and seed data..." -ForegroundColor Yellow
        } else {
            Write-Host "`n-ForceRecreate switch specified. Re-building schema and seed data..." -ForegroundColor Yellow
        }

        # Apply Master Schema
        Invoke-SqlScript -Connection $connection -FilePath $schemaScriptPath -StepName "Database Schema (Tables)"

        # Apply Master Seed Data
        Invoke-SqlScript -Connection $connection -FilePath $seedScriptPath -StepName "Master Seed Data"
    } else {
        Write-Host "Tables already exist in '$targetDb' ($tableCount tables found). Skipping Schema and Seed Data." -ForegroundColor Green
    }

    # 4. Always Drop and Re-create Stored Procedures
    Write-Host "`nDropping all existing stored procedures..." -ForegroundColor Cyan
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
    Write-Host "All existing stored procedures dropped successfully!" -ForegroundColor Green

    # Apply Stored Procedures
    Invoke-SqlScript -Connection $connection -FilePath $spScriptPath -StepName "Stored Procedures"

    Write-Host "`n==========================================" -ForegroundColor Green
    Write-Host " Database Sync & Setup Completed Successfully! " -ForegroundColor Green
    Write-Host "==========================================" -ForegroundColor Green
}
catch {
    Write-Error "Database execution failed: $_"
}
finally {
    if ($connection.State -eq [System.Data.ConnectionState]::Open) {
        $connection.Close()
    }
}

