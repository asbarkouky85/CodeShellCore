cd..
xcopy /E /I %cd%\Configurator.UI\Pages  							C:\ASGA_TFS\Helpers\Projects\Configurator.UI\Pages /Y
xcopy /E /I %cd%\Configurator.UI\wwwroot\*							C:\ASGA_TFS\Helpers\Projects\Configurator.UI\wwwroot /Y
xcopy /E /I %cd%\Configurator.UI\Controllers\*						C:\ASGA_TFS\Helpers\Projects\Configurator.UI\Controllers /Y

timeout 4