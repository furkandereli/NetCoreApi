using CleanApp.API.Filters;

namespace CleanApp.API.Extension;

public static class ControllerExtensions
{
    public static IServiceCollection AddControllersWithFiltersExt(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<FluentValidationFilter>();
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
        });

        services.AddScoped(typeof(NotFoundFilter<,>));

        return services;
    }
}
