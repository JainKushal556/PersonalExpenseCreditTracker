CREATE PROC spInsertLent
	@UserID INT,
	@PersonID INT,
	@PaymentID INT,
	@StatusID INT,
	@Amount DECIMAL(10,2),
	@ReturnedAmount DECIMAL(10,2),
	@RemainingAmount DECIMAL(10,2),
	@DeadlineAT DATETIME,
	@Description VARCHAR(MAX)
AS
BEGIN
	INSERT INTO tblLent
	(UserID, PersonID, PaymentID, StatusID, Amount, ReturnedAmount, RemainingAmount, DeadlineAt, Description)
	VALUES
	(@UserID,@PersonID,@PaymentID,@StatusID,@Amount,@ReturnedAmount,@RemainingAmount,@DeadlineAT,@Description);
END

EXEC spInsertLent
    @UserID = 1,
    @PersonID = 2,
    @PaymentID = 2,
    @StatusID = 3,
    @Amount = 5000.00,
    @ReturnedAmount = 0.00,
    @RemainingAmount = 5000.00,
    @DeadlineAT = '2026-06-06',
    @Description = 'Lent money for PG expense'

	SELECT * FROM tblLent


-- last ee ae test er code kokhon oo dibi na srtore procidure er modhe . 
-- expense table e insertion hoy ni loent korle seta to satghe sathe ee expense table ee insert hbe same procedure er under eee. 
-- amount validation nae kau 0 dile setao ase jbe kau negative amount dile setao ase jbe 
-- try catch nae jodi procedure eroor khay handle hbe ki kore ? 
-- succes message show korbe . 