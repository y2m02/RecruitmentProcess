namespace RecruitmentManagementApi.Models.Responses.Base
{
    public class SuccessResult<T> : BaseResult
    {
        public SuccessResult(T model)
        {
            Model = model;
        }

        public T Model { get; }
    }
}
