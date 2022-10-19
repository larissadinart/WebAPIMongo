using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAPIMongo.Models;
using WebAPIMongo.Services;

namespace WebAPIMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AddressService _addressService;
        public AddressController(AddressService addressService)
        {
            _addressService = addressService;
        }
        [HttpGet("{id:length(24)}",Name = "GetAddress")]
        public ActionResult<List<Address>> Get() => _addressService.Get();

        [HttpGet]
        public ActionResult<Address> Get(string id)
        {
            var address = _addressService.Get(id);

            if(address == null)
            {
                return NotFound();
            }
            return Ok(address);

        }
        [HttpPost]
        public ActionResult<Address> Creat(Address address)
        {
            _addressService.Create(address);
            return CreatedAtRoute("GetClient", new { id = address.Id.ToString() }, address);
        }
        [HttpPut]
        public ActionResult<Address> Update(string id, Address address)
        {
            var Address = _addressService.Get(id);
            if (Address == null)
            {
                return NotFound();
            }
            _addressService.Update(id, address);
            Address = _addressService.Get(id);
            return NoContent();
        }
        [HttpDelete]
        public ActionResult Delete(string id)
        {
            var client = _addressService.Get(id);
            if (client == null)
            {
                return NotFound();
            }
            _addressService.Remove(client);
            return NoContent();
        }




    }
}
