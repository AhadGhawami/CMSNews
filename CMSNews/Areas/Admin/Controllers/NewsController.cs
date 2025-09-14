using CMSNews.Service.Service;
using Microsoft.AspNetCore.Mvc;
using CMSNews.Models.ViewModels;
using CMSNews.Model.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace CMSNews.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly INewsGroupService _newsGroupService;

        public NewsController(INewsService newsService, INewsGroupService newsGroupService)
        {
            _newsService = newsService;
            _newsGroupService = newsGroupService;
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
            if (id == null)
            {
                return BadRequest();
            }
            tblNews news = _newsService.GetEntity(id.Value);
            if (news == null)
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
            ViewBag.NewsGroupId = new SelectList(_newsGroupService.GetAll(), "NewsGroupId", "NewsGroupTitle");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewsViewModel newsViewModel, IFormFile? imgUpload)
        {
            if (ModelState.IsValid)
            {
                #region save Image to Server
                string imageName = "nophoto.png";

                if (imgUpload != null && imgUpload.Length > 0)
                {
                    // فقط پسوندهای مجاز
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var extension = Path.GetExtension(imgUpload.FileName).ToLower();

                    if (allowedExtensions.Contains(extension))
                    {
                        // اسم یکتا
                        imageName = Guid.NewGuid().ToString() + extension;

                        // مسیر پوشه
                        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", "news");

                        // اگر نبود بسازه
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        string savePath = Path.Combine(folderPath, imageName);

                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            imgUpload.CopyTo(stream);
                        }
                    }
                }
                #endregion
                tblNews news = new tblNews
                {
                    NewsId = Guid.NewGuid(),
                    NewsTitle = newsViewModel.NewsTitle,
                    Description = newsViewModel.Description,
                    ImageName = imageName,
                    RegisterDate = DateTime.Now,
                    IsActive = true,
                    Like = 0,
                    See = 0,
                    UserId = Guid.Parse(User.FindFirst("UserId")?.Value ?? Guid.Empty.ToString())

                };
                _newsService.Add(news);
                _newsService.Save();
                RedirectToAction("Index");
            }

            ViewBag.NewsGroupId = new SelectList(_newsGroupService.GetAll(), "NewsGroupId", "NewsGroupTitle");

            return View(newsViewModel);
        }
    }
}
