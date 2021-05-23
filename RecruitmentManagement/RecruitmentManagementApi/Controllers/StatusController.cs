﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagementApi.Models.Request.Statuses;
using RecruitmentManagementApi.Models.Responses;
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
            var result = await statusService.Create(request).ConfigureAwait(false);

            if (result.HasValidations())
            {
                return BadRequest(result);
            }

            return result.IsSuccess()
                ? Ok(result)
                : InternalServerError(result);
        }

        [HttpPost]
        [Route("BatchCreate")]
        public async Task<IActionResult> BatchCreate(IEnumerable<StatusRequest> requests)
        {
            var result = await statusService.BatchCreate(requests).ConfigureAwait(false);

            if (result.IsPartialSuccess() || result.IsSuccess())
            {
                return Ok(result);
            }

            return result.HasValidations()
                ? BadRequest(result)
                : InternalServerError(result);
        }

        
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateStatusRequest request)
        {
            var result = await statusService.Update(request).ConfigureAwait(false);

            if (result.HasValidations())
            {
                return BadRequest(result);
            }

            return result.IsSuccess()
                ? Ok(result)
                : InternalServerError(result);
        }

        //[HttpDelete]
        //[Route("Delete/{id}")]
        //public async Task Delete(int id)
        //{
        //    await _statusRepository.Delete(Mapper.Map<Status>(new DeleteStatusRequest(id)));
        //}
    }
}