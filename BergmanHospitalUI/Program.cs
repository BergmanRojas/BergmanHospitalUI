using BergmanHospitalUI;
using BergmanHospitalUI.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<TokenProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

builder.Services.AddScoped<CustomAuthorizationMessageHandler>(sp =>
{
    var tokenProvider = sp.GetRequiredService<TokenProvider>();
    var handler = new CustomAuthorizationMessageHandler(tokenProvider);
    handler.InnerHandler = new HttpClientHandler();
    return handler;
});

builder.Services.AddScoped(sp => new HttpClient(sp.GetRequiredService<CustomAuthorizationMessageHandler>())
{
    BaseAddress = new Uri("https://localhost:7247/")
});

await builder.Build().RunAsync();
