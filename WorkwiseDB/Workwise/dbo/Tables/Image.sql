CREATE TABLE [dbo].[Image] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [ImagePath] NVARCHAR (MAX) NULL,
    [IsActive]  BIT            NOT NULL,
    CONSTRAINT [PK_dbo.UserImages] PRIMARY KEY CLUSTERED ([Id] ASC)
);

