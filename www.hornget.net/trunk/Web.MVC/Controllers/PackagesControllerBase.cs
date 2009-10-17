using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Caching;
using System.Web.Mvc;
using Web.MVC.Model;

namespace Web.MVC.Controllers
{
    public class PackagesControllerBase : Controller
    {
        protected IList<Category> Categories
        {
            get
            {
                return PackageStructure().GetAllCategories().ToList();
            }
        }

        protected IList<Package> Packages
        {
            get
            {
                return Categories.GetAllPackages().ToList();
            }
        }

        protected PackageStructure PackageStructure()
        {
            var structure = HttpContext.Cache["PackageStructure"] as PackageStructure;
            
            if (structure == null)
            {
                var packageStructurePath = ConfigurationManager.AppSettings["PackageStructurePath"];
                var loader = new PackageStructureLoader();
                structure = loader.Load(packageStructurePath);
                HttpContext.Cache.Add("PackageStructure", structure, null, Cache.NoAbsoluteExpiration, TimeSpan.FromHours(24), CacheItemPriority.NotRemovable, null);                
                //HttpContext.Cache.Add("PackageStructure", structure, new CacheDependency(packageStructurePath), Cache.NoAbsoluteExpiration, TimeSpan.FromHours(24), CacheItemPriority.NotRemovable, null);                
            }

            return structure;
        }
    }
}