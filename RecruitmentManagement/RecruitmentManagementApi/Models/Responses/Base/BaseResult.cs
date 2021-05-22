using System.Collections.Generic;

namespace RecruitmentManagementApi.Models.Responses.Base
{
    public class BaseResult
    {
        public bool IsSuccess()
        {
            return this is SuccessResult<bool>;
        }

        public bool IsSuccess<TModel>()
        {
            return this is SuccessResult<TModel>;
        }

        public TModel GetSuccessModel<TModel>()
        {
            return AsSuccess<TModel>().Model;
        }

        public bool HasValidations()
        {
            return this is ValidationResult;
        }

        public IEnumerable<string> GetValidations()
        {
            return AsValidation().ValidationErrors;
        }

        public bool Failed()
        {
            return this is FailureResult;
        }

        public string GetFailureError()
        {
            return AsFailure().ErrorMessage;
        }

        private SuccessResult<TModel> AsSuccess<TModel>()
        {
            return this as SuccessResult<TModel>;
        }

        private ValidationResult AsValidation()
        {
            return this as ValidationResult;
        }

        private FailureResult AsFailure()
        {
            return this as FailureResult;
        }
    }
}