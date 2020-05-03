@set PWD=%CD%
@set MYD=%~dp0

@rem Add path to MSBuild
@if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin" set PATH=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin;%PATH%

@rem call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\VsDevCmd.bat"

msbuild -t:Rebuild -p:Configuration=Release -p:Platform="Any CPU" -m Cherry.sln
