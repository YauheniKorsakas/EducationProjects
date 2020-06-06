using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace Education.Web.Filters {
    public class ExceptionFilter : IExceptionFilter {
        public void OnException(ExceptionContext context) {
            ModifyResponse(context.HttpContext.Response, async (response) => {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                await response.WriteAsync("From exception filter.");
            });
            context.ExceptionHandled = true;
        }

        private void ModifyResponse(HttpResponse response, Action<HttpResponse> action) {
            action?.Invoke(response);
            var hash = GetHashCode();
            var responseHash = response.GetHashCode();
        }
    }
}
