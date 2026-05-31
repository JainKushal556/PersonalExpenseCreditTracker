CREATE PROC spGetUpcomingLentReminders
    @UserID INT
AS
BEGIN
    DECLARE @Today DATE = CAST(GETDATE() AS DATE);
	DECLARE @StatusID INT;
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


		SELECT TOP 1 @StatusID = StatusID FROM tblLentBorrowStatus
				WHERE StatusName = 'Pending';

		IF @StatusID IS NULL
		BEGIN
			SELECT 'Pending Status Not Found' AS Message;
			RETURN;
		END

        IF NOT EXISTS
		(
			SELECT 1
			FROM tblLent
			WHERE UserID = @UserID
			AND StatusID = @StatusID
			AND DATEDIFF
			(
				DAY,
				@Today,
				CAST(DeadlineAt AS DATE)
			) IN (7,3,1)
		)
		BEGIN
			SELECT 'No Upcoming Pending Lent Found' AS Message;
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
			L.DeadlineAt,
			DATEDIFF(
					 DAY, 
					 @Today, 
					 CAST(L.DeadlineAt AS DATE)
					) AS RemainingDays,
			L.Description
		FROM tblLent L
		LEFT JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
		LEFT JOIN tblPersons Prsn ON L.PersonID = Prsn.PersonID
		LEFT JOIN tblPaymenttype Pay ON L.PaymentID = Pay.PaymentID
		WHERE L.UserID = @UserID
		AND L.DeadlineAt >= @Today
		AND L.StatusID = @StatusID
		AND DATEDIFF
		(
			DAY,
			@Today,
			CAST(L.DeadlineAt AS DATE)
		) IN (7,3,1)
		ORDER BY L.DeadlineAt ASC

    END TRY

    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS Message;
    END CATCH

END