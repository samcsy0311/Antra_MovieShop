IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Genres] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Genre] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211206164328_InitialMigration', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Genres] DROP CONSTRAINT [PK_Genres];
GO

EXEC sp_rename N'[Genres]', N'Genre';
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Genre]') AND [c].[name] = N'Name');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Genre] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Genre] ALTER COLUMN [Name] nvarchar(24) NOT NULL;
GO

ALTER TABLE [Genre] ADD CONSTRAINT [PK_Genre] PRIMARY KEY ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211206171330_UpdatingGenreTable', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Movie] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(256) NOT NULL,
    [Overview] nvarchar(max) NOT NULL,
    [Tagline] nvarchar(512) NOT NULL,
    [Budget] decimal(18,4) NULL DEFAULT 9.9,
    [Revenue] decimal(18,4) NULL DEFAULT 9.9,
    [ImdbUrl] nvarchar(2084) NOT NULL,
    [TmdbUrl] nvarchar(2084) NOT NULL,
    [PosterUrl] nvarchar(2084) NOT NULL,
    [BackdropUrl] nvarchar(2084) NOT NULL,
    [OriginalLanguage] nvarchar(64) NOT NULL,
    [ReleaseDate] datetime2 NULL,
    [RunTime] int NULL,
    [Price] decimal(5,2) NULL DEFAULT 9.9,
    [CreatedDate] datetime2 NULL DEFAULT (getdate()),
    [UpdatedDate] datetime2 NULL,
    [UpdatedBy] nvarchar(max) NOT NULL,
    [CreatedBy] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Movie] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211206172843_CreatingMovieTable', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Trailer] (
    [Id] int NOT NULL IDENTITY,
    [TrailerUrl] nvarchar(2084) NOT NULL,
    [Name] nvarchar(2084) NOT NULL,
    CONSTRAINT [PK_Trailer] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211206173521_CreatingTrailerTable', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Trailer] ADD [MovieId] int NOT NULL DEFAULT 0;
GO

CREATE INDEX [IX_Trailer_MovieId] ON [Trailer] ([MovieId]);
GO

ALTER TABLE [Trailer] ADD CONSTRAINT [FK_Trailer_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211206174952_MovieTrailerRelation', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Role] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211206185219_CreatingRoleTable', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [User] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(128) NOT NULL,
    [LastName] nvarchar(128) NOT NULL,
    [DateOfBirth] datetime2 NULL,
    [Email] nvarchar(256) NOT NULL,
    [HashedPassword] nvarchar(1024) NOT NULL,
    [Salt] nvarchar(1024) NOT NULL,
    [PhoneNumber] nvarchar(16) NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockOutEndDate] datetime2 NULL,
    [LastLoginDateTime] datetime2 NULL,
    [isLocked] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211206191336_CreatingUserTable', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Favorite] (
    [Id] int NOT NULL IDENTITY,
    [MovieId] int NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Favorite] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Favorite_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Favorite_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Favorite_MovieId] ON [Favorite] ([MovieId]);
GO

CREATE INDEX [IX_Favorite_UserId] ON [Favorite] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211206192035_CreatingFavoriteTable', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Purchase] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [PurchaseNumber] uniqueidentifier NOT NULL,
    [TotalPrice] decimal(18,2) NULL,
    [PurchaseDateTime] datetime2 NULL,
    [MovieId] int NOT NULL,
    CONSTRAINT [PK_Purchase] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Purchase_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Purchase_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Purchase_MovieId] ON [Purchase] ([MovieId]);
GO

