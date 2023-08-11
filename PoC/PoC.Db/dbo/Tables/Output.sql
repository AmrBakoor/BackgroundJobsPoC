CREATE TABLE [dbo].[Output] (
    [Id]                        INT IDENTITY (1, 1) NOT NULL,
    [Number]                    INT NULL,
    [IsDivisibleByThree]        BIT NULL,
    [IsDivisibleByFive]         BIT NULL,
    [IsDivisibleByThreeAndFive] BIT NULL,
    [IsNeutral]                 BIT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

