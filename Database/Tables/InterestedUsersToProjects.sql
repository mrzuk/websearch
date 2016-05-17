USE [WebDb]
GO

/****** Object:  Table [dbo].[InterestedUsers_Projects]    Script Date: 16.05.2016 19:12:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[InterestedUsers_Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ProjectId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.InterestedUsers_Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[InterestedUsers_Projects]  WITH CHECK ADD  CONSTRAINT [FK_dbo.InterestedUsers_Projects.ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[InterestedUsers_Projects] CHECK CONSTRAINT [FK_dbo.InterestedUsers_Projects.ProjectId]
GO

ALTER TABLE [dbo].[InterestedUsers_Projects]  WITH CHECK ADD  CONSTRAINT [FK_dbo.InterestedUsers_Projects.UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[InterestedUsers_Projects] CHECK CONSTRAINT [FK_dbo.InterestedUsers_Projects.UserId]
GO


