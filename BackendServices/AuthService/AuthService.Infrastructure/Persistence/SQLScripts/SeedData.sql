GO
SET IDENTITY_INSERT [dbo].[Roles] ON 
GO
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (1, N'Admin')
GO
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (2, N'User')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [Name], [Email], [Password], [PhoneNumber], [EmailConfirmed], [CreatedDate]) VALUES (1, N'User', N'user@gmail.com', N'$2a$11$iTKXRsW8HvJ39mRXP14d.urshQj9nvQthfxI1xZhUuQq4UJIysQW6', N'9876543210', 0, CAST(N'2022-06-05T17:15:47.1917701' AS DateTime2))
GO
INSERT [dbo].[Users] ([Id], [Name], [Email], [Password], [PhoneNumber], [EmailConfirmed], [CreatedDate]) VALUES (2, N'Admin', N'admin@gmail.com', N'$2a$11$i5l9/BdNnhwpIK8JaXkNROQZnTFJFc6eaDiSy.cQhyh8Vxec8olh.', N'9876543210', 0, CAST(N'2022-06-05T17:16:59.8050624' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (1, 2)
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (2, 1)
GO
