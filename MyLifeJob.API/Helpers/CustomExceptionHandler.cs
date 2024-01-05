using Microsoft.AspNetCore.Diagnostics;
using MyLifeJob.Business.Exceptions.User;

namespace MyLifeJob.API.Helpers;

public static class CustomExceptionHandler
{
    public static void UseCustomExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(handlerApp =>
        {
            handlerApp.Run(async context =>
            {
                var future = context.Features.Get<IExceptionHandlerPathFeature>();
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                if (future?.Error is IBaseException ex)
                {
                    context.Response.StatusCode = ex.StatusCode;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        StatusCode = ex.StatusCode,
                        Message = ex.ErrorMessage
                    });
                }
                else if (future?.Error is ArgumentNullException)
                {
                    await context.Response.WriteAsJsonAsync(new
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = future?.Error.Message
                    });
                }
            });
        });
    }
}
