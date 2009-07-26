﻿install ninject:
	description "Ninject: Lightning-fast dependency injection for .NET"
	get_from svn("http://ninject.googlecode.com/svn/trunk")
	
	build_with nant, buildfile("Ninject.build"), FrameworkVersion35
	
	switches:
		parameters "skip.tests=true"

	with:
		tasks release,clean,all
		
	shared_library "lib"
	build_root_dir "bin/net-3.5/release"

dependencies:
	depend "castle"	>> "Castle.Core"
	depend "castle" >> "Castle.DynamicProxy2"
	depend "castle" >> "Castle.MicroKernel"
	depend "castle" >> "Castle.Windsor"
	depend "castle" >> "Castle.MonoRail.Framework"
	depend "linfu"	>> "LinFu.DynamicProxy"

package.homepage = "http://ninject.org/"
package.forum    = "http://groups.google.com/group/ninject"
