using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using ModelLayer;
using BusinessLogicLayer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly BusinessLogicClass _businessLogicClass;
        public BuggyController(BusinessLogicClass businessLogicClass)
        {
            _businessLogicClass = businessLogicClass;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }

        [HttpGet("not-found")]
        public async Task<ActionResult<string>> GetNotFound()
        {
            var thing = await _businessLogicClass.GetUser(-1);

            if (thing == null) return NotFound();

            return Ok(thing);
        }

        [HttpGet("server-error")]
        public async Task<ActionResult<string>> GetServerError()
        {
            var thing = await _businessLogicClass.GetUser(-1);

            var thingToReturn = thing.ToString();

            return thingToReturn;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest();
        }
    }
}