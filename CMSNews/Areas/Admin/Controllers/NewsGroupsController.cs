using AutoMapper;
using CMSNews.Mappings;
using CMSNews.Model.Context;
using CMSNews.Model.Models;
using CMSNews.Models.ViewModels;
using CMSNews.Service.Service;
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
                id = x.id,
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
        public IActionResult Create(tblNewsGroup newsGroup)
        {
            if (ModelState.IsValid)
            {
                _newsGroupService.Add(newsGroup);
                _newsGroupService.Save();
                return RedirectToAction("Index");
            }
            return View(newsGroup);
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
            return View(newsGroup);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(tblNewsGroup newsGroup)
        {
            if (ModelState.IsValid)
            {
                _newsGroupService.Update(newsGroup);
                _newsGroupService.Save();
                return RedirectToAction("Index");
            }
            return View(newsGroup);
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
