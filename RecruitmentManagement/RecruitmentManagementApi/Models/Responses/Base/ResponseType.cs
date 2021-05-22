using System.Collections.Generic;

namespace RecruitmentManagementApi.Models.Responses.Base
{
    public class ResponseType
    {
        public bool IsSuccess()
        {
            return this is Success<bool>;
        }

        public bool IsSuccess<TModel>()
        {
            return this is Success<TModel>;
        }

        public TModel GetSuccessModel<TModel>()
        {
            return AsSuccess<TModel>().Model;
        }

        public bool HasValidations()
        {
            return this is Validation;
        }

        public IEnumerable<string> GetValidations()
        {
            return AsValidation().ValidationErrors;
        }

        public bool Failed()
        {
            return this is Failure;
        }

        public string GetFailureError()
        {
            return AsFailure().ErrorMessage;
        }

        private Success<TModel> AsSuccess<TModel>()
        {
            return this as Success<TModel>;
        }

        private Validation AsValidation()
        {
            return this as Validation;
        }

        private Failure AsFailure()
        {
            return this as Failure;
        }
    }
}
