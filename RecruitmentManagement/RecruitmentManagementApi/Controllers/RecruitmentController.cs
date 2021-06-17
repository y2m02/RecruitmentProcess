using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request.Logs;
using RecruitmentManagementApi.Models.Request.Recruitments;
using RecruitmentManagementApi.Services;

namespace RecruitmentManagementApi.Controllers
{
    public class RecruitmentController : BaseApiController
    {
        private readonly IRecruitmentService recruitmentService;

        public RecruitmentController(
            IAuthorizationKeyService authorizationKeyService,
            IRecruitmentService recruitmentService,
            ILogService logService
        ) : base(authorizationKeyService, logService)
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
                async () =>
                {
                    return await ValidateResult(
                        await recruitmentService.GetAll().ConfigureAwait(false)
                    ).ConfigureAwait(false);
                }
            ).ConfigureAwait(false);
        }

        [HttpGet]
        [Route("GetHistoryById/{id}")]
        public async Task<IActionResult> GetHistoryById([FromHeader] string apiKey, int id)
        {
            return await ValidateApiKey(
                apiKey,
                Permission.Read,
                async () =>
                {
                    return await ValidateResult(
                        await recruitmentService.GetHistoryById(id).ConfigureAwait(false)
                    ).ConfigureAwait(false);
                }
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
                async () =>
                {
                    var logRequest = new LogRequest
                    {
                        RunAt = DateTime.Now,
                        Api = Api.Recruitment,
                        Endpoint = nameof(Update),
                        ApiKey = apiKey,
                        AffectedEntity = request.Id,
                    };

                    return await ValidateResult(
                        await recruitmentService.Update(request).ConfigureAwait(false),
                        () => LogService.Create(logRequest)
                    ).ConfigureAwait(false);
                }
            );
        }
    }
}
