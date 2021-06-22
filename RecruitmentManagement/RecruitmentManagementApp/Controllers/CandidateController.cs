using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagementApp.Client;
using RecruitmentManagementApp.Models.Requests;
using RecruitmentManagementApp.Models.ViewModels;

namespace RecruitmentManagementApp.Controllers
{
    public class CandidateController : Controller
    {
        private readonly IApiClient client;

        public CandidateController(IApiClient client)
        {
            this.client = client;
        }

        public IActionResult Index() => View();

        public async Task<JsonResult> GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var response = await client
                .Get<List<CandidateViewModel>>("Candidate/Get")
                .ConfigureAwait(false);

            return Json(await response.ToDataSourceResultAsync(request));
        }

        [HttpPost]
        public async Task<JsonResult> Upsert(UpsertCandidateRequest request)
        {
            if (request.IsUpdate())
            {
                var updated = await client
                    .Put<CandidateViewModel>(resource: "Candidate/Update", body: request)
                    .ConfigureAwait(false);

                return Json(new { isUpdate = true, data = updated });
            }

            var created = await client
                .Post<CandidateViewModel>(resource: "Candidate/Create", body: request)
                .ConfigureAwait(false);

             return Json(new { isUpdate = false, data = created });;
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(int id)
        {
            var response = await client
                .Delete<string>(
                    resource: "Candidate/Delete",
                    body: new { Id = id }
                )
                .ConfigureAwait(false);

            return Json(response);
        }
    }
}