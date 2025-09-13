using CMSNews.Model.Context;
using CMSNews.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMSNews.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly DbCMSNewsContext _dbContext;
        private readonly UserManager<IdentityUser<Guid>> _userManager;

        public DashboardController(DbCMSNewsContext dbContext, UserManager<IdentityUser<Guid>> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var model = new AdminDashboardViewModel
            {
                TotalUsers = _userManager.Users.Count(),
                ActiveUsers = _dbContext.tblUser.Count(u => u.IsActive),
                InactiveUsers = _dbContext.tblUser.Count(u => !u.IsActive),
                TotalNews = _dbContext.tblNews.Count(),
                NewsGroupsCount = _dbContext.tblNewsGroup.Count()
            };

            return View(model);
        }

    }
}
