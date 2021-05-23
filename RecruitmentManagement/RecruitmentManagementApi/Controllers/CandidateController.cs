using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagementApi.Models.Request.Candidates;
using RecruitmentManagementApi.Models.Responses;
using RecruitmentManagementApi.Services;

namespace RecruitmentManagementApi.Controllers
{
    public class CandidateController : BaseApiController
    {
        private readonly ICandidateService candidateService;

        public CandidateController(
            ICandidateService candidateService
        )
        {
            this.candidateService = candidateService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetAll()
        {
            var response = await candidateService.GetAll<CandidateResponse>().ConfigureAwait(false);

            return response.IsSuccess()
                ? Ok(response)
                : InternalServerError(response);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CandidateRequest request)
        {
            return ValidateResult(await candidateService.Create(request).ConfigureAwait(false));
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateCandidateRequest request)
        {
            return ValidateResult(await candidateService.Update(request).ConfigureAwait(false));
        }
    }
}