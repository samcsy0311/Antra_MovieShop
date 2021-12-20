using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MovieShopMVC.Helpers;
using Serilog;
using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
     .WriteTo.File(new CompactJsonFormatter(), "log.json"));

// Add services to the container.
// Registering your Services/dependencies
builder.Services.AddControllersWithViews();

// Services Injection
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserService, UserService>();

// Repositories Injection
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Inject the connection string into the MovieShopDbContext constructor using DbContextOptions
builder.Services.AddDbContext<MovieShopDbContext>(
     options => options.UseSqlServer(builder.Configuration.GetConnectionString("MovieShopDbConnection"))
);

// DI for cache
// builder.Services.AddMemoryCache();

// tell our asp.net what kind of authentication we are using
// cookie-based authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
     .AddCookie(option =>
     {
          option.Cookie.Name = "MovieShopAuthCookie";
          option.ExpireTimeSpan = TimeSpan.FromDays(1);
          option.LoginPath = "/account/login";
     });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
     // app.UseExceptionHandler("/Home/Error");
     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
     app.UseMovieShopExceptionMiddleware();
     app.UseHsts();
}
else
{
     app.UseMovieShopExceptionMiddleware();
}

// 6 built-in middleware (order matters)
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // authentication first, then gives role
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
