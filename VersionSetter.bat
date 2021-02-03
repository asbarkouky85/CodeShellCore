@echo off

cd CodeShellCore.Batch
start moldster_files.bat
start db_backup.bat
cd..
toolset -p CodeShellCore 			2.15.13 -d %cd%
toolset -p CodeShellCore.Caching 	2.15.13 -d %cd%
toolset -p CodeShellCore.MQ 		2.15.13 -d %cd%
toolset -p CodeShellCore.Reporting 	2.15.13 -d %cd%

toolset -p Asga.Auth 				2.15.13 -d %cd%
toolset -p Asga.Auth.Module 		2.15.13 -d %cd%

toolset -p Asga.Public 				2.15.13 -d %cd%
toolset -p Asga.Public.Module 		2.15.13 -d %cd%

toolset -p Asga.Mobile 				2.15.13 -d %cd%
toolset -p Asga.Mobile.Module 		2.15.13 -d %cd%

toolset -p Asga.Dashboard 			2.15.13 -d %cd%

toolset -p CodeShellCore.Moldster 	2.15.13.0 -d %cd%

toolset -p CodeShellCore.Web 		2.15.14 -d %cd%
toolset -p CodeShellCore.Web.Razor 	2.15.14.0 -d %cd%

toolset -p Asga.Auth.Web 			2.15.14 -d %cd%
toolset -p Asga.Public.Web 			2.15.14 -d %cd%
toolset -p Asga.Mobile.Web 			2.15.14 -d %cd%
toolset -p Asga.Dashboard.Web 		2.15.14 -d %cd%

toolset -p Asga.Auth.Molds 			2.14.2 -d %cd%
toolset -p Asga.Public.Molds 		2.14.2 -d %cd%

timeout 10