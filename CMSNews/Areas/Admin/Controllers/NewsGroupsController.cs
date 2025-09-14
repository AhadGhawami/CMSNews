using AutoMapper;
using CMSNews.Mappings;
using CMSNews.Model.Context;
using CMSNews.Model.Models;
using CMSNews.Models.ViewModels;
using CMSNews.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Net;

namespace CMSNews.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class NewsGroupsController : Controller
    {
        private readonly INewsGroupService _newsGroupService;

        public NewsGroupsController(INewsGroupService newsGroupService)
        {
            _newsGroupService = newsGroupService;
        }



        public IActionResult Index()
        {
            var newsGroup = _newsGroupService.GetAll();
            //var newsGroupViewModels = MappingProfile.mapper.Map<List<tblNewsGroup>, List<NewsGroupViewModel>>(newsGroups);
            //var newsGroupViewModels = MappingProfile.mapper.Map<IEnumerable<tblNewsGroup>,List<NewsGroupViewModel>>(newsGroups); 
            var newsGroupViewModels = newsGroup.Select(x => new NewsGroupViewModel
            {
                NewsGroupId = x.NewsGroupId,
                NewsGroupTitle = x.NewsGroupTitle,
                ImageName = x.ImageName
            }).ToList();


            return View(newsGroupViewModels);
        }
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            tblNewsGroup newsGroup = _newsGroupService.GetEntity(id.Value);
            if (newsGroup == null)
            {
                return NotFound();
            }
            var newsGroupViewModel = new NewsGroupViewModel
            {
                NewsGroupId = newsGroup.NewsGroupId,
                NewsGroupTitle = newsGroup.NewsGroupTitle,
                ImageName = newsGroup.ImageName
            };
            return View(newsGroupViewModel);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewsGroupViewModel newsGroupViewModel, IFormFile? imgUpload)
        {
            if (ModelState.IsValid)
            {
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
                        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", "news-group");

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

                tblNewsGroup newsGroup = new tblNewsGroup
                {
                    NewsGroupId = Guid.NewGuid(),
                    NewsGroupTitle = newsGroupViewModel.NewsGroupTitle,
                    ImageName = imageName
                };

                _newsGroupService.Add(newsGroup);
                _newsGroupService.Save();

                return RedirectToAction("Index");
            }

            return View(newsGroupViewModel);
        }

        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var newsGroup = _newsGroupService.GetEntity(id.Value);
            if (newsGroup == null)
            {
                return NotFound();
            }

            // تبدیل به ViewModel
            var newsGroupViewModel = new NewsGroupViewModel
            {
                NewsGroupId = newsGroup.NewsGroupId,
                NewsGroupTitle = newsGroup.NewsGroupTitle,
                ImageName = newsGroup.ImageName
            };

            return View(newsGroupViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NewsGroupViewModel newsGroupViewModel, IFormFile imgUpload)
        {
            ModelState.Remove("ImageName");

            if (!ModelState.IsValid)
            {
                return View(newsGroupViewModel);
            }

            string imageName = newsGroupViewModel.ImageName ?? "nophoto.png";

            if (imgUpload != null && imgUpload.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(imgUpload.FileName).ToLower();

                if (allowedExtensions.Contains(extension))
                {
                    // ✅ حذف عکس قبلی فقط اگر nophoto نبود
                    if (!string.IsNullOrEmpty(newsGroupViewModel.ImageName) &&
                        newsGroupViewModel.ImageName != "nophoto.png")
                    {
                        string oldPath = Path.Combine(Directory.GetCurrentDirectory(),
                                                      "wwwroot", "Image", "news-group",
                                                      newsGroupViewModel.ImageName);

                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    // ذخیره عکس جدید
                    imageName = Guid.NewGuid().ToString() + extension;
                    string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", "news-group");

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

            var newsGroup = new tblNewsGroup
            {
                NewsGroupId = newsGroupViewModel.NewsGroupId,
                NewsGroupTitle = newsGroupViewModel.NewsGroupTitle,
                ImageName = imageName
            };

            _newsGroupService.Update(newsGroup);
            _newsGroupService.Save();

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            var entity = _newsGroupService.GetEntity(id);
            if (entity == null)
            {
                return NotFound();
            }

            // حذف تصویر از سرور (به جز تصویر پیش‌فرض)
            if (!string.IsNullOrEmpty(entity.ImageName) && entity.ImageName != "nophoto.png")
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", "news-group", entity.ImageName);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _newsGroupService.Delete(entity);
            _newsGroupService.Save();

            return RedirectToAction("Index");
        }
    }
}
