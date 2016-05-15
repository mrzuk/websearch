CREATE TABLE [dbo].[Configurations] (
    [Id]   Int IDENTITY(1,1) NOT NULL,
    [Key] NVARCHAR (256) NOT NULL,
	[Value] NVARCHAR(MAX) NOT NULL,
    CONSTRAINT [PK_dbo.Configuration] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Configuration_Key]
    ON [dbo].[Configurations]([Key] ASC);
