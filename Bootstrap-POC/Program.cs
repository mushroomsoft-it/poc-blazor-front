using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Bootstrap_POC;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Add application services
builder.Services.AddSingleton<Bootstrap_POC.Services.ProductService>();
builder.Services.AddSingleton<Bootstrap_POC.Services.TaskService>();
builder.Services.AddSingleton<Bootstrap_POC.Services.StatisticsService>();

await builder.Build().RunAsync();
