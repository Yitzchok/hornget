install castle.components:
	description "Castle validator, templating engine etc."
	
	prebuild:
		cmd "xcopy /s /y \"../Patch\" ."
		
	include:
		repository(castle, part("SharedLibs"), to("SharedLibs"))
		repository(castle, part("Components"), to("Components"))
		repository(castle, part("common.xml"), to("common.xml"))
		repository(castle, part("common-project.xml"), to("common-project.xml"))
		repository(castle, part("CastleKey.snk"), to("CastleKey.snk"))
		
	build_with nant, buildfile("Components/components.build"), FrameworkVersion35
	
	switches:
		parameters "sign=true","common.testrunner.enabled=false", "common.silverlight=false"
		
	shared_library "SharedLibs"
	build_root_dir "build"
	
dependencies:
	dependency "castle.tools" >> "castle.core"
	
package.homepage = "http://www.castleproject.org/"
package.forum    = "http://groups.google.co.uk/group/castle-project-users?hl=en"  