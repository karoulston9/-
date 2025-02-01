using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace sin_bin_app_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]  // This ensures Firebase authentication is required
    public abstract class BaseApiController : ControllerBase
    {
        protected string GetCurrentUserId()
        {
            return User.FindFirst("user_id")?.Value ?? string.Empty;
        }
    }
}
