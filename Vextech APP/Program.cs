var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages();
builder.Services.AddResponseCompression();
var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
}

// UseResponseCompression makes it so that text is compressed. Speeds up like 1-4 secs load times.
app.UseResponseCompression();
app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();

    endpoints.MapControllerRoute("Default",
    "/{controller}/{action}/{id?}",
    new { controller = "App", action = "Index" });
});

app.Run();
