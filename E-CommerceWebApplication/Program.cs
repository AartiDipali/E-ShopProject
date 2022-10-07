using Microsoft.EntityFrameworkCore;
using E_CommerceWebApplication.DAL.Data;
using Microsoft.AspNetCore.Identity;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//
builder.Services.AddDbContext<ApplicationDbcontext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("E-commerceDb"));
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbcontext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(opt =>
{
    // Password settings 
    opt.Password.RequireDigit = true;
    opt.Password.RequiredLength = 10;

    // Lockout settings 
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
    opt.Lockout.MaxFailedAccessAttempts = 5;

    //Signin option
    opt.SignIn.RequireConfirmedEmail = false;

    // User settings 
    opt.User.RequireUniqueEmail = true;

    //Token Option
    opt.Tokens.AuthenticatorTokenProvider = "Name of AuthenticatorTokenProvider";

});

builder.Services.AddNotyf(config => {config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });

builder.Services.AddMemoryCache();

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(1);
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        Dbintializer.InitializeAsync(services, userManager).Wait();
    }

    catch (Exception ex)
    {

    }
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseNotyf();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
