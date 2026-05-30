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

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblLent
            WHERE UserID = @UserID
            AND DeadlineAt >= @Today
            AND StatusID = 1
        )
        BEGIN
            SELECT 'No Pending Upcoming Lent Found' AS Message;
            RETURN;
        END

		SELECT @StatusID = StatusID FROM tblLentBorrowStatus
				WHERE StatusName = 'Pending';

        SELECT L.LentID,
			Prsn.PersonName,
			L.Amount,
			L.ReturnedAmount,
			L.RemainingAmount,
			Pay.PaymentName,
			S.StatusName,
			L.LentAt,
			L.DeadlineAt,
			DATEDIFF(DAY, @Today, L.DeadlineAt) AS RemainingDays,
			L.Description
		FROM tblLent L
		LEFT JOIN tblLentBorrowStatus S ON L.StatusID = S.StatusID
		LEFT JOIN tblPersons Prsn ON L.PersonID = Prsn.PersonID
		LEFT JOIN tblPaymenttype Pay ON L.PaymentID = Pay.PaymentID
		WHERE L.UserID = @UserID
		AND L.DeadlineAt >= @Today
		AND L.StatusID = @StatusID
		ORDER BY L.DeadlineAt ASC

    END TRY

    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS Message;
    END CATCH

END

--first ee statusid=1 use korchis okhane direct number na diye name diye table theke id khuj then variable ee assin kore variable take ue kor 
-- sob validation e id use kor name er jaygay
-- pending status na thakle seta o check nae 
-- deadline cehck ee all pending diye debe but upcoming reminder ee sudhu 7,3,1 day er reminder debe seta o check korbe
-- . DATEDIFF(DAY, @Today, L.DeadlineAt) ae tay deadline take cast kor then function use kor better answer asbe
-- status id direct 1 use korche 