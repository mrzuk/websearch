CREATE TABLE [dbo].[Cause_Project] (
    [Id]               INT  IDENTITY(1,1) NOT NULL,
    [ProjectId]      INT NOT NULL,
	[CauseId]       INT NOT NULL,
	CONSTRAINT [PK_dbo.Cause_Project] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.Cause_Project.ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id]),
	CONSTRAINT [FK_dbo.Cause_Project.CauseId] FOREIGN KEY ([CauseId]) REFERENCES [dbo].[Cause] ([Id])
);

GO
