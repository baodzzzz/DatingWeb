using System;
using API.Data;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        public BuggyController(DatingAppContext context) : base (context)
        {
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret(){
            return "secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<string> GetNotFound(){
            var thing = _context.Users.Find(-1);

            if (thing == null) return NotFound();

            return Ok(thing);
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError(){
            try{
                var thing = _context.Users.Find(-1);

                var thingToReturn = thing.ToString();

                return thingToReturn;
            } catch (Exception ){
                return StatusCode(500, "Computer says no!");
            }
            
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest(){
            return BadRequest("This was not a good request");
        }
    }
}