﻿namespace CleanApp.API.Extension;

public static class ConfigurePipelineExtensions
{
    public static IApplicationBuilder UseConfigurePipelineExt(this WebApplication app)
    {
        app.UseExceptionHandler(x => { });

        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerExt();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        return app;
    }
}
