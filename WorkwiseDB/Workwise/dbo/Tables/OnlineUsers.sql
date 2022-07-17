CREATE TABLE [dbo].[OnlineUsers] (
    [OnlineUserId] INT            IDENTITY (1, 1) NOT NULL,
    [UserId]       NVARCHAR (MAX) NULL,
    [ConnectionId] NVARCHAR (MAX) NULL,
    [IsOnline]     BIT            NOT NULL,
    [CreatedOn]    DATETIME       NOT NULL,
    [UpdatedOn]    DATETIME       NOT NULL,
    [IsActive]     BIT            NOT NULL,
    CONSTRAINT [PK_dbo.OnlineUsers] PRIMARY KEY CLUSTERED ([OnlineUserId] ASC)
);

