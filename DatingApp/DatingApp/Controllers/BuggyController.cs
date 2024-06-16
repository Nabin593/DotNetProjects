using DatingApp.Data;
using DatingApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;

        public BuggyController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }

        [HttpGet("Not-Found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var things = _context.Users.Find(-1);
            if (things == null) { return NotFound(); }
            return things;

        }

        [HttpGet("Server-error")]
        public ActionResult<string> GetServerError()
        {
            try
            {
                var things = _context.Users.Find(-1);
                var thingToReturn = things.ToString();
                return thingToReturn;
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Computer says no!");
            }
        }

        [HttpGet("Bad-request")]
        public ActionResult<string> GetBadrequests()
        {
            return "This was not a good request";
        }
    }
}
