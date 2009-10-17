using System.Web.Mvc;

namespace Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Packages");
        }
    }
}