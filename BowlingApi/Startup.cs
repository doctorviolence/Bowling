using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BowlingApi
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
            //services.AddCors();
            //services.AddMvc();
            services.AddCors(options =>
            {
                options.AddPolicy("bowling",
                    builder => 
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .WithExposedHeaders("content-disposition")
                            .AllowAnyHeader()
                            .AllowCredentials()
                            .SetPreflightMaxAge(TimeSpan.FromSeconds(3600)));
            });
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("bowling",
            //        policy => policy.WithOrigins("http://localhost:63342"));
            //});
            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(new CorsAuthorizationFilterFactory("bowling"));
            //});
            //services.AddCors(); 
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); 
            app.UseCors("bowling");
            //app.UseCors(b => b.WithOrigins("http://localhost:63342").AllowAnyHeader().AllowAnyMethod());
            /*app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });*/
            
            app.UseMvc();
        }
    }
}