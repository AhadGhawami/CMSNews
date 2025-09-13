using CMSNews.Model.Context;
using CMSNews.Model.Models;
using CMSNews.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMSNews.Controllers
{
    public class AccountController : Controller
    {
        private readonly DbCMSNewsContext _dbContext;

        public AccountController(DbCMSNewsContext dbContext)
        {
            _dbContext = dbContext;
        }

        // نمایش فرم ورود
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        // پردازش ورود
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _dbContext.tblUser
                .FirstOrDefaultAsync(u => u.MobileNumber == model.MobileNumber && u.Password == model.Password);

            if (user == null || !user.IsActive)
            {
                ModelState.AddModelError("", "شماره موبایل یا رمز عبور اشتباه است");
                return View(model);
            }

            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            HttpContext.Session.SetString("MobileNumber", user.MobileNumber);

            return !string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl)
                ? Redirect(model.ReturnUrl)
                : RedirectToAction("Index", "Home");
        }

        // خروج از حساب
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        // نمایش فرم ثبت‌نام
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // پردازش ثبت‌نام
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var exists = await _dbContext.tblUser.AnyAsync(u => u.MobileNumber == model.MobileNumber);
            if (exists)
            {
                ModelState.AddModelError("", "شماره موبایل قبلاً ثبت شده است");
                return View(model);
            }

            var newUser = new tblUser
            {
                UserId = Guid.NewGuid(),
                MobileNumber = model.MobileNumber,
                Password = model.Password,
                RegisterDate = DateTime.Now,
                IsActive = true
            };

            _dbContext.tblUser.Add(newUser);
            await _dbContext.SaveChangesAsync();

            HttpContext.Session.SetString("UserId", newUser.UserId.ToString());
            HttpContext.Session.SetString("MobileNumber", newUser.MobileNumber);

            return RedirectToAction("Index", "Home");
        }

        // لیست کاربران
        public async Task<IActionResult> UserList()
        {
            var users = await _dbContext.tblUser.ToListAsync();

            var userList = users.Select(u => new UserListViewModel
            {
                UserId = u.UserId,
                MobileNumber = u.MobileNumber,
                IsActive = u.IsActive,
                NewsCount = u.Newses?.Count ?? 0
            }).ToList();

            return View(userList);
        }

        // نمایش فرم ویرایش
        public async Task<IActionResult> EditUser(Guid id)
        {
            var user = await _dbContext.tblUser.FindAsync(id);
            if (user == null)
                return NotFound();

            var model = new EditUserViewModel
            {
                UserId = user.UserId,
                MobileNumber = user.MobileNumber,
                IsActive = user.IsActive,
                RegisterDate = user.RegisterDate
            };

            return View(model);
        }

        // ذخیره تغییرات
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _dbContext.tblUser.FindAsync(model.UserId);
            if (user == null)
                return NotFound();

            user.MobileNumber = model.MobileNumber;
            user.IsActive = model.IsActive;
            user.RegisterDate = model.RegisterDate;

            _dbContext.tblUser.Update(user);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("UserList");
        }

        // حذف کاربر
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _dbContext.tblUser.FindAsync(id);
            if (user == null)
                return NotFound();

            _dbContext.tblUser.Remove(user);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("UserList");
        }

        // تغییر وضعیت فعال بودن
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            var user = await _dbContext.tblUser.FindAsync(id);
            if (user == null)
                return NotFound();

            user.IsActive = !user.IsActive;
            _dbContext.tblUser.Update(user);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("UserList");
        }
    }
}