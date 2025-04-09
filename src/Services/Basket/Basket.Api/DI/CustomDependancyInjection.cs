using BuildingBlocks.Behaviors;
namespace Basket.Api.DI;

public static class CustomDependancyInjection
{
    public static void AddDependancyInjection
        (this IServiceCollection services, IConfiguration configuration /*, IWebHostEnvironment environment */)
    {
        var assembly = typeof(Program).Assembly;
        var connectionString = configuration.GetConnectionString("Database")!;

        ////
        /// Add MediatR configurations
        services.AddMediatR(config => 
        {
            config.RegisterServicesFromAssemblies(assembly);
            ////
            /// this to add behavior as a pipeline behavior into mediatR
            config.AddOpenBehavior(typeof(ValidationBehavior<,>)); // validation behavior 
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));    // logging behavior 
        });

        ////
        /// Add fluent validations to our requests
        services.AddValidatorsFromAssembly(assembly);


        services.AddCarter();

    }
}