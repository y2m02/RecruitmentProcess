using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagementApi.Models.Request.Recruitments;
using RecruitmentManagementApi.Models.Responses;
using RecruitmentManagementApi.Services;

namespace RecruitmentManagementApi.Controllers
{
    public class RecruitmentController : BaseApiController
    {
        private readonly IRecruitmentService recruitmentService;

        public RecruitmentController(
            IRecruitmentService recruitmentService
        )
        {
            this.recruitmentService = recruitmentService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetAll()
        {
            var response = await recruitmentService.GetAll<RecruitmentResponse>().ConfigureAwait(false);

            return response.Succeeded()
                ? Ok(response)
                : InternalServerError(response);
        }

        [HttpGet]
        [Route("GetHistoryById/{id}")]
        public async Task<IActionResult> GetHistoryById(int id)
        {
            var response = await recruitmentService.GetHistoryById(id).ConfigureAwait(false);

            return response.Succeeded()
                ? Ok(response)
                : InternalServerError(response);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateRecruitmentRequest request)
        {
            return ValidateResult(await recruitmentService.Update(request).ConfigureAwait(false));
        }
    }
}