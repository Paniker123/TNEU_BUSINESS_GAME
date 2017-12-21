using System;
using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Helper
{
    
    public class FileUploadFilter : IOperationFilter
    {
        private static List<string> requiredFiles = new List<string>() {};


        private static List<string> nonRequiredFiles = new List<string>() { };

        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (requiredFiles.Contains(operation.OperationId, StringComparer.CurrentCultureIgnoreCase))
            {
                ConfifureOperation(operation, context, true);
            }

            if (nonRequiredFiles.Contains(operation.OperationId, StringComparer.CurrentCultureIgnoreCase))
            {
                ConfifureOperation(operation, context, false);
            }
        }

        private void ConfifureOperation(Operation operation, OperationFilterContext context, bool isRequired)
        {
            var toRemove = operation.Parameters.FirstOrDefault(x => x.Name == "file");
            operation.Parameters.Remove(toRemove);

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "file",
                In = "formData",
                Description = "Upload File",
                Required = isRequired,
                Type = "file"
            });
            operation.Consumes.Add("multipart/form-data");
        }
    }
}
