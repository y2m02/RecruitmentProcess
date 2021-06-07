using System.Collections.Generic;
using System.Linq;
using HelpersLibrary.Extensions;

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
            Error = $"There was an error while processing:\n\n {errorMessage}";
        }

        public Result(IEnumerable<string> validationErrors, string errorMessage)
        {
            ValidationErrors = validationErrors;
            Error = $"There was an error while processing:\n\n {errorMessage}";
        }

        public object Response { get; }

        public IEnumerable<string> ValidationErrors { get; } = new List<string>();

        public string Error { get; }

        public bool Succeeded() => Response.HasValue() && !HasValidations() && !Failed();

        public bool HasValidations() => ValidationErrors.Any();

        public bool Failed() => Error.IsNotEmpty();

        public bool PartialSucceeded() => Response.HasValue() && HasValidations();
    }
}