@pushd %~dp0
@if ERRORLEVEL 1 goto end

@cd ..\..\packages\SpecRun.Runner.*\tools

@set profile=%1
@if "%profile%" == "" set profile=Default

SpecRun.exe run %profile%.srprofile /baseFolder:"../../../Out/Release/Attika.Specs" /log:specrun.log %2 %3 %4 %5
if errorlevel 200 exit /b %errorlevel%

:end

@popd
