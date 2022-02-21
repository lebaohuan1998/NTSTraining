using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NTS.Common.Files
{
    public static class UploadFileCollectionExtensions
    {
        public static IServiceCollection AddNtsUpload(this IServiceCollection services, IConfiguration configuration)
        {
            var uploadSetting = configuration.GetSection("UploadSetting");
            services.Configure<UploadSettingModel>(uploadSetting);

            services.AddSingleton<IUploadFileService, UploadFileService>();

            return services;
        }

        public static IApplicationBuilder UseNtsUploadStaticFiles(this IApplicationBuilder app)
        {
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //     Path.Combine(Directory.GetCurrentDirectory(), "FileUpload/aaa")),
            //    RequestPath = "/FileUpload/aaa"
            //});

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                 Path.Combine(Directory.GetCurrentDirectory(), "FileUpload")),
                RequestPath = "/FileUpload"
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                 Path.Combine(Directory.GetCurrentDirectory(), "Export")),
                RequestPath = "/Export"
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                 Path.Combine(Directory.GetCurrentDirectory(), "Template")),
                RequestPath = "/Template"
            });

            return app;
        }
    }
}
