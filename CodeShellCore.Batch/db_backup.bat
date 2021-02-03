@echo off
timeout 5
start /B /W back_local Example .\..\Example\SQL
start /B /W back_local Example.Config .\..\Example.Config.Api\SQL\Backups

timeout 5
exit