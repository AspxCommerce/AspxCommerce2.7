echo off
echo ---------------------------------------------------------------------------
echo This script generates new classes from the file apiv2.xsd.
echo If the API changes, you should download the newest apiv2.xsd from 
echo http://code.google.com/apis/checkout/apiv2.xsd and run this script again.
echo ---------------------------------------------------------------------------
echo on
"C:\Program Files\Microsoft.NET\SDK\v1.1\Bin\xsd" apiv2.xsd /c /n:GCheckout.AutoGen
pause
rem this next one will assume you are using the vs.net 2003 command prompt
xsd.exe apiv2.xsd /c /n:GCheckout.AutoGen
