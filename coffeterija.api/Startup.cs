using System;
using coffeterija.api.Middlewares;
using coffeterija.api.Services;
using coffeterija.application.Commands;
using coffeterija.dataaccess;
using coffeterija.efcommands;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace coffeterija.api
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
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<CoffeeContext>();
            services.AddTransient<IGetContinents, GetContinents>();
            services.AddScoped<ILoginService, LoginService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiddleware<LoginMiddleware>();
            // app.UseHttpsRedirection(); TODO: add in production
            /*
             * We handle every exception here
             */
            app.UseExceptionHandler(errApp =>
            {
                errApp.Run(async context =>
                {
                    IExceptionHandlerPathFeature feature = context.Features.Get<IExceptionHandlerPathFeature>();
                    Exception exception = feature.Error;

                    await context.Response.WriteAsync(exception.Message);
                });
            });
            app.UseMvc();
        }
    }
}
