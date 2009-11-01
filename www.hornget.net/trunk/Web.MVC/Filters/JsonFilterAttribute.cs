using System;
using System.Linq;
using System.Web.Mvc;

namespace Web.MVC.Filters
{
    public class JsonFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var types = filterContext.HttpContext.Request.AcceptTypes.Where(c => c.Contains("application/json")).FirstOrDefault();

            if (!String.IsNullOrEmpty(types))
            {
                var viewResult = filterContext.Result as ViewResult;

                var model = viewResult.ViewData.Model;

                var json = new JsonResult();
                json.Data = model;

                filterContext.Result = json;
            }
        }

    }
}