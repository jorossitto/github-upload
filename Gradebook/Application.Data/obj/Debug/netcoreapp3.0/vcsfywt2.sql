IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Address] (
    [EntityState] int NOT NULL,
    [HasChanges] bit NOT NULL,
    [IsNew] bit NOT NULL,
    [AddressId] int NOT NULL IDENTITY,
    [AddressType] int NOT NULL,
    [City] nvarchar(max) NULL,
    [Country] nvarchar(255) NOT NULL,
    [PostalCode] nvarchar(max) NULL,
    [State] nvarchar(max) NULL,
    [StreetLine1] nvarchar(max) NULL,
    [StreetLine2] nvarchar(max) NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY ([AddressId])
);

GO

CREATE TABLE [Restaurants] (
    [EntityState] int NOT NULL,
    [HasChanges] bit NOT NULL,
    [IsNew] bit NOT NULL,
    [ID] int NOT NULL IDENTITY,
    [Name] nvarchar(80) NOT NULL,
    [AddressId] int NOT NULL,
    [Cuisine] int NOT NULL,
    CONSTRAINT [PK_Restaurants] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Restaurants_Address_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Address] ([AddressId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Restaurants_AddressId] ON [Restaurants] ([AddressId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190927164658_initialcreate', N'3.0.0');

GO

CREATE TABLE [Categories] (
    [CategoryId] int NOT NULL IDENTITY,
    [CategoryName] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryId])
);

GO

CREATE TABLE [Pies] (
    [PieId] int NOT NULL IDENTITY,
    [EntityState] int NOT NULL,
    [HasChanges] bit NOT NULL,
    [IsNew] bit NOT NULL,
    [CurrentPrice] decimal(18,2) NULL,
    [ProductDescription] nvarchar(max) NULL,
    [ProductId] int NOT NULL,
    [ShortDescription] nvarchar(max) NULL,
    [LongDescription] nvarchar(max) NULL,
    [ImageUrl] nvarchar(max) NULL,
    [ImageThumbnailUrl] nvarchar(max) NULL,
    [CategoryId] int NULL,
    [Price] decimal(18,2) NOT NULL,
    [InStock] bit NOT NULL,
    [Name] nvarchar(max) NULL,
    [ListPrice] real NOT NULL,
    [AllergyInformation] nvarchar(max) NULL,
    [IsPieOfTheWeek] bit NOT NULL,
    CONSTRAINT [PK_Pies] PRIMARY KEY ([PieId]),
    CONSTRAINT [FK_Pies_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Pies_CategoryId] ON [Pies] ([CategoryId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191007165619_InitalMigration', N'3.0.0');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191007173259_InitalMigration2', N'3.0.0');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191007173830_InitalMigration3', N'3.0.0');

GO

ALTER TABLE [Pies] DROP CONSTRAINT [FK_Pies_Categories_CategoryId];

GO

DROP INDEX [IX_Pies_CategoryId] ON [Pies];
DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Pies]') AND [c].[name] = N'CategoryId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Pies] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Pies] ALTER COLUMN [CategoryId] int NOT NULL;
CREATE INDEX [IX_Pies_CategoryId] ON [Pies] ([CategoryId]);

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'CategoryName', N'Description') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] ON;
INSERT INTO [Categories] ([CategoryId], [CategoryName], [Description])
VALUES (1, N'Fruit pies', N'All-Fruit Pies');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'CategoryName', N'Description') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'CategoryName', N'Description') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] ON;
INSERT INTO [Categories] ([CategoryId], [CategoryName], [Description])
VALUES (2, N'Cheese cakes', N'Cheesy all the way');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'CategoryName', N'Description') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'CategoryName', N'Description') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] ON;
INSERT INTO [Categories] ([CategoryId], [CategoryName], [Description])
VALUES (3, N'Seasonal Pies', N'Get in the mood for a seasonal pie');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CategoryId', N'CategoryName', N'Description') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PieId', N'AllergyInformation', N'CategoryId', N'CurrentPrice', N'EntityState', N'HasChanges', N'ImageThumbnailUrl', N'ImageUrl', N'InStock', N'IsNew', N'IsPieOfTheWeek', N'ListPrice', N'LongDescription', N'Name', N'Price', N'ProductDescription', N'ProductId', N'ShortDescription') AND [object_id] = OBJECT_ID(N'[Pies]'))
    SET IDENTITY_INSERT [Pies] ON;
