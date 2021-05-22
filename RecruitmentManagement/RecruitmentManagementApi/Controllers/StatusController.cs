using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<BaseResponse> GetAll()
        {
            var baseResponse = await statusService.GetAll<StatusResponse>();

            return baseResponse;
        }

        //[HttpGet]
        //[Route("Get/{id}")]
        //public async Task<StatusResponse> GetById(int id)
        //{
        //    return Mapper.Map<StatusResponse>(await _statusRepository.GetById(id));
        //}

        //[HttpPost]
        //[Route("Create")]
        //public async Task Create(StatusRequest request)
        //{
        //    await _statusRepository.Create(Mapper.Map<Status>(request));
        //}

        //[HttpPost]
        //[Route("BatchCreate")]
        //public async Task BatchCreate(IEnumerable<StatusRequest> request)
        //{
        //    await _statusRepository.BatchCreate(Mapper.Map<IEnumerable<Status>>(request));
        //}

        //[HttpPut]
        //[Route("Update")]
        //public async Task Update(UpdateStatusRequest request)
        //{
        //    await _statusRepository.Update(Mapper.Map<Status>(request));
        //}

        //[HttpPut]
        //[Route("BatchUpdate")]
        //public async Task BatchUpdate(IEnumerable<UpdateStatusRequest> request)
        //{
        //    await _statusRepository.BatchUpdate(Mapper.Map<IEnumerable<Status>>(request));
        //}

        //[HttpDelete]
        //[Route("Delete/{id}")]
        //public async Task Delete(int id)
        //{
        //    await _statusRepository.Delete(Mapper.Map<Status>(new DeleteStatusRequest(id)));
        //}
    }
}