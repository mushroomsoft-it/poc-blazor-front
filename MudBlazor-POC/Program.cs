using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor_POC;
using MudBlazor.Services;
using MudBlazor_POC.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Add MudBlazor services
builder.Services.AddMudServices();

// Add application services
builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<TaskService>();
builder.Services.AddSingleton<StatisticsService>();

await builder.Build().RunAsync();
