﻿using System;
using coffeterija.api.Middlewares;
using coffeterija.api.Services;
using coffeterija.application;
using coffeterija.application.Commands;
using coffeterija.application.Commands.Continents;
using coffeterija.application.Exceptions;
using coffeterija.application.Requests;
using coffeterija.application.Responses;
using coffeterija.dataaccess;
using coffeterija.efcommands.Continents;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

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
            services.AddTransient<IUpdateContinent, UpdateContinent>();
            services.AddTransient<IGetContinents, GetContinents>();
            services.AddTransient<ICreateContinent, CreateContinent>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ITokenService<int, UserLoginDTO>, JWTUserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // app.UseStaticFiles();

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
                    if (feature.Error is HttpException exception)
                    {
                        ErrorResponse resp = new ErrorResponse()
                        {
                            ErrorDetails = exception.HttpMessage
                        };
                        context.Response.StatusCode = exception.HttpStatus;
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(resp));
                    }
                    else
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Internal Server Error");
                    }
                });
            });
            app.UseMvc();
        }
    }
}
