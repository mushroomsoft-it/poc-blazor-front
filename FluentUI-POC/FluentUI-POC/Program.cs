using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using FluentUI_POC;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Add FluentUI services
builder.Services.AddFluentUIComponents();

// Add application services
builder.Services.AddSingleton<FluentUI_POC.Services.ProductService>();
builder.Services.AddSingleton<FluentUI_POC.Services.TaskService>();
builder.Services.AddSingleton<FluentUI_POC.Services.StatisticsService>();

await builder.Build().RunAsync();
