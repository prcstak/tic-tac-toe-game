using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace account.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    internal string UserId => !User.Identity.IsAuthenticated
        ? string.Empty
        : (User.FindFirst(ClaimTypes.NameIdentifier).Value);
}