using Contacts.Services;
using Infrastructure.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : Controller
    {
        private IIdentityService _identityService;
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        [HttpPut("Login")]
        public async Task<ActionResult<string>> Login(LoginUser loginUser)
        {
            return await _identityService.Login(loginUser);
        }
    }
}
