namespace RecruitmentManagementApi.Models.Responses.Base
{
    public class Success<T> : ResponseType
    {
        public Success(T model)
        {
            Model = model;
        }

        public T Model { get; }
    }
}