INSERT INTO [Pies] ([PieId], [AllergyInformation], [CategoryId], [CurrentPrice], [EntityState], [HasChanges], [ImageThumbnailUrl], [ImageUrl], [InStock], [IsNew], [IsPieOfTheWeek], [ListPrice], [LongDescription], [Name], [Price], [ProductDescription], [ProductId], [ShortDescription])
VALUES (1, NULL, 1, NULL, 0, CAST(0 AS bit), N'https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypiesmall.jpg', N'https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypie.jpg', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS real), N'Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.', N'Strawberry Pie', 15.95, NULL, 0, N'Lorem Ipsum'),
(3, NULL, 1, NULL, 0, CAST(0 AS bit), N'https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpiesmall.jpg', N'https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpie.jpg', CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(0 AS real), N'Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.', N'Rhubarb Pie', 15.95, NULL, 0, N'Lorem Ipsum'),
(2, NULL, 2, NULL, 0, CAST(0 AS bit), N'https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecakesmall.jpg', N'https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecake.jpg', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS real), N'Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.', N'Cheese cake', 18.95, NULL, 0, N'Lorem Ipsum'),
(4, NULL, 3, NULL, 0, CAST(0 AS bit), N'https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpiesmall.jpg', N'https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpie.jpg', CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(0 AS real), N'Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.', N'Pumpkin Pie', 12.95, NULL, 0, N'Lorem Ipsum');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PieId', N'AllergyInformation', N'CategoryId', N'CurrentPrice', N'EntityState', N'HasChanges', N'ImageThumbnailUrl', N'ImageUrl', N'InStock', N'IsNew', N'IsPieOfTheWeek', N'ListPrice', N'LongDescription', N'Name', N'Price', N'ProductDescription', N'ProductId', N'ShortDescription') AND [object_id] = OBJECT_ID(N'[Pies]'))
    SET IDENTITY_INSERT [Pies] OFF;

GO

