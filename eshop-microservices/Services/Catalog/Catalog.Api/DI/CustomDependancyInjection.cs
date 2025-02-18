namespace Catalog.Api.DI;

public static class CustomDependancyInjection
{
    public static void AddDependancyInjection(this IServiceCollection services)
    {
        services.AddCarter();
        services.AddMediatR(config => 
        {
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });
    }
}
