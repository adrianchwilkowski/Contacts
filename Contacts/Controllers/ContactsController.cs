using Contacts.Services;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : Controller
    {
        private IContactService _contactService;
        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }


        [HttpPost("Add", Name = "Add")]
        public async Task<ActionResult<Guid>> Add([FromBody] ContactCommand command)
        {
            var result = await _contactService.Add(command);
            return Ok(result);
        }
        [HttpDelete("Delete/{id}", Name = "Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _contactService.Delete(id);
            return Ok();
        }
        [HttpGet("Get/{id}", Name = "Get")]
        public async Task<ActionResult<Contact>> Get(Guid id)
        {
            var result = await _contactService.Get(id);
            return Ok(result);
        }
        [HttpGet("GetList", Name = "GetList")]
        public async Task<ActionResult<List<Contact>>> GetList()
        {
            var result = await _contactService.GetList();
            return Ok(result);
        }
        [HttpPut("Update", Name = "Update")]
        public async Task<IActionResult> Update([FromBody] Contact command)
        {
            await _contactService.Update(command);
            return Ok();
        }
    }
}
