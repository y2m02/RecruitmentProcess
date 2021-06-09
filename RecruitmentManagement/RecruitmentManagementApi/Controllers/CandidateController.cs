using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request;
using RecruitmentManagementApi.Models.Request.Candidates;
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
                Permission.Read,
                async () => ValidateResult(await candidateService.GetAll().ConfigureAwait(false))
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
                Permission.Write,
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
                Permission.Write,
                async () => ValidateResult(await candidateService.Update(request).ConfigureAwait(false))
            ).ConfigureAwait(false);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(
            [FromHeader] string apiKey,
            DeleteRequest request
        )
        {
            return await ValidateApiKey(
                apiKey,
                Permission.Delete,
                async () => ValidateResult(await candidateService.Delete(request).ConfigureAwait(false))
            ).ConfigureAwait(false);
        }
    }
}
