CREATE TABLE [dbo].[Tags] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Tag]     NVARCHAR (MAX) NULL,
    [Post_Id] INT            NULL,
    CONSTRAINT [PK_dbo.Tags] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Tags_dbo.Posts_Post_Id] FOREIGN KEY ([Post_Id]) REFERENCES [dbo].[Posts] ([Id])
);

