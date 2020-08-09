    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Orchestrate.TaskManager.Web.Services;
using Orchestrate.TaskManager.Web.Infrastructure.HttpMessageHandlers;

namespace Orchestrate.TaskManager.Web
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
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddControllersWithViews();
            services.AddOptions();
            services.Configure<AppSettings>(Configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();


            services.AddHttpClient<IUserInfoService, UserInfoService>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(15))
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddControllers();

            services.AddAuthentication(options =>
            {
                //options.DefaultScheme = "cookie";
                //options.DefaultChallengeScheme = "oidc";
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie(setup => {
                setup.ExpireTimeSpan = TimeSpan.FromMinutes(3600);
                setup.AccessDeniedPath = "/Account/AccessDenied";
            })
            .AddOpenIdConnect(options=>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = @"https://localhost:53556";
                options.SignedOutRedirectUri = "https://localhost:56691";
                options.ClientId = "mvc1";
                options.ClientSecret= "SuperSecretPassword";
                options.ResponseType = "code id_token";
                options.UsePkce = false;
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.RequireHttpsMetadata = false;
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("api1.read");
                options.Scope.Add("role");
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    RoleClaimType = "role"
                };
                options.ClaimActions.MapUniqueJsonKey("role", "role");
                

            });
            //.AddOpenIdConnect("oidc", options =>
            //{
            //    //options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.Authority = @"https://localhost:53556";
            //    //options.SignedOutRedirectUri = @"https://localhost:56691";
            //    options.ClientId = "oidcClient";
            //    options.ClientSecret = "SuperSecretPassword";
            //    options.ResponseType = "code";
            //    options.ResponseMode = "query";
            //    options.UsePkce = true;
            //    options.SaveTokens = true;
            //    //options.GetClaimsFromUserInfoEndpoint = true;
            //    //options.RequireHttpsMetadata = false;
            //    options.Scope.Add("openid");
            //    options.Scope.Add("profile");
            //    options.Scope.Add("api1.read");

            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
