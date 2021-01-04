CREATE TABLE [dbo].[ErrorLog] (
    [ErrorLogId]     INT           IDENTITY (1, 1) NOT NULL,
    [ErrorNumber]    INT           NULL,
    [ErrorSeverity]  INT           NULL,
    [ErrorState]     INT           NULL,
    [ErrorProcedure] VARCHAR (50)  NULL,
    [ErrorLine]      INT           NULL,
    [ErrorMessage]   VARCHAR (MAX) NULL,
    [CreatedBy]      VARCHAR (128) NULL,
    [CreatedDate]    DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([ErrorLogId] ASC)
);

