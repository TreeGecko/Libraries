c:\projects\libraries\src\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe ^
	-register:user ^
	-output:c:\projects\libraries\tglibraries-coverage.xml ^
	"-filter:+[tg*]*  -[tg*]*Properties.*" ^
	-excludebyattribute:"System.CodeDom.Compiler.GeneratedCodeAttribute" ^
	"-target:c:\projects\libraries\test.bat"