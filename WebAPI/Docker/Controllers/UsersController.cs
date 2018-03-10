namespace Microsoft.CGC.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.CGC.Comm;

    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserService.UserServiceClient client;

        public UsersController(IUserServiceClient client)
        {
            this.client = client.Client;
        }

        // GET api/users
        [HttpGet(Name = nameof(GetUsers))]
        public JsonResult GetUsers()
        {
            return Json( new List<User> {
                new User() { Name = "user1" },
                new User() { Name = "user2" }
            });
        }

        // GET api/users/a858207a-2d2f-43ba-b94b-c1f97a56bb39
        [HttpGet("{id}")]
        public async Task<User> Get(Guid id)
        {
            var request = new GetUsersRequest()
            {
                UserIds = new List<Guid>() { id }
            };

            var response = await client.GetUsersAsync(request);
            var result = response.Payload.Deserialize();

            return result.Users[0];
        }

        // POST api/values
        [HttpPost]
        public void PostUser([FromBody]string value)
        {
            // Create new user
        }

        // PUT api/values/a858207a-2d2f-43ba-b94b-c1f97a56bb39
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            // Update user
        }

        // DELETE api/values/a858207a-2d2f-43ba-b94b-c1f97a56bb39
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
