@echo off
cd..

xcopy /E /I %cd%\Configurator.UI\wwwroot\css  							C:\ASGA_TFS\Helpers\Configurator.UI\wwwroot\css /Y
xcopy /E /I %cd%\Configurator.UI\wwwroot\js  							C:\ASGA_TFS\Helpers\Configurator.UI\wwwroot\css /Y
xcopy /E /I %cd%\Configurator.UI\wwwroot\img  							C:\ASGA_TFS\Helpers\Configurator.UI\wwwroot\css /Y

toolset -z 	%cd%\Configurator.UI\wwwroot\css 							%cd%\CodeShellCore.Moldster\Files\css.zip
toolset -z 	%cd%\Configurator.UI\wwwroot\js 							%cd%\CodeShellCore.Moldster\Files\js.zip
toolset -z 	%cd%\Configurator.UI\wwwroot\img 							%cd%\CodeShellCore.Moldster\Files\img.zip
toolset -z 	%cd%\Configurator.UI\Core\codeshell 						%cd%\CodeShellCore.Moldster\Files\codeshell.zip
copy 		Configurator.UI\Pages\Index.cshtml 							CodeShellCore.Moldster\Files\Index_cshtml.txt
copy 		Configurator.UI\appsettings.development.json 				CodeShellCore.Moldster\Files\appsettings_json.txt

copy 		Configurator.UI\package.json 								CodeShellCore.Moldster\Angular\Files\package_json.txt
copy 		Configurator.UI\WebpackSharedConfig.js 						CodeShellCore.Moldster\Angular\Files\WebPackSharedConfig_js.txt
copy 		Configurator.UI\webpack.config.vendor.js 					CodeShellCore.Moldster\Angular\Files\webpack_config_vendor_js.txt
copy 		Configurator.UI\Core\Example\Main\Login.html				CodeShellCore.Moldster\Angular\Files\Login_html.txt
copy 		Configurator.UI\Core\Example\Main\Login.ts 					CodeShellCore.Moldster\Angular\Files\Login_ts.txt
copy 		Configurator.UI\Core\Example\Main\navigationSideBar.html	CodeShellCore.Moldster\Angular\Files\navigationSideBar_html.txt
copy 		Configurator.UI\Core\Example\Main\navigationSideBar.ts 		CodeShellCore.Moldster\Angular\Files\navigationSideBar_ts.txt

timeout 5
exit