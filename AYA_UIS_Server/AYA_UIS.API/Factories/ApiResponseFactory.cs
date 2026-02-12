using Microsoft.AspNetCore.Mvc;
using Shared.Dtos.ErrorModels;

namespace AYA_UIS.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult CustomValidationErrorResponse(ActionContext context)
        {
            // context => errors , Key[Field]
            // context.Modelstate ==> < string , Model State Entry >
            // string => Name of Field 
            // ModelStateEntry => Errors => Error Message 


            //IEnemrable <ValidationError>

            var errors = context.ModelState
                .Where(error => error.Value?.Errors.Any() == true)
                .Select(error => new ValidationError()
                {
                    Field = error.Key,
                    Errors = error.Value?.Errors.Select(error => error.ErrorMessage) ?? new List<string>()
                });


            var response = new ValidationErrorResponse()
            {
                Errors = errors,
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "One Or More ValidationError Happend"

            };
            return new BadRequestObjectResult(response);

        }
    }
}
