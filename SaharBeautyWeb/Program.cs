using Microsoft.AspNetCore.Diagnostics;
using SaharBeautyWeb.Configurations.Outofacs;

var builder = WebApplication.CreateBuilder(args);

var baseAddress = builder.Configuration["ApiSettings:BaseUrl"];

// Add services to the container.
builder.Services.AddRazorPages(option =>
{
    option.Conventions.ConfigureFilter(new AjaxExceptionFilter());
});

builder.Services.AddHttpClient();

builder.Host.AddAutofac(baseAddress!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // در محیط توسعه، جزئیات کامل خطا نمایش داده شود
    app.UseDeveloperExceptionPage();
}
else
{
    // در محیط تولید / هاست، خطاها به صفحه Error هدایت شوند (امن)
    app.UseExceptionHandler(error =>
    {
        error.Run(async context =>
        {
            var feature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = feature?.Error;
            var path = context.Request.Path;

            if (path.StartsWithSegments("/Landing"))
            {
                context.Response.Redirect($"/Landing/Error?message={exception?.Message}");
            }
            else if (path.StartsWithSegments("/UserPanels"))
            {
                context.Response.Redirect($"/UserPanels/Error?message={exception?.Message}");
            }
            else
            {
                context.Response.Redirect($"/Error?message={exception?.Message}");
            }
        });
    });

    // HSTS فقط برای محیط Production
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
