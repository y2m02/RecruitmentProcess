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
            IAuthorizationKeyService authorizationKeyService,
            ICandidateService candidateService
        ) : base(authorizationKeyService)
        {
            this.candidateService = candidateService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetAll([FromHeader] string apiKey)
        {
            return await ValidateApiKey(
                apiKey,
                async () =>
                {
                    var response = await candidateService.GetAll<CandidateResponse>().ConfigureAwait(false);

                    return response.Succeeded()
                        ? Ok(response)
                        : InternalServerError(response);
                }
            ).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(
            [FromHeader] string apiKey,
            CandidateRequest request
        )
        {
            return await ValidateApiKey(
                apiKey,
                async () => ValidateResult(await candidateService.Create(request).ConfigureAwait(false))
            ).ConfigureAwait(false);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(
            [FromHeader] string apiKey,
            UpdateCandidateRequest request
        )
        {
            return await ValidateApiKey(
                apiKey,
                async () => ValidateResult(await candidateService.Update(request).ConfigureAwait(false))
            ).ConfigureAwait(false);
        }
    }
}
