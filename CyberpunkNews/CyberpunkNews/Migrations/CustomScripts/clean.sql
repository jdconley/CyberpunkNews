﻿DECLARE @table_schema nvarchar(100)
       ,@table_name nvarchar(128)
       ,@constraint_schema nvarchar(100)
       ,@constraint_name nvarchar(128)
       ,@cmd nvarchar(2000)

-- drop all the constraints
DECLARE constraint_cursor CURSOR FOR
  select CONSTRAINT_SCHEMA, CONSTRAINT_NAME, TABLE_SCHEMA, TABLE_NAME
    from INFORMATION_SCHEMA.TABLE_CONSTRAINTS
   where TABLE_NAME != 'sysdiagrams'
    order by CONSTRAINT_TYPE asc -- FOREIGN KEY, then PRIMARY KEY

OPEN constraint_cursor
FETCH NEXT FROM constraint_cursor INTO @constraint_schema, @constraint_name, @table_schema, @table_name


WHILE @@FETCH_STATUS = 0
BEGIN
     SELECT @cmd = 'SET QUOTED_IDENTIFIER ON;ALTER TABLE ' + @table_schema + '.[' + @table_name + '] DROP CONSTRAINT ' + QUOTENAME(@constraint_name)	 
     EXEC sp_executesql @cmd
     --PRINT @cmd
     FETCH NEXT FROM constraint_cursor INTO @constraint_schema, @constraint_name, @table_schema, @table_name
END

CLOSE constraint_cursor
DEALLOCATE constraint_cursor

-- drop all the tables
DECLARE table_cursor CURSOR FOR
  select TABLE_SCHEMA, TABLE_NAME
    from INFORMATION_SCHEMA.TABLES
   where TABLE_NAME != 'sysdiagrams'

OPEN table_cursor
FETCH NEXT FROM table_cursor INTO @table_schema, @table_name

WHILE @@FETCH_STATUS = 0
BEGIN
     SELECT @cmd = 'DROP TABLE ' + @table_schema + '.[' + @table_name + ']'
     EXEC sp_executesql @cmd
     --PRINT @cmd
     FETCH NEXT FROM table_cursor INTO @table_schema, @table_name
END

CLOSE table_cursor
DEALLOCATE table_cursor

if exists(SELECT distinct 1 FROM INFORMATION_SCHEMA.SCHEMATA where SCHEMA_NAME = 'SignalR') begin DROP SCHEMA [SignalR] end