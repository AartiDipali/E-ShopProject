using Microsoft.EntityFrameworkCore;
using E_CommerceWebApplication.DAL.Data;
using Microsoft.AspNetCore.Identity;
using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using NToastNotify;
using E_CommerceWebApplication.BLL.Infrastrastructure;
using E_CommerceWebApplication.BLL.Repository;
using E_CommerceWebApplication.BLL.ServiceRepository;
using E_CommerceWebApplication.BLL.Service;
using Serilog;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Smo.Wmi;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


//Email configuration Pending Global service creation

//var emailConfig = Configuration.GetSection("EmailConfiguration")
//  .Get<EmailConfiguration>();

//builder.Services.AddSingleton(emailConfig);
//builder.Services.AddScoped<IEmailSender, EmailSender>();
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//Read database connection value from appsettings.json file
builder.Services.AddDbContext<ApplicationDbcontext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("E-commerceDb"));
});
//Setup identity with Default dbcontext
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opt=>
{
    opt.Password.RequiredLength = 7;
    opt.Password.RequireDigit = false;
    opt.Password.RequireUppercase = false;
    opt.User.RequireUniqueEmail = true;
    opt.SignIn.RequireConfirmedEmail = false;
    opt.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
}).AddEntityFrameworkStores<ApplicationDbcontext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Account/Login";
});


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



//Register application dependancies.
builder.Services.AddTransient<IAccount, Account>();
builder.Services.AddTransient<IEmail, ServiceRepo>();
builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);


//Logging(global Logging,program.cs configuration move to app config,custom log in controller)
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("C:\\Applog\\Log-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

//Toster Notification
builder.Services.AddRazorPages().AddNToastNotifyNoty(new NotyOptions
{
    ProgressBar = true,
    Timeout = 5000
});

// Add ToastNotification
builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 5;
    config.IsDismissable = true;
    config.Position = NotyfPosition.TopRight;
});

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
//Remember me cookie

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
