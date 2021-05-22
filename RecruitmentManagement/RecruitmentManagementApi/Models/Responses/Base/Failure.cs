namespace RecruitmentManagementApi.Models.Responses.Base
{
    public class Failure : BaseResponse
    {
        public Failure(string errorMessage)
        {
            ErrorMessage = $"Hubo un error duranto el proceso\n\n: {errorMessage}";
        }

        public string ErrorMessage { get; }
    }
}
