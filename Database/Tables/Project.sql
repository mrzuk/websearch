CREATE TABLE [dbo].[Project] (
    [Id]               INT            NOT NULL,
    [ProjectArea]      NVARCHAR (MAX) NOT NULL,
    [Description]      NVARCHAR (MAX) NOT NULL,
    [SpecificProjects] NVARCHAR (MAX) NULL,
    [SuitableSubjects] NVARCHAR (450) NULL,
    [Cause]            NVARCHAR (450) NULL,
    [SuitableLevel]    NVARCHAR (MAX) NULL,
    [Skills]           NVARCHAR (MAX) NULL,
    [TimeRequired]     NVARCHAR (MAX) NULL,
    [Impact]           NVARCHAR (MAX) NULL,
    [KeyContacts]      NVARCHAR (MAX) NULL,
    [OtherContacts]    NVARCHAR (MAX) NULL,
    [Source]           NVARCHAR (MAX) NULL,
    [SuggestedReading] NVARCHAR (MAX) NULL,
    [UserId]           NVARCHAR (128) NOT NULL,
    [Date]             DATETIME       NOT NULL,
    CONSTRAINT [PK_dbo.Project] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Project.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ProjectId]
    ON [dbo].[Project]([Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SuitableSubjects]
    ON [dbo].[Project]([SuitableSubjects] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Cause]
    ON [dbo].[Project]([Cause] ASC);

