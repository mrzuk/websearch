CREATE TABLE [dbo].[SuitableLevel] (
    [Id]               INT  IDENTITY(1,1) NOT NULL,
    [Description]      NVARCHAR (MAX) NOT NULL,
	CONSTRAINT [PK_dbo.SuitableLevel] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
