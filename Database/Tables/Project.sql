CREATE TABLE [dbo].[Project] (
    [Id]               INT IDENTITY(1,1) NOT NULL,
    [ProjectArea]      NVARCHAR (MAX) NOT NULL,
    [Description]      NVARCHAR (MAX) NOT NULL,
    [SpecificProjects] NVARCHAR (MAX) NULL,
	[Impact]           NVARCHAR (MAX) NOT NULL,
    [SuitableSubjectId] INT NOT NULL,
    [CauseId]            INT NOT NULL,
    [SuitableLevelId]    INT NOT NULL,
    [Skills]           NVARCHAR (MAX) NULL,
    [SourceLink]           NVARCHAR (MAX) NULL,
    [SuggestedReading] NVARCHAR (MAX) NULL,
	[SuggestedMethods] NVARCHAR (MAX) NULL,
    [UserId]           NVARCHAR (128) NOT NULL,
    [Date]             DATETIME       NOT NULL,
	[IsApproved]		BIT NOT NULL DEFAULT 0,
    CONSTRAINT [PK_dbo.Project] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Project.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
	CONSTRAINT [FK_dbo.Project.Cause_CauseId] FOREIGN KEY ([CauseId]) REFERENCES [dbo].[Cause] ([Id]),
	CONSTRAINT [FK_dbo.Project.SuitableSubject_SuitableSubjectId] FOREIGN KEY ([SuitableSubjectId]) REFERENCES [dbo].[SuitableSubject] ([Id]),
	CONSTRAINT [FK_dbo.Project.SuitableSubject_SuitablelevelId] FOREIGN KEY ([SuitableLevelId]) REFERENCES [dbo].[SuitableLevel] ([Id])
);

GO
CREATE NONCLUSTERED INDEX [IX_SuitableSubjects]
    ON [dbo].[Project]([SuitableSubjectId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Cause]
    ON [dbo].[Project]([CauseId] ASC);

