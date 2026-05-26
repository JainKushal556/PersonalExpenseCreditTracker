CREATE PROCEDURE spCheckActiveUserWithDetails  
(  
    @UserID INT  
)  
AS  
BEGIN  
  
    IF EXISTS  
    (  
        SELECT 1  
        FROM tblUserAuthentication  
        WHERE   
            UserID = @UserID  
            AND Active = 1  
    )  
    BEGIN  
  
        SELECT   
            'User Is Active' AS Message,
            U.UserID,  
            U.UserName,  
            P.Name,  
            P.ProfilePhoto,  
            C.Email,  
            C.PhoneNumber,  
            A.Active,  
            U.CreatedAt  
        FROM tblUsers U  
        INNER JOIN tblUserProfile P  
            ON U.UserID = P.UserID  
        INNER JOIN tblUserContact C  
            ON U.UserID = C.UserID  
        INNER JOIN tblUserAuthentication A  
            ON U.UserID = A.UserID  
        WHERE   
            U.UserID = @UserID  
            AND A.Active = 1;  
  
    END  
  
    ELSE  
    BEGIN  
        SELECT 'User Is Not Active' AS Message;
    END  
  
END;