using Microsoft.Extensions.FileProviders;

namespace Identity.API.Extensions;

public static class ProfilePictureExtensions
{
    public static IApplicationBuilder UseProfilePictureStorage(
        this IApplicationBuilder app,
        IWebHostEnvironment environment)
    {
        var uploadPath = Path.Combine(
            environment.ContentRootPath,
            "uploads");

        Directory.CreateDirectory(uploadPath);

        app.UseStaticFiles(
            new StaticFileOptions
            {
                FileProvider =
                    new PhysicalFileProvider(uploadPath),

                RequestPath = "/uploads"
            });

        return app;
    }
}