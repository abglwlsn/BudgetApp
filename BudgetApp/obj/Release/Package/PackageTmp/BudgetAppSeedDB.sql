USE [master]
GO
/****** Object:  Database [awest-budget]    Script Date: 2/29/2016 9:24:38 AM ******/
CREATE DATABASE [awest-budget]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'awest-budget', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\awest-budget.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'awest-budget_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\awest-budget_log.ldf' , SIZE = 816KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [awest-budget] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [awest-budget].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [awest-budget] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [awest-budget] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [awest-budget] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [awest-budget] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [awest-budget] SET ARITHABORT OFF 
GO
ALTER DATABASE [awest-budget] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [awest-budget] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [awest-budget] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [awest-budget] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [awest-budget] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [awest-budget] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [awest-budget] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [awest-budget] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [awest-budget] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [awest-budget] SET  ENABLE_BROKER 
GO
ALTER DATABASE [awest-budget] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [awest-budget] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [awest-budget] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [awest-budget] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [awest-budget] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [awest-budget] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [awest-budget] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [awest-budget] SET RECOVERY FULL 
GO
ALTER DATABASE [awest-budget] SET  MULTI_USER 
GO
ALTER DATABASE [awest-budget] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [awest-budget] SET DB_CHAINING OFF 
GO
ALTER DATABASE [awest-budget] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [awest-budget] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [awest-budget] SET DELAYED_DURABILITY = DISABLED 
GO
USE [awest-budget]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 2/29/2016 9:24:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 2/29/2016 9:24:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 2/29/2016 9:24:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 2/29/2016 9:24:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 2/29/2016 9:24:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 2/29/2016 9:24:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[HouseholdId] [int] NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[HasAdminRights] [bit] NOT NULL DEFAULT ((0)),
	[IsSuperUser] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BankAccounts]    Script Date: 2/29/2016 9:24:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankAccounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HouseholdId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[ReconciledBalance] [decimal](18, 2) NOT NULL DEFAULT ((0)),
	[IsSoftDeleted] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_dbo.BankAccounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BudgetItems]    Script Date: 2/29/2016 9:24:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[HouseholdId] [int] NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
	[CreatorId] [nvarchar](128) NULL,
	[AmountLimit] [decimal](18, 2) NOT NULL DEFAULT ((0)),
	[Balance] [decimal](18, 2) NOT NULL DEFAULT ((0)),
	[WarningId] [int] NULL DEFAULT ((0)),
	[AllowEdits] [bit] NOT NULL DEFAULT ((0)),
	[Income] [bit] NOT NULL DEFAULT ((0)),
	[IsSoftDeleted] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_dbo.BudgetItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 2/29/2016 9:24:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[HouseholdId] [int] NULL,
 CONSTRAINT [PK_dbo.Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CategoryStandards]    Script Date: 2/29/2016 9:24:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryStandards](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.CategoryStandards] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Households]    Script Date: 2/29/2016 9:24:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Households](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Households] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvitedUsers]    Script Date: 2/29/2016 9:24:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvitedUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HouseholdId] [int] NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[InviteCode] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NOT NULL DEFAULT (''),
	[InvitedBy] [nvarchar](max) NULL,
	[HasAdminRights] [bit] NOT NULL DEFAULT ((0)),
	[InvitedDate] [datetimeoffset](7) NOT NULL DEFAULT ('0001-01-01T00:00:00.000+00:00'),
 CONSTRAINT [PK_dbo.InvitedUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 2/29/2016 9:24:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BankAccountId] [int] NOT NULL,
	[CategoryId] [int] NULL,
	[BudgetItemId] [int] NULL,
	[Transacted] [datetimeoffset](7) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Reconciled] [bit] NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[Entered] [datetimeoffset](7) NOT NULL DEFAULT ('0001-01-01T00:00:00.000+00:00'),
	[Income] [bit] NOT NULL DEFAULT ((0)),
	[IsSoftDeleted] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_dbo.Transactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Warnings]    Script Date: 2/29/2016 9:24:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Warnings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WarningLevel] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Warnings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 2/29/2016 9:24:39 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 2/29/2016 9:24:39 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 2/29/2016 9:24:39 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 2/29/2016 9:24:39 AM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 2/29/2016 9:24:39 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HouseholdId]    Script Date: 2/29/2016 9:24:39 AM ******/
CREATE NONCLUSTERED INDEX [IX_HouseholdId] ON [dbo].[AspNetUsers]
(
	[HouseholdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 2/29/2016 9:24:39 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HouseholdId]    Script Date: 2/29/2016 9:24:39 AM ******/
CREATE NONCLUSTERED INDEX [IX_HouseholdId] ON [dbo].[BankAccounts]
(
	[HouseholdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CategoryId]    Script Date: 2/29/2016 9:24:39 AM ******/
CREATE NONCLUSTERED INDEX [IX_CategoryId] ON [dbo].[BudgetItems]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_CreatorId]    Script Date: 2/29/2016 9:24:39 AM ******/
CREATE NONCLUSTERED INDEX [IX_CreatorId] ON [dbo].[BudgetItems]
(
	[CreatorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HouseholdId]    Script Date: 2/29/2016 9:24:39 AM ******/
CREATE NONCLUSTERED INDEX [IX_HouseholdId] ON [dbo].[BudgetItems]
(
	[HouseholdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_WarningId]    Script Date: 2/29/2016 9:24:39 AM ******/
CREATE NONCLUSTERED INDEX [IX_WarningId] ON [dbo].[BudgetItems]
(
	[WarningId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HouseholdId]    Script Date: 2/29/2016 9:24:39 AM ******/
CREATE NONCLUSTERED INDEX [IX_HouseholdId] ON [dbo].[Categories]
(
	[HouseholdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HouseholdId]    Script Date: 2/29/2016 9:24:39 AM ******/
CREATE NONCLUSTERED INDEX [IX_HouseholdId] ON [dbo].[InvitedUsers]
(
	[HouseholdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BankAccountId]    Script Date: 2/29/2016 9:24:39 AM ******/
CREATE NONCLUSTERED INDEX [IX_BankAccountId] ON [dbo].[Transactions]
(
	[BankAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BudgetItemId]    Script Date: 2/29/2016 9:24:39 AM ******/
CREATE NONCLUSTERED INDEX [IX_BudgetItemId] ON [dbo].[Transactions]
(
	[BudgetItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CategoryId]    Script Date: 2/29/2016 9:24:39 AM ******/
CREATE NONCLUSTERED INDEX [IX_CategoryId] ON [dbo].[Transactions]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 2/29/2016 9:24:39 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Transactions]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUsers_dbo.Households_HouseholdId] FOREIGN KEY([HouseholdId])
REFERENCES [dbo].[Households] ([Id])
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_dbo.AspNetUsers_dbo.Households_HouseholdId]
GO
ALTER TABLE [dbo].[BankAccounts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BankAccounts_dbo.Households_HouseholdId] FOREIGN KEY([HouseholdId])
REFERENCES [dbo].[Households] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BankAccounts] CHECK CONSTRAINT [FK_dbo.BankAccounts_dbo.Households_HouseholdId]
GO
ALTER TABLE [dbo].[BudgetItems]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BudgetItems_dbo.AspNetUsers_Creator_Id] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[BudgetItems] CHECK CONSTRAINT [FK_dbo.BudgetItems_dbo.AspNetUsers_Creator_Id]
GO
ALTER TABLE [dbo].[BudgetItems]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BudgetItems_dbo.Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BudgetItems] CHECK CONSTRAINT [FK_dbo.BudgetItems_dbo.Categories_CategoryId]
GO
ALTER TABLE [dbo].[BudgetItems]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BudgetItems_dbo.Households_HouseholdId] FOREIGN KEY([HouseholdId])
REFERENCES [dbo].[Households] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BudgetItems] CHECK CONSTRAINT [FK_dbo.BudgetItems_dbo.Households_HouseholdId]
GO
ALTER TABLE [dbo].[BudgetItems]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BudgetItems_dbo.Warnings_WarnAtId] FOREIGN KEY([WarningId])
REFERENCES [dbo].[Warnings] ([Id])
GO
ALTER TABLE [dbo].[BudgetItems] CHECK CONSTRAINT [FK_dbo.BudgetItems_dbo.Warnings_WarnAtId]
GO
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Categories_dbo.Households_HouseholdId] FOREIGN KEY([HouseholdId])
REFERENCES [dbo].[Households] ([Id])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_dbo.Categories_dbo.Households_HouseholdId]
GO
ALTER TABLE [dbo].[InvitedUsers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.InvitedUsers_dbo.Households_HouseholdId] FOREIGN KEY([HouseholdId])
REFERENCES [dbo].[Households] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InvitedUsers] CHECK CONSTRAINT [FK_dbo.InvitedUsers_dbo.Households_HouseholdId]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Transactions_dbo.AspNetUsers_User_Id] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_dbo.Transactions_dbo.AspNetUsers_User_Id]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Transactions_dbo.BankAccounts_BankAccountId] FOREIGN KEY([BankAccountId])
REFERENCES [dbo].[BankAccounts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_dbo.Transactions_dbo.BankAccounts_BankAccountId]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Transactions_dbo.BudgetItems_BudgetItemId] FOREIGN KEY([BudgetItemId])
REFERENCES [dbo].[BudgetItems] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_dbo.Transactions_dbo.BudgetItems_BudgetItemId]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Transactions_dbo.Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_dbo.Transactions_dbo.Categories_CategoryId]
GO
USE [master]
GO
ALTER DATABASE [awest-budget] SET  READ_WRITE 
GO
