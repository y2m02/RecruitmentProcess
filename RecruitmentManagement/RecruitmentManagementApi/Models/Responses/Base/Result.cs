using System.Collections.Generic;
using System.Linq;
using RecruitmentManagementApi.Models.Extensions;

namespace RecruitmentManagementApi.Models.Responses.Base
{
    public class Result
    {
        public Result(object response)
        {
            Response = response;
        }

        public Result(IEnumerable<string> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public Result(object response, IEnumerable<string> validationErrors)
        {
            Response = response;
            ValidationErrors = validationErrors;
        }

        public Result(string errorMessage)
        {
            Error = $"Hubo un error duranto el proceso:\n\n {errorMessage}";
        }

        public Result(IEnumerable<string> validationErrors, string errorMessage)
        {
            ValidationErrors = validationErrors;
            Error = $"Hubo un error duranto el proceso:\n\n {errorMessage}";
        }

        public object Response { get; }

        public IEnumerable<string> ValidationErrors { get; } = new List<string>();

        public string Error { get; }

        public bool IsSuccess() => Response.HasValue() && !HasValidations() && !Failed();

        public bool HasValidations() => ValidationErrors.Any();

        public bool Failed() => Error.IsNotEmpty();

        public bool IsPartialSuccess() => IsSuccess() && HasValidations();
    }
}