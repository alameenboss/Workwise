CREATE TABLE [dbo].[FriendMappings] (
    [FriendMappingId] INT            IDENTITY (1, 1) NOT NULL,
    [UserId]          NVARCHAR (MAX) NULL,
    [EndUserId]       NVARCHAR (MAX) NULL,
    [RequestStatus]   NVARCHAR (MAX) NULL,
    [CreatedOn]       DATETIME       NOT NULL,
    [UpdatedOn]       DATETIME       NOT NULL,
    [IsActive]        BIT            NOT NULL,
    CONSTRAINT [PK_dbo.FriendMappings] PRIMARY KEY CLUSTERED ([FriendMappingId] ASC)
);

