IF not exist "Dist\lib\portable-net45+win8+wp8+wpa81\" (md "Dist\lib\portable-net45+win8+wp8+wpa81\")
IF not exist "Dist\lib\portable-win81+wpa81\" (md "Dist\lib\portable-win81+wpa81\")
IF not exist "Dist\lib\uap10.0\" (md "Dist\lib\uap10.0\")
IF not exist "Dist\lib\windowsphone8\" (md "Dist\lib\windowsphone8\")

COPY /B aerogear-windows-push\bin\Release\aerogear-windows-push.pdb "Dist\lib\portable-net45+win8+wp8+wpa81\"
COPY /B aerogear-windows-push\bin\Release\aerogear-windows-push.dll "Dist\lib\portable-net45+win8+wp8+wpa81\"

COPY /B lib\bin\Release\aerogear-windows-push.pdb "Dist\lib\portable-win81+wpa81\"
COPY /B lib\bin\Release\aerogear-windows-push.dll "Dist\lib\portable-win81+wpa81\"
COPY /B lib\bin\Release\lib.pdb "Dist\lib\portable-win81+wpa81\"
COPY /B lib\bin\Release\lib.dll "Dist\lib\portable-win81+wpa81\"

COPY /B lib\bin\Release\aerogear-windows-push.pdb "Dist\lib\uap10.0\"
COPY /B lib\bin\Release\aerogear-windows-push.dll "Dist\lib\uap10.0\"
COPY /B lib\bin\Release\lib.pdb "Dist\lib\uap10.0\lib10.pdb"
COPY /B lib\bin\Release\lib.dll "Dist\lib\uap10.0\lib10.dll"

COPY /B cordova\bin\Release\aerogear-windows-push.pdb "Dist\lib\windowsphone8\"
COPY /B cordova\bin\Release\aerogear-windows-push.dll "Dist\lib\windowsphone8\"
COPY /B cordova\bin\Release\cordova.pdb "Dist\lib\windowsphone8\"
COPY /B cordova\bin\Release\cordova.dll "Dist\lib\windowsphone8\"


IF not exist "Dist\src\aerogear-windows-push\" (md "Dist\src\aerogear-windows-push\")
IF not exist "Dist\src\lib\" (md "Dist\src\lib\")
XCOPY aerogear-windows-push\*.cs Dist\src\aerogear-windows-push /sy
XCOPY lib\*.cs Dist\src\lib /sy