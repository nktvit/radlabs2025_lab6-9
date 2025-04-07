using BlazorDataServices;
using Blazored.Toast;
using DataServices;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RAD30223Week8BlazorAppS00242994;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient for the Web API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7126") });

// Add Blazored Toast
builder.Services.AddBlazoredToast();

// Add AppState and LocalStorageService
builder.Services.AddScoped<AppState>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();

// Add Generic HTTP client service
builder.Services.AddScoped<IHttpClientService, HttpClientService>();

await builder.Build().RunAsync();
