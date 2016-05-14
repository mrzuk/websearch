CREATE TABLE [dbo].[Cause] (
    [Id]               INT  IDENTITY(1,1) NOT NULL,
    [Description]      NVARCHAR (MAX) NOT NULL,
	CONSTRAINT [PK_dbo.Cause] PRIMARY KEY CLUSTERED ([Id] ASC)
   
);

