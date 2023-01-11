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
//using Microsoft.SqlServer.Management.Smo;
//using Microsoft.SqlServer.Management.Smo.Wmi;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


//Email configuration Pending Global service creation

//var emailConfig = Configuration.GetSection("EmailConfiguration")
//  .Get<EmailConfiguration>();

//builder.Services.AddSingleton(emailConfig);
//builder.Services.AddScoped<IEmailSender, EmailSender>();
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddControllersWithViews()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddDbContext<ApplicationDbcontext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("E-commerceDb"));
});
builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
   opt.TokenLifespan = TimeSpan.FromHours(2));
//Setup identity with Default dbcontext
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opt=>
{
    opt.Password.RequiredLength = 7;
    opt.Password.RequireDigit = false;
    opt.Password.RequireUppercase = false;
    opt.User.RequireUniqueEmail = true;
    opt.SignIn.RequireConfirmedEmail = true;
    
}).AddEntityFrameworkStores<ApplicationDbcontext>().AddSignInManager<SignInManager<ApplicationUser>>().AddDefaultTokenProviders();

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
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//Toster Notification
builder.Services.AddRazorPages().AddNToastNotifyNoty(new NotyOptions
{
    ProgressBar = true,
    Timeout = 7000
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

//Logging(what is logging,and Why we use serial log)
var logger = new LoggerConfiguration()

  .ReadFrom.Configuration(builder.Configuration)
  .WriteTo.Debug(outputTemplate: DateTime.Now.ToString())
     .CreateLogger();
//Read dynamic log file Path From  appsettings configuration

//.WriteTo.Seq("/*https://localhost:7234*/")//we can log our evevnt to seq also,console,File,database


builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        Dbintializer.InitializeAsync(services, userManager).Wait();
    }

    catch (Exception ex)
    {
        Log.Error(ex.Message);
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
//app.UseSerilogRequestLogging();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseNotyf();
app.UseAuthorization();

app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
