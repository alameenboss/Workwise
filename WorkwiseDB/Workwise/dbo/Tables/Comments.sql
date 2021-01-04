CREATE TABLE [dbo].[Comments] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Description]        NVARCHAR (MAX) NULL,
    [CommentedBy_UserId] NVARCHAR (128) NULL,
    [Post_Id]            INT            NULL,
    CONSTRAINT [PK_dbo.Comments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Comments_dbo.Posts_Post_Id] FOREIGN KEY ([Post_Id]) REFERENCES [dbo].[Posts] ([Id]),
    CONSTRAINT [FK_dbo.Comments_dbo.UserProfiles_CommentedBy_UserId] FOREIGN KEY ([CommentedBy_UserId]) REFERENCES [dbo].[UserProfiles] ([UserId])
);

