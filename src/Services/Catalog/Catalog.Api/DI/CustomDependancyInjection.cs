using Marten;

namespace Catalog.Api.DI;

public static class CustomDependancyInjection
{
    public static void AddDependancyInjection(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddMarten(options =>
        {
            options.Connection(configuration.GetConnectionString("Database")!);
        }).UseLightweightSessions();

        services.AddCarter();
        services.AddMediatR(config => 
        {
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });


        //services.AddScoped<IDocumentSession>();
    }
}
