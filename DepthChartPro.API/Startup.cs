using DepthChartPro.BL.Interfaces;
using DepthChartPro.BL.Services;
using DepthChartPro.DAL.DataAccess;
using DepthChartPro.DAL.Interfaces;
using DepthChartPro.DAL.Interfaces.Repository;
using DepthChartPro.DAL.Repository;
using DepthChartPro.DAL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepthChartPro.API
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
            services.AddDbContext<DepthChartContext>(options => options.UseInMemoryDatabase(databaseName:"DepthChartDB"));
            //services.AddSingleton<SeedingService>();
            // AddDbContext
            var sp = services.BuildServiceProvider();
            var dbContext = sp.GetRequiredService<DepthChartContext>();
            SeedingService project = new SeedingService(dbContext);
            services.AddSingleton(typeof(SeedingService), project);
            services.AddScoped<IDepthChartRepository, DepthChartRepository>();
            services.AddTransient<IDepthChartService, DepthChartService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DepthChartPro.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DepthChartPro.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
