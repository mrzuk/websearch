CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   NVARCHAR (128) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[AspNetUsers]([UserName] ASC);

INSERT INTO [dbo].[AspNetUsers] (Id,	Email,	EmailConfirmed,	PasswordHash,	SecurityStamp,	PhoneNumber,	PhoneNumberConfirmed,	TwoFactorEnabled,	LockoutEndDateUtc,	LockoutEnabled,	AccessFailedCount,	UserName)
VALUES('38570c3e-942c-4c16-9868-9436e1075438','admin1@admin.com',0,'AACP+drr7o3HY2KTPZ9/MFLEiqLJYnUnYPS4Ttu8s67Xr+E20+cjXSlTHwNmCuUb2g==','4720a7cf-346b-4dc8-ae64-6015b8839738',	NULL,	0,	0,	NULL,	1,	0,	'admin1@admin.com')

INSERT