CREATE PROCEDURE [dbo].[AddorEditUsers]
@mode nvarchar(10),
@UserId int,
@UserName nvarchar(10),
@UserType nvarchar(10),
@UserFlagged nvarchar(10)
AS
IF @mode='Add'
	BEGIN
	INSERT INTO [User]
	(
	UserId,
	UserName,
	UserType,
	UserFlagged
	)	
	VALUES
	(
	@UserId,
	@UserName,
	@UserType,
	@UserFlagged
	)
	END