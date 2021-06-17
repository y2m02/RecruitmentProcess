using System;
using System.Net;
using System.Threading.Tasks;
using HelpersLibrary.Extensions;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagementApi.Models;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request.Base;
using RecruitmentManagementApi.Models.Request.Logs;
using RecruitmentManagementApi.Models.Responses;
using RecruitmentManagementApi.Models.Responses.Base;
using RecruitmentManagementApi.Services;

namespace RecruitmentManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : Controller
    {
        private readonly IAuthorizationKeyService authorizationKeyService;
        protected readonly ILogService LogService;

        public BaseApiController(
            IAuthorizationKeyService authorizationKeyService,
            ILogService logService
        )
        {
            this.authorizationKeyService = authorizationKeyService;
            this.LogService = logService;
        }

        protected ObjectResult InternalServerError(object value)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, value);
        }

        protected async Task<IActionResult> ValidateResult(
            Result result,
            Func<Task<Result>> executor = null
        )
        {
            if (result.Succeeded())
            {
                if (executor is not null)
                {
                    await executor().ConfigureAwait(false);
                }

                return Ok(result);
            }

            return result.HasValidations()
                ? BadRequest(result)
                : InternalServerError(result);
        }

        protected async Task<IActionResult> ValidateApiKey(
            string apiKey,
            Permission permission,
            Func<Task<IActionResult>> executor
        )
        {
            if (apiKey.IsEmpty())
            {
                return Unauthorized(new { error = ConsumerMessages.ApiKeyRequired });
            }

            var result = await authorizationKeyService.Get(apiKey).ConfigureAwait(false);

            if (result.Failed())
            {
                return InternalServerError(result);
            }

            if (result.Response is not AuthorizationKeyResponse authorizationKey)
            {
                return Unauthorized(new { error = ConsumerMessages.InvalidApiKey.Format(apiKey) });
            }

            var isAuthorized = permission switch
            {
                Permission.FullAccess => authorizationKey.HasFullAccess(),
                Permission.Read => authorizationKey.CanRead(),
                Permission.Write => authorizationKey.CanWrite(),
                Permission.Delete => authorizationKey.CanDelete(),
            };

            if (!isAuthorized)
            {
                return Unauthorized(new { error = ConsumerMessages.NotAllowedForApiKey.Format(apiKey) });
            }

            return await executor().ConfigureAwait(false);
        }
    }
}