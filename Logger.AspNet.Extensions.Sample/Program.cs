using Logger.AspNet.Extensions;
using Logger.Extensions;
using Microsoft.Extensions;
using QuickLogger.Extensions.NetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddQuickLogger();

builder.Services.AddCorrelationService();

builder.Services.AddHttpContextAccessor();

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

app.MapGet("/test", () =>
{
    app.Logger.LogInformation("test");

    var correlationService = app.Services.GetRequiredService<ICorrelationService>();

    var correlation = correlationService.GetCurrentCorrelation();

    using var httpclient = new HttpClient();

    httpclient.UseCorrelationHeaders(correlation);

    var content = httpclient.GetAsync("http://localhost:5205").Result.Content.ReadAsStringAsync().Result;

    var ok = content == $"Hello World! - {correlation.CorrelationId}";

    return ok ? content : "Ko";
});

app.Run();
