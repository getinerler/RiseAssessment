using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ReportMicroservice.Database;
using ReportMicroservice.Hangfire;
using ReportMicroservice.Service;
using System;
using System.IO;

namespace ReportMicroservice
{
    public class Startup
    {
        public IConfiguration _config { get; }

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHangfire(x => x.UsePostgreSqlStorage(_config.GetConnectionString("DefaultConnection")));

            services.AddDbContext<DataContext>(options =>
               options.UseNpgsql(_config.GetConnectionString("DefaultConnection")));

            services.AddTransient<IHangfireService, HangfireService>();
            services.AddTransient<IReportService, ReportService>();
            
            services.AddTransient<IHangfireRepo, HangfireRepo>();
            services.AddTransient<IReportRepo, ReportRepo>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Report Microservice API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReportMicroserviceApi");
            });

            string path = Path.Combine(Directory.GetCurrentDirectory(), "ExcelFiles");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire");

            HangfireJobScheduler.ScheduleJobs();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(path),
                RequestPath = new PathString("/ExcelFiles")
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
