using Ayllu.Composition;
using Ayllu.Infrastructure.Common.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// ---------------------------
// 1. Configura Ayllu (Application + Infrastructure)
// ---------------------------
builder.Services.AddAyllu(builder.Configuration);

// ---------------------------
// 2. CORS
// ---------------------------
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ---------------------------
// 3. Controllers
// ---------------------------
builder.Services.AddControllers();

// ---------------------------
// 4. Swagger / OpenAPI
// ---------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// ---------------------------
// 5. JWT Authentication
// ---------------------------
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings.GetValue<string>("SecretKey");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
        ValidAudience = jwtSettings.GetValue<string>("Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)),
        ClockSkew = TimeSpan.Zero
    };
});
var app = builder.Build();

// ---------------------------
// Aplica migrations antes do pipeline
// ---------------------------
app.Services.ApplyMigrationsSafely();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

// Middleware customizado para validar JWT invalidados
app.UseMiddleware<InvalidatedTokenMiddleware>();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
