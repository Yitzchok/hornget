﻿install horngit:
	description "Mercurial test"
	get_from hg("http://hornget.googlecode.com/svn/trunk/")
	build_with msbuild, buildfile("src/horn.sln"), FrameworkVersion35
                
package.homepage = "http://code.google.com/p/scotaltdotnet/"
package.forum    = "http://groups.google.co.uk/group/horn-development?hl=en"
