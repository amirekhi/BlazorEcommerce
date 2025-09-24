using BlazorEcommerce.Components;
using Classlibrary1.Services;
using BlazorEcommerce.Data;
using ClassLibrary1.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ClassLibrary1.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ClassLibrary1.Interfaces;
using BlazorEcommerce.Interfaces;
using Classlibrary1.Intefaces;
using BlazorEcommerce.Services;
using BlazorEcommerce.Client.Services;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();


builder.Services.AddControllers();


// builder.Services.AddScoped(http => new HttpClient
// {
//     BaseAddress = new Uri("https://localhost:5275/"),
// });

builder.Services.AddHttpClient<IClientProductService, ClientProductService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5275/"); // Or from config
});

builder.Services.AddHttpClient<IClientOrderRepository, ClientOrderRepository>(client =>
{
    client.BaseAddress = new Uri("https://localhost:5275/");
});


builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 12;
})
.AddEntityFrameworkStores<DataContext>();



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
   options.Events = new JwtBearerEvents
   {
       OnMessageReceived = context =>
       {
           var token = context.Request.Cookies["jwt"];
          
           if (!string.IsNullOrEmpty(token))
           {
               context.Token = token;
           }
           return Task.CompletedTask;
       }
   };


    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
           System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
        ),
        ValidateLifetime = true,
            // ✅ Add these two lines
      
    };
});


builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IClientOrderRepository, ClientOrderRepository>();
builder.Services.AddScoped<IClientProductService, ClientProductService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, NewCategoryService>();
builder.Services.AddScoped<IServerCartRepository, EfCartRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IServerOrderRepository, OrderRepository>();
builder.Services.AddScoped<IClientCartRepository, ClientCartRepository>();
builder.Services.AddScoped<CartStateService>();
builder.Services.AddScoped<IClientAdminUserService , ClientAdminUserService>();
builder.Services.AddScoped<IClientAnalyticsRepository, ClientAnalyticsRepository>();
builder.Services.AddScoped<IClientAnalyticsService, ClientAnalyticsService>();
builder.Services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorEcommerce.Client._Imports).Assembly);

app.Run();