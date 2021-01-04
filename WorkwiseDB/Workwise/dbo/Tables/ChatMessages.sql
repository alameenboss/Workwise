CREATE TABLE [dbo].[ChatMessages] (
    [ChatMessageId] INT            IDENTITY (1, 1) NOT NULL,
    [FromUserId]    NVARCHAR (MAX) NULL,
    [ToUserId]      NVARCHAR (MAX) NULL,
    [Message]       NVARCHAR (MAX) NULL,
    [Status]        NVARCHAR (MAX) NULL,
    [CreatedOn]     DATETIME       NOT NULL,
    [UpdatedOn]     DATETIME       NOT NULL,
    [ViewedOn]      DATETIME       NOT NULL,
    [IsActive]      BIT            NOT NULL,
    CONSTRAINT [PK_dbo.ChatMessages] PRIMARY KEY CLUSTERED ([ChatMessageId] ASC)
);

