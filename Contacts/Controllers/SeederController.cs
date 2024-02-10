using Contacts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeederController : Controller
    {
        private IContactService _contactService;
        private IIdentityService _identityService;
        public SeederController(IContactService contactService, IIdentityService identityService)
        {
            _contactService = contactService;
            _identityService = identityService;
        }
        [HttpGet("SeedContacts")]
        public async Task<IActionResult> SeedContacts()
        {
            await _contactService.SeedData();
            return Ok();
        }
        [HttpGet("SeedUser")]
        public async Task<IActionResult> SeedUser()
        {
            await _identityService.SeedUser();
            return Ok();
        }
    }
}
