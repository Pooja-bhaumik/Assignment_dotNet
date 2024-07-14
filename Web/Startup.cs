
using Api;
using Contracts;
using Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
             .AddNewtonsoftJson(options =>
              {
                  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                 
              });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/UserLogin/Index";

                    });
               services.AddAuthorization(options =>
                {
                options.AddPolicy("RequireAdminOrManager", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        return context.User.HasClaim(c => (c.Type == ClaimTypes.Role && (c.Value == "Admin" || c.Value == "Manager")));
                    });
                });


            });
            // Configure settings from appsettings.json
            services.Configure<WebApplicationSettings>(Configuration.GetSection("WebApplicationSettings"));

            // Add DbContext for UserDbContext
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("UserDbConnectionString")));


            services.AddHttpClient();

            // Add scoped services
            services.AddScoped<IUserLoginContract, UserLogin>();
            services.AddScoped<IEmployeeContract, Employee>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            
            {
                app.UseDeveloperExceptionPage(); 
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); 
                app.UseHsts(); 
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(); 

            app.UseRouting(); 

            app.UseAuthentication(); 
            app.UseAuthorization(); 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=UserLogin}/{action=Index}/{id?}");
            });
        }
    }
}

