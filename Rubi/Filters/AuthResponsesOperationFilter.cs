using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rubi.Filters
{
    public class AuthResponsesOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (!context.ApiDescription.TryGetMethodInfo(out System.Reflection.MethodInfo methodInfo))
                return;

            var classAuthAttributes = methodInfo
                .DeclaringType
                .GetCustomAttributes(false)
                .OfType<AuthorizeAttribute>();

            var authAttributes = methodInfo
                .GetCustomAttributes(false)
                .OfType<AuthorizeAttribute>();

            if (authAttributes.Any() || classAuthAttributes.Any())
            {
                operation.Responses.Add("401", new Response { Description = "Unauthorized" });
                operation.Responses.Add("403", new Response { Description = "Forbidden" });

                operation.Security = new List<IDictionary<string, IEnumerable<string>>>();
                operation.Security.Add(new Dictionary<string, IEnumerable<string>>
                {
                    {
                       "Bearer", new string[]{ }
                    }
                });
            }
        }
    }
}