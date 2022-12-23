using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

[ExcludeFromCodeCoverage]
internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<HeadOutlet>("head::after");

        IServiceCollection services = builder.Services;

        services.AddHttpClient("LandingApp.ServerAPI", (sp, client) =>
        {
            var server = sp.GetRequiredService<IServer>();
            var addressFeature = server.Features.Get<IServerAddressesFeature>();
            string baseAddress = addressFeature.Addresses.First();
            client.BaseAddress = new Uri(baseAddress);
        })
        .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

        // Supply HttpClient instances that include access tokens when making requests to the server project
        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("LandingApp.ServerAPI"));

        services.AddApiAuthorization();

        await builder.Build().RunAsync();
    }
}