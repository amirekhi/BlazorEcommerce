
using BlazorEcommerce.Client.Services;
using ClassLibrary1.Interfaces;
using ClassLibrary1.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.Services.AddScoped<JwtAuthorizationMessageHandler>();

// Register HttpClient with the handler
builder.Services.AddHttpClient("AuthorizedClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5275/");
})
.AddHttpMessageHandler<JwtAuthorizationMessageHandler>();

// Provide a named HttpClient and default one for convenience
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("AuthorizedClient"));


builder.Services.AddScoped<IClientProductService, ClientProductService>();
builder.Services.AddScoped<IClientOrderRepository, ClientOrderRepository>();
builder.Services.AddScoped<IClientCartRepository, ClientCartRepository>();
builder.Services.AddScoped<CartStateService>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IClientAccountRepository, ClientAccountRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IClientAdminUserService, ClientAdminUserService>();
builder.Services.AddScoped<IClientAnalyticsRepository, ClientAnalyticsRepository>();
builder.Services.AddScoped<IClientAnalyticsService, ClientAnalyticsService>();






await builder.Build().RunAsync();
