echo USER: %USERNAME%

set sourceDir=C:\Build\Compiled\itoil
set targetDir=\\yuhimchuk-zv\netsharing\__build

echo Copy %sourceDir%\bin to %targetDir%\bin_\

xcopy %sourceDir%\bin %targetDir%\bin_\ /q /e

echo %TIME% Begin copying static files

echo aspx, ashx, asmx, svc - only copy, no need to update

for %%e in (aspx asmx ashx svc) do (
	echo __copying %%e
	robocopy %sourceDir%\ %targetDir%\ *.%%e /r:2 /w:5 /s /xx /xo /xn /ns /ndl /njh /njs	
)

for %%e in (sitemap bmp jpeg jpg gif ico png htm html xml xsl xslt doc docx xls xlsx pdf mrt css js config) do (
	echo __copying %%e	
    XCOPY "%sourceDir%\*.%%e" %targetDir%\ /S /F /H /Y /D /R
)

echo %TIME% static files copied

set rnd=%RANDOM%
echo Rename %targetDir%\bin to %targetDir%\bin%rnd%

move %targetDir%\bin %targetDir%\bin%rnd%

echo Rename bin_ in bin 
echo %targetDir%\bin_ %targetDir%\bin
move %targetDir%\bin_ %targetDir%\bin

echo %TIME% Finished
