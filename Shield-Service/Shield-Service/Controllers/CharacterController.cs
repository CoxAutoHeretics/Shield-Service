using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shield_Service.Models;

namespace Shield_Service.Controllers
{
    public class CharacterController : ApiController
    {
        private HttpClient _client;

        public CharacterController()
        {
            _client = new HttpClient();
            _client.MaxResponseContentBufferSize = 524288;    // bytes
        }

        // GET: api/Character
        public IEnumerable<string> Get()
        {
            return new string[] { "Character1", "Character2" };
        }

        // GET: api/Character/5
        public Character Get(int id)
        {
            Character c = new Character()
            {
                ID = id,
                Name = "Wolverine",
                BirthDate = DateTime.Parse("11-29-1897")
            };
            return c;
        }

        // POST: api/Character
        public void Post([FromBody]Character value)
        {
        }

        // PUT: api/Character/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Character/5
        public void Delete(int id)
        {
        }
    }
}
