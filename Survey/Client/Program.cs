using Blazored.LocalStorage;
using Blazored.Toast;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Survey.Client.Auth;
using Survey.Client.Repository;
using Survey.Client.Repository.Interfaces;
using Survey.Client.Unit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Survey.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();
            services.AddBlazoredToast();

            //services.AddScoped<IHttpService, HttpService>();

            //services.AddSingleton<IBoardRepository, BoardRepository>();
            
            services.AddScoped<IBoardRepository, BoardRepository>();
            services.AddScoped<BoardRepository>();
            services.AddScoped<BoardFillerRepository>();
            
            
            





            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<CardRepository>();
            services.AddScoped<CardBoardFillerRepository>();
            services.AddScoped<BoardFillerRepository>();

            services.AddAuthorizationCore();
            services.AddScoped<JWTAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider, JWTAuthenticationStateProvider>(
                provider => provider.GetRequiredService<JWTAuthenticationStateProvider>()
                );
            services.AddScoped<ILoginService, JWTAuthenticationStateProvider>(
                provider => provider.GetRequiredService<JWTAuthenticationStateProvider>()
                );
            services.AddScoped<IAccountsRepository, AccountsRepository>();
        }

    }

}
