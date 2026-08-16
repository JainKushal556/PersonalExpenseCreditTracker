CREATE OR ALTER PROCEDURE spGetActiveUserId
AS
BEGIN

    SELECT UserID 
    FROM tblUserAuthentication 
    WHERE Active = 1;
END
GO
