using CMSNews.Service.Service;
using Microsoft.AspNetCore.Mvc;
using CMSNews.Models.ViewModels;
using CMSNews.Model.Models;

namespace CMSNews.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        public IActionResult Index()
        {
            var news = _newsService.GetAll();
            var newsViewModel = news.Select(x => new NewsViewModel
            {
                NewsId = x.NewsId,
                NewsTitle = x.NewsTitle,
                Description = x.Description,
                ImageName = x.ImageName,
                RegisterDate = x.RegisterDate,
                IsActive = x.IsActive,
                See = x.See,
                Like = x.Like

            }).ToList();
            return View(newsViewModel);
        }
        public IActionResult Details(Guid? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            tblNews news = _newsService.GetEntity(id.Value);
            if(news == null)
            {
                return NotFound();
            }
            var newsViewModel = new NewsViewModel
            {
                NewsId = news.NewsId,
                NewsTitle = news.NewsTitle,
                Description = news.Description,
                ImageName = news.ImageName,
                RegisterDate = news.RegisterDate,
                IsActive = news.IsActive,
                See = news.See,
                Like = news.Like
            };
            return View(newsViewModel);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewsViewModel newsViewModel, IFormFile? imgUpload)
        {
            return View(newsViewModel);
        }
    }
}
