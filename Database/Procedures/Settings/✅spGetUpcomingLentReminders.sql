CREATE PROC spGetUpcomingLentReminders
    @UserID INT
AS
BEGIN
    DECLARE @Today DATE = CAST(GETDATE() AS DATE);
	
    BEGIN TRY

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblUsers
            WHERE UserID = @UserID
        )
        BEGIN
            SELECT 'UserID Does Not Exist' AS Message;
            RETURN;
        END

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblLent
            WHERE UserID = @UserID
            AND DeadlineAt >= @Today
            AND StatusID = 1
        )
        BEGIN
            SELECT 'No Pending Upcoming Tasks Found' AS Message;
            RETURN;
        END

        SELECT L.LentID,
			Prsn.PersonName,
			L.Amount,
			L.ReturnedAmount,
			L.RemainingAmount,
			Pay.PaymentName,
			S.StatusName,
			L.LentAt,
			DATEDIFF(DAY, @Today, L.DeadlineAt) AS RemainingDays,
			L.Description
		FROM tblLent L
		LEFT JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
		LEFT JOIN tblPersons Prsn ON L.PersonID = Prsn.PersonID
		LEFT JOIN tblPaymenttype Pay ON L.PaymentID = Pay.PaymentID
		WHERE L.UserID = @UserID
		AND L.DeadlineAt >= @Today
		AND L.StatusID = 1
		ORDER BY L.DeadlineAt DESC

    END TRY

    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS Message;
    END CATCH

END
