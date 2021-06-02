using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

namespace MeusInvestimentosApi.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureExcpetionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = Application.Json;
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        CustomError customError = new CustomError
                        {
                            Title = "Erro Interno no servidor",
                            Status = context.Response.StatusCode
                        };

                        // TODO : logar erro
                        await context.Response.WriteAsync(customError.ToString());
                    }
                });
            });
        }
    }
}
