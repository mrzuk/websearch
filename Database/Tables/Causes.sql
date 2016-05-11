CREATE TABLE [dbo].[Cause] (
    [Id]               INT  IDENTITY(1,1) NOT NULL,
    [Description]      NVARCHAR (MAX) NOT NULL,
	CONSTRAINT [PK_dbo.Cause] PRIMARY KEY CLUSTERED ([Id] ASC)
   
);

GO

INSERT INTO [dbo].[Cause] VALUES('Animal welfare');
INSERT INTO [dbo].[Cause] VALUES('Global poverty');