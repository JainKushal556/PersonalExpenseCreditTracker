CREATE PROCEDURE spGetAllNoteColors
AS
BEGIN
    BEGIN TRY
        SELECT
            NoteColorID,
            ColorName,
            ColorHexCode
        FROM tblNoteColor
        ORDER BY NoteColorID ASC;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS Message;
    END CATCH
END
