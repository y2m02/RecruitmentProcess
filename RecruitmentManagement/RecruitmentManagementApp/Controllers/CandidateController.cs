using System.Collections.Generic;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagementApp.Client;
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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var response = await client
                .Get<List<CandidateViewModel>>("Candidate/Get")
                .ConfigureAwait(false);

            return Json(await response.ToDataSourceResultAsync(request));
        }
    }
}
