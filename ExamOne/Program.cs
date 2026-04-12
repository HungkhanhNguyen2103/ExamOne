using ExamOne;
using ExamOne.Entity;
using ExamOne.Helper;
using ExamOne.Service;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StackExchange.Redis;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    //.WriteTo.Console()
    .WriteTo.File("logs/myredisservice.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = "localhost:6379"; // Redis server
//    options.InstanceName = "ExamOneAppCache_";     // prefix key
//});

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = builder.Configuration.GetConnectionString("Redis")
                        ?? "localhost:6379";
    return ConnectionMultiplexer.Connect(configuration);
});

builder.Services.AddHostedService<MyRedisService>();

builder.Services.Configure<Encryption>(
    builder.Configuration.GetSection("Encryption"));

builder.Services.AddSingleton<AesHelper>();

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("ExamOneMongoDB"));

builder.Services.AddSingleton<ExamOneMongoDBContext>();

builder.Services.AddDbContext<ExamOneDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ExamOneDb")));

//var googleConfig = builder.Configuration.GetSection("GoogleSSO");
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
//})
//.AddCookie()
//.AddGoogle(options =>
//{
//    options.ClientId = googleConfig["ClientId"];
//    options.ClientSecret = googleConfig["ClientSecret"];

//    options.Scope.Add("profile");
//    options.Scope.Add("email");
//    // options.CallbackPath = "/auth/google/callback";
//});

builder.Services.AddIdentity<Account, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<ExamOneDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/tai-khoan/dang-nhap";
    options.LogoutPath = "/tai-khoan/dang-xuat";
    options.AccessDeniedPath = "/tai-khoan/truy-cap";
});

builder.Services.AddScoped<IUserClaimsPrincipalFactory<Account>, AppClaimsPrincipalFactory>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IScoreService, ScoreService>();
builder.Services.AddScoped<IImageManagement, ImageManagement>();

builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
