using CMSNews.Mappings;
using CMSNews.Model.Context;
using CMSNews.Repository.Repository;
using CMSNews.Service.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// بارگذاری فایل‌های تنظیمات
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// افزودن سرویس‌های MVC
builder.Services.AddControllersWithViews();

// اتصال به دیتابیس
var connectionString = builder.Configuration.GetConnectionString("cs");
builder.Services.AddDbContext<DbCMSNewsContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// ثبت AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// ثبت ریپازیتوری جنریک برای همه موجودیت‌ها
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// ثبت سرویس‌های اختصاصی
builder.Services.AddScoped<INewsGroupService, NewsGroupService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICommentService, CommentService>();

// فعال‌سازی Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// فعال‌سازی احراز هویت با کوکی
builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });

var app = builder.Build();

// تنظیمات pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// فعال‌سازی Session و Authentication
app.UseSession();
app.UseAuthentication(); // حتماً قبل از Authorization باشه
app.UseAuthorization();

// مسیردهی برای ناحیه‌ها (Areas)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Default}/{action=Index}/{id?}");

// مسیردهی پیش‌فرض
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();