USE [master]
GO
/****** Object:  Database [whomake_database_on]    Script Date: 15.08.2017 21:25:45 ******/
CREATE DATABASE [whomake_database_on]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'whomake_database_on', FILENAME = N'D:\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\whomake_database_on.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'whomake_database_on_log', FILENAME = N'D:\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\whomake_database_on_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [whomake_database_on] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [whomake_database_on].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [whomake_database_on] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [whomake_database_on] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [whomake_database_on] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [whomake_database_on] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [whomake_database_on] SET ARITHABORT OFF 
GO
ALTER DATABASE [whomake_database_on] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [whomake_database_on] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [whomake_database_on] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [whomake_database_on] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [whomake_database_on] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [whomake_database_on] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [whomake_database_on] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [whomake_database_on] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [whomake_database_on] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [whomake_database_on] SET  DISABLE_BROKER 
GO
ALTER DATABASE [whomake_database_on] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [whomake_database_on] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [whomake_database_on] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [whomake_database_on] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [whomake_database_on] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [whomake_database_on] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [whomake_database_on] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [whomake_database_on] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [whomake_database_on] SET  MULTI_USER 
GO
ALTER DATABASE [whomake_database_on] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [whomake_database_on] SET DB_CHAINING OFF 
GO
ALTER DATABASE [whomake_database_on] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [whomake_database_on] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [whomake_database_on] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [whomake_database_on] SET QUERY_STORE = OFF
GO
USE [whomake_database_on]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [whomake_database_on]
GO
/****** Object:  Table [dbo].[bought_task]    Script Date: 15.08.2017 21:25:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bought_task](
	[bought_task_id] [int] IDENTITY(1,1) NOT NULL,
	[bought_task_id_person] [int] NOT NULL,
	[bought_task_id_task] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[category]    Script Date: 15.08.2017 21:25:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[category_id] [int] IDENTITY(1,1) NOT NULL,
	[category_name] [varchar](100) NOT NULL,
	[category_price] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[category_performers]    Script Date: 15.08.2017 21:25:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category_performers](
	[cat_perf_id] [int] IDENTITY(1,1) NOT NULL,
	[category_performers_id_person] [int] NOT NULL,
	[category_performers_id_category] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[offer_tasks]    Script Date: 15.08.2017 21:25:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[offer_tasks](
	[offer_tasks_id] [int] IDENTITY(1,1) NOT NULL,
	[offer_tasks_id_person] [int] NULL,
	[offer_tasks_id_task] [int] NULL,
	[offer_tasks_id_who] [int] NULL,
	[offer_tasks_name] [varchar](350) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[operations_history]    Script Date: 15.08.2017 21:25:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[operations_history](
	[operations_history_id] [int] NULL,
	[operations_history_id_person] [int] NULL,
	[operations_history_sum] [int] NULL,
	[operations_history_date] [datetime] NULL,
	[operations_history_id_task] [int] NULL,
	[operations_history_raise] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[performers]    Script Date: 15.08.2017 21:25:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[performers](
	[performers_id] [int] IDENTITY(1,1) NOT NULL,
	[performers_name] [varchar](100) NULL,
	[performers_secname] [varchar](100) NULL,
	[performers_age] [int] NULL,
	[performers_photo] [varchar](500) NULL,
	[performers_yourself] [varchar](1000) NULL,
	[performers_services] [varchar](1000) NULL,
	[performers_works] [varchar](1000) NULL,
	[performers_phone] [varchar](15) NULL,
	[performers_email] [varchar](100) NULL,
	[performers_id_person] [int] NULL,
	[performers_status] [varchar](50) NULL,
	[performers_money] [int] NULL,
	[performers_viewes] [int] NULL,
	[performers_datereg] [date] NULL,
	[performers_last] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[red_admin_red]    Script Date: 15.08.2017 21:25:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[red_admin_red](
	[red_admin_red_id] [int] IDENTITY(1,1) NOT NULL,
	[red_admin_red_email] [varchar](100) NULL,
	[red_admin_red_pass] [varchar](500) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[services]    Script Date: 15.08.2017 21:25:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[services](
	[services_id] [int] IDENTITY(1,1) NOT NULL,
	[services_id_category] [int] NOT NULL,
	[services_name] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[session]    Script Date: 15.08.2017 21:25:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[session](
	[id_session] [varchar](200) NULL,
	[putdate] [datetime] NULL,
	[user] [varchar](100) NULL,
	[ip] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tasks]    Script Date: 15.08.2017 21:25:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tasks](
	[tasks_id] [int] IDENTITY(1,1) NOT NULL,
	[tasks_id_person] [int] NOT NULL,
	[tasks_category] [int] NOT NULL,
	[tasks_service] [int] NOT NULL,
	[tasks_name] [varchar](350) NOT NULL,
	[tasks_description] [varchar](1000) NOT NULL,
	[tasks_price] [int] NOT NULL,
	[tasks_phone] [varchar](16) NOT NULL,
	[tasks_date_begin] [date] NOT NULL,
	[tasks_date_end] [date] NOT NULL,
	[tasks_date_exeq_begin] [date] NOT NULL,
	[tasks_time_exeq_begin] [time](7) NOT NULL,
	[tasks_date_exeq_end] [date] NOT NULL,
	[tasks_time_exeq_end] [time](7) NOT NULL,
	[tasks_adres] [varchar](350) NOT NULL,
	[tasks_name_person] [varchar](250) NOT NULL,
	[tasks_secname_person] [varchar](250) NOT NULL,
	[tasks_creation] [datetime] NOT NULL,
	[tasks_status] [varchar](100) NOT NULL,
	[tasks_views] [int] NOT NULL,
	[tasks_money] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tech_user]    Script Date: 15.08.2017 21:25:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tech_user](
	[tech_id] [int] IDENTITY(1,1) NOT NULL,
	[Tech_ip] [varchar](50) NULL,
	[tech_dnsname] [varchar](200) NULL,
	[tech_lang] [varchar](300) NULL,
	[tech_browser] [varchar](300) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[uploads_images]    Script Date: 15.08.2017 21:25:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[uploads_images](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[person_id] [int] NOT NULL,
	[image] [varchar](255) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 15.08.2017 21:25:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[users_id] [int] IDENTITY(1,1) NOT NULL,
	[users_name] [varchar](50) NULL,
	[users_secname] [varchar](100) NULL,
	[users_pass] [varchar](500) NULL,
	[users_email] [varchar](100) NULL,
	[users_money] [float] NULL,
	[users_accept] [int] NULL,
	[users_regdate] [datetime] NULL,
	[users_ip] [varchar](50) NULL,
	[users_ban] [int] NULL
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [whomake_database_on] SET  READ_WRITE 
GO
