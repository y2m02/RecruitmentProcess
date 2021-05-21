namespace RecruitmentManagementApp.Models.ViewModels.Base
{
    public class Failure : BaseReturnViewModel
    {
        public Failure(string errorMessage)
        {
            ErrorMessage = $"Hubo un error duranto el proceso\n\n: {errorMessage}";
        }

        public string ErrorMessage { get; }
    }
}
