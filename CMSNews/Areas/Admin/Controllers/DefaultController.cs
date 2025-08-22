using Microsoft.AspNetCore.Mvc;

namespace CMSNews.Areas.Admin.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
