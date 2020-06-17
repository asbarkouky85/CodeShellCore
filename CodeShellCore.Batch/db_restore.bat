@echo off
cd..
toolset -r "User Id=app;Password=123456;Server=.;" Example "%cd%/Example/SQL/Example.bak"
toolset -r "User Id=app;Password=123456;Server=.;" Example.Config "%cd%/Example.Config.Api/SQL/Backups/Example.Config.bak"
toolset -r "User Id=app;Password=123456;Server=.;" Configurator.Config "%cd%/Configurator.Config.Api/Backups/Configurator.Config.bak"