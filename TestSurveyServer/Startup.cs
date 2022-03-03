using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;
using Microsoft.IdentityModel.Tokens;
using Survey.Server.Data;
using Survey.Server.Hubs;
using Survey.Server.Model;
using Survey.Server.Services;
using Survey.Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestSurveyServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddApplicationPart(Assembly.Load("Survey.Server")).AddControllersAsServices();

            services.AddDbContext<SurveyDbContext>();
            services.AddSignalR();
            services.AddScoped<IBoardService, BoardService>();
            services.AddFeatureManagement();
            services.AddScoped<IAccountService, AccountService>();
            services.AddRazorPages();
            services.AddDbContext<SurveyDbContext>(options =>
            {
                options.UseInMemoryDatabase("MyDatabase-" + Guid.NewGuid());
            }, ServiceLifetime.Singleton);

            //services.AddDbContextFactory<SurveyDbContext>(opt => opt.UseSqlite($"Data Source=../MyBlog.db"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapFallbackToFile("index.html");
            });

            //await Seeding(app);

        }

        private static async Task Seeding(IApplicationBuilder app)
        {
            RoleManager<IdentityRole> roleManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<IdentityUser> userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            await SeedAdministratorAndUser.Seed(roleManager, userManager);
        }
    }


}
