using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var services = builder.Services;
services.AddApiAuthorization();

await builder.Build().RunAsync();
