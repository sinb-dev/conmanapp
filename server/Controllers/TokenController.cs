using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Docker.DotNet;
using Docker.DotNet.Models;
using Conman.Models;

namespace Conman.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        class TokenRequestFormData {
            public string className {get;set;}
            public string name {get;set;}
        }
        private readonly ConmanContext _context;
        public TokenController(ConmanContext context) {
            _context = context;
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody]Token token)
        {
            if (!_context.ValidateUserToken(token.TokenId)) 
                return Ok("invalid");
            else 
                return Ok("verified");
            
        }

        [HttpGet("{requestToken}")]
        public async Task<IActionResult> GetAll(string requestToken)
        {
            if (!_context.ValidateUserToken(requestToken)) return this.Unauthorized();
            return Ok(_context.Tokens.ToList());
        }


        [HttpPost("request")]
        //New user requesting a new token
        public async Task<IActionResult> RequestNewToken([FromBody] Token t )
        {
            if (string.IsNullOrEmpty(t.Owner) ) return this.Problem("Missing owner");
            if (string.IsNullOrEmpty(t.Class) ) return this.Problem("Missing class");
            var token = Token.CreateRequest(t.Owner,t.Class);
            
            _context.Add(token);
            await _context.SaveChangesAsync();

            return Ok(token);
        }
        [HttpPost("{requestToken}/create")]
        [HttpGet("{requestToken}/create")]
        public async Task<ActionResult<Token>> CreateToken(Token token ,string requestToken )
        {
            return Problem("Must create either an user or an admin using [apikey]/create/admin or [apikey]/create/user ");
        }
        [HttpPost("{requestToken}/create/user")]
        public async Task<ActionResult<Token>> CreateUserToken(string requestToken,[FromForm] Token t )
        {
            if (!_context.ValidateAdminToken(requestToken)) return this.Unauthorized();
            
            if (string.IsNullOrEmpty(t.Owner) ) return this.Problem("Missing owner");
            if (string.IsNullOrEmpty(t.Class) ) return this.Problem("Missing class");
            var token = Token.CreateUser(t.Owner,t.Class);

            _context.Add(token);
            await _context.SaveChangesAsync();

            return Ok(token);
        }

        [HttpPost("{requestToken}/create/admin")]
        public async Task<ActionResult<Token>> CreateAdminToken(string requestToken,[FromForm] Token t )
        {
            if (!_context.ValidateAdminToken(requestToken)) return this.Unauthorized();
            
            if (string.IsNullOrEmpty(t.Owner) ) return this.Problem("Missing owner");
            var token = Token.CreateAdmin(t.Owner);
            
            _context.Add(token);
            await _context.SaveChangesAsync();

            return Ok(token);
        }
                
        [HttpGet("{requestToken}/remove/{userToken}")]
        public async Task<ActionResult<Token>> CreateAdminToken(string requestToken, string userToken)
        {
            if (!_context.ValidateAdminToken(requestToken)) return this.Unauthorized();
            
            var result = _context.Tokens.Where( a => a.TokenId == userToken).ToList();
            if (result.Count == 0) {
                return Ok("No such key");
            }

            _context.Tokens.Remove(result[0]);

            await _context.SaveChangesAsync();

            return Ok("removed");
        }
    }
}
