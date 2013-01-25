/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Category
	DROP CONSTRAINT FK_Category_RoomCategory
GO
ALTER TABLE dbo.RoomCategory SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Category
	(
	CategoryID int NOT NULL IDENTITY (1, 1),
	Name varchar(255) NOT NULL,
	Name_en varchar(255) NOT NULL,
	Name_pl nvarchar(255) NOT NULL,
	CategoryContent varchar(MAX) NULL,
	CategoryContent_en varchar(MAX) NULL,
	CategoryContent_pl varchar(MAX) NULL,
	RoomCategoryID int NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Category SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Category ON
GO
IF EXISTS(SELECT * FROM dbo.Category)
	 EXEC('INSERT INTO dbo.Tmp_Category (CategoryID, Name, Name_en, Name_pl, CategoryContent, CategoryContent_en, CategoryContent_pl, RoomCategoryID)
		SELECT CategoryID, Name, Name_en, CONVERT(nvarchar(255), Name_pl), CategoryContent, CategoryContent_en, CategoryContent_pl, RoomCategoryID FROM dbo.Category WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Category OFF
GO
ALTER TABLE dbo.Gallery
	DROP CONSTRAINT FK_Gallery_Category
GO
DROP TABLE dbo.Category
GO
EXECUTE sp_rename N'dbo.Tmp_Category', N'Category', 'OBJECT' 
GO
ALTER TABLE dbo.Category ADD CONSTRAINT
	PK_Category PRIMARY KEY CLUSTERED 
	(
	CategoryID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Category ADD CONSTRAINT
	FK_Category_RoomCategory FOREIGN KEY
	(
	RoomCategoryID
	) REFERENCES dbo.RoomCategory
	(
	RoomCategoryID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Gallery ADD CONSTRAINT
	FK_Gallery_Category FOREIGN KEY
	(
	CategoryID
	) REFERENCES dbo.Category
	(
	CategoryID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Gallery SET (LOCK_ESCALATION = TABLE)
GO
COMMIT




/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Settings ADD
	Keywords varchar(250) NULL,
	Keywords_en varchar(250) NULL,
	Keywords_pl nvarchar(250) NULL,
	Description varchar(200) NULL,
	Description_en varchar(200) NULL,
	Description_pl nvarchar(200) NULL
GO
ALTER TABLE dbo.Settings SET (LOCK_ESCALATION = TABLE)
GO
COMMIT





/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Category ADD
	Keywords varchar(250) NULL,
	Keywords_en varchar(250) NULL,
	Keywords_pl nvarchar(250) NULL,
	Description varchar(200) NULL,
	Description_en varchar(200) NULL,
	Description_pl nvarchar(200) NULL
GO
ALTER TABLE dbo.Category SET (LOCK_ESCALATION = TABLE)
GO
COMMIT





IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('LoadSettingsByPrimaryKey') AND sysstat & 0xf = 4)
    DROP PROCEDURE [LoadSettingsByPrimaryKey];
GO

CREATE PROCEDURE [LoadSettingsByPrimaryKey]
(
	@SettingID int
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[SettingID],
		[UserName],
		[Password],
		[Keywords],
		[Keywords_en],
		[Keywords_pl],
		[Description],
		[Description_en],
		[Description_pl]
	FROM [Settings]
	WHERE
		([SettingID] = @SettingID)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: LoadSettingsByPrimaryKey Succeeded'
ELSE PRINT 'Procedure Creation: LoadSettingsByPrimaryKey Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('LoadAllSettings') AND sysstat & 0xf = 4)
    DROP PROCEDURE [LoadAllSettings];
GO

CREATE PROCEDURE [LoadAllSettings]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[SettingID],
		[UserName],
		[Password],
		[Keywords],
		[Keywords_en],
		[Keywords_pl],
		[Description],
		[Description_en],
		[Description_pl]
	FROM [Settings]

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: LoadAllSettings Succeeded'
ELSE PRINT 'Procedure Creation: LoadAllSettings Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('UpdateSettings') AND sysstat & 0xf = 4)
    DROP PROCEDURE [UpdateSettings];
GO

CREATE PROCEDURE [UpdateSettings]
(
	@SettingID int,
	@UserName varchar(50),
	@Password varchar(50),
	@Keywords varchar(250) = NULL,
	@Keywords_en varchar(250) = NULL,
	@Keywords_pl nvarchar(250) = NULL,
	@Description varchar(200) = NULL,
	@Description_en varchar(200) = NULL,
	@Description_pl nvarchar(200) = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	UPDATE [Settings]
	SET
		[UserName] = @UserName,
		[Password] = @Password,
		[Keywords] = @Keywords,
		[Keywords_en] = @Keywords_en,
		[Keywords_pl] = @Keywords_pl,
		[Description] = @Description,
		[Description_en] = @Description_en,
		[Description_pl] = @Description_pl
	WHERE
		[SettingID] = @SettingID


	SET @Err = @@Error


	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: UpdateSettings Succeeded'
ELSE PRINT 'Procedure Creation: UpdateSettings Error on Creation'
GO




IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('InsertSettings') AND sysstat & 0xf = 4)
    DROP PROCEDURE [InsertSettings];
GO

CREATE PROCEDURE [InsertSettings]
(
	@SettingID int = NULL output,
	@UserName varchar(50),
	@Password varchar(50),
	@Keywords varchar(250) = NULL,
	@Keywords_en varchar(250) = NULL,
	@Keywords_pl nvarchar(250) = NULL,
	@Description varchar(200) = NULL,
	@Description_en varchar(200) = NULL,
	@Description_pl nvarchar(200) = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [Settings]
	(
		[UserName],
		[Password],
		[Keywords],
		[Keywords_en],
		[Keywords_pl],
		[Description],
		[Description_en],
		[Description_pl]
	)
	VALUES
	(
		@UserName,
		@Password,
		@Keywords,
		@Keywords_en,
		@Keywords_pl,
		@Description,
		@Description_en,
		@Description_pl
	)

	SET @Err = @@Error

	SELECT @SettingID = SCOPE_IDENTITY()

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: InsertSettings Succeeded'
ELSE PRINT 'Procedure Creation: InsertSettings Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('DeleteSettings') AND sysstat & 0xf = 4)
    DROP PROCEDURE [DeleteSettings];
GO

CREATE PROCEDURE [DeleteSettings]
(
	@SettingID int
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	DELETE
	FROM [Settings]
	WHERE
		[SettingID] = @SettingID
	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: DeleteSettings Succeeded'
ELSE PRINT 'Procedure Creation: DeleteSettings Error on Creation'
GO





IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('LoadCategoryByPrimaryKey') AND sysstat & 0xf = 4)
    DROP PROCEDURE [LoadCategoryByPrimaryKey];
GO

CREATE PROCEDURE [LoadCategoryByPrimaryKey]
(
	@CategoryID int
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[CategoryID],
		[Name],
		[Name_en],
		[Name_pl],
		[CategoryContent],
		[CategoryContent_en],
		[CategoryContent_pl],
		[RoomCategoryID],
		[Keywords],
		[Keywords_en],
		[Keywords_pl],
		[Description],
		[Description_en],
		[Description_pl]
	FROM [Category]
	WHERE
		([CategoryID] = @CategoryID)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: LoadCategoryByPrimaryKey Succeeded'
ELSE PRINT 'Procedure Creation: LoadCategoryByPrimaryKey Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('LoadAllCategory') AND sysstat & 0xf = 4)
    DROP PROCEDURE [LoadAllCategory];
GO

CREATE PROCEDURE [LoadAllCategory]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[CategoryID],
		[Name],
		[Name_en],
		[Name_pl],
		[CategoryContent],
		[CategoryContent_en],
		[CategoryContent_pl],
		[RoomCategoryID],
		[Keywords],
		[Keywords_en],
		[Keywords_pl],
		[Description],
		[Description_en],
		[Description_pl]
	FROM [Category]

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: LoadAllCategory Succeeded'
ELSE PRINT 'Procedure Creation: LoadAllCategory Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('UpdateCategory') AND sysstat & 0xf = 4)
    DROP PROCEDURE [UpdateCategory];
GO

CREATE PROCEDURE [UpdateCategory]
(
	@CategoryID int,
	@Name varchar(255),
	@Name_en varchar(255),
	@Name_pl nvarchar(255),
	@CategoryContent varchar(MAX) = NULL,
	@CategoryContent_en varchar(MAX) = NULL,
	@CategoryContent_pl varchar(MAX) = NULL,
	@RoomCategoryID int = NULL,
	@Keywords varchar(250) = NULL,
	@Keywords_en varchar(250) = NULL,
	@Keywords_pl nvarchar(250) = NULL,
	@Description varchar(200) = NULL,
	@Description_en varchar(200) = NULL,
	@Description_pl nvarchar(200) = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	UPDATE [Category]
	SET
		[Name] = @Name,
		[Name_en] = @Name_en,
		[Name_pl] = @Name_pl,
		[CategoryContent] = @CategoryContent,
		[CategoryContent_en] = @CategoryContent_en,
		[CategoryContent_pl] = @CategoryContent_pl,
		[RoomCategoryID] = @RoomCategoryID,
		[Keywords] = @Keywords,
		[Keywords_en] = @Keywords_en,
		[Keywords_pl] = @Keywords_pl,
		[Description] = @Description,
		[Description_en] = @Description_en,
		[Description_pl] = @Description_pl
	WHERE
		[CategoryID] = @CategoryID


	SET @Err = @@Error


	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: UpdateCategory Succeeded'
ELSE PRINT 'Procedure Creation: UpdateCategory Error on Creation'
GO




IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('InsertCategory') AND sysstat & 0xf = 4)
    DROP PROCEDURE [InsertCategory];
GO

CREATE PROCEDURE [InsertCategory]
(
	@CategoryID int = NULL output,
	@Name varchar(255),
	@Name_en varchar(255),
	@Name_pl nvarchar(255),
	@CategoryContent varchar(MAX) = NULL,
	@CategoryContent_en varchar(MAX) = NULL,
	@CategoryContent_pl varchar(MAX) = NULL,
	@RoomCategoryID int = NULL,
	@Keywords varchar(250) = NULL,
	@Keywords_en varchar(250) = NULL,
	@Keywords_pl nvarchar(250) = NULL,
	@Description varchar(200) = NULL,
	@Description_en varchar(200) = NULL,
	@Description_pl nvarchar(200) = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [Category]
	(
		[Name],
		[Name_en],
		[Name_pl],
		[CategoryContent],
		[CategoryContent_en],
		[CategoryContent_pl],
		[RoomCategoryID],
		[Keywords],
		[Keywords_en],
		[Keywords_pl],
		[Description],
		[Description_en],
		[Description_pl]
	)
	VALUES
	(
		@Name,
		@Name_en,
		@Name_pl,
		@CategoryContent,
		@CategoryContent_en,
		@CategoryContent_pl,
		@RoomCategoryID,
		@Keywords,
		@Keywords_en,
		@Keywords_pl,
		@Description,
		@Description_en,
		@Description_pl
	)

	SET @Err = @@Error

	SELECT @CategoryID = SCOPE_IDENTITY()

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: InsertCategory Succeeded'
ELSE PRINT 'Procedure Creation: InsertCategory Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('DeleteCategory') AND sysstat & 0xf = 4)
    DROP PROCEDURE [DeleteCategory];
GO

CREATE PROCEDURE [DeleteCategory]
(
	@CategoryID int
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	DELETE
	FROM [Category]
	WHERE
		[CategoryID] = @CategoryID
	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: DeleteCategory Succeeded'
ELSE PRINT 'Procedure Creation: DeleteCategory Error on Creation'
GO


