@echo off

set db_name=%~1
set target_folder=%~2

set db_user=app
set db_password=123456
set db_server=.

set backup_folder=C:\Work\Backups

echo Backing up database %db_name% ...
echo.
sqlcmd -U %db_user% -P %db_password% -S %db_server% -Q "BACKUP DATABASE [%db_name%] TO DISK = '%backup_folder%\%db_name%.bak' WITH FORMAT"
echo.
echo File saved to %sql_backup_folder%\%db_name%.bak
copy "%backup_folder%\%db_name%.bak" %target_folder%\%db_name%.bak
echo File copied to %target_folder%\%db_name%.bak
echo.
echo -----------------------------------------------------------------------
echo.
exit