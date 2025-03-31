using Database;
using Domain.User;
using Helpdesk.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<CookieFilter>();
	options.Filters.Add<NavigationFilter>();
});

builder.Services.AddDbContextPool<HelpdeskDbContext>(options => 
{
    options.UseNpgsql(builder.Configuration["DbSettings:ConnectionString"], b => b.MigrationsAssembly("Database"));
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication()
	.AddCookie(IdentityConstants.ApplicationScheme);
builder.Services.AddIdentityCore<User>()
	.AddEntityFrameworkStores<HelpdeskDbContext>();

builder.Services.AddApplicationCollection(options => { });
builder.Services.AddStorageCollection(options =>
{
	options.SessionMemoryCacheEntryOptions.SlidingExpiration = TimeSpan.FromMinutes(30);
});
builder.Services.AddEventPlannerCollection(options => { });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

await app.Services.ApplyMigrations(builder.Configuration);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller}/{action}",
	defaults: new { controller = "Layout", action= "Index" });

app.Run();