ALTER TABLE [Pies] ADD CONSTRAINT [FK_Pies_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191007194842_seedDataAdded', N'3.0.0');

GO

ALTER TABLE [Pies] ADD [Notes] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191007211052_NotesAddedToPie', N'3.0.0');

GO

CREATE TABLE [ShoppingCartItem] (
    [ShoppingCartItemId] int NOT NULL IDENTITY,
    [PieId] int NULL,
    [Amount] int NOT NULL,
    [ShoppingCartId] nvarchar(max) NULL,
    CONSTRAINT [PK_ShoppingCartItem] PRIMARY KEY ([ShoppingCartItemId]),
    CONSTRAINT [FK_ShoppingCartItem_Pies_PieId] FOREIGN KEY ([PieId]) REFERENCES [Pies] ([PieId]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_ShoppingCartItem_PieId] ON [ShoppingCartItem] ([PieId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191015211525_ShoppingCartAdded', N'3.0.0');

GO

ALTER TABLE [ShoppingCartItem] DROP CONSTRAINT [FK_ShoppingCartItem_Pies_PieId];

GO

ALTER TABLE [ShoppingCartItem] DROP CONSTRAINT [PK_ShoppingCartItem];

GO

EXEC sp_rename N'[ShoppingCartItem]', N'ShoppingCartItems';

GO

EXEC sp_rename N'[ShoppingCartItems].[IX_ShoppingCartItem_PieId]', N'IX_ShoppingCartItems_PieId', N'INDEX';

GO

ALTER TABLE [ShoppingCartItems] ADD CONSTRAINT [PK_ShoppingCartItems] PRIMARY KEY ([ShoppingCartItemId]);

GO

ALTER TABLE [ShoppingCartItems] ADD CONSTRAINT [FK_ShoppingCartItems_Pies_PieId] FOREIGN KEY ([PieId]) REFERENCES [Pies] ([PieId]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191015214904_ShoppingCartAdded2', N'3.0.0');

GO

UPDATE [Pies] SET [AllergyInformation] = N'', [ImageThumbnailUrl] = N'https://gillcleerenpluralsight.blob.core.windows.net/files/applepiesmall.jpg', [ImageUrl] = N'https://gillcleerenpluralsight.blob.core.windows.net/files/applepie.jpg', [IsPieOfTheWeek] = CAST(1 AS bit), [Name] = N'Apple Pie', [Price] = 12.95, [ShortDescription] = N'Our famous apple pies!'
WHERE [PieId] = 1;
SELECT @@ROWCOUNT;


GO

UPDATE [Pies] SET [AllergyInformation] = N'', [ImageThumbnailUrl] = N'https://gillcleerenpluralsight.blob.core.windows.net/files/blueberrycheesecakesmall.jpg', [ImageUrl] = N'https://gillcleerenpluralsight.blob.core.windows.net/files/blueberrycheesecake.jpg', [Name] = N'Blueberry Cheese Cake', [ShortDescription] = N'You''ll love it!'
WHERE [PieId] = 2;
SELECT @@ROWCOUNT;


GO

UPDATE [Pies] SET [AllergyInformation] = N'', [CategoryId] = 2, [ImageThumbnailUrl] = N'https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecakesmall.jpg', [ImageUrl] = N'https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecake.jpg', [IsPieOfTheWeek] = CAST(0 AS bit), [Name] = N'Cheese Cake', [Price] = 18.95, [ShortDescription] = N'Plain cheese cake. Plain pleasure.'
WHERE [PieId] = 3;
SELECT @@ROWCOUNT;


GO

UPDATE [Pies] SET [AllergyInformation] = N'', [CategoryId] = 1, [ImageThumbnailUrl] = N'https://gillcleerenpluralsight.blob.core.windows.net/files/cherrypiesmall.jpg', [ImageUrl] = N'https://gillcleerenpluralsight.blob.core.windows.net/files/cherrypie.jpg', [IsPieOfTheWeek] = CAST(0 AS bit), [Name] = N'Cherry Pie', [Price] = 15.95, [ShortDescription] = N'A summer classic!'
WHERE [PieId] = 4;
SELECT @@ROWCOUNT;


GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PieId', N'AllergyInformation', N'CategoryId', N'CurrentPrice', N'EntityState', N'HasChanges', N'ImageThumbnailUrl', N'ImageUrl', N'InStock', N'IsNew', N'IsPieOfTheWeek', N'ListPrice', N'LongDescription', N'Name', N'Notes', N'Price', N'ProductDescription', N'ProductId', N'ShortDescription') AND [object_id] = OBJECT_ID(N'[Pies]'))
    SET IDENTITY_INSERT [Pies] ON;
INSERT INTO [Pies] ([PieId], [AllergyInformation], [CategoryId], [CurrentPrice], [EntityState], [HasChanges], [ImageThumbnailUrl], [ImageUrl], [InStock], [IsNew], [IsPieOfTheWeek], [ListPrice], [LongDescription], [Name], [Notes], [Price], [ProductDescription], [ProductId], [ShortDescription])
VALUES (5, N'', 3, NULL, 0, CAST(0 AS bit), N'https://gillcleerenpluralsight.blob.core.windows.net/files/christmasapplepiesmall.jpg', N'https://gillcleerenpluralsight.blob.core.windows.net/files/christmasapplepie.jpg', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS real), N'Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.', N'Christmas Apple Pie', NULL, 13.95, NULL, 0, N'Happy holidays with this pie!'),
(6, N'', 3, NULL, 0, CAST(0 AS bit), N'https://gillcleerenpluralsight.blob.core.windows.net/files/cranberrypiesmall.jpg', N'https://gillcleerenpluralsight.blob.core.windows.net/files/cranberrypie.jpg', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS real), N'Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.', N'Cranberry Pie', NULL, 17.95, NULL, 0, N'A Christmas favorite'),
(7, N'', 1, NULL, 0, CAST(0 AS bit), N'https://gillcleerenpluralsight.blob.core.windows.net/files/peachpiesmall.jpg', N'https://gillcleerenpluralsight.blob.core.windows.net/files/peachpie.jpg', CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS real), N'Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.', N'Peach Pie', NULL, 15.95, NULL, 0, N'Sweet as peach'),
(8, N'', 3, NULL, 0, CAST(0 AS bit), N'https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpiesmall.jpg', N'https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpie.jpg', CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(0 AS real), N'Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.', N'Pumpkin Pie', NULL, 12.95, NULL, 0, N'Our Halloween favorite'),
(9, N'', 1, NULL, 0, CAST(0 AS bit), N'https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpiesmall.jpg', N'https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpie.jpg', CAST(1 AS bit), CAST(0 AS bit), CAST(1 AS bit), CAST(0 AS real), N'Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.', N'Rhubarb Pie', NULL, 15.95, NULL, 0, N'My God, so sweet!'),
(10, N'', 1, NULL, 0, CAST(0 AS bit), N'https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypiesmall.jpg', N'https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypie.jpg', CAST(1 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS real), N'Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.', N'Strawberry Pie', NULL, 15.95, NULL, 0, N'Our delicious strawberry pie!'),
(11, N'', 2, NULL, 0, CAST(0 AS bit), N'https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrycheesecakesmall.jpg', N'https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrycheesecake.jpg', CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS bit), CAST(0 AS real), N'Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.', N'Strawberry Cheese Cake', NULL, 18.95, NULL, 0, N'You''ll love it!');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PieId', N'AllergyInformation', N'CategoryId', N'CurrentPrice', N'EntityState', N'HasChanges', N'ImageThumbnailUrl', N'ImageUrl', N'InStock', N'IsNew', N'IsPieOfTheWeek', N'ListPrice', N'LongDescription', N'Name', N'Notes', N'Price', N'ProductDescription', N'ProductId', N'ShortDescription') AND [object_id] = OBJECT_ID(N'[Pies]'))
    SET IDENTITY_INSERT [Pies] OFF;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191017181049_AddingPies', N'3.0.0');

GO

CREATE TABLE [Orders] (
    [OrderId] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [AddressLine1] nvarchar(max) NULL,
    [AddressLine2] nvarchar(max) NULL,
    [ZipCode] nvarchar(max) NULL,
    [City] nvarchar(max) NULL,
    [State] nvarchar(max) NULL,
    [Country] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [OrderTotal] decimal(18,2) NOT NULL,
    [OrderPlaced] datetime2 NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([OrderId])
);

GO

CREATE TABLE [OrderDetails] (
    [OrderDetailId] int NOT NULL IDENTITY,
    [OrderId] int NOT NULL,
    [PieId] int NOT NULL,
    [Amount] int NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY ([OrderDetailId]),
    CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([OrderId]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderDetails_Pies_PieId] FOREIGN KEY ([PieId]) REFERENCES [Pies] ([PieId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_OrderDetails_OrderId] ON [OrderDetails] ([OrderId]);

GO

CREATE INDEX [IX_OrderDetails_PieId] ON [OrderDetails] ([PieId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191018171140_orders', N'3.0.0');

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'ZipCode');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Orders] ALTER COLUMN [ZipCode] nvarchar(10) NOT NULL;

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'State');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Orders] ALTER COLUMN [State] nvarchar(20) NULL;

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'PhoneNumber');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Orders] ALTER COLUMN [PhoneNumber] nvarchar(25) NOT NULL;

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'LastName');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Orders] ALTER COLUMN [LastName] nvarchar(50) NOT NULL;

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'FirstName');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Orders] ALTER COLUMN [FirstName] nvarchar(50) NOT NULL;

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'Email');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Orders] ALTER COLUMN [Email] nvarchar(50) NOT NULL;

GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'Country');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Orders] ALTER COLUMN [Country] nvarchar(50) NOT NULL;

GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'City');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Orders] ALTER COLUMN [City] nvarchar(50) NOT NULL;

GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'AddressLine1');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Orders] ALTER COLUMN [AddressLine1] nvarchar(100) NOT NULL;

GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191022154311_identityadded', N'3.0.0');

GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'State');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Orders] ALTER COLUMN [State] nvarchar(20) NULL;

GO

CREATE TABLE [Location] (
    [LocationId] int NOT NULL IDENTITY,
    [VenueName] nvarchar(max) NULL,
    [Address1] nvarchar(max) NULL,
    [Address2] nvarchar(max) NULL,
    [Address3] nvarchar(max) NULL,
    [CityTown] nvarchar(max) NULL,
    [StateProvince] nvarchar(max) NULL,
    [PostalCode] nvarchar(max) NULL,
    [Country] nvarchar(max) NULL,
    CONSTRAINT [PK_Location] PRIMARY KEY ([LocationId])
);

GO

CREATE TABLE [Speakers] (
    [SpeakerId] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [MiddleName] nvarchar(max) NULL,
    [Company] nvarchar(max) NULL,
    [CompanyUrl] nvarchar(max) NULL,
    [BlogUrl] nvarchar(max) NULL,
    [Twitter] nvarchar(max) NULL,
    [GitHub] nvarchar(max) NULL,
    CONSTRAINT [PK_Speakers] PRIMARY KEY ([SpeakerId])
);

