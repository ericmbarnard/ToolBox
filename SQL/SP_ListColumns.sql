USE [master]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_listcolumns]
    @table  SYSNAME,
    @list   CHAR(1) = 'L',
    @prefix SYSNAME = '',
    @schema SYSNAME = 'dbo'
AS
BEGIN
SET NOCOUNT ON;
SET QUOTED_IDENTIFIER OFF

DECLARE @columnlist NVARCHAR(4000)
       ,@colctr INT = 1
       ,@sqlcmd NVARCHAR(500)

CREATE TABLE #column
(
    ColumnName NVARCHAR(256),
    Ordinal INT
)

CREATE UNIQUE CLUSTERED INDEX IC_ColumnList99 ON #column (Ordinal)

SET @columnlist =    ''
SET @table =        LTRIM(RTRIM(@table))
SET @list =         LTRIM(RTRIM(@list))
SET @prefix =       LTRIM(RTRIM(@prefix))
SET @schema =       LTRIM(RTRIM(@schema))

INSERT INTO #column (ColumnName, Ordinal)
SELECT  '['+LTRIM(RTRIM(COLUMN_NAME))+']', ORDINAL_POSITION
FROM    INFORMATION_SCHEMA.COLUMNS
WHERE   TABLE_NAME = @table AND
        TABLE_SCHEMA = @schema
ORDER BY ORDINAL_POSITION

-- Check to make sure we actually got usuable input from the User
IF ((SELECT COUNT(*) FROM #column) = 0)
RAISERROR('Bad Table Information - Please Try Again', 16, 1, 1)

IF(@prefix <> '')
BEGIN
    UPDATE #column
    SET    ColumnName = @prefix + ColumnName
END

IF(@list = 'L')
    BEGIN
        -- If it is 'L' just append a ',' to each line, output would be:
        -- TestCol1,
        -- TestCol2

        UPDATE  C
        SET     @columnlist = @columnlist + C.ColumnName + ',' + CHAR(10)
        FROM    #column C
    END
ELSE
    BEGIN
        -- else we output a 'w' where it just a ',' separated list of Column names
        UPDATE  C
        SET     @columnlist = @columnlist + C.ColumnName + ','
        FROM    #column C

        -- Remove the last comma
        SET @columnlist = LEFT(@columnlist,LEN(@columnlist) - 1)
    END

-- Output the results so that they can be nicely copy
-- and pasted from the comment window
PRINT(@columnlist)

SET QUOTED_IDENTIFIER ON
SET NOCOUNT OFF

END

-- And the Magic
EXECUTE sp_MS_marksystemobject 'sp_listcolumns'