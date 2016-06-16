IF not exist "Dist\lib\portable-net45+wp80+wp81+win8+wpa81+monoandroid\" (md "Dist\lib\portable-net45+wp80+wp81+win8+wpa81+monoandroid\")
IF not exist "Dist\lib\portable-win81+wp81+wpa81\" (md "Dist\lib\portable-win81+wp81+wpa81\")


COPY /B aerogear-windows-push\bin\Release\aerogear-windows-push.pdb "Dist\lib\portable-net45+wp80+wp81+win8+wpa81+monoandroid\"
COPY /B aerogear-windows-push\bin\Release\aerogear-windows-push.dll "Dist\lib\portable-net45+wp80+wp81+win8+wpa81+monoandroid\"

COPY /B lib\bin\Release\aerogear-windows-push.pdb "Dist\lib\portable-win81+wp81+wpa81\"
COPY /B lib\bin\Release\aerogear-windows-push.dll "Dist\lib\portable-win81+wp81+wpa81\"
COPY /B lib\bin\Release\lib.pdb "Dist\lib\portable-win81+wp81+wpa81\"
COPY /B lib\bin\Release\lib.dll "Dist\lib\portable-win81+wp81+wpa81\"

