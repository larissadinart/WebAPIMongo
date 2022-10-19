using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIMongo.Models;
using WebAPIMongo.Services;

namespace WebAPIMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientServices _clientServices;

        public ClientController(ClientServices clientServices)
        {
            _clientServices = clientServices;
        }

        [HttpGet]
        public ActionResult<List<Client>> Get() => _clientServices.Get();

        //obrigatoriedade do objeto ter um haschcode de 24 caracteres
        [HttpGet("{id:length(24)}", Name = "GetClient")]
        public ActionResult<Client>Get(string id)
        {
            var client = _clientServices.Get(id);

            if(client == null)
            {
                return NotFound();
            }
            return client;
        }

        [HttpPost]
        public ActionResult<Client> Create(Client client)
        {
            _clientServices.Create(client);
            return CreatedAtRoute("GetClient", new {Id = client.Id.ToString()}, client);
        }

        [HttpPut]
        public ActionResult<Client>Update(Client clientIn, string id)
        {
            var cl = _clientServices.Get(id);
            if (cl == null)
                return NotFound();

            _clientServices.Update(clientIn, id);
            cl = _clientServices.Get(id);

            return CreatedAtRoute("GetClient", new { Id = cl.Id.ToString() }, cl);
            //return Ok(cl);
        }
        [HttpDelete]
        public ActionResult Delete(string id)
        {
            var client = _clientServices.Get(id);

            if (client == null)
                return NotFound();

            _clientServices.Remove(client);
            return NoContent();
        }
    }
}
