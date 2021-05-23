using System.Net;
using Microsoft.AspNetCore.Mvc;
using RecruitmentManagementApi.Models.Responses.Base;

namespace RecruitmentManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : Controller
    {
        protected ObjectResult InternalServerError(object value)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, value);
        }

        protected ActionResult ValidateResult(Result result)
        {
            if (result.IsSuccess())
            {
                return Ok(result);
            }

            return result.HasValidations()
                ? BadRequest(result)
                : InternalServerError(result);
        }
    }
}