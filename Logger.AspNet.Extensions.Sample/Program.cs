using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions;
using QuickLogger.Extensions.NetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddQuickLogger();

var app = builder.Build();

app.UseHttpLogging();

app.UseLoggerCorrelationIdMiddleware();

app.MapGet("/", () =>
{
    app.Logger.LogInformation("test");
    return "Hello World!";
});

app.Run();
