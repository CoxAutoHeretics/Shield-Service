using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ShieldService.Models;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Shield_Service.Tools;

namespace ShieldService.Controllers
{
    public class CharacterController : ApiController
    {
        private List<Character> _cache = MarvelClient.GetAllCharacters();
        
        public CharacterController()
        {
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
                id = id,
                name = "Wolverine",
                thumbnail = "https://www.google.com/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png",
                description = "This guy's bad news for anyone with a vowel in their name."
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
