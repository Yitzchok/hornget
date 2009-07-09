install castle:
	description "Castle is an open source project for .net that aspires to simplify the development of enterprise and web applications."
	get_from svn("http://svn.castleproject.org:8080/svn/castle/trunk/")
	build_with nant, buildfile("default.build"), FrameworkVersion35
	
	with:
		tasks quick, rebuild
		
	switches:
		parameters "sign=true"
		
	shared_library "SharedLibs/net/2.0"
	output "build/net-3.5/debug"		
	
dependencies:
	depend @log4net >> "log4net"