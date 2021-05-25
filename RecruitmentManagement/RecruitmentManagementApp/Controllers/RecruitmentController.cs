using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using RecruitmentManagementApp.Client;
using RecruitmentManagementApp.Models.Requests;
using RecruitmentManagementApp.Models.ViewModels;

namespace RecruitmentManagementApp.Controllers
{
    public class RecruitmentController : Controller
    {
        private readonly IApiClient client;

        public RecruitmentController(IApiClient client)
        {
            this.client = client;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var response = await client
                .Get<List<RecruitmentViewModel>>("Recruitment/Get")
                .ConfigureAwait(false);

            return Json(await response.ToDataSourceResultAsync(request));
        }

        public async Task<IActionResult> GetHistoryByRecruitmentId(
            int recruitmentId,
            [DataSourceRequest] DataSourceRequest request
        )
        {
            var response = await client
                .Get<List<RecruitmentViewModel>>("Recruitment/Get")
                .ConfigureAwait(false);

            var history = response
                .SelectMany(x => x.RecruitmentUpdateHistories)
                .Where(x => x.RecruitmentId == recruitmentId);

            return Json(await history.ToDataSourceResultAsync(request));
        }

        [HttpPut]
        public async Task<JsonResult> Update(UpdateRecruitmentRequest request)
        {
            var response = await client
                .Put<string>(resource: "Recruitment/Update", body: request)
                .ConfigureAwait(false);

            return Json(response);
        }
    }
}
