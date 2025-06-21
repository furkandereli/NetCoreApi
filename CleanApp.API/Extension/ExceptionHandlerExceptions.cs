using CleanApp.API.ExceptionHandler;

namespace CleanApp.API.Extension;

public static class ExceptionHandlerExceptions
{
    public static IServiceCollection AddExceptionHandlerExt(this IServiceCollection services)
    {

        services.AddExceptionHandler<CriticalExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }
}
