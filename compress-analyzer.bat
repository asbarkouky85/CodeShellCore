@echo off
cd Dependency.Analyzer
ECHO Deleting all BIN and OBJ folders...
ECHO.

FOR /d /r . %%d in (bin,obj) DO (
	IF EXIST "%%d" (		 	 
		ECHO %%d | FIND /I "\node_modules\" > Nul && ( 
			ECHO.Skipping: %%d
		) || (
			ECHO.Deleting: %%d
			rd /s/q "%%d"
		)
	)
)
cd..
toolset zip Dependency.Analyzer ./CodeShellCore.ToolSet/Resources/dependency_analyzer_csproj.zip