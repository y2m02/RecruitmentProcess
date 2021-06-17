using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request;
using RecruitmentManagementApi.Models.Request.Candidates;
using RecruitmentManagementApi.Models.Request.Logs;
using RecruitmentManagementApi.Services;

namespace RecruitmentManagementApi.Controllers
{
    public class CandidateController : BaseApiController
    {
        private readonly ICandidateService candidateService;

        public CandidateController(
            IAuthorizationKeyService authorizationKeyService,
            ICandidateService candidateService,
            ILogService logService
        ) : base(authorizationKeyService, logService)
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
                async () =>
                {
                    return await ValidateResult(
                        await candidateService.GetAll().ConfigureAwait(false)
                    ).ConfigureAwait(false);
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
                Permission.Write,
                async () =>
                {
                    var logRequest = new LogRequest
                    {
                        RunAt = DateTime.Now,
                        Api = Api.Candidate,
                        Endpoint = nameof(Create),
                        ApiKey = apiKey,
                    };

                    return await ValidateResult(
                        await candidateService.Create(request).ConfigureAwait(false),
                        () => LogService.Create(logRequest)
                    ).ConfigureAwait(false);
                }
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
                async () =>
                {
                    var logRequest = new LogRequest
                    {
                        RunAt = DateTime.Now,
                        Api = Api.Candidate,
                        Endpoint = nameof(Update),
                        ApiKey = apiKey,
                        AffectedEntity = request.Id,
                    };

                    return await ValidateResult(
                        await candidateService.Update(request).ConfigureAwait(false),
                        () => LogService.Create(logRequest)
                    ).ConfigureAwait(false);
                }
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
                async () =>
                {
                    var logRequest = new LogRequest
                    {
                        RunAt = DateTime.Now,
                        Api = Api.Candidate,
                        Endpoint = nameof(Delete),
                        ApiKey = apiKey,
                        AffectedEntity = request.Id,
                    };

                    return await ValidateResult(
                        await candidateService.Delete(request).ConfigureAwait(false),
                        () => LogService.Create(logRequest)
                    ).ConfigureAwait(false);
                }
            ).ConfigureAwait(false);
        }
    }
}
