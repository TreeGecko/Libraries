c:\projects\libraries\src\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe ^
	-register:user ^
	-output:c:\projects\libraries\tglibraries-coverage.xml ^
	"-filter:+[tg]*  -[tgAWS]TreeGecko.Library.AWS.Properties.*;[tgCommonLibrary]TreeGecko.Library.Common.Properties.*;[tgGeospatial]TreeGecko.Library.Geospatial.Properties.*;[tgLoggly]TreeGecko.Library.Loggly.Properties.*;[tgMongo]TreeGecko.Library.Mongo.Properties.*;[tgNet]TreeGecko.Library.Net.Properties.*" ^
	-excludebyattribute:"System.CodeDom.Compiler.GeneratedCodeAttribute" ^
	"-target:c:\projects\libraries\test.bat"