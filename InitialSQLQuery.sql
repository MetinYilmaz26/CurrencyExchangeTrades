IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [CurrencySymbols] (
    [Id] int NOT NULL IDENTITY,
    [Symbol] nvarchar(3) NOT NULL,
    [Definition] nvarchar(50) NOT NULL,
    [CreateTime] datetime2 NULL,
    [UpdateTime] datetime2 NULL,
    [Creater] int NOT NULL,
    CONSTRAINT [PK_CurrencySymbols] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Trades] (
    [Id] int NOT NULL IDENTITY,
    [ClientId] int NOT NULL,
    [TradeDate] datetime2 NOT NULL,
    [From] int NOT NULL,
    [To] int NOT NULL,
    [Amount] float NOT NULL,
    [CreateTime] datetime2 NULL,
    [UpdateTime] datetime2 NULL,
    [Creater] int NOT NULL,
    CONSTRAINT [PK_Trades] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221006182154_CurrencyExchangeTradesInitial', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Trades]') AND [c].[name] = N'Creater');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Trades] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Trades] DROP COLUMN [Creater];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CurrencySymbols]') AND [c].[name] = N'Creater');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [CurrencySymbols] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [CurrencySymbols] DROP COLUMN [Creater];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221008054842_foreingkey', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Trades] ADD [Type] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221008055915_tradetype', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Trades] ADD [Rate] float NOT NULL DEFAULT 0.0E0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221008070437_traderate', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Trades].[To]', N'ToId', N'COLUMN';
GO

EXEC sp_rename N'[Trades].[From]', N'FromId', N'COLUMN';
GO

CREATE INDEX [IX_Trades_FromId] ON [Trades] ([FromId]);
GO

CREATE INDEX [IX_Trades_ToId] ON [Trades] ([ToId]);
GO

ALTER TABLE [Trades] ADD CONSTRAINT [FK_Trades_CurrencySymbols_FromId] FOREIGN KEY ([FromId]) REFERENCES [CurrencySymbols] ([Id]);
GO

ALTER TABLE [Trades] ADD CONSTRAINT [FK_Trades_CurrencySymbols_ToId] FOREIGN KEY ([ToId]) REFERENCES [CurrencySymbols] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221008105248_foreingtable', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221008110434_foreingtableCollection', N'6.0.9');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Trades] DROP CONSTRAINT [FK_Trades_CurrencySymbols_FromId];
GO

ALTER TABLE [Trades] DROP CONSTRAINT [FK_Trades_CurrencySymbols_ToId];
GO

ALTER TABLE [Trades] ADD CONSTRAINT [FK_Trades_CurrencySymbols_FromId] FOREIGN KEY ([FromId]) REFERENCES [CurrencySymbols] ([Id]);
GO

ALTER TABLE [Trades] ADD CONSTRAINT [FK_Trades_CurrencySymbols_ToId] FOREIGN KEY ([ToId]) REFERENCES [CurrencySymbols] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221008111547_columnsrequeried', N'6.0.9');
GO

COMMIT;
GO

