﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using thePlayList.Data;

namespace thePlayList
{
    public class Startup
    {

        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuraton)
        {
            Configuration = configuraton;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<MusicDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("AzureDeployment")));
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
           {
               routes.MapRoute(
                   name: "default",
                   template: "{controller=User}/{action=Home}/{id?}");
           });

            app.UseStaticFiles();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Fix Me!");
            });
        }
    }
}