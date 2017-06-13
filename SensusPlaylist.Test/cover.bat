@echo off

SET dotnet="dotnet.exe"  
SET opencover="%USERPROFILE%\.nuget\packages\OpenCover\4.6.519\tools\OpenCover.Console.exe"
SET reportgenerator="%USERPROFILE%\.nuget\packages\ReportGenerator\2.5.2\tools\ReportGenerator.exe"
SET opencoverConverter="%USERPROFILE%\.nuget\packages\OpenCoverToCoberturaConverter\0.2.6\tools\OpenCoverToCoberturaConverter.exe"

SET targetargs="test SensusPlaylist.Test\SensusPlaylist.Test.csproj --configuration Test"  
SET filter="+[SensusPlaylist]* -[*.Test]* -[xunit.*]* -[FluentValidation]*" 
SET coveragefile=SensusPlaylist.Test\Coverage.xml
SET coberturaCoverageFile=SensusPlaylist.Test\Cobertura-Coverage.xml
SET coveragedir=SensusPlaylist.Test\Coverage

REM Run code coverage analysis  
%opencover% -oldStyle -register:user -target:%dotnet% -output:%coveragefile% -filter:%filter% -targetargs:%targetargs% -skipautoprops

REM Generate the Cobertura Report
%opencoverConverter% -input:%coveragefile% -output:%coberturaCoverageFile% -sources:..

REM Generate the HTML report

%reportgenerator% -targetdir:%coveragedir% -reporttypes:Html;HtmlChart;Badges -reports:%coveragefile% -verbosity:Error

REM Open the report  
REM start "report" "%coveragedir%\index.htm"