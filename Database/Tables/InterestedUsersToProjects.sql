CREATE TABLE [dbo].[InterestedUsers_Projects] (
    [Id]               INT  IDENTITY(1,1) NOT NULL,
	[ProjectId]       INT NOT NULL,
	[FullName]	NVARCHAR(MAX) NOT NULL,
	[Email] NVARCHAR(MAX) NOT NULL,
	[AdditionalInfo] NVARCHAR(MAX) NOT NULL,
	CONSTRAINT [PK_dbo.InterestedUsers_Projects] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_dbo.InterestedUsers_Projects.ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id]) ON DELETE CASCADE
);

GO
