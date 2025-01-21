using Fiap.Domain.Interfaces;
using Fiap.Infraestructure.Context;
using Fiap.Infraestructure.Repositories;
using FiapCadContato.Validators;
using FluentValidation;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using Prometheus;
using System.Globalization;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IDbContext, DbContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IContatoRepository<>), typeof(ContatoRepository<>));
builder.Services.AddTransient<IValidator<ContatoInput>, ContatoValidator>();
builder.Services.AddMetrics();//Add this -> to set default metrics data configuration



builder.Services.UseHttpClientMetrics();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiContatos", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

});

builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseMetricServer();
app.UseHttpMetrics();

app.MapGet("/random-number", () =>
{
    var number = Random.Shared.Next(0, 10);
    if (number >= 7)
        return Results.Unauthorized();
    return Results.Ok(number);
});

app.UseRouting();

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();
//place before app.UseEndpoints() to avoid losing some metrics
app.UseMetricServer();

app.MapControllers();

var cultureInfo = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

app.Run();
