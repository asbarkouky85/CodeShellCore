@echo off

cd Batch
start moldster_files.bat
start db_backup.bat
cd..
toolset -p CodeShellCore 2.7.24 -d %cd%
toolset -p CodeShellCore.Caching 2.7.24 -d %cd%
toolset -p CodeShellCore.MQ 2.7.24 -d %cd%
toolset -p CodeShellCore.Reporting 2.7.24 -d %cd%
toolset -p CodeShellCore.Web 2.7.24 -d %cd%

toolset -p Asga 2.7.24 -d %cd%
toolset -p Asga.Common 2.7.24 -d %cd%
toolset -p Asga.Auth 2.7.24 -d %cd%
toolset -p Asga.Public 2.7.24 -d %cd%
toolset -p Asga.Web 2.7.24 -d %cd%

toolset -p CodeShellCore.Moldster 2.7.24.1 -d %cd%
toolset -p CodeShellCore.Web.Razor 2.7.24.1 -d %cd%

toolset -p Configurator.UI 2.9.11.0 -d %cd%

timeout 10