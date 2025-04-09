var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependancyInjection(builder.Configuration /*, builder.Environment */);

var app = builder.Build();

app.MapCarter();

app.MapGet("/", () => "Basket Api is up and running !!!");

app.Run();