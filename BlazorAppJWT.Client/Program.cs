using BlazorAppJWT.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorAppJWT.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<TokenProviderService>();

            await builder.Build().RunAsync();
        }
    }
}
