@echo off

cd CodeShellCore.Batch
start moldster_files.bat

cd..
toolset -p CodeShellCore 						5.2.0 -d %cd%
toolset -p CodeShellCore.Domain.Shared			5.2.0 -d %cd%
toolset -p CodeShellCore.Domain					5.2.0 -d %cd%
toolset -p CodeShellCore.Application.Contracts	5.2.0 -d %cd%
toolset -p CodeShellCore.Application			5.2.0 -d %cd%
toolset -p CodeShellCore.EntityFramework 		5.2.0 -d %cd%
toolset -p CodeShellCore.Caching 				5.2.0 -d %cd%
toolset -p CodeShellCore.MQ 					5.2.0 -d %cd%
toolset -p CodeShellCore.Reporting 				5.2.0 -d %cd%

toolset -p CodeShellCore.FileServer 			5.2.0 -d %cd%
toolset -p CodeShellCore.FileServer.Module 		5.2.0 -d %cd%

toolset -p Asga.Auth 							5.2.0 -d %cd%
toolset -p Asga.Auth.Module 					5.2.0 -d %cd%

toolset -p Asga.Public 							5.2.0 -d %cd%
toolset -p Asga.Public.Module 					5.2.0 -d %cd%

toolset -p Asga.Mobile 							5.2.0 -d %cd%
toolset -p Asga.Mobile.Module 					5.2.0 -d %cd%

toolset -p Asga.Dashboard 						5.2.0 -d %cd%

toolset -p CodeShellCore.Moldster 				5.2.0.0 -d %cd%

toolset -p CodeShellCore.Web 					5.2.0 -d %cd%
toolset -p CodeShellCore.Web.Razor 				5.2.0.0 -d %cd%

toolset -p Asga.Auth.Web 						5.2.0 -d %cd%
toolset -p Asga.Public.Web 						5.2.0 -d %cd%
toolset -p Asga.Mobile.Web 						5.2.0 -d %cd%
toolset -p Asga.Dashboard.Web 					5.2.0 -d %cd%
toolset -p CodeShellCore.FileServer.Web 		5.2.0 -d %cd%

toolset -p Asga.Auth.Molds 						5.2.0 -d %cd%
toolset -p Asga.Public.Molds 					5.2.0 -d %cd%
toolset -p CodeShellCore.Cli					1.0.3 -d %cd%

timeout 10