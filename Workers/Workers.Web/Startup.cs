using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workers.Application.Services.Implementations;
using Workers.Application.Services.Interfaces;
using Workers.Infrastructure.Data.Context;

namespace Workers.Web
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
            services.AddDbContext<WorkerDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                });

            //services.AddTransient<IAuthorizationHandler, AgeHandler>();

            services.AddAuthorization(opts => {
                //opts.AddPolicy("ForEmail", policy =>
                //{
                //    policy.RequireClaim(ClaimTypes.Email, "test21@gmail.com", "nikita@gmail.com");
                //});
                //opts.AddPolicy("ForEmail", policy =>
                //{
                //    policy.RequireClaim("email", "test21@gmail.com", "nikita@gmail.com");
                //});

            });

            services.AddControllersWithViews();
            #region Need_IoC
            services.AddScoped<IEmployeeService, EmployeeService>(); //будте награмождение сервисов, нужно делать IoC контейнер
            #endregion
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

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
