CREATE TABLE [dbo].[Tweet] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [UserId]      INT            NOT NULL,
    [Title]       NVARCHAR (100) NOT NULL,
    [Content]     NVARCHAR (150) NOT NULL,
    [Slug]        VARCHAR (100)  NOT NULL,
    [CreatedTime] DATETIME       NOT NULL,
    CONSTRAINT [PK_Tweet] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tweet_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Tweet_ByUser]
    ON [dbo].[Tweet]([UserId] ASC, [CreatedTime] DESC);

