using Microsoft.AspNetCore.Diagnostics;
using Project_Core.Dtos;
using Project_Service.Exceptions;
using System.Text.Json;

namespace Project_API.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundException => 404,
                        _ => 500
                    };

                    context.Response.StatusCode = statusCode;

                    var errorMessage = exceptionFeature.Error.Message;

                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode, errorMessage);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));

                });
            });
        }
    }
}
