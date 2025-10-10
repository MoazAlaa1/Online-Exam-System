using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineExamSystem.BL;
using OnlineExamSystem.Models;
using OnlineExamystem.BL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ExamSystemContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{
    option.Password.RequiredLength = 8;
    option.Password.RequireNonAlphanumeric = true;
    option.Password.RequireUppercase = true;
    option.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<ExamSystemContext>();

builder.Services.AddScoped<IExam, ClsExam>();
builder.Services.AddScoped<IQuestion, ClsQuestion>();
builder.Services.AddScoped<IChoice, ClsChoice>();
builder.Services.AddScoped<ISubmission, ClsSubmission>();
builder.Services.AddScoped<IDashBoard, ClsDashBoard>();


builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.ConfigureApplicationCookie(option =>
{
    option.AccessDeniedPath = "/Error/E403";
    option.Cookie.Name = "Cookie";
    option.Cookie.HttpOnly = true;
    option.ExpireTimeSpan = TimeSpan.FromMinutes(720);
    option.LoginPath = "/Account/LogIn";
    option.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    option.SlidingExpiration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.UseSession();

app.UseEndpoints(endpoints =>
{

    endpoints.MapControllerRoute(
        name: "Admin",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");


});

app.Run();
