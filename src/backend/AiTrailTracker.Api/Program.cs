// Modified by AI on 05/10/2026. Edit #1.
// Modified by AI on 05/11/2026. Edit #2.
using AiTrailTracker.Api.Auth;
using AiTrailTracker.Api.Services;
using AiTrailTracker.Api.Storage;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Storage — Azure Table Storage or in-memory fallback
var connectionString = builder.Configuration["AzureTableStorage:ConnectionString"];
if (!string.IsNullOrEmpty(connectionString))
{
    builder.Services.AddSingleton<ITrailStorage, AzureTableStorage>();
}
else
{
    builder.Services.AddSingleton<ITrailStorage, InMemoryStorage>();
}

// Services
builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<IAdminService, AdminService>();

// Anti-forgery (CSRF protection)
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-XSRF-TOKEN";
    options.Cookie.Name = "XSRF-TOKEN";
    options.Cookie.HttpOnly = false; // Must be readable by JS
});

// Auth — dev bypass or Azure AD
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddAuthentication("DevBypass")
        .AddScheme<AuthenticationSchemeOptions, DevBypassAuthHandler>("DevBypass", null);
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminOnly", policy =>
            policy.RequireClaim("roles", "TrailAdmin"));
    });
}
else
{
    builder.Services.AddAzureAdAuth(builder.Configuration);
}

builder.Services.AddControllers(options =>
    {
        options.Filters.Add(new Microsoft.AspNetCore.Mvc.AutoValidateAntiforgeryTokenAttribute());
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

// Set anti-forgery token cookie on every request
app.Use(async (context, next) =>
{
    var antiforgery = context.RequestServices.GetRequiredService<Microsoft.AspNetCore.Antiforgery.IAntiforgery>();
    var tokens = antiforgery.GetAndStoreTokens(context);
    context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken!,
        new CookieOptions { HttpOnly = false, SameSite = SameSiteMode.Strict });
    await next(context);
});

app.MapControllers();

app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));

app.Run();
