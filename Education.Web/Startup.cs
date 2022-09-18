using Education.Web.Filters;
using Education.Web.Services;
using Education.Web.Services.HttpServices;
using Education.Web.Services.Interfaces;
using Education.Web.Utils.Constants;
using Education.Web.Utils.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace Education.Web {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers(options => {
                options.RespectBrowserAcceptHeader = true;
                options.ReturnHttpNotAcceptable = true;
                options.Filters.Add(new LogExceptionFilter());
                options.Filters.Add(new ExceptionFilter());
            });
            services.AddScoped<IDataService, DataService>();
            services.AddHostedService<DataHostedService>();
            services.Configure<UserDataOptions>("defaultOptions", Configuration.GetSection("UserData"));
            services.Configure<UserDataOptions>("manualUserOptions", options => {
                (options.Name, options.Surname) = ("custom name", "custom surname");
            });
            services.Configure<UserDataAdditionalOptions>(Configuration.GetSection("UserData:Additional"));
            services.AddHttpClient();
            services.AddHttpClient(Microservices.Misc, httpClient => {
                httpClient.BaseAddress = new Uri("https://localhost:44310/");
            });
            services.AddHttpClient<MiscHttpService>(httpClient => {
                httpClient.BaseAddress = new Uri("https://localhost:44310/");
            });
            // services.AddMvc().AddXmlSerializerFormatters();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider) {
            if (env.IsDevelopment()) {
                // app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseExceptionHandler(context => 
            //    context.Run(async cont => {

            //    });
            //});
            //app.UseStatusCodePages("text/plain", "Status code page");
            var options = serviceProvider.GetRequiredService<IOptions<UserDataAdditionalOptions>>().Value;

            //app.Use(async (context, next) => {
            //    var a = 1;
            //    // await next.Invoke(); // if not call next.invoke in pipeline, request cant get further to next handler middlewares.
            //});

            //app.UseResponseCompression(); // reduce the size of the static files response.
            app.Map(
                "/lols/lols",
                appBuilrder => appBuilrder.Run(context => {
                    context.Response.Headers.Add("custom-header", "some value for custom header");
                    return Task.CompletedTask;
                })
            );

            app.Use(async (context, next) => {
                context.Request.Headers.Add("dynamicaly-added-header", new StringValues("new header value"));
                await next.Invoke();
            });


            //app.Map(
            //    "/api/data/get-number",
            //    appBuilder => appBuilder.Use((context, next) => {
            //        if (!context.Response.HasStarted) {
            //            context.Response.Headers.Add("custom-header", new StringValues("custom header value"));
            //        }

            //        return Task.CompletedTask;
            //    })
            //);

            //app.Map(
            //    "/api/format/GetHtmlPage",
            //    //appBuilrder => appBuilrder.Run(context => {
            //    //    context.Response.Headers.Add("custom-header", "some value for custom header");
            //    //    return Task.CompletedTask;
            //    //})
            //    //appBuilder => appBuilder.Use(async (context, next) => {
            //    //    context.Response.Headers.Add("html-header", new StringValues("header-value"));
            //    //    await next.Invoke();
            //    //})
            //);

            app.Map("/error", config => {
                config.Run(async context => {
                    await context.Response.WriteAsync("Some error has happened.");
                });
            });

            app.UseRouting();

            //app.Use(next => context => {
            //    var endpoint = context.GetEndpoint();
                
            //    return Task.CompletedTask;
            //});

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context => {
                    StringValues val = new StringValues("custom value");
                    context.Response.Headers.Add("customer-header", val);
                });
            });

            app.UseStaticFiles();
        }
    }
}