CREATE INDEX [IX_Purchase_UserId] ON [Purchase] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211206192707_CreatingPurchaseTable', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [MovieGenre] (
    [MovieID] int NOT NULL,
    [GenreID] int NOT NULL,
    CONSTRAINT [PK_MovieGenre] PRIMARY KEY ([MovieID], [GenreID]),
    CONSTRAINT [FK_MovieGenre_Genre_GenreID] FOREIGN KEY ([GenreID]) REFERENCES [Genre] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MovieGenre_Movie_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_MovieGenre_GenreID] ON [MovieGenre] ([GenreID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211207161144_Movie-GenreConnection', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Review] (
    [MovieId] int NOT NULL,
    [UserId] int NOT NULL,
    [Rating] decimal(3,2) NULL,
    [ReviewText] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Review] PRIMARY KEY ([MovieId], [UserId]),
    CONSTRAINT [FK_Review_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Review_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Review_UserId] ON [Review] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211207162416_AddReviewTable', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[User]') AND [c].[name] = N'isLocked');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [User] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [User] ALTER COLUMN [isLocked] bit NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[User]') AND [c].[name] = N'TwoFactorEnabled');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [User] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [User] ALTER COLUMN [TwoFactorEnabled] bit NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[User]') AND [c].[name] = N'Salt');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [User] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [User] ALTER COLUMN [Salt] nvarchar(1024) NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[User]') AND [c].[name] = N'PhoneNumber');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [User] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [User] ALTER COLUMN [PhoneNumber] nvarchar(16) NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[User]') AND [c].[name] = N'LastName');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [User] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [User] ALTER COLUMN [LastName] nvarchar(128) NULL;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[User]') AND [c].[name] = N'HashedPassword');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [User] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [User] ALTER COLUMN [HashedPassword] nvarchar(1024) NULL;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[User]') AND [c].[name] = N'FirstName');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [User] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [User] ALTER COLUMN [FirstName] nvarchar(128) NULL;
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[User]') AND [c].[name] = N'AccessFailedCount');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [User] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [User] ALTER COLUMN [AccessFailedCount] int NULL;
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Review]') AND [c].[name] = N'Rating');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Review] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Review] ALTER COLUMN [Rating] decimal(3,2) NOT NULL;
ALTER TABLE [Review] ADD DEFAULT 0.0 FOR [Rating];
GO

CREATE TABLE [UserRole] (
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_UserRole_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Role] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRole_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_User_Email] ON [User] ([Email]);
GO

CREATE INDEX [IX_UserRole_RoleId] ON [UserRole] ([RoleId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211207165150_AddNullableValuesandReviewTable', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP INDEX [IX_User_Email] ON [User];
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[User]') AND [c].[name] = N'Email');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [User] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [User] ALTER COLUMN [Email] nvarchar(256) NULL;
GO

CREATE UNIQUE INDEX [IX_User_Email] ON [User] ([Email]) WHERE [Email] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211207165429_NullableUserEmail', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movie]') AND [c].[name] = N'UpdatedBy');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Movie] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [Movie] ALTER COLUMN [UpdatedBy] nvarchar(max) NULL;
GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movie]') AND [c].[name] = N'TmdbUrl');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Movie] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [Movie] ALTER COLUMN [TmdbUrl] nvarchar(2084) NULL;
GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movie]') AND [c].[name] = N'Tagline');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Movie] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [Movie] ALTER COLUMN [Tagline] nvarchar(512) NULL;
GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movie]') AND [c].[name] = N'PosterUrl');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Movie] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [Movie] ALTER COLUMN [PosterUrl] nvarchar(2084) NULL;
GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movie]') AND [c].[name] = N'Overview');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Movie] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [Movie] ALTER COLUMN [Overview] nvarchar(max) NULL;
GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movie]') AND [c].[name] = N'OriginalLanguage');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Movie] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [Movie] ALTER COLUMN [OriginalLanguage] nvarchar(64) NULL;
GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movie]') AND [c].[name] = N'ImdbUrl');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Movie] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [Movie] ALTER COLUMN [ImdbUrl] nvarchar(2084) NULL;
GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movie]') AND [c].[name] = N'CreatedBy');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Movie] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [Movie] ALTER COLUMN [CreatedBy] nvarchar(max) NULL;
GO

