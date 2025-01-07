var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
	// Global action filters
    options.Filters.Add<CookieFilter>();
	options.Filters.Add<NavigationFilter>();
});

// Hosted Services
builder.Services.AddSingleton<EventPlanner>();
builder.Services.AddHostedService(ep => ep.GetRequiredService<EventPlanner>());

// Integral Services
builder.Services.AddSingleton<IApplicationSettings, ApplicationSettings>(_ => { return ApplicationSettings.GetApplicationSettings(); });
builder.Services.AddSingleton<ISessionStorage, SessionCache>();

builder.Services.AddScoped<IScopeStorage, ScopeCache>();
builder.Services.AddScoped<IStorageManager, StorageManager>();

builder.Services.AddTransient<IEventPlannerManager, EventPlannerManager>();

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
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller}/{action}",
	defaults: new { controller = "Layout", action= "Index" });

app.Run();