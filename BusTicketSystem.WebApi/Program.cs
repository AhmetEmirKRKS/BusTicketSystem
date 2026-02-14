using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BusTicketSystem.Application;
using BusTicketSystem.Infrastructure;
using BusTicketSystem.WebApi.Middlewares;
using Microsoft.EntityFrameworkCore;
using BusTicketSystem.Infrastructure.Persistence.Contexts;
using Microsoft.OpenApi.Models;
using BusTicketSystem.Infrastructure.Persistence.Seeds;
using Hangfire;
using Hangfire.PostgreSql;

var builder = WebApplication.CreateBuilder(args);

// ------ Katmanlarýn Servislerini Yükleme ------
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// ------ Redis Cache servisi -------
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["RedisSettings:Url"];
    options.InstanceName = "BusTicket_"; //Cache anahtarlarýnýn baþýna bunu koyar (karýþmasýn diye)
});

// ------ Hangfire Servisi ------
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHangfireServer();

//Web api katmaný
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//   ------ Swagger Ayarlarý (Kilit Ýkonu Ýçin) ------
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BusTicketSystem API", Version = "v1" });

    // Swagger'a JWT desteði ekle
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1Ni...\""
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!))
        };
    });

var app = builder.Build();

// ------ Middleware ------

app.UseHangfireDashboard();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ------ Seed Data entegrasyonu(Clean Way) ------
//Uygulama ayaða kalkarken bir kerelik Scope oluþturup Admin var mý diye bakar
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        await UserSeeder.SeedAsync(context);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"---> Seed iþlemi sýrasýnda hata oluþtu: {ex.Message}");
    }
}



app.Run();
