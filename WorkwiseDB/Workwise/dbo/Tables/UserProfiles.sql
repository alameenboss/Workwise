CREATE TABLE [dbo].[UserProfiles] (
    [UserId]      NVARCHAR (128) NOT NULL,
    [ImageUrl]    NVARCHAR (MAX) NULL,
    [FirstName]   NVARCHAR (MAX) NULL,
    [LastName]    NVARCHAR (MAX) NULL,
    [Designation] NVARCHAR (MAX) NULL,
    [Gender]      NVARCHAR (MAX) NULL,
    [DOB]         DATETIME       NULL,
    [Bio]         NVARCHAR (MAX) NULL,
    [CreatedOn]   DATETIME       NULL,
    [UpdatedOn]   DATETIME       NULL,
    [IsActive]    BIT            NOT NULL,
    [Post_Id]     INT            NULL,
    [Post_Id1]    INT            NULL,
    CONSTRAINT [PK_dbo.UserProfiles] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_dbo.UserProfiles_dbo.Posts_Post_Id] FOREIGN KEY ([Post_Id]) REFERENCES [dbo].[Posts] ([Id]),
    CONSTRAINT [FK_dbo.UserProfiles_dbo.Posts_Post_Id1] FOREIGN KEY ([Post_Id1]) REFERENCES [dbo].[Posts] ([Id])
);

