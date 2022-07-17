CREATE TABLE [dbo].[UserNotifications] (
    [NotificationId]   INT            IDENTITY (1, 1) NOT NULL,
    [ToUserId]         NVARCHAR (MAX) NULL,
    [FromUserId]       NVARCHAR (MAX) NULL,
    [NotificationType] NVARCHAR (MAX) NULL,
    [Status]           NVARCHAR (MAX) NULL,
    [CreatedOn]        DATETIME       NOT NULL,
    [UpdatedOn]        DATETIME       NOT NULL,
    [IsActive]         BIT            NOT NULL,
    CONSTRAINT [PK_dbo.UserNotifications] PRIMARY KEY CLUSTERED ([NotificationId] ASC)
);

