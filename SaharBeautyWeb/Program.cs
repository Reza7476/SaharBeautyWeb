using SaharBeautyWeb.Configurations.Extensions;
using SaharBeautyWeb.Configurations.Outofacs;
using SaharBeautyWeb.ProjectMiddleware;

var builder = WebApplication.CreateBuilder(args);

var baseAddress = builder.Configuration["ApiSettings:BaseUrl"];

// Add services to the container.
builder.Services.AddRazorPages(option =>
{
    option.Conventions.ConfigureFilter(new AjaxExceptionFilter());
});

builder.Services.AddHttpClient();

builder.Host.AddAutofac(baseAddress!);
builder.Services.AddSession();

var app = builder.Build();

app.UseCustomExceptionHandling();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();

app.UseMiddleware<JwtTokenInitializerMiddleware>();
app.MapRazorPages();
app.Run();
