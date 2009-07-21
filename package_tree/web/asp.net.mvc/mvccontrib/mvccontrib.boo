install mvccontrib:
	description "This project adds additional functionality on top of the MVC Framework."
	
	get_from svn("http://mvccontrib.googlecode.com/svn/trunk/")
	build_with nant, buildfile("nant.build"), FrameworkVersion35	
		
	shared_library "bin"
	build_root_dir "build"		
	
dependencies:  
	depend "castle.tools"		 >>   "Castle.Core"     
	depend "castle.tools"		 >>   "Castle.DynamicProxy"
	depend "castle.tools"		 >>   "Castle.DynamicProxy2"
	depend "castle.activerecord" >>   "Castle.ActiveRecord"
	depend "castle.activerecord" >>   "Castle.ActiveRecord"
	depend "castle.components"	 >>	  "Castle.Components.Binder"
	depend "castle.components"	 >>	  "Castle.Components.Common.EmailSender"
	depend "castle.components"	 >>	  "Castle.Components.Common.TemplateEngine"
	depend "castle.components"	 >>	  "Castle.Components.Common.TemplateEngine.NVelocityTemplateEngine"
	depend "castle.components"	 >>	  "Castle.Components.DictionaryAdapter"
	depend "castle.components"	 >>	  "Castle.Components.Pagination"
	depend "castle.components"	 >>	  "Castle.Components.Scheduler"
	depend "castle.components"	 >>	  "Castle.Components.Scheduler.WindsorExtension"
	depend "castle.components"	 >>	  "Castle.Components.Validator"
	depend "castle.facilities"	 >>	  "Castle.Facilities.ActiveRecordIntegration"
	depend "castle.facilities"	 >>	  "Castle.Facilities.AutomaticTransactionManagement"
	depend "castle.facilities"	 >>	  "Castle.Facilities.BatchRegistration"
	depend "castle.facilities"	 >>	  "Castle.Facilities.Cache"
	depend "castle.facilities"	 >>	  "Castle.Facilities.Logging"
	depend "castle.facilities"	 >>	  "Castle.Facilities.NHibernateIntegration"
	depend "castle.facilities"	 >>	  "Castle.Facilities.Synchronize"
	depend "castle.facilities"	 >>	  "Castle.Facilities.WcfIntegration"
	depend "castle.windsor"	     >>	  "Castle.MicroKernel"
	depend "castle.windsor"	     >>	  "Castle.Windsor"

package.homepage = "http://www.codeplex.com/MVCContrib"
package.forum    = "http://groups.google.co.uk/group/mvccontrib-discuss"
