CREATE PROCEDURE [dbo].[SearchUserId]
@UserId int
AS
	SELECT 
	User.UserId, 
	User.UserName, 
	Screenings.CloseContact, 
	Screenings.Travelled,
	Screenings.Symptoms
	FROM [User] 
	INNER JOIN [Screenings] 
	ON User.UserId=Screenings.ScreeningId
	WHERE User.UserId LIKE @UserId+'%'

