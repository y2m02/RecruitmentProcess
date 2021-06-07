using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request.Recruitments;
using RecruitmentManagementApi.Models.Responses;
using RecruitmentManagementApi.Services;

namespace RecruitmentManagementApi.Controllers
{
    public class RecruitmentController : BaseApiController
    {
        private readonly IRecruitmentService recruitmentService;

        public RecruitmentController(
            IAuthorizationKeyService authorizationKeyService,
            IRecruitmentService recruitmentService
        ) : base(authorizationKeyService)
        {
            this.recruitmentService = recruitmentService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetAll([FromHeader] string apiKey)
        {
            return await ValidateApiKey(
                apiKey,
                Permission.Read,
                async () => ValidateResult(
                    await recruitmentService.GetAll<RecruitmentResponse>().ConfigureAwait(false)
                )
            ).ConfigureAwait(false);
        }

        [HttpGet]
        [Route("GetHistoryById/{id}")]
        public async Task<IActionResult> GetHistoryById([FromHeader] string apiKey, int id)
        {
            return await ValidateApiKey(
                apiKey,
                Permission.Read,
                async () => ValidateResult(await recruitmentService.GetHistoryById(id).ConfigureAwait(false))
            ).ConfigureAwait(false);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(
            [FromHeader] string apiKey,
            UpdateRecruitmentRequest request
        )
        {
            return await ValidateApiKey(
                apiKey,
                Permission.Write,
                async () => ValidateResult(await recruitmentService.Update(request).ConfigureAwait(false))
            );
        }
    }
}
