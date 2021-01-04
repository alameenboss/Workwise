CREATE TABLE [dbo].[Posts] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Worktype]    INT             NOT NULL,
    [Rate]        DECIMAL (18, 2) NOT NULL,
    [Title]       NVARCHAR (MAX)  NULL,
    [PostedOn]    DATETIME        NOT NULL,
    [Location]    NVARCHAR (MAX)  NULL,
    [Description] NVARCHAR (MAX)  NULL,
    [PostedById]  NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_dbo.Posts] PRIMARY KEY CLUSTERED ([Id] ASC)
);

