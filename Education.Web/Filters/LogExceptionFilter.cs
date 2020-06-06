using Microsoft.AspNetCore.Mvc.Filters;

namespace Education.Web.Filters {
    public class LogExceptionFilter : IExceptionFilter {
        public void OnException(ExceptionContext context) {
            context.ExceptionHandled = true;
        }
    }
}
