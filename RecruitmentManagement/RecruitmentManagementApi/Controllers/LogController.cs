using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request.Logs;
using RecruitmentManagementApi.Services;

namespace RecruitmentManagementApi.Controllers
{
    public class LogController : BaseApiController
    {
        public LogController(
            IAuthorizationKeyService authorizationKeyService,
            ILogService logService
        ) : base(authorizationKeyService, logService) { }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetAll([FromHeader] string apiKey)
        {
            return await ValidateApiKey(
                apiKey,
                Permission.FullAccess,
                async () =>
                {
                    return await ValidateResult(
                        await LogService.GetAll().ConfigureAwait(false)
                    ).ConfigureAwait(false);
                }
            ).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(
            [FromHeader] string apiKey,
            LogRequest request
        )
        {
            return await ValidateApiKey(
                apiKey,
                Permission.FullAccess,
                async () =>
                {
                    return await ValidateResult(
                        await LogService.Create(request).ConfigureAwait(false)
                    ).ConfigureAwait(false);
                }
            ).ConfigureAwait(false);
        }
    }
}