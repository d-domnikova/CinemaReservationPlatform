using JWTAuthentication.Services.Interfaces;
using JWTAuthentication.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JWTAuthentication.Entities;
using Microsoft.AspNetCore.Identity;
using JWTAuthentication;
using JWTAuthentication.Configuration;
using JWTAuthenticationAPI.Settings;

namespace JWTAuthenticationAPI
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Configuration from AppSettings
            services.Configure<JWT>(_configuration.GetSection("JWT"));

            services.AddTransient<JwtTokenConfiguration>();
            services.AddTransient<IJwtService, JwtService>();

            //User Manager Service
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<JWTAuthenticationContext>();
            services.AddScoped<IUserService, UserService>();


            //Adding DB Context with MSSQL
            services.AddDbContext<JWTAuthenticationContext>(options =>
                options.UseSqlServer(
                    _configuration.GetConnectionString("MSSQLConnection"),
                    b => b.MigrationsAssembly(typeof(JWTAuthenticationContext).Assembly.FullName)));

            //Adding Athentication - JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,

                        ValidIssuer = _configuration["JWT:Issuer"],
                        ValidAudience = _configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]))
                    };
                });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
