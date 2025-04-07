
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependancyInjection(builder.Configuration, builder.Environment);

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options => { });

app.MapGet("/", () => "Api is up and running !!!");

app.UseHealthChecks("/health"
    , new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
