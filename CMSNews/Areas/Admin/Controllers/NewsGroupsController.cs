using AutoMapper;
using CMSNews.Mappings;
using CMSNews.Model.Context;
using CMSNews.Model.Models;
using CMSNews.Models.ViewModels;
using CMSNews.Service.Service;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Net;

namespace CMSNews.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsGroupsController : Controller
    {
        private readonly NewsGroupService _newsGroupService;

        public NewsGroupsController(NewsGroupService newsGroupService)
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
            return View(newsGroup);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewsGroupViewModel newsGroupViewModel, IFormFile imgUpload)
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
            tblNewsGroup newsGroup = _newsGroupService.GetEntity(id.Value);
            if (newsGroup == null)
            {
                return NotFound();
            }
            // تبدیل دستی به ViewModel
            NewsGroupViewModel newsGroupViewModel = new NewsGroupViewModel
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
            if (ModelState.IsValid)
            {
                if(imgUpload != null)
                {
                    if(newsGroupViewModel.ImageName != "nophoto.png")
                    {
                        //System.IO.File.Delete(Server.MapPath("/images/news-group/")+newsGroupViewModel.ImageName);
                    }
                }
                tblNewsGroup newsGroup = new tblNewsGroup
                {
                    NewsGroupId = newsGroupViewModel.NewsGroupId,
                    NewsGroupTitle = newsGroupViewModel.NewsGroupTitle,
                    ImageName = newsGroupViewModel.ImageName
                };
                _newsGroupService.Update(newsGroup);
                _newsGroupService.Save();
                return RedirectToAction("Index");
            }
            return View(newsGroupViewModel);
        }
        public IActionResult Delete(Guid? id)
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
            return View(newsGroup);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            //tblNewsGroup newsGroup = _newsGroupService.GetEntity(id);
            _newsGroupService.Delete(id);
            _newsGroupService.Save();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            _newsGroupService.Dispose();
        }
    }
}
