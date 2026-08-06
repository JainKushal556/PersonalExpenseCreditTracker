CREATE PROCEDURE spFilterNoteByDateRange
    @UserID INT,
    @FromDate DATETIME,
    @ToDate DATETIME
AS
BEGIN

    IF NOT EXISTS
    (
        SELECT 1
        FROM tblUsers
        WHERE UserID = @UserID
    )
    BEGIN
        SELECT 'UserID Does Not Exists' AS Message;
        RETURN;
    END;


    IF @FromDate > @ToDate
    BEGIN
        SELECT 'FromDate Cannot Be Greater Than ToDate' AS Message;
        RETURN;
    END;


    IF NOT EXISTS
    (
        SELECT 1
        FROM tblNote
        WHERE UserID = @UserID
          AND CAST(CreatedAt AS DATE)
              BETWEEN CAST(@FromDate AS DATE)
              AND CAST(@ToDate AS DATE)
    )
    BEGIN
        SELECT 'No Notes Found For This Date Range' AS Message;
        RETURN;
    END;


    BEGIN TRY

        SELECT
            tblNote.NoteID,
            tblNote.NotePriorityID,
            tblNote.NoteColorID,

            tblNoteColor.ColorName,
            tblNoteColor.ColorHexCode,

            tblNote.NoteTitle,
            tblNote.Description,

            tblNotePriorities.NotePriorityName,

            tblNote.CreatedAt

        FROM tblNote

        LEFT JOIN tblNotePriorities
            ON tblNote.NotePriorityID =
               tblNotePriorities.NotePriorityID

        LEFT JOIN tblNoteColor
            ON tblNote.NoteColorID =
               tblNoteColor.NoteColorID

        WHERE tblNote.UserID = @UserID
          AND CAST(tblNote.CreatedAt AS DATE)
              BETWEEN CAST(@FromDate AS DATE)
              AND CAST(@ToDate AS DATE)

        ORDER BY tblNote.CreatedAt DESC;

    END TRY

    BEGIN CATCH

        SELECT ERROR_MESSAGE() AS Message;

    END CATCH

END
GO
