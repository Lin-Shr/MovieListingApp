using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using MovieApp.Areas.Identity.Data;
using MovieApp.Data;
using MovieApp.Interfaces;
using MovieApp.Repository;
using MovieApp.Seed;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MovieAppContextConnection") ?? throw new InvalidOperationException("Connection string 'MovieAppContextConnection' not found.");

builder.Services.AddDbContext<MovieAppContext>(options =>
    options.UseSqlServer(connectionString));

/*builder.Services.AddDefaultIdentity<MovieAppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MovieAppContext>();*/
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IMovies, MovieRepository>();
builder.Services.AddScoped<IGenre, GenreRepository>();
builder.Services.AddScoped<IGenreMovie, GenreMovieRepository>();
builder.Services.AddScoped<IReviews, ReviewRepository>();
builder.Services.AddScoped<IEmail, EmailRepository>();
builder.Services.AddScoped<IdbInitializer, DbInitializer>();
builder.Services.AddIdentity<MovieAppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<MovieAppContext>()
        .AddSignInManager<SignInManager<MovieAppUser>>()
        .AddDefaultUI()
        .AddTokenProvider<DataProtectorTokenProvider<MovieAppUser>>(TokenOptions.DefaultProvider);
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

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

SeedDatabase();

app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IdbInitializer>();
        dbInitializer.Initialize();
    }
}