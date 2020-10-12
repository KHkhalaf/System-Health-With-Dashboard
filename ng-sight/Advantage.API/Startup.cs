using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Advantage.API.Models;
using Microsoft.EntityFrameworkCore;
using Advantage.API;

namespace Advantage.API
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
            services.AddCors(opt =>
                {
                    opt.AddPolicy("CorsPolicy",
                        b => b.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader());
                }
            );

            services.AddControllers(options => options.EnableEndpointRouting = false);
            services.AddMvc();

            services.AddDbContext<ApiContext>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("NGsightDB"));
            });
            services.AddTransient<DataSeed>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataSeed seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            var nCustomers = 20;
            var nOrders = 1000;
            seeder.SeedData(nCustomers, nOrders);

            app.UseMvc(routes =>
                routes.MapRoute("default", "api/{controller}/{action}/{id?}")
            );
        }
    }
}