DECLARE @var19 sysname;
SELECT @var19 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movie]') AND [c].[name] = N'BackdropUrl');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Movie] DROP CONSTRAINT [' + @var19 + '];');
ALTER TABLE [Movie] ALTER COLUMN [BackdropUrl] nvarchar(2084) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211207170035_MovieTableNullable', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var20 sysname;
SELECT @var20 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Trailer]') AND [c].[name] = N'TrailerUrl');
IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [Trailer] DROP CONSTRAINT [' + @var20 + '];');
ALTER TABLE [Trailer] ALTER COLUMN [TrailerUrl] nvarchar(2084) NULL;
GO

DECLARE @var21 sysname;
SELECT @var21 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Trailer]') AND [c].[name] = N'Name');
IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Trailer] DROP CONSTRAINT [' + @var21 + '];');
ALTER TABLE [Trailer] ALTER COLUMN [Name] nvarchar(2084) NULL;
GO

DECLARE @var22 sysname;
SELECT @var22 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Role]') AND [c].[name] = N'Name');
IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [Role] DROP CONSTRAINT [' + @var22 + '];');
ALTER TABLE [Role] ALTER COLUMN [Name] nvarchar(20) NULL;
GO

DECLARE @var23 sysname;
SELECT @var23 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Review]') AND [c].[name] = N'ReviewText');
IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [Review] DROP CONSTRAINT [' + @var23 + '];');
ALTER TABLE [Review] ALTER COLUMN [ReviewText] nvarchar(max) NULL;
GO

DECLARE @var24 sysname;
SELECT @var24 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Purchase]') AND [c].[name] = N'TotalPrice');
IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [Purchase] DROP CONSTRAINT [' + @var24 + '];');
ALTER TABLE [Purchase] ALTER COLUMN [TotalPrice] decimal(18,2) NOT NULL;
ALTER TABLE [Purchase] ADD DEFAULT 0.0 FOR [TotalPrice];
GO

DECLARE @var25 sysname;
SELECT @var25 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Purchase]') AND [c].[name] = N'PurchaseDateTime');
IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [Purchase] DROP CONSTRAINT [' + @var25 + '];');
ALTER TABLE [Purchase] ALTER COLUMN [PurchaseDateTime] datetime2 NOT NULL;
ALTER TABLE [Purchase] ADD DEFAULT '0001-01-01T00:00:00.0000000' FOR [PurchaseDateTime];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211207180022_EditNullable', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Cast] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(128) NULL,
    [Gender] nvarchar(max) NULL,
    [TmdbUrl] nvarchar(max) NULL,
    [ProfilePath] nvarchar(2084) NULL,
    CONSTRAINT [PK_Cast] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211207180834_AddCastTable', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [MovieCast] (
    [MovieId] int NOT NULL,
    [CastId] int NOT NULL,
    [Character] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_MovieCast] PRIMARY KEY ([MovieId], [CastId], [Character]),
    CONSTRAINT [FK_MovieCast_Cast_CastId] FOREIGN KEY ([CastId]) REFERENCES [Cast] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MovieCast_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_MovieCast_CastId] ON [MovieCast] ([CastId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211207181305_MovieCastTable', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Crew] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(128) NULL,
    [Gender] nvarchar(max) NULL,
    [TmdbUrl] nvarchar(max) NULL,
    [ProfilePath] nvarchar(2084) NULL,
    CONSTRAINT [PK_Crew] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211207181810_AddCrewTable', N'6.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [MovieCrew] (
    [MovieId] int NOT NULL,
    [CrewID] int NOT NULL,
    [Department] nvarchar(128) NOT NULL,
    [Job] nvarchar(128) NOT NULL,
    CONSTRAINT [PK_MovieCrew] PRIMARY KEY ([MovieId], [CrewID], [Department], [Job]),
    CONSTRAINT [FK_MovieCrew_Crew_CrewID] FOREIGN KEY ([CrewID]) REFERENCES [Crew] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MovieCrew_Movie_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movie] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_MovieCrew_CrewID] ON [MovieCrew] ([CrewID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211207182511_AddMovieCrewTable', N'6.0.0');
GO

COMMIT;
GO

