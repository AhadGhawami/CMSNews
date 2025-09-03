using Microsoft.AspNetCore.Mvc;

namespace CMSNews.Areas.Admin.Controllers
{
    public class NewsGroupsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
