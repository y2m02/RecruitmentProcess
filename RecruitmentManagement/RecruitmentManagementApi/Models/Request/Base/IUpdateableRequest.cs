namespace RecruitmentManagementApi.Models.Request.Base
{
    public interface IUpdateableRequest : IRequest
    {
        public int Id { get; set; }
    }
}