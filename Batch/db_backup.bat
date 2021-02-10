@echo off

set db_user=app
set db_password=123456
set db_server=(local)
set backup_folder=C:\Work\Backups

CALL :config

TIMEOUT 5
exit

:config
set target_folder=.\..\Configurator.Config.Api\Backups
set db_name=Configurator.Config
CALL :do_backup
copy "%backup_folder%\%db_name%.bak" %target_folder%\%db_name%.bak
exit /B 0

:do_backup
echo.
echo Backing up database %db_name% ...
echo -----------------------------------------------------------------------
echo.
sqlcmd -U %db_user% -P %db_password% -S %db_server% -Q "BACKUP DATABASE [%db_name%] TO DISK = '%backup_folder%\%db_name%.bak' WITH FORMAT"
echo.
echo File saved to %backup_folder%\%db_name%.bak
echo -----------------------------------------------------------------------
echo.
exit /B 0

