@echo off

cd CodeShellCore.Batch
start moldster_files.bat
start db_backup.bat
cd..
toolset -p CodeShellCore 2.10.7 -d %cd%
toolset -p CodeShellCore.Caching 2.10.7 -d %cd%
toolset -p CodeShellCore.MQ 2.10.7 -d %cd%
toolset -p CodeShellCore.Reporting 2.10.7 -d %cd%
toolset -p CodeShellCore.Web 2.10.7 -d %cd%

toolset -p Asga 2.10.7 -d %cd%
toolset -p Asga.Common 2.10.7 -d %cd%
toolset -p Asga.Auth 2.10.7 -d %cd%
toolset -p Asga.Public 2.10.7 -d %cd%
toolset -p Asga.Web 2.10.7 -d %cd%

toolset -p Asga.Auth.Molds 2.10.1 -d %cd%

toolset -p CodeShellCore.Moldster 2.10.44 -d %cd%
toolset -p CodeShellCore.Web.Razor 2.10.44 -d %cd%
toolset -p Configurator.UI 2.10.42.0 -d %cd%


timeout 10