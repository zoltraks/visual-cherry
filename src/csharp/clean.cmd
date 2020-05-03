@set PWD=%CD%
@set MYD=%~dp0

@rem Add path to MSBuild
@if exist "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin" set PATH=%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin;%PATH%

msbuild -t:Clean -p:Configuration=Debug Cherry.sln
msbuild -t:Clean -p:Configuration=Release Cherry.sln
