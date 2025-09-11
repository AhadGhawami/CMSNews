using CMSNews.Mappings;
using CMSNews.Model.Context;
using CMSNews.Repository.Repository;
using CMSNews.Service.Service;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddScoped<NewsGroupService>();
// اگر سرویس‌های دیگه‌ای مثل NewsService یا UserService داری، اینجا اضافه کن:
// builder.Services.AddScoped<NewsService>();
// builder.Services.AddScoped<UserService>();

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