using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagementApi.Models.Request.Statuses;
using RecruitmentManagementApi.Models.Responses;
using RecruitmentManagementApi.Models.Responses.Base;
using RecruitmentManagementApi.Services;

namespace RecruitmentManagementApi.Controllers
{
    public class StatusController : BaseApiController
    {
        private readonly IStatusService statusService;

        public StatusController(
            IStatusService statusService
        )
        {
            this.statusService = statusService;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetAll()
        {
            var response = await statusService.GetAll<StatusResponse>().ConfigureAwait(false);

            return response.IsSuccess()
                ? Ok(response)
                : InternalServerError(response);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(StatusRequest request)
        {
            return base.ValidateResult(await statusService.Create(request).ConfigureAwait(false));
        }

        [HttpPost]
        [Route("BatchCreate")]
        public async Task<IActionResult> BatchCreate(IEnumerable<StatusRequest> requests)
        {
            return ValidateResult(await statusService.BatchCreate(requests).ConfigureAwait(false));
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateStatusRequest request)
        {
            return base.ValidateResult(await statusService.Update(request).ConfigureAwait(false));
        }

        [HttpPut]
        [Route("BatchUpdate")]
        public async Task<IActionResult> BatchUpdate(IEnumerable<UpdateStatusRequest> requests)
        {
            return ValidateResult(await statusService.BatchUpdate(requests).ConfigureAwait(false));
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(DeleteStatusRequest request)
        {
            return base.ValidateResult(await statusService.Delete(request).ConfigureAwait(false));
        }

        [HttpDelete]
        [Route("BatchDelete")]
        public async Task<IActionResult> BatchDelete(IEnumerable<DeleteStatusRequest> requests)
        {
            return ValidateResult(await statusService.BatchDelete(requests).ConfigureAwait(false));
        }

        private new IActionResult ValidateResult(Result result)
        {
            if (result.IsPartialSuccess() || result.IsSuccess())
            {
                return Ok(result);
            }

            return result.HasValidations()
                ? BadRequest(result)
                : InternalServerError(result);
        }
    }
}