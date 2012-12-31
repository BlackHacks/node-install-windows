echo off
cls
color 0A
echo This will install Node.js and all needed dependancies

start http://nodejs.org/dist/v0.8.16/x64/node-v0.8.16-x64.msi
start /wait msiexec /i node-v0.8.16-x64.msi /quiet

if "%1"=="/l" (
    echo Installation of LightWeight libs -- to be edited and picked Libs
npm install express -g
) 
if "%1"=="/m" (
    echo Installation of MiddleWeight libs -- to be edited and picked Libs
) 
if "%1"=="/h" (
    echo Installation of HeavyWeight libs -- to be edited and picked Libs
) 
