namespace RecruitmentManagementApp.Models.Requests
{
    public class BaseRequest
    {
        public int Id { get; set; }

        public bool IsUpdate() => Id > 0;
    }
}