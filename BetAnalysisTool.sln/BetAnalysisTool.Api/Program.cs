using BetAnalysisTool.Api.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authentication.JwtBearer; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Auth0 authentication middleware
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
    options.Audience = builder.Configuration["Auth0:Audience"];
});
//

builder.Services.AddScoped<IStatsService, StatsService>();

// BallDontLie HttpClient with Authorization header
builder.Services.AddHttpClient<IStatsService, StatsService>(client =>
{
    // 1. Set the Base URL (using the new V1 endpoint)
    var baseUrl = builder.Configuration["BallDontLie:BaseUrl"] ?? "https://api.balldontlie.io/v1/";
    client.BaseAddress = new Uri(baseUrl);

    // 2. Add the API Key Header
    // Ensure "BallDontLie:ApiKey" exists in your appsettings.json
    client.DefaultRequestHeaders.Add("Authorization", builder.Configuration["BallDontLie:ApiKey"]);
});

// Enable in-memory caching for rate-limit mitigation ===
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication(); //must come BEFORE authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
