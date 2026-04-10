using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net;

namespace ExamOne
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var logLevel = LogLevel.Error;
            string errorMessage = "Đã có lỗi xảy ra. Vui lòng thử lại sau.";

            switch (exception)
            {
                case ArgumentException:
                    statusCode = HttpStatusCode.BadRequest;
                    errorMessage = exception.Message;
                    logLevel = LogLevel.Warning;
                    break;

                case KeyNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    errorMessage = exception.Message;
                    logLevel = LogLevel.Warning;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    logLevel = LogLevel.Error;
                    break;
            }

            _logger.Log(logLevel, exception, "Unhandled exception: {Message}", exception.Message);

            context.Response.Clear();
            context.Response.ContentType = "text/html; charset=utf-8";
            context.Response.StatusCode = (int)statusCode;


            var errorModel = new
            {
                Code = (int)statusCode,
                Message = errorMessage,
                Detail = exception.Message,
            };

            await ViewRenderer.RenderViewAsync(context, "/Views/Account/Error.cshtml", errorModel);
        }
    }

    public static class ViewRenderer
    {
        public static async Task RenderViewAsync(HttpContext context, string viewPath, object? model = null)
        {
            var viewEngine = context.RequestServices.GetRequiredService<IRazorViewEngine>();
            var tempDataProvider = context.RequestServices.GetRequiredService<ITempDataProvider>();
            var serviceProvider = context.RequestServices.GetRequiredService<IServiceProvider>();

            var actionContext = new ActionContext(context, new RouteData(), new ActionDescriptor());

            var viewResult = viewEngine.GetView(executingFilePath: null, viewPath: viewPath, isMainPage: true);

            if (!viewResult.Success)
            {
                throw new FileNotFoundException($"Không tìm thấy view: {viewPath}");
            }

            var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };
            await using var writer = new StreamWriter(context.Response.Body);
            var viewContext = new ViewContext(
                actionContext,
                viewResult.View,
                viewDictionary,
                new TempDataDictionary(context, tempDataProvider),
                writer,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);
            await writer.FlushAsync();
        }
    }

}
