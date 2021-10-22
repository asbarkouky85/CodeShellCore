@echo off
timeout 5

start /B /W sql_backup loc Configurator.Config .\..\Configurator.Config.Api\Backups

timeout 5
exit