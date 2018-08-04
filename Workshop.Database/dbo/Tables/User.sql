CREATE TABLE [dbo].[User] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Email]        NVARCHAR (255) NOT NULL,
    [PasswordHash] VARCHAR (100)  NULL,
    [GoogleId]     VARCHAR (50)   NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_User_Email]
    ON [dbo].[User]([Email] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_User_GoogleId]
    ON [dbo].[User]([GoogleId] ASC);

