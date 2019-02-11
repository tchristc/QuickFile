https://docs.microsoft.com/en-us/sql/relational-databases/blob/enable-and-configure-filestream?view=sql-server-2017

EXEC sp_configure filestream_access_level, 3 
RECONFIGURE  

CREATE DATABASE QuickFile 
ON
PRIMARY ( NAME = QuickFile,
    FILENAME = 'c:\data\QuickFile.mdf'),
FILEGROUP QuickFileFileStreamGroup CONTAINS FILESTREAM( NAME = QuickFileFileStream,
    FILENAME = 'c:\data\QuickFile_filestream')
LOG ON  ( NAME = QuickFileLog,
    FILENAME = 'c:\data\QuickFile_log.ldf')
GO

dotnet ef dbcontext scaffold "Server=(local);Database=QuickFile;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models

INSERT INTO [dbo].[FileStore]
           ([FileData])
     VALUES (CAST('doing this for testing purposes' AS VARBINARY(MAX)))