using Contacts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SeederController : Controller
    {
        private IContactService _contactService;
        public SeederController(IContactService contactService)
        {
            _contactService = contactService;
        }
        [HttpGet]
        public IActionResult Seed()
        {
            _contactService.SeedData();
            return Ok();
        }
    }
}
