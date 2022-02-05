@echo off

cd CodeShellCore.Batch
start moldster_files.bat

cd..
toolset set-version CodeShellCore 							5.2.0 %cd%
toolset set-version CodeShellCore.Domain.Shared				5.2.0 %cd%
toolset set-version CodeShellCore.Domain					5.2.0 %cd%
toolset set-version CodeShellCore.Application.Contracts		5.2.0 %cd%
toolset set-version CodeShellCore.Application				5.2.0 %cd%
toolset set-version CodeShellCore.EntityFramework 			5.2.0 %cd%
toolset set-version CodeShellCore.Caching 					5.2.0 %cd%
toolset set-version CodeShellCore.MQ 						5.2.0 %cd%
toolset set-version CodeShellCore.Reporting 				5.2.0 %cd%

toolset set-version CodeShellCore.FileServer 				5.2.0 %cd%
toolset set-version CodeShellCore.FileServer.Module 		5.2.0 %cd%

toolset set-version Asga.Auth 								5.2.0 %cd%
toolset set-version Asga.Auth.Module 						5.2.0 %cd%

toolset set-version Asga.Public 							5.2.0 %cd%
toolset set-version Asga.Public.Module 						5.2.0 %cd%

toolset set-version Asga.Mobile 							5.2.0 %cd%
toolset set-version Asga.Mobile.Module 						5.2.0 %cd%

toolset set-version Asga.Dashboard 							5.2.0 %cd%

toolset set-version CodeShellCore.Moldster 					5.2.1.0 %cd%

toolset set-version CodeShellCore.Web 						5.2.0 %cd%
toolset set-version CodeShellCore.Web.Razor 				5.2.1.0 %cd%

toolset set-version Asga.Auth.Web 							5.2.0 %cd%
toolset set-version Asga.Public.Web 						5.2.0 %cd%
toolset set-version Asga.Mobile.Web 						5.2.0 %cd%
toolset set-version Asga.Dashboard.Web 						5.2.0 %cd%
toolset set-version CodeShellCore.FileServer.Web 			5.2.0 %cd%

toolset set-version Asga.Auth.Molds 						5.2.0 %cd%
toolset set-version Asga.Public.Molds 						5.2.0 %cd%
toolset set-version CodeShellCore.Cli						1.1.0 %cd%
toolset set-version CodeShellCore.ToolSet.Cli				5.0.3 %cd%

toolset set-version Configurator.UI							5.0.2 %cd%

timeout 10