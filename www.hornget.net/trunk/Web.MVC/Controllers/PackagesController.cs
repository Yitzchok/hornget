using System.Linq;
using System.Web.Mvc;
using Web.MVC.Filters;
using Web.MVC.Model;

namespace Web.MVC.Controllers
{
    public class PackagesController : PackagesControllerBase
    {
        [JsonFilter]
        public ActionResult Index(string url)
        {
            SetPrimaryCategoryNavigation();

            if (string.IsNullOrEmpty(url))
            {
                return View(PackageStructure().Categories);
            }

            var category = Categories.Where(c => c.Url.ToLower() == url).FirstOrDefault();

            if (category != null)
                return ShowCategory(category);

            var package = Packages.Where(p => p.Url.ToLower() == url).FirstOrDefault();

            return package == null
                   ? NotFound()
                   : ShowPackage(package);
        }

        void SetPrimaryCategoryNavigation()
        {
            ViewData["Categories"] = PackageStructure().Categories;
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