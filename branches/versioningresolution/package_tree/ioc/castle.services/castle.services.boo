install castle.services:
	description "Castle provides a number of services."
		
	include:
		repository(castle, part("SharedLibs"), to("SharedLibs"))
		repository(castle, part("Services"), to("Services"))
		repository(castle, part("common.xml"), to("common.xml"))
		repository(castle, part("common-project.xml"), to("common-project.xml"))
		repository(castle, part("CastleKey.snk"), to("CastleKey.snk"))
		
	build_with nant, buildfile("Services/services.build"), FrameworkVersion35
	
	switches:
		parameters "sign=true","common.testrunner.enabled=false", "common.silverlight=false"
		
	shared_library "SharedLibs"
	build_root_dir "build"
	
dependencies:
	dependency "castle.tools" >> "castle.core"
	
package.homepage = "http://www.castleproject.org/"
package.forum    = "http://groups.google.co.uk/group/castle-project-users?hl=en"  