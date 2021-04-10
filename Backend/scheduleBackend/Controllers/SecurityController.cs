using Microsoft.AspNetCore.Mvc;
using scheduleBackend.Models.ClientResponse;
using scheduleBackend.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace scheduleBackend.Controllers
{

    [ApiController]
    [Route("api/")]
    public class SecurityController : ControllerBase
    {

        private ISecurityService _Service;

        public SecurityController( ISecurityService s )
        {
            _Service = s;
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyToken( [FromBody] GoogleAuthResponse res )
        {
            /*Payload payload;
            try
            {
                payload = await ValidateAsync(res.TokenId, new ValidationSettings
                {
                    Audience = new[] { "1029632734628-0hskb0pputp0bal8p8v7dif1iigfk3uo.apps.googleusercontent.com" }
                });
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest( e );
            }*/
            if(res != null)
            {
              InnerToken result =  await _Service.CheckLoginGoogle(res);
                if (result.isValid)
                {
                    return Ok(new { token = result.Token });
                }
                else
                {
                    return BadRequest( new { Errors= result.Error  });
                }
            }
            return BadRequest( new {Error = "Check the format" } ); 
            
        }

    }
}
