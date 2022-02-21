using NTSTraining.Models.Entities;
using NTSTraining.Services.Combobox;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using NTS.Common.Attributes;
using NTS.Common.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ToolManage.Models.Helpers;
using ToolManage.Models.Models.Settings;
using NTSTraining.Services.LoaiKhachHang;
using NTSTraining.Services.KhachHang;
using NTSTraining.Services.Department;
using NTSTraining.Services.Employee;

namespace ToolManage.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", p =>
                {
                    p.AllowAnyHeader()
                     .AllowAnyMethod()
                     .SetIsOriginAllowed((host) => true)
                     .AllowCredentials()
                     //p.AllowAnyOrigin()
                     //.AllowAnyHeader() 
                     //.AllowAnyMethod();
                     //.WithOrigins("http://example.com",
                     //               "http://www.contoso.com")
                     ;
                });
            });

            services.AddDbContext<NTSTrainingContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            var appSettingsSection = Configuration.GetSection("AppSetting");
            services.Configure<AppSettingModel>(appSettingsSection);

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NTSTraining", Version = "v1" });
            });

            services.AddMvc(config =>
            {
                config.Filters.Add(new ApiHandleExceptionSystemAttribute());
            })
                 .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                 .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

            services.AddScoped<IComboboxService, ComboboxService>();
            services.AddScoped<ILoaiKhachHangService, LoaiKhachHangService>();
            services.AddScoped<IKhachHangService, KhachHangService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzc0MzQ0QDMxMzgyZTM0MmUzMG0va3I2Smp2bW1iR3RNeXNzL3N6OWNwY05XdUFuS0dHZlFQVkF0YTlPa0U9");
            InitializeDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NTSTraining v1"));
            }

            loggerFactory.AddFile("Logs/log-{Date}.txt");
            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseFileServer();
            app.UseRouting();
            app.UseNtsUploadStaticFiles();
            app.UseAuthorization();

            //app.UseMvc(routes =>
            //{
            ////    routes.MapRoute(
            ////          name: "default",
            ////          template: "{controller=AngularHome}/{action=Index}/{id?}");
            //});

            //app.UseSpaStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                  pattern: "{controller=angularhome}/{action=index}/{id?}"
                );
            });

            //app.UseSpa(spa =>
            //{
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                //spa.Options.SourcePath = "ClientApp";

                //if (env.IsDevelopment())
                //{
                //    spa.UseAngularCliServer(npmScript: "start");
                //    spa.Options.StartupTimeout = TimeSpan.FromSeconds(600);
                //}
            //});
            //app.Map("/nts-manage",
            //    adminApp =>
            //    {
            //        adminApp.UseSpa(spa =>
            //        {
            //            spa.Options.SourcePath = "Web/NTSManage";
            //            spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
            //            {
            //                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Web", "NTSManage"))
            //            };

            //            //if (env.IsDevelopment())
            //            //    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
            //        });
            //    });

            //app.Map("/ba-manage",
            //  userApp =>
            //  {
            //      userApp.UseSpa(spa =>
            //      {
            //          spa.Options.SourcePath = "Web/NTSTraining";
            //          spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
            //          {
            //              FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Web", "NTSTraining"))
            //          };

            //          //if (env.IsDevelopment())
            //          //    spa.UseProxyToSpaDevelopmentServer("http://localhost:4201");
            //      });
            //  });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<NTSTrainingContext>();
                context.Database.Migrate();

            }
        }
    }
}
