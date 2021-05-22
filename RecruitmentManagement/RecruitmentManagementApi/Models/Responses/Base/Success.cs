namespace RecruitmentManagementApi.Models.Responses.Base
{
    public class Success<T> : BaseResponse
    {
        public Success(T model)
        {
            Model = model;
        }

        public T Model { get; }
    }
}
