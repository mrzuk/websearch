
CREATE TABLE [dbo].[InterestedUsers_Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[UserName] NVARCHAR (256) NOT NULL,
	[UserEmail] NVARCHAR (256) NOT NULL,
 CONSTRAINT [PK_dbo.InterestedUsers_Projects] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [FK_dbo.InterestedUsers_Projects.ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Project] ([Id]) ON DELETE CASCADE
);
GO
