CREATE TABLE [dbo].[SuitableSubject] (
    [Id]               INT   IDENTITY(1,1) NOT NULL,
    [Description]      NVARCHAR (MAX) NOT NULL
   CONSTRAINT [PK_dbo.SuitableSubject] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
