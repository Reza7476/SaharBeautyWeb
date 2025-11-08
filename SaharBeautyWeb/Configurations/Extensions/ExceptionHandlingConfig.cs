using Microsoft.AspNetCore.Diagnostics;

namespace SaharBeautyWeb.Configurations.Extensions;

public static class ExceptionHandlingConfig
{

    public static void UseCustomExceptionHandling(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = feature?.Error;
                    var path = context.Request.Path;

                    //if (path.StartsWithSegments("/Landing"))
                    //{
                    //    context.Response.Redirect($"/Landing/Error?message={exception?.Message}");
                    //}
                    //else if (path.StartsWithSegments("/UserPanels"))
                    //{
                    //    context.Response.Redirect($"/UserPanels/Error?message={exception?.Message}");
                    //}
                    //else
                    //{
                    //    context.Response.Redirect($"/Error?message={exception?.Message}");
                    //}
                    context.Response.Redirect($"/Error?message={exception?.Message}");
                });
            });

            app.UseHsts();
        }

    }
}
