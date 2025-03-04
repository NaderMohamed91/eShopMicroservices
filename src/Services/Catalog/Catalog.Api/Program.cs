var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependancyInjection(builder.Configuration);

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options => { });

app.MapGet("/", () => "Api is up and running !!!");

app.Run();
