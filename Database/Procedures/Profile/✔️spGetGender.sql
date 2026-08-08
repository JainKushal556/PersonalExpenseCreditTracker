CREATE PROCEDURE spGetGender
AS
BEGIN
    SET NOCOUNT OFF;

    SELECT 
        GenderID,
        GenderName
    FROM tblGender
    ORDER BY GenderID;
END
GO
