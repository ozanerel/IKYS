using IK.BLL.DependencyResolvers;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); //Db update hatasý aldýðým için loglarý konsola yazdýrmamýz için iki kodu yazdýk


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContextService(builder.Configuration);
builder.Services.AddIdentityService();
//builder.Services.AddRepositoryService();
//builder.Services.AddManagerService();
//builder.Services.AddHttpClient();

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
