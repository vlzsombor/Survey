using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Survey.Client.Helpers;
using Survey.Client.Helpers.Providers;
using Survey.Client.Repository;
using System.Net.Http;
using System.Threading.Tasks;


namespace Survey.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AppAuthenticationStateProvider>();

            builder.Services.AddScoped<AuthenticationStateProvider>(
                            provider => 
                            provider.GetRequiredService<AppAuthenticationStateProvider>());

            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICardRepository, CardApiRepository>();
            services.AddScoped<IHttpService, HttpService>();





            //services.AddScoped<JWTAuthenticationStateProvider>();
            //services.AddScoped<AuthenticationStateProvider, JWTAuthenticationStateProvider>(
            //    provider => provider.GetRequiredService<JWTAuthenticationStateProvider>()
            //    );
            //services.AddScoped<ILoginService, JWTAuthenticationStateProvider>(
            //    provider => provider.GetRequiredService<JWTAuthenticationStateProvider>()
            //    );
            //services.AddScoped<IAccountsRepository, AccountsRepository>();
        }

    }

}
