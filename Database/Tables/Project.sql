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

