install n2cms:
	description "N2 is a lightweight CMS framework to help you build great web sites that anyone can update"
	get_from svn("http://n2cms.googlecode.com/svn/trunk/")
	build_with msbuild, buildfile("build/n2.proj"), FrameworkVersion35

	switches:
		parameters "/p:DefineConstants=NH2_1"

	with:
		tasks Deploy

	build_root_dir "output"
	shared_library "lib"

dependencies:
	depend "nhibernate"             >> "NHibernate"     
	depend "nhibernate"             >> "NHibernate.ByteCode.Castle"
	depend "nhibernate"          	>> "Iesi.Collections"
	depend "nhibernate.caches"   	>> "NHibernate.Caches.SysCache2"
	depend "nhibernate.linq"   		>> "NHibernate.Linq"
	depend "nhibernate.jetdriver"   >> "NHibernate.JetDriver"
	depend @log4net					>>  "1.2.10" 	>> "log4net"
	depend "castle.windsor"  		>> "castle.core"
	depend "castle.windsor"  		>> "Castle.DynamicProxy2"
	depend "castle.windsor"  		>> "castle.microKernel"
	depend "castle.windsor"  		>> "castle.windsor"
	depend "rhino"  				>> "Rhino.Mocks"
	depend "mvccontrib" 			>> "MvcContrib"

package.homepage = "http://n2cms.com/"
package.forum    = "http://www.codeplex.com/n2/Thread/List.aspx"
