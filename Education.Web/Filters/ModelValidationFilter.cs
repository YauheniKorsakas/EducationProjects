using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Education.Web.Filters {
    public class ModelValidationFilter : Attribute, IActionFilter {
        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context) {
            var modelState = context.ModelState;
        }
    }
}
