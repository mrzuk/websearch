CREATE TABLE [dbo].[SuitableSubjects_Project] (
    [Id]               INT  IDENTITY(1,1) NOT NULL,
    [SuitableSubjectId]      INT NOT NULL,
	[ProjectId]       INT NOT NULL,
	CONSTRAINT [PK_dbo.SuitableSubjects_Project] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.SuitableSubjects_Project.ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id]),
	CONSTRAINT [FK_dbo.SuitableSubjects_Project.SuitableSubjectId] FOREIGN KEY ([SuitableSubjectId]) REFERENCES [dbo].[SuitableSubject] ([Id])
);

GO