GO

CREATE TABLE [Camps] (
    [CampId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Moniker] nvarchar(max) NULL,
    [LocationId] int NULL,
    [EventDate] datetime2 NOT NULL,
    [Length] int NOT NULL,
    CONSTRAINT [PK_Camps] PRIMARY KEY ([CampId]),
    CONSTRAINT [FK_Camps_Location_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Location] ([LocationId]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Talks] (
    [TalkId] int NOT NULL IDENTITY,
    [CampId] int NULL,
    [Title] nvarchar(max) NULL,
    [Abstract] nvarchar(max) NULL,
    [Level] int NOT NULL,
    [SpeakerId] int NULL,
    CONSTRAINT [PK_Talks] PRIMARY KEY ([TalkId]),
    CONSTRAINT [FK_Talks_Camps_CampId] FOREIGN KEY ([CampId]) REFERENCES [Camps] ([CampId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Talks_Speakers_SpeakerId] FOREIGN KEY ([SpeakerId]) REFERENCES [Speakers] ([SpeakerId]) ON DELETE NO ACTION
);

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'LocationId', N'Address1', N'Address2', N'Address3', N'CityTown', N'Country', N'PostalCode', N'StateProvince', N'VenueName') AND [object_id] = OBJECT_ID(N'[Location]'))
    SET IDENTITY_INSERT [Location] ON;
INSERT INTO [Location] ([LocationId], [Address1], [Address2], [Address3], [CityTown], [Country], [PostalCode], [StateProvince], [VenueName])
VALUES (1, N'123 Main Street', NULL, NULL, N'Atlanta', N'USA', N'12345', N'GA', N'Atlanta Convention Center');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'LocationId', N'Address1', N'Address2', N'Address3', N'CityTown', N'Country', N'PostalCode', N'StateProvince', N'VenueName') AND [object_id] = OBJECT_ID(N'[Location]'))
    SET IDENTITY_INSERT [Location] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SpeakerId', N'BlogUrl', N'Company', N'CompanyUrl', N'FirstName', N'GitHub', N'LastName', N'MiddleName', N'Twitter') AND [object_id] = OBJECT_ID(N'[Speakers]'))
    SET IDENTITY_INSERT [Speakers] ON;
INSERT INTO [Speakers] ([SpeakerId], [BlogUrl], [Company], [CompanyUrl], [FirstName], [GitHub], [LastName], [MiddleName], [Twitter])
VALUES (1, N'http://wildermuth.com', N'Wilder Minds LLC', N'http://wilderminds.com', N'Shawn', N'shawnwildermuth', N'Wildermuth', NULL, N'shawnwildermuth');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SpeakerId', N'BlogUrl', N'Company', N'CompanyUrl', N'FirstName', N'GitHub', N'LastName', N'MiddleName', N'Twitter') AND [object_id] = OBJECT_ID(N'[Speakers]'))
    SET IDENTITY_INSERT [Speakers] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SpeakerId', N'BlogUrl', N'Company', N'CompanyUrl', N'FirstName', N'GitHub', N'LastName', N'MiddleName', N'Twitter') AND [object_id] = OBJECT_ID(N'[Speakers]'))
    SET IDENTITY_INSERT [Speakers] ON;
