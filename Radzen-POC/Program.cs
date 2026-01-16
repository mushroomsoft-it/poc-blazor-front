using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Radzen_POC;
using Radzen_POC.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Add Radzen services
builder.Services.AddRadzenComponents();

// Add application services
builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<TaskService>();
builder.Services.AddSingleton<StatisticsService>();

await builder.Build().RunAsync();
