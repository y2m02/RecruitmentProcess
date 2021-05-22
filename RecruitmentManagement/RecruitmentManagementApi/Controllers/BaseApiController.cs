using System.Net;
using Microsoft.AspNetCore.Mvc;

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
    }
}