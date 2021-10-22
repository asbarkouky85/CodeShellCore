@echo off
cd..
toolset -r "User Id=app;Password=123456;Server=.;" Configurator.Config "%cd%/Configurator.Config.Api/Backups/Configurator.Config.bak"