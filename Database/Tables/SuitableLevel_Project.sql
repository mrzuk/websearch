CREATE TABLE [dbo].[SuitableLevel_Project] (
    [Id]               INT  IDENTITY(1,1) NOT NULL,
    [SuitableLevelId]      INT NOT NULL,
	[ProjectId]       INT NOT NULL,
	CONSTRAINT [PK_dbo.SuitableLevel_Project] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.SuitableLevel_Project.ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.SuitableLevel_Project.SuitableLevelId] FOREIGN KEY ([SuitableLevelId]) REFERENCES [dbo].[SuitableLevel] ([Id]) ON DELETE CASCADE
);

GO
