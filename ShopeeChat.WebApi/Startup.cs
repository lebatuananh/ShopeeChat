using DVG.AutoPortal.Indian.API.Application.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShopeeChat.CoreAPI.Filters;
using ShopeeChat.CoreAPI.Middlewares;
using ShopeeChat.CoreAPI.Statics;
using ShopeeChat.Infrastructure.Utility;
using ShopeeChat.WebApi.Data;
using ShopeeChat.WebApi.Data.Entities;
using System;
using System.Text;

namespace ShopeeChat.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettings.Instance.SetConfiguration(Configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //1. Setup entity framework
            services.AddDbContext<ShopeeChatDbContext>(options =>
                options.UseSqlServer(AppSettings.Get<string>("Databases:SqlServer:ConnectionStrings:DefaultConnection")));

            services.AddIdentity<Membership, IdentityRole>()
                .AddEntityFrameworkStores<ShopeeChatDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.AddAuthentication(o =>
            {
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(o =>
               {
                   o.TokenValidationParameters = new TokenValidationParameters
                   {
                       ClockSkew = TimeSpan.Zero,
                       ValidateLifetime = true,
                       ValidateAudience = true,
                       ValidateIssuer = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = ApiStaticVariables.AuthConfig.Issuer,
                       ValidAudience = ApiStaticVariables.AuthConfig.Audience,
                       IssuerSigningKey = new SymmetricSecurityKey(
                           Encoding.ASCII.GetBytes(ApiStaticVariables.AuthConfig.Key))
                   };
               });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "ShopeeChat.API", Version = "v1" });
                c.OperationFilter<SwaggerHeaderFilter>();
            });
            IoC.RegisterTypes(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ProcessExceptionMiddleware>();
            app.UseMiddleware<DetectClientAppMiddleware>();
            app.UseMiddleware<CoppyJsonBodyMiddleware>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopeeChat.API");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}