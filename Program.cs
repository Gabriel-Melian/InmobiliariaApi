
using Microsoft.AspNetCore.Authentication.JwtBearer;//Nuevo
using Microsoft.IdentityModel.Tokens;//Nuevo
using System.Text;//Nuevo
using InmobiliariaApi.Services;//Nuevo
using InmobiliariaApi.Data;//Nuevo
using Microsoft.EntityFrameworkCore;//Nuevo

var builder = WebApplication.CreateBuilder(args);

//Nuevo
//Configurar Entity Framework con MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//Nuevo
//Configurar JWT
builder.Services.AddScoped<JwtService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();//Nuevo
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//Habilitar autenticacion
app.UseAuthentication();//Nuevo
//Habilitar autorizacion
app.UseAuthorization();//Nuevo
app.MapControllers();//Nuevo

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

//Paquetes de Entity Framework y Dependencias
//dotnet add package Microsoft.EntityFrameworkCore
//dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.10//Use este que si es compatible
//dotnet add package Pomelo.EntityFrameworkCore.MySql
//dotnet add package MySql.Data
//dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
//dotnet add package System.IdentityModel.Tokens.Jwt
//dotnet add package BCrypt.Net-Next
//dotnet add package Swashbuckle.AspNetCore//No lo instale, pero creo que ya esta incluido el Swagger
