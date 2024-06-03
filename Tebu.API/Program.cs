using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using Tebu.API.Data;
using Tebu.API.Events;
using Tebu.API.Middlewares;
using Tebu.API.Repository.Extentions;
using Tebu.API.Service.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//dotnet ef migrations add Initial --project Tebu.API --output-dir ./Data/Migrations

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(p =>
    {
        p.AllowAnyHeader().AllowCredentials().AllowAnyMethod().SetIsOriginAllowed(e => true);
    });

});

builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();

builder.Services.AddDbContext<TebuDbContext>(options =>
{
    var conString = "Host=localhost;Port=5432;Database=tebudb;Username=postgres;Password=tamuro174;";
    options.UseNpgsql(conString);
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MES API",
        Description = "MES API Backend Swagger Documentation for API Usage",
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


builder.Services.AddScoped<CustomCookieAuthenticationEvents>();
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.IsEssential = true;
        options.Cookie.SameSite = SameSiteMode.None;
        options.EventsType = typeof(CustomCookieAuthenticationEvents);
    });


builder.Services.AddRepositories();
builder.Services.AddServices();

var app = builder.Build();
app.UseCors();

using (var scope = app.Services.CreateScope())
{
    using (var db = scope.ServiceProvider.GetRequiredService<TebuDbContext>())
    {
        db.Database.Migrate();
    }
}


// Configure the HTTP request pipeline.

app.UseCustomExceptionHandlerMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "";
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MES API V1");
});

app.Run();
