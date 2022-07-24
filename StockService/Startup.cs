﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StockService.Data;
using StockService.Extensions;

namespace StockService
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDBContext(_config);


            services.AddControllers();
            services.AddCors();
            services.AddSignalR();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));
            }

            app.UseRouting();
            FillDatabaseExtension.PrepDatabase(app);
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
            .AllowCredentials().WithOrigins("https://localhost:4200"));

            app.UseAuthentication();
            app.UseAuthorization();

            //context.Database.EnsureCreated();
            context.Database.Migrate();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
