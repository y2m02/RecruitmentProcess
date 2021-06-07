using System;
using System.Net;
using System.Threading.Tasks;
using HelpersLibrary.Extensions;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagementApi.Models;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Responses;
using RecruitmentManagementApi.Models.Responses.Base;
using RecruitmentManagementApi.Services;

namespace RecruitmentManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : Controller
    {
        protected readonly IAuthorizationKeyService AuthorizationKeyService;

        public BaseApiController(IAuthorizationKeyService authorizationKeyService)
        {
            AuthorizationKeyService = authorizationKeyService;
        }

        protected ObjectResult InternalServerError(object value)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, value);
        }

        protected IActionResult ValidateResult(Result result)
        {
            if (result.Succeeded())
            {
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

            var result = await AuthorizationKeyService.Get(apiKey).ConfigureAwait(false);

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