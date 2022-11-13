EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'SchoolDb'
GO
USE [master]
GO
ALTER DATABASE [SchoolDb] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
GO
USE [master]
GO
/****** Object:  Database [SchoolDb]    Script Date: 16.10.2022 11:49:16 ******/
DROP DATABASE [SchoolDb]
GO
