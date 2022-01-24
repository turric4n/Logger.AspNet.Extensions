using Logger.AspNet.Extensions;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions;
using QuickLogger.Extensions.NetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddQuickLogger();

builder.Services.AddCorrelationService();

var app = builder.Build();

app.UseHttpLogging();

app.UseLoggerCorrelationMiddleware();

app.MapGet("/", () =>
{
    app.Logger.LogInformation("test");

    var correlationService = app.Services.GetRequiredService<ICorrelationService>();

    var correlation = correlationService.GetCurrentCorrelation();

    return $"Hello World! - {correlation.CorrelationId}";
});

app.Run();
