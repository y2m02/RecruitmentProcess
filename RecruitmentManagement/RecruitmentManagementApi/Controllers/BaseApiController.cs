using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagementApi.Models.Responses.Base;
using RecruitmentManagementApi.Services;

namespace RecruitmentManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : Controller
    {
        private readonly IAuthorizationKeyService authorizationKeyService;

        public BaseApiController(IAuthorizationKeyService authorizationKeyService)
        {
            this.authorizationKeyService = authorizationKeyService;
        }

        protected ObjectResult InternalServerError(object value)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, value);
        }

        protected ActionResult ValidateResult(Result result)
        {
            if (result.Succeeded())
            {
                return Ok(result);
            }

            return result.HasValidations()
                ? BadRequest(result)
                : InternalServerError(result);
        }



        protected async Task<IActionResult> ValidateRequest(string apiKey, Func<Task<IActionResult>> executor)
        {
            if (!await authorizationKeyService.Exists(apiKey).ConfigureAwait(false))
            {
                return Unauthorized(
                    new { error = $"The key '{apiKey}' is invalid for this API" }
                );
            }

            return await executor().ConfigureAwait(false);
        }
    }
}