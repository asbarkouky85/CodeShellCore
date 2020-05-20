@echo off

cd CodeShellCore.Batch
start moldster_files.bat
start db_backup.bat
cd..
toolset -p CodeShellCore 2.9.15 -d %cd%
toolset -p CodeShellCore.Caching 2.9.15 -d %cd%
toolset -p CodeShellCore.MQ 2.9.15 -d %cd%
toolset -p CodeShellCore.Reporting 2.9.15 -d %cd%
toolset -p CodeShellCore.Web 2.9.15 -d %cd%

toolset -p Asga 2.9.15 -d %cd%
toolset -p Asga.Common 2.9.15 -d %cd%
toolset -p Asga.Auth 2.9.15 -d %cd%
toolset -p Asga.Public 2.9.15 -d %cd%
toolset -p Asga.Web 2.9.15 -d %cd%

toolset -p Asga.Auth.Molds 2.9.13 -d %cd%

toolset -p CodeShellCore.Moldster 2.10.15 -d %cd%
toolset -p CodeShellCore.Web.Razor 2.10.15 -d %cd%
toolset -p Configurator.UI 2.10.8.0 -d %cd%

timeout 10