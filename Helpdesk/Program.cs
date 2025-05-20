using Database;
using Domain.User;
using Helpdesk.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Filters
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<CookieFilter>();
	options.Filters.Add<NavigationFilter>();
	//options.Filters.Add<DbTransactionFilter>();
});

// Database collections
builder.Services.AddDbContextPool<HelpdeskDbContext>(options => 
{
    options.UseNpgsql(builder.Configuration["DbSettings:ConnectionStringDevelopment"], b => b.MigrationsAssembly("Database"));
});
builder.Services.AddRepositories();

// Indentity collections
//builder.Services.AddAuthorization();
//builder.Services.AddAuthentication()
//	.AddCookie(IdentityConstants.ApplicationScheme);
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<HelpdeskDbContext>()
    .AddDefaultTokenProviders();

// Application collections
builder.Services.AddApplicationCollection(options => { });
builder.Services.AddStorageCollection(options =>
{
	options.SessionMemoryCacheEntryOptions.SlidingExpiration = TimeSpan.FromMinutes(30);
});
builder.Services.AddEventPlannerCollection(options => { });

// Build
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
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

using (var scope = app.Services.CreateScope())
{
    // docker run --name HelpdeskPostgres -p 32668:5432 -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=0iZZkGCfP4slqqd -d postgres:latest

    var dbBuilder = new DbDataBuilder(scope.ServiceProvider);

    await dbBuilder.GenerateRolesAsync();
    await dbBuilder.GenerateUsersAsync();
    await dbBuilder.GenerateAdminAsync();
    await dbBuilder.GenerateTicketsAsync();
}

app.Run();