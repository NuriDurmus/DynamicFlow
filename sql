USE [ConditionHandler]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 5/24/2020 22:36:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Condition]    Script Date: 5/24/2020 22:36:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Condition](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PropertyName] [nvarchar](250) NULL,
	[ConditionName] [nvarchar](250) NULL,
	[ConditionValue] [nvarchar](max) NULL,
	[ConditionPropertyValue] [nvarchar](250) NULL,
	[ConditionSetId] [int] NULL,
 CONSTRAINT [PK_Condition] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConditionAction]    Script Date: 5/24/2020 22:36:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConditionAction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ActionName] [nvarchar](250) NULL,
	[ActionParameterValues] [nvarchar](max) NULL,
	[MasterConditionSetId] [int] NULL,
	[CronExpression] [nvarchar](max) NULL,
	[OrderId] [int] NULL,
	[StopOnException] [bit] NULL,
	[RetryCount] [int] NULL,
 CONSTRAINT [PK_ConditionAction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConditionSet]    Script Date: 5/24/2020 22:36:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConditionSet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MasterConditionISetd] [int] NULL,
 CONSTRAINT [PK_ConditionSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MasterConditionSet]    Script Date: 5/24/2020 22:36:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterConditionSet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Description] [nvarchar](250) NULL,
 CONSTRAINT [PK_MasterConditionSet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200215204412_initialMigration', N'3.1.1')
GO
SET IDENTITY_INSERT [dbo].[Condition] ON 
GO
INSERT [dbo].[Condition] ([Id], [PropertyName], [ConditionName], [ConditionValue], [ConditionPropertyValue], [ConditionSetId]) VALUES (1, N'PersonName', N'DynamicBuilder.Business.ConditionHelper.Contains', N'Nuri', N'', 1)
GO
INSERT [dbo].[Condition] ([Id], [PropertyName], [ConditionName], [ConditionValue], [ConditionPropertyValue], [ConditionSetId]) VALUES (2, N'Age', N'DynamicBuilder.Business.ConditionHelper.Bigger', N'18', N'', 1)
GO
SET IDENTITY_INSERT [dbo].[Condition] OFF
GO
SET IDENTITY_INSERT [dbo].[ConditionAction] ON 
GO
INSERT [dbo].[ConditionAction] ([Id], [ActionName], [ActionParameterValues], [MasterConditionSetId], [CronExpression], [OrderId], [StopOnException], [RetryCount]) VALUES (1, N'DynamicFlow.Business.EmailService.SendEmail', N'testbody,testsubject,false', 1, N'', 0, 1, 0)
GO
INSERT [dbo].[ConditionAction] ([Id], [ActionName], [ActionParameterValues], [MasterConditionSetId], [CronExpression], [OrderId], [StopOnException], [RetryCount]) VALUES (2, N'DynamicFlow.Business.EmailService.SendEmail', N'testbody2,testsubject2,false', 1, N'', 0, 1, 0)
GO
INSERT [dbo].[ConditionAction] ([Id], [ActionName], [ActionParameterValues], [MasterConditionSetId], [CronExpression], [OrderId], [StopOnException], [RetryCount]) VALUES (1002, N'DynamicFlow.Business.RolePermissionService.ChangePersonRole', N'1,1,false', 1, N'', 0, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[ConditionAction] OFF
GO
SET IDENTITY_INSERT [dbo].[ConditionSet] ON 
GO
INSERT [dbo].[ConditionSet] ([Id], [MasterConditionISetd]) VALUES (1, 1)
GO
SET IDENTITY_INSERT [dbo].[ConditionSet] OFF
GO
SET IDENTITY_INSERT [dbo].[MasterConditionSet] ON 
GO
INSERT [dbo].[MasterConditionSet] ([Id], [Name], [Description]) VALUES (1, N'Kişi Kontrolü', N'Kişi kontrolü yaparak yetkilendirme yapar ve mail gönderir')
GO
SET IDENTITY_INSERT [dbo].[MasterConditionSet] OFF
GO
ALTER TABLE [dbo].[Condition]  WITH CHECK ADD  CONSTRAINT [FK_Conditions_ConditionSet1] FOREIGN KEY([ConditionSetId])
REFERENCES [dbo].[ConditionSet] ([Id])
GO
ALTER TABLE [dbo].[Condition] CHECK CONSTRAINT [FK_Conditions_ConditionSet1]
GO
ALTER TABLE [dbo].[ConditionAction]  WITH CHECK ADD  CONSTRAINT [FK_ConditionActions_MasterConditionSet] FOREIGN KEY([MasterConditionSetId])
REFERENCES [dbo].[MasterConditionSet] ([Id])
GO
ALTER TABLE [dbo].[ConditionAction] CHECK CONSTRAINT [FK_ConditionActions_MasterConditionSet]
GO
ALTER TABLE [dbo].[ConditionSet]  WITH CHECK ADD  CONSTRAINT [FK_ConditionSet_MasterConditionSet] FOREIGN KEY([MasterConditionISetd])
REFERENCES [dbo].[MasterConditionSet] ([Id])
GO
ALTER TABLE [dbo].[ConditionSet] CHECK CONSTRAINT [FK_ConditionSet_MasterConditionSet]
GO
