using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WelshVerbsBlazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services
	.AddHttpClient("verbs", c => c.BaseAddress = new Uri(builder.Configuration.GetSection("AppSettings")["VerbsMinimalAPI"].ToString()));
	
await builder.Build().RunAsync();
