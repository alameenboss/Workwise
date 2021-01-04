CREATE TABLE [dbo].[MapPostImage] (
    [Id]      INT IDENTITY (1, 1) NOT NULL,
    [PostId]  INT NULL,
    [ImageId] INT NULL,
    CONSTRAINT [PK_dbo.ImageModels] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Image_Id_MapPostImage_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [dbo].[Image] ([Id]),
    CONSTRAINT [FK_Post_Id_MapPostImage_PostId] FOREIGN KEY ([PostId]) REFERENCES [dbo].[Posts] ([Id])
);

