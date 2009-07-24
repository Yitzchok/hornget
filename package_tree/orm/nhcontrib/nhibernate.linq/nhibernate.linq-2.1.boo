install nhibernate.linq:
	description "Nhibernate Linq - Linq provider for NHibernate "
	
	get_from svn("https://nhcontrib.svn.sourceforge.net/svnroot/nhcontrib/trunk/src/NHibernate.Linq/")
	build_with nant, buildfile("default.build"), FrameworkVersion35	
		
	generate_strong_key
		
	shared_library "lib"
	build_root_dir "build"		
	
dependencies:
	depend "castle.tools" >> "Castle.Core"
	depend "castle.tools" >> "Castle.DynamicProxy2"
	depend "nhibernate"   >> "2.1" >> "NHibernate"       
	depend "nhibernate"   >> "2.1" >> "Iesi.Collections" 
	depend "nhibernate"   >> "2.1" >> "NHibernate.ByteCode.Castle" 	

package.homepage = "http://www.ohloh.net/p/NHibernateContrib"
package.forum    = "http://groups.google.co.uk/group/nhusers?hl=en"