using Bogdan_Auto.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Bogdan_Auto.Services;
using Bogdan_Auto.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Bogdan_Auto;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;




services.AddAuthentication().AddFacebook(facebookOptions =>
{
    facebookOptions.AppId = configuration["Authentication:Facebook:AppId"];
    facebookOptions.AppSecret = configuration["Authentication:Facebook:AppSecret"];
    facebookOptions.AccessDeniedPath = "/AccessDeniedPathInfo";
});
services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
});
services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
{
    microsoftOptions.ClientId = configuration["Authentication:Microsoft:ClientId"];
    microsoftOptions.ClientSecret = configuration["Authentication:Microsoft:ClientSecret"];
});

    services.Configure<CookiePolicyOptions>(options =>
    {
        // This lambda determines whether user consent for non-essential 
        // cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        // requires using Microsoft.AspNetCore.Http;
        options.MinimumSameSitePolicy = SameSiteMode.None;
    });
    services.AddSession(options =>
    {
        // Set a short timeout for easy testing.
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        //options.Cookie.HttpOnly = true;
        // Make the session cookie essential
        options.Cookie.IsEssential = true;
    });

services.AddRazorPages();
    services.AddDistributedMemoryCache();

services.ConfigureApplicationCookie(o => {
    o.ExpireTimeSpan = TimeSpan.FromDays(5);
    o.SlidingExpiration = true;
});


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentity<CustomerUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddRoleManager<RoleManager<IdentityRole>>().AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddTokenProvider<DataProtectorTokenProvider<CustomerUser>>(TokenOptions.DefaultProvider);
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

void initService(UserManager<CustomerUser> userManager, RoleManager<IdentityRole> roleManager)
{
    SeedData.Seed(userManager, roleManager);
}



app.UseEndpoints(endpoints =>
    {
      

        endpoints.MapAreaControllerRoute(
            name: "AdminArea",
            areaName: "Admin",
            pattern: "Admin/{controller=AdminController}/{action=Index}/{id?}");

        endpoints.MapAreaControllerRoute(
            name: "DeliveryStaffArea",
            areaName: "DeliveryStaff",
            pattern: "DeliveryStaff/{controller=DeliveryController}/{action=Index}/{id?}");

        endpoints.MapAreaControllerRoute(
            name: "Info",
            areaName: "Info",
            pattern: "{controller=Info}/{action=Privacy}");
        endpoints.MapAreaControllerRoute(
            name: "Products",
            areaName: "Products",
            pattern: "Products/{controller=ProductController}/{action=Index}/{id?}"
            );

        endpoints.MapAreaControllerRoute(
            name: "Order",
            areaName: "Order",
            pattern: "Order/{controller=OrderController}/{action=Checkout}/{id?}"
            );

        endpoints.MapControllerRoute(
            name: "Home",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });


app.MapRazorPages();

app.Run();

