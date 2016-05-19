CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   NVARCHAR (128) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[AspNetUsers]([UserName] ASC);

	
GO


CREATE TABLE [dbo].[AspNetRoles] (
    [Id]   NVARCHAR (128) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([Name] ASC);

GO

CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     NVARCHAR (128) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserClaims]([UserId] ASC);

CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,
    [UserId]        NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserLogins]([UserId] ASC);
	
GO

CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] NVARCHAR (128) NOT NULL,
    [RoleId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserRoles]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_RoleId]
    ON [dbo].[AspNetUserRoles]([RoleId] ASC);

GO

CREATE TABLE [dbo].[Project] (
    [Id]               INT IDENTITY(1,1) NOT NULL,
	[Title]				NVARCHAR (MAX) NOT NULL,
    [ProjectArea]      NVARCHAR (MAX) NOT NULL,
    [Description]      NVARCHAR (MAX) NOT NULL,
    [SpecificProjects] NVARCHAR (MAX) NULL,
	[Impact]           NVARCHAR (MAX) NOT NULL,
    [Skills]           NVARCHAR (MAX) NULL,
    [SourceLink]           NVARCHAR (MAX) NULL,
    [SuggestedReading] NVARCHAR (MAX) NULL,
	[SuggestedMethods] NVARCHAR (MAX) NULL,
    [Date]             DATETIME       NOT NULL,
	[IsApproved]		BIT NOT NULL DEFAULT 0,
	[WasRevised]	BIT NOT NULL DEFAULT 0,
	[AddedByName]	NVARCHAR (256) NOT NULL,
	[AddedByEmail]	NVARCHAR (256) NOT NULL,
	[Comment]		NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Project] PRIMARY KEY CLUSTERED ([Id] ASC)

);




GO
CREATE NONCLUSTERED INDEX [IX_Project_IsApproved]
    ON [dbo].[Project]([IsApproved] ASC);

GO


CREATE TABLE [dbo].[SuitableLevel] (
    [Id]               INT  IDENTITY(1,1) NOT NULL,
    [Description]      NVARCHAR (MAX) NOT NULL,
	CONSTRAINT [PK_dbo.SuitableLevel] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO


CREATE TABLE [dbo].[SuitableLevel_Project] (
    [Id]               INT  IDENTITY(1,1) NOT NULL,
    [SuitableLevelId]      INT NOT NULL,
	[ProjectId]       INT NOT NULL,
	CONSTRAINT [PK_dbo.SuitableLevel_Project] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.SuitableLevel_Project.ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.SuitableLevel_Project.SuitableLevelId] FOREIGN KEY ([SuitableLevelId]) REFERENCES [dbo].[SuitableLevel] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [dbo].[Cause] (
    [Id]               INT  IDENTITY(1,1) NOT NULL,
    [Description]      NVARCHAR (MAX) NOT NULL,
	CONSTRAINT [PK_dbo.Cause] PRIMARY KEY CLUSTERED ([Id] ASC)
   
);

GO

CREATE TABLE [dbo].[Cause_Project] (
    [Id]               INT  IDENTITY(1,1) NOT NULL,
    [ProjectId]      INT NOT NULL,
	[CauseId]       INT NOT NULL,
	CONSTRAINT [PK_dbo.Cause_Project] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.Cause_Project.ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.Cause_Project.CauseId] FOREIGN KEY ([CauseId]) REFERENCES [dbo].[Cause] ([Id]) ON DELETE CASCADE
);

GO


CREATE TABLE [dbo].[SuitableSubject] (
    [Id]               INT   IDENTITY(1,1) NOT NULL,
    [Description]      NVARCHAR (MAX) NOT NULL
   CONSTRAINT [PK_dbo.SuitableSubject] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO


CREATE TABLE [dbo].[SuitableSubjects_Project] (
    [Id]               INT  IDENTITY(1,1) NOT NULL,
    [SuitableSubjectId]      INT NOT NULL,
	[ProjectId]       INT NOT NULL,
	CONSTRAINT [PK_dbo.SuitableSubjects_Project] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.SuitableSubjects_Project.ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.SuitableSubjects_Project.SuitableSubjectId] FOREIGN KEY ([SuitableSubjectId]) REFERENCES [dbo].[SuitableSubject] ([Id]) ON DELETE CASCADE
);

GO


CREATE TABLE [dbo].[Configurations] (
    [Id]   Int IDENTITY(1,1) NOT NULL,
    [Key] NVARCHAR (256) NOT NULL,
	[Value] NVARCHAR(MAX) NOT NULL,
    CONSTRAINT [PK_dbo.Configuration] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Configuration_Key]
    ON [dbo].[Configurations]([Key] ASC);

	


CREATE TABLE [dbo].[InterestedUsers_Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[UserName] NVARCHAR (256) NOT NULL,
	[UserEmail] NVARCHAR (256) NOT NULL,
 CONSTRAINT [PK_dbo.InterestedUsers_Projects] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [FK_dbo.InterestedUsers_Projects.ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id]) ON DELETE CASCADE
);
GO






