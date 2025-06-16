using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace App.Services.ExceptionHandler;

public class CriticalExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if(exception is CriticalException)
        {
            Console.WriteLine("Hata iletildi.");
        }

        return ValueTask.FromResult(false);
    }
}

