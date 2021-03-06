USE [master]
GO
/****** Object:  Database [EmployeeLeaveManagementSys]    Script Date: 5/20/2018 1:09:58 AM ******/
CREATE DATABASE [EmployeeLeaveManagementSys]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EmployeeLeaveManagementSys', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\EmployeeLeaveManagementSys.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'EmployeeLeaveManagementSys_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\EmployeeLeaveManagementSys_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EmployeeLeaveManagementSys].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET ARITHABORT OFF 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET  MULTI_USER 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [EmployeeLeaveManagementSys]
GO
/****** Object:  Table [dbo].[Designation]    Script Date: 5/20/2018 1:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Designation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DesignationName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Designation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 5/20/2018 1:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeUserId] [nvarchar](50) NOT NULL,
	[EmployeeName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[DesignationId] [int] NOT NULL,
	[PhoneNo] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeeLeaveAllocation]    Script Date: 5/20/2018 1:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeLeaveAllocation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DesignationId] [int] NOT NULL,
	[LeaveTypeId] [int] NOT NULL,
	[TotalLeave] [int] NOT NULL,
 CONSTRAINT [PK_EmployeeLeaveAllocation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeeLeaveRequest]    Script Date: 5/20/2018 1:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmployeeLeaveRequest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[LeaveTypeId] [int] NOT NULL,
	[Reason] [nvarchar](50) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[EntryDate] [date] NOT NULL,
	[TotalDay] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL,
 CONSTRAINT [PK_EmployeeLeaveRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmployeePassword]    Script Date: 5/20/2018 1:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EmployeePassword](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[Password] [varchar](50) NOT NULL,
 CONSTRAINT [PK_SetEmployeePassword] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmployeeUserType]    Script Date: 5/20/2018 1:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeUserType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[UserTypeId] [int] NOT NULL,
 CONSTRAINT [PK_SetEmployeeUserType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LeaveType]    Script Date: 5/20/2018 1:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LeaveTypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_LeaveType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserType]    Script Date: 5/20/2018 1:09:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserTypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Designation] ON 

INSERT [dbo].[Designation] ([Id], [DesignationName]) VALUES (1, N'Programmer')
INSERT [dbo].[Designation] ([Id], [DesignationName]) VALUES (2, N'System Administrator')
INSERT [dbo].[Designation] ([Id], [DesignationName]) VALUES (3, N'System Support Enginner')
SET IDENTITY_INSERT [dbo].[Designation] OFF
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([Id], [EmployeeUserId], [EmployeeName], [Email], [DesignationId], [PhoneNo], [Address]) VALUES (1, N'E-100101', N'Jasim Uddin', N'ujasim683@gmail.com', 1, N'01815289765', N'Shymoly, Dhaka')
SET IDENTITY_INSERT [dbo].[Employee] OFF
SET IDENTITY_INSERT [dbo].[EmployeeLeaveAllocation] ON 

INSERT [dbo].[EmployeeLeaveAllocation] ([Id], [DesignationId], [LeaveTypeId], [TotalLeave]) VALUES (5, 1, 1, 8)
SET IDENTITY_INSERT [dbo].[EmployeeLeaveAllocation] OFF
SET IDENTITY_INSERT [dbo].[EmployeeLeaveRequest] ON 

INSERT [dbo].[EmployeeLeaveRequest] ([Id], [EmployeeId], [LeaveTypeId], [Reason], [StartDate], [EndDate], [EntryDate], [TotalDay], [Status]) VALUES (8, 1, 1, N'Dicentry', CAST(0x3E3E0B00 AS Date), CAST(0x3E3E0B00 AS Date), CAST(0x3E3E0B00 AS Date), 1, N'Approved')
INSERT [dbo].[EmployeeLeaveRequest] ([Id], [EmployeeId], [LeaveTypeId], [Reason], [StartDate], [EndDate], [EntryDate], [TotalDay], [Status]) VALUES (9, 1, 1, N'Fabour', CAST(0x403E0B00 AS Date), CAST(0x413E0B00 AS Date), CAST(0x3E3E0B00 AS Date), 2, N'Approved')
SET IDENTITY_INSERT [dbo].[EmployeeLeaveRequest] OFF
SET IDENTITY_INSERT [dbo].[EmployeePassword] ON 

INSERT [dbo].[EmployeePassword] ([Id], [EmployeeId], [Password]) VALUES (4, 1, N'123456')
SET IDENTITY_INSERT [dbo].[EmployeePassword] OFF
SET IDENTITY_INSERT [dbo].[EmployeeUserType] ON 

INSERT [dbo].[EmployeeUserType] ([Id], [EmployeeId], [UserTypeId]) VALUES (4, 1, 1)
SET IDENTITY_INSERT [dbo].[EmployeeUserType] OFF
SET IDENTITY_INSERT [dbo].[LeaveType] ON 

INSERT [dbo].[LeaveType] ([Id], [LeaveTypeName]) VALUES (1, N'SickLeave')
INSERT [dbo].[LeaveType] ([Id], [LeaveTypeName]) VALUES (2, N'CasualLeave')
SET IDENTITY_INSERT [dbo].[LeaveType] OFF
SET IDENTITY_INSERT [dbo].[UserType] ON 

INSERT [dbo].[UserType] ([Id], [UserTypeName]) VALUES (1, N'Superadmin')
INSERT [dbo].[UserType] ([Id], [UserTypeName]) VALUES (2, N'Admin')
INSERT [dbo].[UserType] ([Id], [UserTypeName]) VALUES (3, N'User')
SET IDENTITY_INSERT [dbo].[UserType] OFF
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Designation] FOREIGN KEY([DesignationId])
REFERENCES [dbo].[Designation] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Designation]
GO
ALTER TABLE [dbo].[EmployeeLeaveAllocation]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeLeaveAllocation_Designation] FOREIGN KEY([DesignationId])
REFERENCES [dbo].[Designation] ([Id])
GO
ALTER TABLE [dbo].[EmployeeLeaveAllocation] CHECK CONSTRAINT [FK_EmployeeLeaveAllocation_Designation]
GO
ALTER TABLE [dbo].[EmployeeLeaveAllocation]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeLeaveAllocation_LeaveType] FOREIGN KEY([LeaveTypeId])
REFERENCES [dbo].[LeaveType] ([Id])
GO
ALTER TABLE [dbo].[EmployeeLeaveAllocation] CHECK CONSTRAINT [FK_EmployeeLeaveAllocation_LeaveType]
GO
ALTER TABLE [dbo].[EmployeeLeaveRequest]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeLeaveRequest_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[EmployeeLeaveRequest] CHECK CONSTRAINT [FK_EmployeeLeaveRequest_Employee]
GO
ALTER TABLE [dbo].[EmployeeLeaveRequest]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeLeaveRequest_LeaveType] FOREIGN KEY([LeaveTypeId])
REFERENCES [dbo].[LeaveType] ([Id])
GO
ALTER TABLE [dbo].[EmployeeLeaveRequest] CHECK CONSTRAINT [FK_EmployeeLeaveRequest_LeaveType]
GO
ALTER TABLE [dbo].[EmployeePassword]  WITH CHECK ADD  CONSTRAINT [FK_EmployeePassword_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[EmployeePassword] CHECK CONSTRAINT [FK_EmployeePassword_Employee]
GO
ALTER TABLE [dbo].[EmployeeUserType]  WITH CHECK ADD  CONSTRAINT [FK_SetEmployeeUserType_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[EmployeeUserType] CHECK CONSTRAINT [FK_SetEmployeeUserType_Employee]
GO
ALTER TABLE [dbo].[EmployeeUserType]  WITH CHECK ADD  CONSTRAINT [FK_SetEmployeeUserType_UserType] FOREIGN KEY([UserTypeId])
REFERENCES [dbo].[UserType] ([Id])
GO
ALTER TABLE [dbo].[EmployeeUserType] CHECK CONSTRAINT [FK_SetEmployeeUserType_UserType]
GO
USE [master]
GO
ALTER DATABASE [EmployeeLeaveManagementSys] SET  READ_WRITE 
GO
