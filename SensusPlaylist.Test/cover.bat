@echo off

SET dotnet="dotnet.exe"  
SET opencover="%USERPROFILE%\.nuget\packages\OpenCover\4.6.519\tools\OpenCover.Console.exe"
SET reportgenerator="%USERPROFILE%\.nuget\packages\ReportGenerator\2.5.2\tools\ReportGenerator.exe"

SET targetargs="test --configuration Test"  
SET filter="+[SensusPlaylist]* -[*.Test]* -[xunit.*]* -[FluentValidation]*" 
SET coveragefile=Coverage.xml
SET coveragedir=Coverage

REM Run code coverage analysis  
%opencover% -oldStyle -register:user -target:%dotnet% -output:%coveragefile% -filter:%filter% -targetargs:%targetargs% -skipautoprops
REM Generate the report  
%reportgenerator% -targetdir:%coveragedir% -reporttypes:Html;Badges -reports:%coveragefile% -verbosity:Error

REM Open the report  
REM start "report" "%coveragedir%\index.htm"