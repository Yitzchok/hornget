using System;
using System.Linq;
using System.Web.Mvc;
using Web.MVC.Model;

namespace Web.MVC.Controllers
{
    public class PackagesController : PackagesControllerBase
    {
        public ActionResult Index(string url)
        {
            SetPrimaryCategoryNavigation();

            if (string.IsNullOrEmpty(url))
            {
                return View(PackageStructure().Take(1).FirstOrDefault().Categories);
            }

            var category = Categories.Where(c => c.Url.ToLower().StartsWith(url)).FirstOrDefault();

            if (category != null)
                return ShowCategory(category);

            var package = Packages.Where(p => p.Url.ToLower().StartsWith(url)).FirstOrDefault();

            return package == null
                   ? NotFound()
                   : ShowPackage(package);
        }

        void SetPrimaryCategoryNavigation()
        {
            ViewData["Categories"] = PackageStructure().Take(1).FirstOrDefault().Categories;
        }

        private ActionResult NotFound()
        {
            return View("PackageNotFound");
        }

        private ActionResult ShowPackage(Package package)
        {
            return View("Package", package);
        }

        private ActionResult ShowCategory(Category category)
        {
            return View("Category", category);
        }
    }
}