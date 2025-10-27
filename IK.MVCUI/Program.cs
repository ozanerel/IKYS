using IK.BLL.DependencyResolvers;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); //Db update hatasý aldýðým için loglarý konsola yazdýrmamýz için iki kodu yazdýk


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContextService(builder.Configuration);
builder.Services.AddIdentityService();
builder.Services.AddRepositoryService();
builder.Services.AddManagerService();
builder.Services.AddHttpClient();

//Dosya yükleme limiti
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 50 * 1024 * 1024; // 50 MB
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=JobApplication}/{action=Index}/{id?}");

app.Run();
