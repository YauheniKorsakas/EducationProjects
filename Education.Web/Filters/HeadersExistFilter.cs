using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Education.Web.Filters {
    [AttributeUsage(AttributeTargets.Method)]
    public class HeadersExistFilter : Attribute, IActionFilter {
        private string[] _headers;

        public HeadersExistFilter(params string[] headers) {
            _headers = headers ?? Array.Empty<string>();
        }

        public void OnActionExecuted(ActionExecutedContext context) {
            var a = 1;
        }

        public void OnActionExecuting(ActionExecutingContext context) {
            if (_headers == null || !_headers.Any()) {
                return;
            }

            var nonExistHeaders = new List<string>();

            foreach (var header in _headers) {
                if (!context.HttpContext.Request.Headers.ContainsKey(header)) {
                    nonExistHeaders.Add(header);
                }
            }

            if (nonExistHeaders.Any()) {
                // context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                // context.Result = new BadRequestResult();
            }
        }
    }
}
