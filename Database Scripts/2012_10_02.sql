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
CREATE TABLE dbo.Category
	(
	CategoryID int NOT NULL IDENTITY (1, 1),
	Name varchar(255) NOT NULL,
	Name_en varchar(255) NOT NULL,
	Name_pl varchar(255) NOT NULL,
	CategoryContent varchar(MAX) NULL,
	CategoryContent_en varchar(MAX) NULL,
	CategoryContent_pl varchar(MAX) NULL,
	Price money NULL,
	IsRoom bit NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Category ADD CONSTRAINT
	DF_Category_IsRoom DEFAULT 0 FOR IsRoom
GO
ALTER TABLE dbo.Category ADD CONSTRAINT
	PK_Category PRIMARY KEY CLUSTERED 
	(
	CategoryID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Category SET (LOCK_ESCALATION = TABLE)
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
CREATE TABLE dbo.Gallery
	(
	GalleryID int NOT NULL IDENTITY (1, 1),
	CategoryID int NOT NULL,
	PhotoName varchar(50) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Gallery ADD CONSTRAINT
	PK_Gallery PRIMARY KEY CLUSTERED 
	(
	GalleryID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

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
ALTER TABLE dbo.Category SET (LOCK_ESCALATION = TABLE)
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




IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('LoadGalleryByPrimaryKey') AND sysstat & 0xf = 4)
    DROP PROCEDURE [LoadGalleryByPrimaryKey];
GO

CREATE PROCEDURE [LoadGalleryByPrimaryKey]
(
	@GalleryID int
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[GalleryID],
		[CategoryID],
		[PhotoName]
	FROM [Gallery]
	WHERE
		([GalleryID] = @GalleryID)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: LoadGalleryByPrimaryKey Succeeded'
ELSE PRINT 'Procedure Creation: LoadGalleryByPrimaryKey Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('LoadAllGallery') AND sysstat & 0xf = 4)
    DROP PROCEDURE [LoadAllGallery];
GO

CREATE PROCEDURE [LoadAllGallery]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[GalleryID],
		[CategoryID],
		[PhotoName]
	FROM [Gallery]

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: LoadAllGallery Succeeded'
ELSE PRINT 'Procedure Creation: LoadAllGallery Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('UpdateGallery') AND sysstat & 0xf = 4)
    DROP PROCEDURE [UpdateGallery];
GO

CREATE PROCEDURE [UpdateGallery]
(
	@GalleryID int,
	@CategoryID int,
	@PhotoName varchar(50)
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	UPDATE [Gallery]
	SET
		[CategoryID] = @CategoryID,
		[PhotoName] = @PhotoName
	WHERE
		[GalleryID] = @GalleryID


	SET @Err = @@Error


	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: UpdateGallery Succeeded'
ELSE PRINT 'Procedure Creation: UpdateGallery Error on Creation'
GO




IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('InsertGallery') AND sysstat & 0xf = 4)
    DROP PROCEDURE [InsertGallery];
GO

CREATE PROCEDURE [InsertGallery]
(
	@GalleryID int = NULL output,
	@CategoryID int,
	@PhotoName varchar(50)
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [Gallery]
	(
		[CategoryID],
		[PhotoName]
	)
	VALUES
	(
		@CategoryID,
		@PhotoName
	)

	SET @Err = @@Error

	SELECT @GalleryID = SCOPE_IDENTITY()

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: InsertGallery Succeeded'
ELSE PRINT 'Procedure Creation: InsertGallery Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('DeleteGallery') AND sysstat & 0xf = 4)
    DROP PROCEDURE [DeleteGallery];
GO

CREATE PROCEDURE [DeleteGallery]
(
	@GalleryID int
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	DELETE
	FROM [Gallery]
	WHERE
		[GalleryID] = @GalleryID
	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: DeleteGallery Succeeded'
ELSE PRINT 'Procedure Creation: DeleteGallery Error on Creation'
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
		[Price],
		[IsRoom]
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
		[Price],
		[IsRoom]
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
	@Name_pl varchar(255),
	@CategoryContent varchar(MAX) = NULL,
	@CategoryContent_en varchar(MAX) = NULL,
	@CategoryContent_pl varchar(MAX) = NULL,
	@Price money = NULL,
	@IsRoom bit
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
		[Price] = @Price,
		[IsRoom] = @IsRoom
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
	@Name_pl varchar(255),
	@CategoryContent varchar(MAX) = NULL,
	@CategoryContent_en varchar(MAX) = NULL,
	@CategoryContent_pl varchar(MAX) = NULL,
	@Price money = NULL,
	@IsRoom bit
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
		[Price],
		[IsRoom]
	)
	VALUES
	(
		@Name,
		@Name_en,
		@Name_pl,
		@CategoryContent,
		@CategoryContent_en,
		@CategoryContent_pl,
		@Price,
		@IsRoom
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
CREATE TABLE dbo.Settings
	(
	SettingID int NOT NULL IDENTITY (1, 1),
	UserName varchar(50) NOT NULL,
	Password varchar(50) NOT NULL
	)  ON [PRIMARY]
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
ALTER TABLE dbo.Settings ADD CONSTRAINT
	PK_Settings PRIMARY KEY CLUSTERED 
	(
	SettingID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Settings SET (LOCK_ESCALATION = TABLE)
GO
COMMIT




USE [Ekran]
GO

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
		[Password]
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
		[Password]
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
	@Password varchar(50)
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	UPDATE [Settings]
	SET
		[UserName] = @UserName,
		[Password] = @Password
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
	@Password varchar(50)
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [Settings]
	(
		[UserName],
		[Password]
	)
	VALUES
	(
		@UserName,
		@Password
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




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_Gallery_LoadByCategoryID]
(
	@CategoryID int
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT [GalleryID]
      ,[CategoryID]
      ,[PhotoName]
	FROM 
		[Gallery]
	WHERE
		[CategoryID] = @CategoryID

	SET @Err = @@Error

	RETURN @Err
END



INSERT INTO [Ekran].[dbo].[Settings]
           ([UserName]
           ,[Password])
     VALUES
           ('adminekran'
           ,'1v0FyCozWwWu3zreLP+l2A==')
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('Home'
           ,'Home'
           ,'Home'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('Overview'
           ,'Overview'
           ,'Overview'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('Gallery'
           ,'Gallery'
           ,'Gallery'
           )
GO



INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('Transfers'
           ,'Transfers'
           ,'Transfers'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('Services'
           ,'Services'
           ,'Services'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('Guestbook'
           ,'Guestbook'
           ,'Guestbook'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('Rooms & Rates'
           ,'Rooms & Rates'
           ,'Rooms & Rates'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('BEDROOM LUXE'
           ,'BEDROOM LUXE'
           ,'BEDROOM LUXE'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('STANDARD'
           ,'STANDARD'
           ,'STANDARD'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('WEDDING SUITE'
           ,'WEDDING SUITE'
           ,'WEDDING SUITE'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('EKONOM'
           ,'EKONOM'
           ,'EKONOM'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('Dining & Bars'
           ,'Dining & Bars'
           ,'Dining & Bars'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('Restaurant'
           ,'Restaurant'
           ,'Restaurant'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('Mini Restauran'
           ,'Mini Restauran'
           ,'Mini Restauran'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('COURTYARD'
           ,'COURTYARD'
           ,'COURTYARD'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('Summer Terrace'
           ,'Summer Terrace'
           ,'Summer Terrace'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('Mini Restaurant'
           ,'Mini Restaurant'
           ,'Mini Restaurant'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('Meetings and events'
           ,'Meetings and events'
           ,'Meetings and events'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('special events'
           ,'special events'
           ,'special events'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('Caterings'
           ,'Caterings'
           ,'Caterings'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('Conferences, seminars, trainings'
           ,'Conferences, seminars, trainings'
           ,'Conferences, seminars, trainings'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('Special offers'
           ,'Special offers'
           ,'Special offers'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('Contact'
           ,'Contact'
           ,'Contact'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('Contact us'
           ,'Contact us'
           ,'Contact us'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('Location'
           ,'Location'
           ,'Location'
           )
GO


INSERT INTO [Ekran].[dbo].[Category]
           ([Name]
           ,[Name_en]
           ,[Name_pl]
           )
     VALUES
           ('Booking'
           ,'Booking'
           ,'Booking'
           )
GO


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
CREATE TABLE dbo.RoomCategory
	(
	RoomCategoryID int NOT NULL IDENTITY (1, 1),
	Name varchar(50) NOT NULL,
	Name_en varchar(50) NOT NULL,
	Name_pl varchar(50) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.RoomCategory ADD CONSTRAINT
	PK_RoomCategory PRIMARY KEY CLUSTERED 
	(
	RoomCategoryID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.RoomCategory SET (LOCK_ESCALATION = TABLE)
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
CREATE TABLE dbo.Room
	(
	RoomID int NOT NULL IDENTITY (1, 1),
	Number varchar(50) NOT NULL,
	RoomCategoryID int NOT NULL,
	Price money NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Room ADD CONSTRAINT
	PK_Room PRIMARY KEY CLUSTERED 
	(
	RoomID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Room SET (LOCK_ESCALATION = TABLE)
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
ALTER TABLE dbo.RoomCategory SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Room ADD CONSTRAINT
	FK_Room_RoomCategory FOREIGN KEY
	(
	RoomCategoryID
	) REFERENCES dbo.RoomCategory
	(
	RoomCategoryID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Room SET (LOCK_ESCALATION = TABLE)
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
ALTER TABLE dbo.Category
	DROP CONSTRAINT DF_Category_IsRoom
GO
ALTER TABLE dbo.Category
	DROP COLUMN IsRoom
GO
ALTER TABLE dbo.Category SET (LOCK_ESCALATION = TABLE)
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
	RoomCategoryID int NULL
GO
ALTER TABLE dbo.Category SET (LOCK_ESCALATION = TABLE)
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
ALTER TABLE dbo.RoomCategory SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
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
ALTER TABLE dbo.Category SET (LOCK_ESCALATION = TABLE)
GO
COMMIT



IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('LoadRoomCategoryByPrimaryKey') AND sysstat & 0xf = 4)
    DROP PROCEDURE [LoadRoomCategoryByPrimaryKey];
GO

CREATE PROCEDURE [LoadRoomCategoryByPrimaryKey]
(
	@RoomCategoryID int
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[RoomCategoryID],
		[Name],
		[Name_en],
		[Name_pl]
	FROM [RoomCategory]
	WHERE
		([RoomCategoryID] = @RoomCategoryID)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: LoadRoomCategoryByPrimaryKey Succeeded'
ELSE PRINT 'Procedure Creation: LoadRoomCategoryByPrimaryKey Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('LoadAllRoomCategory') AND sysstat & 0xf = 4)
    DROP PROCEDURE [LoadAllRoomCategory];
GO

CREATE PROCEDURE [LoadAllRoomCategory]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[RoomCategoryID],
		[Name],
		[Name_en],
		[Name_pl]
	FROM [RoomCategory]

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: LoadAllRoomCategory Succeeded'
ELSE PRINT 'Procedure Creation: LoadAllRoomCategory Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('UpdateRoomCategory') AND sysstat & 0xf = 4)
    DROP PROCEDURE [UpdateRoomCategory];
GO

CREATE PROCEDURE [UpdateRoomCategory]
(
	@RoomCategoryID int,
	@Name varchar(50),
	@Name_en varchar(50),
	@Name_pl varchar(50)
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	UPDATE [RoomCategory]
	SET
		[Name] = @Name,
		[Name_en] = @Name_en,
		[Name_pl] = @Name_pl
	WHERE
		[RoomCategoryID] = @RoomCategoryID


	SET @Err = @@Error


	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: UpdateRoomCategory Succeeded'
ELSE PRINT 'Procedure Creation: UpdateRoomCategory Error on Creation'
GO




IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('InsertRoomCategory') AND sysstat & 0xf = 4)
    DROP PROCEDURE [InsertRoomCategory];
GO

CREATE PROCEDURE [InsertRoomCategory]
(
	@RoomCategoryID int = NULL output,
	@Name varchar(50),
	@Name_en varchar(50),
	@Name_pl varchar(50)
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [RoomCategory]
	(
		[Name],
		[Name_en],
		[Name_pl]
	)
	VALUES
	(
		@Name,
		@Name_en,
		@Name_pl
	)

	SET @Err = @@Error

	SELECT @RoomCategoryID = SCOPE_IDENTITY()

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: InsertRoomCategory Succeeded'
ELSE PRINT 'Procedure Creation: InsertRoomCategory Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('DeleteRoomCategory') AND sysstat & 0xf = 4)
    DROP PROCEDURE [DeleteRoomCategory];
GO

CREATE PROCEDURE [DeleteRoomCategory]
(
	@RoomCategoryID int
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	DELETE
	FROM [RoomCategory]
	WHERE
		[RoomCategoryID] = @RoomCategoryID
	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: DeleteRoomCategory Succeeded'
ELSE PRINT 'Procedure Creation: DeleteRoomCategory Error on Creation'
GO



IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('LoadRoomByPrimaryKey') AND sysstat & 0xf = 4)
    DROP PROCEDURE [LoadRoomByPrimaryKey];
GO

CREATE PROCEDURE [LoadRoomByPrimaryKey]
(
	@RoomID int
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[RoomID],
		[Number],
		[RoomCategoryID],
		[Price]
	FROM [Room]
	WHERE
		([RoomID] = @RoomID)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: LoadRoomByPrimaryKey Succeeded'
ELSE PRINT 'Procedure Creation: LoadRoomByPrimaryKey Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('LoadAllRoom') AND sysstat & 0xf = 4)
    DROP PROCEDURE [LoadAllRoom];
GO

CREATE PROCEDURE [LoadAllRoom]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[RoomID],
		[Number],
		[RoomCategoryID],
		[Price]
	FROM [Room]

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: LoadAllRoom Succeeded'
ELSE PRINT 'Procedure Creation: LoadAllRoom Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('UpdateRoom') AND sysstat & 0xf = 4)
    DROP PROCEDURE [UpdateRoom];
GO

CREATE PROCEDURE [UpdateRoom]
(
	@RoomID int,
	@Number varchar(50),
	@RoomCategoryID int,
	@Price money = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	UPDATE [Room]
	SET
		[Number] = @Number,
		[RoomCategoryID] = @RoomCategoryID,
		[Price] = @Price
	WHERE
		[RoomID] = @RoomID


	SET @Err = @@Error


	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: UpdateRoom Succeeded'
ELSE PRINT 'Procedure Creation: UpdateRoom Error on Creation'
GO




IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('InsertRoom') AND sysstat & 0xf = 4)
    DROP PROCEDURE [InsertRoom];
GO

CREATE PROCEDURE [InsertRoom]
(
	@RoomID int = NULL output,
	@Number varchar(50),
	@RoomCategoryID int,
	@Price money = NULL
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [Room]
	(
		[Number],
		[RoomCategoryID],
		[Price]
	)
	VALUES
	(
		@Number,
		@RoomCategoryID,
		@Price
	)

	SET @Err = @@Error

	SELECT @RoomID = SCOPE_IDENTITY()

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: InsertRoom Succeeded'
ELSE PRINT 'Procedure Creation: InsertRoom Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('DeleteRoom') AND sysstat & 0xf = 4)
    DROP PROCEDURE [DeleteRoom];
GO

CREATE PROCEDURE [DeleteRoom]
(
	@RoomID int
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	DELETE
	FROM [Room]
	WHERE
		[RoomID] = @RoomID
	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: DeleteRoom Succeeded'
ELSE PRINT 'Procedure Creation: DeleteRoom Error on Creation'
GO



USE [Ekran]
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
		[Price],
		[RoomCategoryID]
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
		[Price],
		[RoomCategoryID]
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
	@Name_pl varchar(255),
	@CategoryContent varchar(MAX) = NULL,
	@CategoryContent_en varchar(MAX) = NULL,
	@CategoryContent_pl varchar(MAX) = NULL,
	@Price money = NULL,
	@RoomCategoryID int = NULL
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
		[Price] = @Price,
		[RoomCategoryID] = @RoomCategoryID
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
	@Name_pl varchar(255),
	@CategoryContent varchar(MAX) = NULL,
	@CategoryContent_en varchar(MAX) = NULL,
	@CategoryContent_pl varchar(MAX) = NULL,
	@Price money = NULL,
	@RoomCategoryID int = NULL
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
		[Price],
		[RoomCategoryID]
	)
	VALUES
	(
		@Name,
		@Name_en,
		@Name_pl,
		@CategoryContent,
		@CategoryContent_en,
		@CategoryContent_pl,
		@Price,
		@RoomCategoryID
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




IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('usp_Room_LoadWithRoomCategoty') AND sysstat & 0xf = 4)
    DROP PROCEDURE [usp_Room_LoadWithRoomCategoty];
GO

CREATE PROCEDURE [usp_Room_LoadWithRoomCategoty]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT     
		Room.RoomID, 
		Room.Number, 
		Room.Price, 
		RoomCategory.Name AS RoomCategoryName
	FROM         
		Room 
	INNER JOIN RoomCategory ON Room.RoomCategoryID = RoomCategory.RoomCategoryID

	SET @Err = @@Error

	RETURN @Err
END
GO





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
ALTER TABLE dbo.Gallery ADD
	IsCover bit NOT NULL CONSTRAINT DF_Gallery_IsCover DEFAULT 0,
	ShowCommon bit NOT NULL CONSTRAINT DF_Gallery_ShowCommon DEFAULT 0
GO
ALTER TABLE dbo.Gallery SET (LOCK_ESCALATION = TABLE)
GO
COMMIT





USE [Ekran]
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('LoadGalleryByPrimaryKey') AND sysstat & 0xf = 4)
    DROP PROCEDURE [LoadGalleryByPrimaryKey];
GO

CREATE PROCEDURE [LoadGalleryByPrimaryKey]
(
	@GalleryID int
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[GalleryID],
		[CategoryID],
		[PhotoName],
		[IsCover],
		[ShowCommon]
	FROM [Gallery]
	WHERE
		([GalleryID] = @GalleryID)

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: LoadGalleryByPrimaryKey Succeeded'
ELSE PRINT 'Procedure Creation: LoadGalleryByPrimaryKey Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('LoadAllGallery') AND sysstat & 0xf = 4)
    DROP PROCEDURE [LoadAllGallery];
GO

CREATE PROCEDURE [LoadAllGallery]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT
		[GalleryID],
		[CategoryID],
		[PhotoName],
		[IsCover],
		[ShowCommon]
	FROM [Gallery]

	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: LoadAllGallery Succeeded'
ELSE PRINT 'Procedure Creation: LoadAllGallery Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('UpdateGallery') AND sysstat & 0xf = 4)
    DROP PROCEDURE [UpdateGallery];
GO

CREATE PROCEDURE [UpdateGallery]
(
	@GalleryID int,
	@CategoryID int,
	@PhotoName varchar(50),
	@IsCover bit,
	@ShowCommon bit
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	UPDATE [Gallery]
	SET
		[CategoryID] = @CategoryID,
		[PhotoName] = @PhotoName,
		[IsCover] = @IsCover,
		[ShowCommon] = @ShowCommon
	WHERE
		[GalleryID] = @GalleryID


	SET @Err = @@Error


	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: UpdateGallery Succeeded'
ELSE PRINT 'Procedure Creation: UpdateGallery Error on Creation'
GO




IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('InsertGallery') AND sysstat & 0xf = 4)
    DROP PROCEDURE [InsertGallery];
GO

CREATE PROCEDURE [InsertGallery]
(
	@GalleryID int = NULL output,
	@CategoryID int,
	@PhotoName varchar(50),
	@IsCover bit,
	@ShowCommon bit
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	INSERT
	INTO [Gallery]
	(
		[CategoryID],
		[PhotoName],
		[IsCover],
		[ShowCommon]
	)
	VALUES
	(
		@CategoryID,
		@PhotoName,
		@IsCover,
		@ShowCommon
	)

	SET @Err = @@Error

	SELECT @GalleryID = SCOPE_IDENTITY()

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: InsertGallery Succeeded'
ELSE PRINT 'Procedure Creation: InsertGallery Error on Creation'
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('DeleteGallery') AND sysstat & 0xf = 4)
    DROP PROCEDURE [DeleteGallery];
GO

CREATE PROCEDURE [DeleteGallery]
(
	@GalleryID int
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	DELETE
	FROM [Gallery]
	WHERE
		[GalleryID] = @GalleryID
	SET @Err = @@Error

	RETURN @Err
END
GO


-- Display the status of Proc creation
IF (@@Error = 0) PRINT 'Procedure Creation: DeleteGallery Succeeded'
ELSE PRINT 'Procedure Creation: DeleteGallery Error on Creation'
GO



IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('usp_Gallery_UpdateCoverByCategoryID') AND sysstat & 0xf = 4)
    DROP PROCEDURE [usp_Gallery_UpdateCoverByCategoryID];
GO

CREATE PROCEDURE [usp_Gallery_UpdateCoverByCategoryID]
(
	@CategoryID int
)
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	UPDATE [Gallery]
	SET
		[IsCover] = 0
	WHERE
		[CategoryID] = @CategoryID


	SET @Err = @@Error


	RETURN @Err
END
GO




USE [Ekran]
GO
/****** Object:  StoredProcedure [dbo].[usp_Gallery_LoadByCategoryID]    Script Date: 10/12/2012 13:33:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[usp_Gallery_LoadByCategoryID]
(
	@CategoryID int
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT [GalleryID]
      ,[CategoryID]
      ,[PhotoName]
      ,IsCover
      ,ShowCommon
	FROM 
		[Gallery]
	WHERE
		[CategoryID] = @CategoryID

	SET @Err = @@Error

	RETURN @Err
END




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
	DROP COLUMN Price
GO
ALTER TABLE dbo.Category SET (LOCK_ESCALATION = TABLE)
GO
COMMIT




USE [Ekran]
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
		[RoomCategoryID]
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
		[RoomCategoryID]
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
	@Name_pl varchar(255),
	@CategoryContent varchar(MAX) = NULL,
	@CategoryContent_en varchar(MAX) = NULL,
	@CategoryContent_pl varchar(MAX) = NULL,
	@RoomCategoryID int = NULL
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
		[RoomCategoryID] = @RoomCategoryID
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
	@Name_pl varchar(255),
	@CategoryContent varchar(MAX) = NULL,
	@CategoryContent_en varchar(MAX) = NULL,
	@CategoryContent_pl varchar(MAX) = NULL,
	@RoomCategoryID int = NULL
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
		[RoomCategoryID]
	)
	VALUES
	(
		@Name,
		@Name_en,
		@Name_pl,
		@CategoryContent,
		@CategoryContent_en,
		@CategoryContent_pl,
		@RoomCategoryID
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



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[udf_GetRoomList]') and xtype in (N'FN', N'IF', N'TF'))
drop function [dbo].[udf_GetRoomList]
GO


CREATE function [dbo].[udf_GetRoomList](
	@RoomCategoryID int,
	@Price money
) 
RETURNS 
	varchar(250)
begin
	DECLARE @RoomList varchar(250),
		@Number varchar(50)
	
	SET @RoomList = '¹ '
	
	DECLARE Room_crsr CURSOR
   	FOR
   	SELECT     
		Number
	FROM         
		Room 
	WHERE     
		RoomCategoryID = @RoomCategoryID 
		AND
		Price = @Price

	OPEN Room_crsr

	FETCH NEXT FROM Room_crsr INTO @Number
	   WHILE @@Fetch_Status = 0
	     BEGIN

		SET @RoomList = @RoomList + @Number + ', '

		FETCH NEXT FROM Room_crsr INTO @Number
	     END

	CLOSE Room_crsr
	DEALLOCATE Room_crsr

	IF LEN(@RoomList) > 0
	 BEGIN
		SET @RoomList = RTRIM(@RoomList)
		SET @RoomList = SUBSTRING(@RoomList,0,LEN(@RoomList))
	 END

	RETURN @RoomList
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO






IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('usp_Room_LoadPriceByRoomCategoryID') AND sysstat & 0xf = 4)
    DROP PROCEDURE [usp_Room_LoadPriceByRoomCategoryID];
GO

CREATE PROCEDURE [usp_Room_LoadPriceByRoomCategoryID]
(
	@RoomCategoryID int
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT 
		[Price]
      ,dbo.udf_GetRoomList([RoomCategoryID], [Price]) AS RoomList
	FROM 
		[dbo].[Room]
	WHERE
		RoomCategoryID = @RoomCategoryID
	GROUP BY
		[Price],
		[RoomCategoryID]
	ORDER BY
		[Price]

	SET @Err = @@Error

	RETURN @Err
END
GO







IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('usp_Gallery_LoadCoverByCategoryID') AND sysstat & 0xf = 4)
    DROP PROCEDURE [usp_Gallery_LoadCoverByCategoryID];
GO

CREATE PROCEDURE [usp_Gallery_LoadCoverByCategoryID]
(
	@CategoryID int
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	DECLARE @PhotoName varchar(50), 
		@PriceRange varchar(50),
		@RoomCategoryID int
	
	SET @PhotoName = ''
	
	SELECT TOP 1
		@PhotoName = [PhotoName]
	FROM 
		[dbo].[Gallery]
	WHERE
		CategoryID = @CategoryID
		AND
		[IsCover] = 1
	
	IF @PhotoName <> ''
	 BEGIN
		SELECT TOP 1
			@PhotoName = [PhotoName]
		FROM 
			[dbo].[Gallery]
		WHERE
			CategoryID = @CategoryID
	 END
	 
	SELECT 
		@RoomCategoryID = ISNULL(RoomCategoryID, 0)
	FROM
		Category
	WHERE
		CategoryID = @CategoryID
		
	IF @RoomCategoryID > 0
	 BEGIN
		DECLARE @MinPrice money,
			@MaxPrice money
			
		SET @MinPrice = 0
		SET @MaxPrice = 0
		
		SELECT
			@MinPrice = ISNULL(MIN(Price), 0)
		FROM
			Room
		WHERE
			RoomCategoryID = @RoomCategoryID
			
		SELECT
			@MaxPrice = ISNULL(MAX(Price), 0)
		FROM
			Room
		WHERE
			RoomCategoryID = @RoomCategoryID
		
		IF @MinPrice > 0 AND @MaxPrice > 0
		 BEGIN
			IF @MinPrice = @MaxPrice
			 BEGIN
				SET @PriceRange = CAST(@MinPrice AS varchar)
			 END
			 ELSE
			 BEGIN
				SET @PriceRange = CAST(@MinPrice AS varchar) + ' - ' + CAST(@MaxPrice AS varchar)
			 END
		 END
			
	 END
	 

	SELECT @PhotoName AS PhotoName, @PriceRange AS PriceRange
	
	SET @Err = @@Error

	RETURN @Err
END
GO





USE [Ekran]
GO
/****** Object:  StoredProcedure [dbo].[usp_Room_LoadWithRoomCategoty]    Script Date: 10/13/2012 18:14:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[usp_Room_LoadWithRoomCategoty]
AS
BEGIN

	SET NOCOUNT ON
	DECLARE @Err int

	SELECT     
		Room.RoomID, 
		Room.Number, 
		Room.Price, 
		RoomCategory.Name AS RoomCategoryName, 
		RoomCategory.Name_en AS RoomCategoryName_en, 
		RoomCategory.Name_pl AS RoomCategoryName_pl
	FROM         
		Room 
	INNER JOIN RoomCategory ON Room.RoomCategoryID = RoomCategory.RoomCategoryID

	SET @Err = @@Error

	RETURN @Err
END



IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('usp_Gallery_LoadGallery') AND sysstat & 0xf = 4)
    DROP PROCEDURE [usp_Gallery_LoadGallery];
GO

CREATE PROCEDURE [usp_Gallery_LoadGallery]
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @Err int

	SELECT [GalleryID]
      ,[CategoryID]
      ,[PhotoName]
      ,IsCover
      ,ShowCommon
	FROM 
		[Gallery]
	WHERE
		ShowCommon = 1
	
	SET @Err = @@Error

	RETURN @Err
END
GO



