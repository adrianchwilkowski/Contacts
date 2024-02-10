//using Contacts.Services;
//using Infrastructure.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace Contacts.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class WeatherForecastController : ControllerBase
//    {
//        private IContactService _contactService;
//        public WeatherForecastController(IContactService contactService)
//        {
//            _contactService = contactService;
//        }
//        [HttpPost(Name = "Add")]
//        public ActionResult<Guid> Add([FromBody] ContactCommand command)
//        {
//            var result = _contactService.Add(command);
//            return Ok(result);
//        }
//        [HttpDelete(Name = "Delete")]
//        public IActionResult Delete(Guid id)
//        {
//            _contactService.Delete(id);
//            return Ok();
//        }
//        [HttpGet(Name = "Get")]
//        public ActionResult<Contact> Get(Guid id)
//        {
//            var result = _contactService.Get(id);
//            return Ok(result);
//        }
//        [HttpGet(Name = "GetList")]
//        public ActionResult<List<Contact>> GetList()
//        {
//            var result = _contactService.GetList();
//            return Ok(result);
//        }
//        [HttpPut(Name = "Update")]
//        public IActionResult Update([FromBody] Contact command)
//        {
//            var result = _contactService.Update(command);
//            return Ok();
//        }
//    }
//}