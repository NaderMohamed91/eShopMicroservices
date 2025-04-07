using BuildingBlocks.Behaviors;
using Catalog.Api.Data;
namespace Catalog.Api.DI;

public static class CustomDependancyInjection
{
    public static void AddDependancyInjection
        (this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        var assembly = typeof(Program).Assembly;

        ////
        /// Add Marten configurations
        services.AddMarten(options =>
        {
            options.Connection(configuration.GetConnectionString("Database")!);
        }).UseLightweightSessions();

        if (environment.IsDevelopment())
            services.InitializeMartenWith<CatalogInitialData>();

        ////
        /// Add MediatR configurations
        services.AddMediatR(config => 
        {
            config.RegisterServicesFromAssembly(assembly);
            ////
            /// this to add behavior as a pipeline behavior into mediatR
            config.AddOpenBehavior(typeof(ValidationBehavior<,>)); // validation behavior 
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));    // logging behavior 
        });

        ////
        /// Add fluent validations to our requests
        services.AddValidatorsFromAssembly(assembly);

        ////
        /// Add carter configurations
        services.AddCarter();

        services.AddExceptionHandler<CustomExceptionHandler>();

        ////
        /// Add health checks
        services.AddHealthChecks();
    }
}