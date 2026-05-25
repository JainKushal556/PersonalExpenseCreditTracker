CREATE PROC spInsertLent
	@UserID INT,
	@PersonID INT,
	@PaymentID INT,
	@StatusID INT,
	@Amount DECIMAL(10,2),
	@DeadlineAT DATETIME,
	@Description VARCHAR(MAX)
AS
BEGIN
	INSERT INTO tblLent
	(UserID, PersonID, PaymentID, StatusID, Amount, DeadlineAt, Description)
	VALUES
	(@UserID,@PersonID,@PaymentID,@StatusID,@Amount,@DeadlineAT,@Description);
END

EXEC spInsertLent
    @UserID = 1,
    @PersonID = 2,
    @PaymentID = 2,
    @StatusID = 3,
    @Amount = 5000.00,
    @DeadlineAT = '2026-06-06',
    @Description = 'Lent money for PG expense'

	SELECT * FROM tblLent