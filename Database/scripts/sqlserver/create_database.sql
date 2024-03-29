/*
USE [master]
GO
DROP DATABASE [antri]
GO
*/

USE [master]
GO
CREATE LOGIN [antrian] WITH PASSWORD=N'TOBASA', CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

CREATE DATABASE [antri]
GO
ALTER DATABASE [antri] SET RECOVERY FULL 
GO

ALTER AUTHORIZATION ON DATABASE::[antri] TO antrian
GO