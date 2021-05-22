namespace RecruitmentManagementApi.Models.Responses.Base
{
    public class FailureResult : BaseResult
    {
        public FailureResult(string errorMessage)
        {
            ErrorMessage = $"Hubo un error duranto el proceso:\n\n {errorMessage}";
        }

        public string ErrorMessage { get; }
    }
}
