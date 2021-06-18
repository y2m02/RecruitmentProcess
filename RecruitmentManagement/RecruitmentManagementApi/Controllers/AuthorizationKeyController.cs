using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagementApi.Models.Enums;
using RecruitmentManagementApi.Models.Request.AuthorizationKey;
using RecruitmentManagementApi.Services;

namespace RecruitmentManagementApi.Controllers
{
    public class AuthorizationKeyController : BaseApiController
    {
        private readonly IAuthorizationKeyService authorizationKeyService;

        public AuthorizationKeyController(
            IAuthorizationKeyService authorizationKeyService
        ) : base(authorizationKeyService)
        {
            this.authorizationKeyService = authorizationKeyService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetAll([FromHeader] string apiKey)
        {
            return await ValidateApiKey(
                apiKey,
                Permission.FullAccess,
                async () => ValidateResult(await authorizationKeyService.GetAll().ConfigureAwait(false))
            ).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(
            [FromHeader] string apiKey,
            AuthorizationKeyRequest request
        )
        {
            return await ValidateApiKey(
                apiKey,
                Permission.FullAccess,
                async () => ValidateResult(await authorizationKeyService.Create(request).ConfigureAwait(false))
            ).ConfigureAwait(false);
        }

        [HttpPut]
        [Route("Activate")]
        public async Task<IActionResult> Activate(
            [FromHeader] string apiKey,
            UpdateAuthorizationKeyStatusRequest request
        )
        {
            return await ValidateApiKey(
                apiKey,
                Permission.FullAccess,
                async () => ValidateResult(await authorizationKeyService.Activate(request).ConfigureAwait(false))
            ).ConfigureAwait(false);
        }

        [HttpPut]
        [Route("Deactivate")]
        public async Task<IActionResult> Deactivate(
            [FromHeader] string apiKey,
            UpdateAuthorizationKeyStatusRequest request
        )
        {
            return await ValidateApiKey(
                apiKey,
                Permission.FullAccess,
                async () => ValidateResult(await authorizationKeyService.Deactivate(request).ConfigureAwait(false))
            ).ConfigureAwait(false);
        }

        [HttpPut]
        [Route("UpdatePermissions")]
        public async Task<IActionResult> UpdatePermissions(
            [FromHeader] string apiKey,
            UpdateAuthorizationKeyPermissionsRequest request
        )
        {
            return await ValidateApiKey(
                apiKey,
                Permission.FullAccess,
                async () => ValidateResult(await authorizationKeyService.UpdatePermissions(request).ConfigureAwait(false))
            ).ConfigureAwait(false);
        }
    }
}