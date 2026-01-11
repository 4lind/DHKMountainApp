CREATE DATABASE [DHKDB]

CREATE TABLE [dbo].[customer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cName] [nvarchar](250) NOT NULL,
	[cPhone] [nvarchar](50) NULL,
	[Address] [nvarchar](255) NULL,
	[Company] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[productName] [nvarchar](250) NOT NULL,
	[productCount] [int] NOT NULL,
	[producttype] [nvarchar](50) NOT NULL,
	[productCouple] [int] NULL
) ON [PRIMARY]





CREATE TABLE [dbo].[Receipts](
    [ReceiptID] [varchar](50) NOT NULL PRIMARY KEY,
    [CustomerName] [nvarchar](255) NOT NULL,
    [CustomerID] [int] NULL,
    [ReceiptDate] [datetime] NOT NULL,
    [TotalCartons] [decimal](18, 2) NOT NULL,
    [TotalPairs] [decimal](18, 2) NOT NULL,
    [TotalAmount] [decimal](18, 2) NOT NULL,
    [Currency] [nvarchar](10) NULL,
    [FilePath] [nvarchar](500) NULL,
    [CreatedDate] [datetime] NULL DEFAULT GETDATE()
);

ALTER TABLE [dbo].[Receipts] 
ADD FOREIGN KEY ([CustomerID]) 
REFERENCES [dbo].[customer] ([id]);

CREATE TABLE [dbo].[ReceiptItems](
    [ItemID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [ReceiptID] [varchar](50) NOT NULL,
    [ProductName] [nvarchar](255) NOT NULL,
    [Cartons] [int] NOT NULL,
    [PairsPerCarton] [int] NOT NULL,
    [UnitPrice] [decimal](18, 2) NOT NULL,
    [ItemTotal] [decimal](18, 2) NOT NULL,
    [RowNumber] [int] NOT NULL
);

ALTER TABLE [dbo].[ReceiptItems] 
ADD FOREIGN KEY ([ReceiptID]) 
REFERENCES [dbo].[Receipts] ([ReceiptID]);