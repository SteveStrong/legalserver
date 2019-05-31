using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSwag.AspNetCore;
using System;

namespace legalserver
{
    public class Startup
    {
        public Startup(IConfiguration config, IHostingEnvironment env, ILoggerFactory log)
        {
            Configuration = config;
            Environment = env;
            Logger = log;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }
        public ILoggerFactory Logger { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
             {
                 builder
                     .AllowCredentials()
                     .AllowAnyMethod()
                     .AllowAnyOrigin()
                     .WithHeaders("X-Requested-With");
             }));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddSwagger();
            services.AddHealthChecks();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            var logger = Logger.CreateLogger<Startup>();

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                logger.LogInformation("Development environment");
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                logger.LogInformation($"Environment: {Environment.EnvironmentName}");
            }

            app.UseCors("CorsPolicy");
            app.UseHealthChecks("/ready");
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();

            app.UseSwaggerUi3WithApiExplorer();

        }

    }
}
