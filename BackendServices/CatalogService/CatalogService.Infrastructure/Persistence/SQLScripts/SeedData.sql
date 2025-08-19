GO
SET IDENTITY_INSERT [dbo].[Categories] ON 
GO
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (1, N'Phones')
GO
INSERT [dbo].[Categories] ([CategoryId], [Name]) VALUES (2, N'Laptops')
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 
GO
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [CreatedDate]) VALUES (1, N'iPhone14', N'iPhone14', CAST(70000.00 AS Decimal(18, 2)), N'/images/iphone14.png', 1, CAST(N'2003-12-12T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [CreatedDate]) VALUES (2, N'Samsung S23', N'Samsung S23', CAST(50000.00 AS Decimal(18, 2)), N'/images/samsungs23.png', 1, CAST(N'2003-12-12T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [CreatedDate]) VALUES (3, N'Dell Laptop', N'Dell Laptop', CAST(60000.00 AS Decimal(18, 2)), N'/images/dell-laptop.png', 2, CAST(N'2003-12-12T00:00:00.0000000' AS DateTime2))
GO
INSERT [dbo].[Products] ([ProductId], [Name], [Description], [UnitPrice], [ImageUrl], [CategoryId], [CreatedDate]) VALUES (4, N'MacBook Pro', N'MacBook Pro', CAST(100000.00 AS Decimal(18, 2)), N'/images/macbook.png', 2, CAST(N'2003-12-12T00:00:00.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO