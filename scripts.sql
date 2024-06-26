USE [master]
GO
/****** Object:  Database [JustBlog]    Script Date: 4/25/2024 2:03:23 PM ******/
CREATE DATABASE [JustBlog]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'JustBlog', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\JustBlog.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'JustBlog_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\JustBlog_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [JustBlog] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [JustBlog].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [JustBlog] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [JustBlog] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [JustBlog] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [JustBlog] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [JustBlog] SET ARITHABORT OFF 
GO
ALTER DATABASE [JustBlog] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [JustBlog] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [JustBlog] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [JustBlog] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [JustBlog] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [JustBlog] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [JustBlog] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [JustBlog] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [JustBlog] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [JustBlog] SET  ENABLE_BROKER 
GO
ALTER DATABASE [JustBlog] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [JustBlog] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [JustBlog] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [JustBlog] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [JustBlog] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [JustBlog] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [JustBlog] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [JustBlog] SET RECOVERY FULL 
GO
ALTER DATABASE [JustBlog] SET  MULTI_USER 
GO
ALTER DATABASE [JustBlog] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [JustBlog] SET DB_CHAINING OFF 
GO
ALTER DATABASE [JustBlog] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [JustBlog] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [JustBlog] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [JustBlog] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'JustBlog', N'ON'
GO
ALTER DATABASE [JustBlog] SET QUERY_STORE = ON
GO
ALTER DATABASE [JustBlog] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [JustBlog]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 4/25/2024 2:03:23 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 4/25/2024 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 4/25/2024 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 4/25/2024 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 4/25/2024 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 4/25/2024 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 4/25/2024 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[Age] [int] NOT NULL,
	[AboutMe] [nvarchar](max) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 4/25/2024 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 4/25/2024 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[UrlSlug] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](1024) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 4/25/2024 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[CommentHeader] [nvarchar](255) NOT NULL,
	[CommentText] [nvarchar](1024) NOT NULL,
	[CommentTime] [datetime2](7) NOT NULL,
	[PostId] [int] NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 4/25/2024 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[ShortDescription] [nvarchar](255) NOT NULL,
	[PostContent] [nvarchar](1045) NOT NULL,
	[UrlSlug] [nvarchar](255) NOT NULL,
	[Published] [bit] NOT NULL,
	[PostedOn] [datetime2](7) NOT NULL,
	[Modified] [datetime2](7) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[ViewCount] [int] NOT NULL,
	[RateCount] [int] NOT NULL,
	[TotalRate] [float] NOT NULL,
	[Rate] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostTagMaps]    Script Date: 4/25/2024 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostTagMaps](
	[TagId] [int] NOT NULL,
	[PostId] [int] NOT NULL,
 CONSTRAINT [PK_PostTagMaps] PRIMARY KEY CLUSTERED 
(
	[TagId] ASC,
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 4/25/2024 2:03:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tags](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[UrlSlug] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](1024) NOT NULL,
	[Count] [int] NOT NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240421145926_InitialJustBlog', N'8.0.4')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'1', N'User', N'USER', N'87BDE785-366D-414B-B7C7-33F21E40DE5C')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'2', N'Contributor', N'CONTRIBUTOR', N'6931E29C-F97A-4708-B620-9ECBB649E2B0')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'3', N'Admin', N'ADMIN', N'86BF9E98-F641-4C52-9251-C87131CF2EE4')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'4', N'BlogOwner', N'BLOGOWNER', N'48D2405A-B6EE-45DC-A2CF-A0F212AB2D31')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'042d094c-d258-4163-b63e-29152d1a2e10', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'92928eed-01c5-4c89-adab-0938c6f98a48', N'1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'042d094c-d258-4163-b63e-29152d1a2e10', N'2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'92928eed-01c5-4c89-adab-0938c6f98a48', N'2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'92928eed-01c5-4c89-adab-0938c6f98a48', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'92928eed-01c5-4c89-adab-0938c6f98a48', N'4')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Age], [AboutMe], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'042d094c-d258-4163-b63e-29152d1a2e10', 3, N'hello world', N'sonTest123', N'SONTEST123', N'SonHXHE172036@fpt.edu.vn', N'SONHXHE172036@FPT.EDU.VN', 1, N'AQAAAAIAAYagAAAAEPWcCznk3WjpzPPY9HSI+COSUB6gNkwbCfHX8qJOt7CDLyD3NatNsXun3bXJ//w3HA==', N'LBQDQUE2AP6GLKHXP4BSMNM5VTVW6GYD', N'713d2813-2985-430b-85e4-fe811e09ef3f', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [Age], [AboutMe], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'92928eed-01c5-4c89-adab-0938c6f98a48', 19, N'Hello World', N'BlogOwner', N'BLOGOWNER', N'mh512475@gmail.com', N'MH512475@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEGyKncOKLEhoNXM36SNTRzv4TscobflQgeMbkQBBzEoxTVLdvMC/x3u5eiQQlD2bxw==', N'QBBUDB5DK6JVH5QYIYGNVMSK7PE26FOS', N'19356fe8-7976-46b1-a753-33541fe23817', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name], [UrlSlug], [Description]) VALUES (1, N'MVC Framework', N'mvc-framework', N'A framework for building web applications using the Model-View-Controller architectural pattern.')
INSERT [dbo].[Categories] ([Id], [Name], [UrlSlug], [Description]) VALUES (2, N'ASP.NET MVC', N'asp-net-mvc', N'A web application framework developed by Microsoft, which implements the MVC pattern.')
INSERT [dbo].[Categories] ([Id], [Name], [UrlSlug], [Description]) VALUES (3, N'Spring MVC', N'spring-mvc', N'A framework in the Spring Framework stack for building web applications using the Model-View-Controller pattern.')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Comments] ON 

INSERT [dbo].[Comments] ([Id], [Name], [Email], [CommentHeader], [CommentText], [CommentTime], [PostId]) VALUES (1, N'sonTest123', N'SonHXHE172036@fpt.edu.vn', N'Comment Header', N'Hello everyone', CAST(N'2024-04-23T17:26:47.2227427' AS DateTime2), 1)
INSERT [dbo].[Comments] ([Id], [Name], [Email], [CommentHeader], [CommentText], [CommentTime], [PostId]) VALUES (2, N'sonTest123', N'SonHXHE172036@fpt.edu.vn', N'Comment Header', N'Hello everyone test ajax', CAST(N'2024-04-23T17:36:11.0864353' AS DateTime2), 1)
INSERT [dbo].[Comments] ([Id], [Name], [Email], [CommentHeader], [CommentText], [CommentTime], [PostId]) VALUES (3, N'sonTest123', N'SonHXHE172036@fpt.edu.vn', N'Comment Header', N'test gere', CAST(N'2024-04-23T17:36:23.8751553' AS DateTime2), 1)
INSERT [dbo].[Comments] ([Id], [Name], [Email], [CommentHeader], [CommentText], [CommentTime], [PostId]) VALUES (4, N'sonTest123', N'SonHXHE172036@fpt.edu.vn', N'Comment Header', N'test here', CAST(N'2024-04-23T17:38:38.5353044' AS DateTime2), 2)
INSERT [dbo].[Comments] ([Id], [Name], [Email], [CommentHeader], [CommentText], [CommentTime], [PostId]) VALUES (5, N'BlogOwner', N'mh512475@gmail.com', N'Comment Header', N'now test
', CAST(N'2024-04-23T17:38:49.7391363' AS DateTime2), 2)
INSERT [dbo].[Comments] ([Id], [Name], [Email], [CommentHeader], [CommentText], [CommentTime], [PostId]) VALUES (6, N'sonTest123', N'SonHXHE172036@fpt.edu.vn', N'Comment Header', N'Hello there
', CAST(N'2024-04-23T17:44:11.2174495' AS DateTime2), 3)
INSERT [dbo].[Comments] ([Id], [Name], [Email], [CommentHeader], [CommentText], [CommentTime], [PostId]) VALUES (7, N'sonTest123', N'SonHXHE172036@fpt.edu.vn', N'Comment Header', N'hello world!', CAST(N'2024-04-23T22:14:27.6200622' AS DateTime2), 8)
INSERT [dbo].[Comments] ([Id], [Name], [Email], [CommentHeader], [CommentText], [CommentTime], [PostId]) VALUES (8, N'BlogOwner', N'mh512475@gmail.com', N'Comment Header', N'Hello world!!!', CAST(N'2024-04-23T23:12:14.9611842' AS DateTime2), 5)
INSERT [dbo].[Comments] ([Id], [Name], [Email], [CommentHeader], [CommentText], [CommentTime], [PostId]) VALUES (9, N'sonTest123', N'SonHXHE172036@fpt.edu.vn', N'Comment Header', N'Hello you', CAST(N'2024-04-23T23:12:26.1518214' AS DateTime2), 5)
INSERT [dbo].[Comments] ([Id], [Name], [Email], [CommentHeader], [CommentText], [CommentTime], [PostId]) VALUES (20, N'BlogOwner', N'mh512475@gmail.com', N'Comment Header', N'rgfedds', CAST(N'2024-04-24T13:43:51.4254911' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[Comments] OFF
GO
SET IDENTITY_INSERT [dbo].[Posts] ON 

INSERT [dbo].[Posts] ([Id], [Title], [ShortDescription], [PostContent], [UrlSlug], [Published], [PostedOn], [Modified], [CategoryId], [ViewCount], [RateCount], [TotalRate], [Rate]) VALUES (1, N'Introduction to MVC Framework', N'Learn the basics of MVC', N'In this post, we introduce the MVC framework and its components. MVC stands for Model-View-Controller, which is a software architectural pattern widely used for web development.', N'asd', 1, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 1, 0, 0, 0, CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[Posts] ([Id], [Title], [ShortDescription], [PostContent], [UrlSlug], [Published], [PostedOn], [Modified], [CategoryId], [ViewCount], [RateCount], [TotalRate], [Rate]) VALUES (2, N'Getting Started with ASP.NET MVC', N'Start building web apps with ASP.NET MVC', N'This post guides you through setting up your development environment for ASP.NET MVC and creating your first MVC application.', N'getting-started-with-asp-net-mvc', 1, CAST(N'2024-04-20T00:00:00.0000000' AS DateTime2), CAST(N'2024-04-20T00:00:00.0000000' AS DateTime2), 2, 60, 15, 55, CAST(4.60 AS Decimal(18, 2)))
INSERT [dbo].[Posts] ([Id], [Title], [ShortDescription], [PostContent], [UrlSlug], [Published], [PostedOn], [Modified], [CategoryId], [ViewCount], [RateCount], [TotalRate], [Rate]) VALUES (3, N'Spring MVC Essentials', N'Essential concepts for Spring MVC', N'Learn about the core concepts of Spring MVC, including controllers, views, and models. This post covers everything you need to know to get started with Spring MVC development.', N'spring-mvc-essentials', 1, CAST(N'2024-04-19T00:00:00.0000000' AS DateTime2), CAST(N'2024-04-19T00:00:00.0000000' AS DateTime2), 3, 70, 20, 60, CAST(4.80 AS Decimal(18, 2)))
INSERT [dbo].[Posts] ([Id], [Title], [ShortDescription], [PostContent], [UrlSlug], [Published], [PostedOn], [Modified], [CategoryId], [ViewCount], [RateCount], [TotalRate], [Rate]) VALUES (4, N'Advanced Topics in MVC', N'Explore advanced features of MVC', N'Delve deeper into MVC framework with this post, where we cover topics such as routing, filters, and dependency injection.', N'advanced-topics-in-mvc', 1, CAST(N'2024-04-18T00:00:00.0000000' AS DateTime2), CAST(N'2024-04-18T00:00:00.0000000' AS DateTime2), 1, 80, 25, 70, CAST(4.70 AS Decimal(18, 2)))
INSERT [dbo].[Posts] ([Id], [Title], [ShortDescription], [PostContent], [UrlSlug], [Published], [PostedOn], [Modified], [CategoryId], [ViewCount], [RateCount], [TotalRate], [Rate]) VALUES (5, N'Building RESTful APIs with ASP.NET MVC', N'Create RESTful APIs using ASP.NET MVC', N'This post demonstrates how to build RESTful APIs using ASP.NET MVC, including implementing CRUD operations and handling authentication.', N'building-restful-apis-with-asp-net-mvc', 1, CAST(N'2024-04-17T00:00:00.0000000' AS DateTime2), CAST(N'2024-04-17T00:00:00.0000000' AS DateTime2), 2, 90, 30, 75, CAST(4.90 AS Decimal(18, 2)))
INSERT [dbo].[Posts] ([Id], [Title], [ShortDescription], [PostContent], [UrlSlug], [Published], [PostedOn], [Modified], [CategoryId], [ViewCount], [RateCount], [TotalRate], [Rate]) VALUES (6, N'Unit Testing in Spring MVC', N'Test your Spring MVC applications', N'Learn how to write unit tests for Spring MVC controllers, services, and repositories. This post covers testing best practices and techniques for ensuring the reliability of your Spring MVC applications.', N'unit-testing-in-spring-mvc', 1, CAST(N'2024-04-16T00:00:00.0000000' AS DateTime2), CAST(N'2024-04-16T00:00:00.0000000' AS DateTime2), 3, 100, 35, 80, CAST(4.80 AS Decimal(18, 2)))
INSERT [dbo].[Posts] ([Id], [Title], [ShortDescription], [PostContent], [UrlSlug], [Published], [PostedOn], [Modified], [CategoryId], [ViewCount], [RateCount], [TotalRate], [Rate]) VALUES (7, N'MVC Best Practices', N'Best practices for MVC development', N'Discover best practices and common pitfalls to avoid when developing applications using the MVC framework. This post covers topics such as code organization, separation of concerns, and performance optimization.', N'mvc-best-practices', 1, CAST(N'2024-04-15T00:00:00.0000000' AS DateTime2), CAST(N'2024-04-15T00:00:00.0000000' AS DateTime2), 1, 110, 40, 85, CAST(4.70 AS Decimal(18, 2)))
INSERT [dbo].[Posts] ([Id], [Title], [ShortDescription], [PostContent], [UrlSlug], [Published], [PostedOn], [Modified], [CategoryId], [ViewCount], [RateCount], [TotalRate], [Rate]) VALUES (8, N'Authentication and Authorization in ASP.NET MVC', N'Secure your ASP.NET MVC applications', N'Learn how to implement authentication and authorization in ASP.NET MVC applications, including using built-in authentication mechanisms and custom authentication providers.', N'authentication-and-authorization-in-asp-net-mvc', 1, CAST(N'2024-04-14T00:00:00.0000000' AS DateTime2), CAST(N'2024-04-14T00:00:00.0000000' AS DateTime2), 2, 120, 45, 90, CAST(4.90 AS Decimal(18, 2)))
INSERT [dbo].[Posts] ([Id], [Title], [ShortDescription], [PostContent], [UrlSlug], [Published], [PostedOn], [Modified], [CategoryId], [ViewCount], [RateCount], [TotalRate], [Rate]) VALUES (9, N'Advanced Dependency Injection in Spring MVC', N'Master dependency injection in Spring MVC', N'This post dives deep into dependency injection in Spring MVC, covering topics such as constructor injection, setter injection, and field injection. Learn how to leverage Spring''s powerful DI capabilities to build flexible and maintainable applications.', N'advanced-dependency-injection-in-spring-mvc', 1, CAST(N'2024-04-13T00:00:00.0000000' AS DateTime2), CAST(N'2024-04-13T00:00:00.0000000' AS DateTime2), 3, 130, 50, 95, CAST(4.80 AS Decimal(18, 2)))
INSERT [dbo].[Posts] ([Id], [Title], [ShortDescription], [PostContent], [UrlSlug], [Published], [PostedOn], [Modified], [CategoryId], [ViewCount], [RateCount], [TotalRate], [Rate]) VALUES (10, N'MVC Security Best Practices', N'Secure your MVC applications', N'Explore best practices for securing MVC applications against common security threats. This post covers topics such as input validation, authentication, authorization, and data protection.', N'mvc-security-best-practices', 1, CAST(N'2024-04-12T00:00:00.0000000' AS DateTime2), CAST(N'2024-04-12T00:00:00.0000000' AS DateTime2), 1, 140, 55, 100, CAST(5.00 AS Decimal(18, 2)))
INSERT [dbo].[Posts] ([Id], [Title], [ShortDescription], [PostContent], [UrlSlug], [Published], [PostedOn], [Modified], [CategoryId], [ViewCount], [RateCount], [TotalRate], [Rate]) VALUES (11, N'Building Real-Time Web Applications with SignalR', N'Create real-time web apps using SignalR', N'Learn how to build real-time web applications using SignalR with ASP.NET MVC. This post covers the basics of SignalR and demonstrates how to create a simple chat application.', N'building-real-time-web-applications-with-signalr', 1, CAST(N'2024-04-11T00:00:00.0000000' AS DateTime2), CAST(N'2024-04-11T00:00:00.0000000' AS DateTime2), 2, 150, 60, 105, CAST(4.90 AS Decimal(18, 2)))
INSERT [dbo].[Posts] ([Id], [Title], [ShortDescription], [PostContent], [UrlSlug], [Published], [PostedOn], [Modified], [CategoryId], [ViewCount], [RateCount], [TotalRate], [Rate]) VALUES (12, N'Internationalization and Localization in Spring MVC', N'Support multiple languages in Spring MVC', N'Discover how to implement internationalization and localization in Spring MVC applications to support multiple languages and cultures. This post covers configuring message bundles, resolving locale, and displaying localized content.', N'internationalization-and-localization-in-spring-mvc', 1, CAST(N'2024-04-10T00:00:00.0000000' AS DateTime2), CAST(N'2024-04-10T00:00:00.0000000' AS DateTime2), 3, 160, 65, 110, CAST(4.80 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Posts] OFF
GO
SET IDENTITY_INSERT [dbo].[Tags] ON 

INSERT [dbo].[Tags] ([Id], [Name], [UrlSlug], [Description], [Count]) VALUES (1, N'ASP.NET', N'asp-net', N'A web development framework by Microsoft.', 10)
INSERT [dbo].[Tags] ([Id], [Name], [UrlSlug], [Description], [Count]) VALUES (2, N'MVC', N'mvc', N'Model-View-Controller architectural pattern.', 15)
INSERT [dbo].[Tags] ([Id], [Name], [UrlSlug], [Description], [Count]) VALUES (3, N'Spring', N'spring', N'A framework for building Java applications.', 20)
INSERT [dbo].[Tags] ([Id], [Name], [UrlSlug], [Description], [Count]) VALUES (4, N'C#', N'c-sharp', N'A programming language developed by Microsoft.', 25)
INSERT [dbo].[Tags] ([Id], [Name], [UrlSlug], [Description], [Count]) VALUES (5, N'Java', N'java', N'A popular programming language for enterprise applications.', 30)
INSERT [dbo].[Tags] ([Id], [Name], [UrlSlug], [Description], [Count]) VALUES (6, N'REST', N'rest', N'Representational State Transfer architectural style.', 35)
INSERT [dbo].[Tags] ([Id], [Name], [UrlSlug], [Description], [Count]) VALUES (7, N'API', N'api', N'Application Programming Interface.', 40)
INSERT [dbo].[Tags] ([Id], [Name], [UrlSlug], [Description], [Count]) VALUES (8, N'Entity Framework', N'entity-framework', N'Object-relational mapping framework for .NET.', 45)
INSERT [dbo].[Tags] ([Id], [Name], [UrlSlug], [Description], [Count]) VALUES (9, N'Hibernate', N'hibernate', N'Object-relational mapping framework for Java.', 50)
INSERT [dbo].[Tags] ([Id], [Name], [UrlSlug], [Description], [Count]) VALUES (10, N'Dependency Injection', N'dependency-injection', N'A software design pattern.', 55)
INSERT [dbo].[Tags] ([Id], [Name], [UrlSlug], [Description], [Count]) VALUES (11, N'Unit Testing', N'unit-testing', N'Testing individual units or components of a software.', 60)
INSERT [dbo].[Tags] ([Id], [Name], [UrlSlug], [Description], [Count]) VALUES (12, N'Integration Testing', N'integration-testing', N'Testing interactions between different units or components of a software.', 65)
INSERT [dbo].[Tags] ([Id], [Name], [UrlSlug], [Description], [Count]) VALUES (13, N'Authentication', N'authentication', N'Verifying the identity of a user.', 70)
INSERT [dbo].[Tags] ([Id], [Name], [UrlSlug], [Description], [Count]) VALUES (14, N'Authorization', N'authorization', N'Determining the access rights of a user.', 75)
INSERT [dbo].[Tags] ([Id], [Name], [UrlSlug], [Description], [Count]) VALUES (15, N'Logging', N'logging', N'Recording events that happen in a software application.', 80)
SET IDENTITY_INSERT [dbo].[Tags] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 4/25/2024 2:03:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 4/25/2024 2:03:23 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 4/25/2024 2:03:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 4/25/2024 2:03:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 4/25/2024 2:03:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 4/25/2024 2:03:23 PM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 4/25/2024 2:03:23 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comments_PostId]    Script Date: 4/25/2024 2:03:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_Comments_PostId] ON [dbo].[Comments]
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Posts_CategoryId]    Script Date: 4/25/2024 2:03:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_Posts_CategoryId] ON [dbo].[Posts]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PostTagMaps_PostId]    Script Date: 4/25/2024 2:03:23 PM ******/
CREATE NONCLUSTERED INDEX [IX_PostTagMaps_PostId] ON [dbo].[PostTagMaps]
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Posts_PostId] FOREIGN KEY([PostId])
REFERENCES [dbo].[Posts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Posts_PostId]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Categories_CategoryId]
GO
ALTER TABLE [dbo].[PostTagMaps]  WITH CHECK ADD  CONSTRAINT [FK_PostTagMaps_Posts_PostId] FOREIGN KEY([PostId])
REFERENCES [dbo].[Posts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PostTagMaps] CHECK CONSTRAINT [FK_PostTagMaps_Posts_PostId]
GO
ALTER TABLE [dbo].[PostTagMaps]  WITH CHECK ADD  CONSTRAINT [FK_PostTagMaps_Tags_TagId] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tags] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PostTagMaps] CHECK CONSTRAINT [FK_PostTagMaps_Tags_TagId]
GO
USE [master]
GO
ALTER DATABASE [JustBlog] SET  READ_WRITE 
GO
