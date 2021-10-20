@echo off
cd..

toolset zip 	%cd%\Example\Localization										%cd%\CodeShellCore.Moldster\Files\Localization.zip
toolset zip 	%cd%\Example.UI\src\assets\moldster\css 						%cd%\CodeShellCore.Moldster\Files\css.zip
toolset zip 	%cd%\Example.UI\src\assets\moldster\js 							%cd%\CodeShellCore.Moldster\Files\js.zip
toolset zip 	%cd%\Example.UI\src\assets\moldster\img 						%cd%\CodeShellCore.Moldster\Files\img.zip
toolset zip 	%cd%\Example.UI\src\core\codeshell								%cd%\CodeShellCore.Moldster\Files\codeshell.zip
toolset zip 	%cd%\Example.Config.Api\ShellComponents 						%cd%\CodeShellCore.Moldster\Files\ShellComponents.zip

copy 		Example.Config.Api\Views\AppComponent.cshtml 					CodeShellCore.Moldster\Files\AppComponent_cshtml.txt

copy 		Example.UI\package.json 											CodeShellCore.Moldster\Angular\Files\package_json.txt
copy 		Example.UI\tsconfig.json 											CodeShellCore.Moldster\Angular\Files\tsconfig_json.txt
copy 		Example.UI\src\index.html											CodeShellCore.Moldster\Angular\Files\index_html.txt
copy 		Example.UI\src\core\base\main\login.component.html					CodeShellCore.Moldster\Angular\Files\Login_html.txt
copy 		Example.UI\src\core\base\main\login.component.ts 					CodeShellCore.Moldster\Angular\Files\Login_ts.txt
copy 		Example.UI\src\core\base\main\navigation-side-bar.component.html	CodeShellCore.Moldster\Angular\Files\navigationSideBar_html.txt
copy 		Example.UI\src\core\base\main\navigation-side-bar.component.ts 		CodeShellCore.Moldster\Angular\Files\navigationSideBar_ts.txt
copy 		Example.UI\src\core\base\main\top-bar.component.html				CodeShellCore.Moldster\Angular\Files\topBar_html.txt
copy 		Example.UI\src\core\base\main\top-bar.component.ts 					CodeShellCore.Moldster\Angular\Files\topBar_ts.txt

timeout 20
exit