INSERT INTO [Speakers] ([SpeakerId], [BlogUrl], [Company], [CompanyUrl], [FirstName], [GitHub], [LastName], [MiddleName], [Twitter])
VALUES (2, N'http://shawnandresa.com', N'Wilder Minds LLC', N'http://wilderminds.com', N'Resa', N'resawildermuth', N'Wildermuth', NULL, N'resawildermuth');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'SpeakerId', N'BlogUrl', N'Company', N'CompanyUrl', N'FirstName', N'GitHub', N'LastName', N'MiddleName', N'Twitter') AND [object_id] = OBJECT_ID(N'[Speakers]'))
    SET IDENTITY_INSERT [Speakers] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CampId', N'EventDate', N'Length', N'LocationId', N'Moniker', N'Name') AND [object_id] = OBJECT_ID(N'[Camps]'))
    SET IDENTITY_INSERT [Camps] ON;
INSERT INTO [Camps] ([CampId], [EventDate], [Length], [LocationId], [Moniker], [Name])
VALUES (1, '2018-10-18T00:00:00.0000000', 1, 1, N'ATL2018', N'Atlanta Code Camp');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CampId', N'EventDate', N'Length', N'LocationId', N'Moniker', N'Name') AND [object_id] = OBJECT_ID(N'[Camps]'))
    SET IDENTITY_INSERT [Camps] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'TalkId', N'Abstract', N'CampId', N'Level', N'SpeakerId', N'Title') AND [object_id] = OBJECT_ID(N'[Talks]'))
    SET IDENTITY_INSERT [Talks] ON;
INSERT INTO [Talks] ([TalkId], [Abstract], [CampId], [Level], [SpeakerId], [Title])
VALUES (1, N'Entity Framework from scratch in an hour. Probably cover it all', 1, 100, 1, N'Entity Framework From Scratch');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'TalkId', N'Abstract', N'CampId', N'Level', N'SpeakerId', N'Title') AND [object_id] = OBJECT_ID(N'[Talks]'))
    SET IDENTITY_INSERT [Talks] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'TalkId', N'Abstract', N'CampId', N'Level', N'SpeakerId', N'Title') AND [object_id] = OBJECT_ID(N'[Talks]'))
    SET IDENTITY_INSERT [Talks] ON;
INSERT INTO [Talks] ([TalkId], [Abstract], [CampId], [Level], [SpeakerId], [Title])
VALUES (2, N'Thinking of good sample data examples is tiring.', 1, 200, 2, N'Writing Sample Data Made Easy');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'TalkId', N'Abstract', N'CampId', N'Level', N'SpeakerId', N'Title') AND [object_id] = OBJECT_ID(N'[Talks]'))
    SET IDENTITY_INSERT [Talks] OFF;

GO

CREATE INDEX [IX_Camps_LocationId] ON [Camps] ([LocationId]);

GO

CREATE INDEX [IX_Talks_CampId] ON [Talks] ([CampId]);

GO

CREATE INDEX [IX_Talks_SpeakerId] ON [Talks] ([SpeakerId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191023143434_campsdata', N'3.0.0');

GO

CREATE TABLE [Battles] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Battles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Samurais] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [BattleId] int NOT NULL,
    CONSTRAINT [PK_Samurais] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Samurais_Battles_BattleId] FOREIGN KEY ([BattleId]) REFERENCES [Battles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Quotes] (
    [Id] int NOT NULL IDENTITY,
    [Text] nvarchar(max) NULL,
    [SamuraiId] int NOT NULL,
    CONSTRAINT [PK_Quotes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Quotes_Samurais_SamuraiId] FOREIGN KEY ([SamuraiId]) REFERENCES [Samurais] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Quotes_SamuraiId] ON [Quotes] ([SamuraiId]);

GO

CREATE INDEX [IX_Samurais_BattleId] ON [Samurais] ([BattleId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191101183601_samurais-added', N'3.0.0');

GO

