CREATE TABLE [dbo].[Jobs] (
    [Id]          INT              IDENTITY (1, 1) NOT NULL,
    [JobId]       UNIQUEIDENTIFIER NOT NULL,
    [HasErros]    BIT              NOT NULL,
    [CreatedAt]   DATETIME         NOT NULL,
    [StartedAt]   DATETIME         NULL,
    [CompletedAt] DATETIME         NULL,
    [StatusId]    INT              NOT NULL,
    [FirstName]   NVARCHAR (150)   NULL,
    [LastName]    NVARCHAR (150)   NULL,
    [Result]      NVARCHAR (MAX)   NULL,
    [Progress]    INT              NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([StatusId]) REFERENCES [dbo].[JobStatus] ([Id])
);







