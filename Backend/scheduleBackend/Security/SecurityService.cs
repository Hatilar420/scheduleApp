using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using scheduleBackend.Models.ClientResponse;
using scheduleBackend.Models.Database;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace scheduleBackend.Security
{
    public class SecurityService:ISecurityService
    {

        private readonly IConfiguration Configuration;
        private UserManager<ApplicationUser> _UserManager;

        public SecurityService(IConfiguration c , UserManager<ApplicationUser> u)
        {
            _UserManager = u;
            Configuration = c;
        }


        public async Task<InnerToken> CheckLoginGoogle( GoogleAuthResponse res)
        {
            Payload payload;
            try
            {

                //If there is no exception , that means the token is authenticated

                payload = await ValidateAsync(res.TokenId, new ValidationSettings
                {
                    Audience = new[] { Configuration["ClientSecret"] }
                });

                ApplicationUser user = await _UserManager.FindByEmailAsync(res.ProfileObj.Email);
    
                
                //If the user exists , then return a normal token
                if(user != null)
                {
                    string jwtToken = GetAuthToken(user.UserName,user.Id,user.Email);
                    return new InnerToken { isValid = true, Token = jwtToken };
                }

                bool re = await CreateUser(res);

                //If it doesn't create 
                if (re)
                {
                    ApplicationUser Createduser = await _UserManager.FindByEmailAsync(res.ProfileObj.Email);
                    string jwtToken = GetAuthToken(Createduser.UserName,Createduser.Id,Createduser.Email);
                    return new InnerToken { isValid = true, Token = jwtToken };
                }
                return new InnerToken { isValid = false, Error = new string[]{"Sorry token couldn't be created"} };
            }
            catch (Exception e)
            {
                //return BadRequest(e);
                return new InnerToken { isValid = false , Error = new string[] { e.Message } };
            }
        }

        private async Task<bool> CreateUser(GoogleAuthResponse res)
        {
           
            try
            {
                ApplicationUser user = new ApplicationUser { Email = res.ProfileObj.Email, UserName = res.ProfileObj.GivenName+res.ProfileObj.FamilyName };
                IdentityResult CreatedUser =  await _UserManager.CreateAsync(user);
                return CreatedUser.Succeeded;
            }
            catch
            {
                return false ;
            }
           

        }


        private string GetAuthToken(string UserName, string UserKey, string Email)
        {

            var token = new JwtSecurityTokenHandler();
            var key = Configuration["SecurityKey"];
            var g = UserKey;
            var Des = new SecurityTokenDescriptor()
            {
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, UserName),
                    new Claim("UserKey", UserKey),    
                    new Claim("Email",Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10)
            };
            var tokenHandler = token.CreateToken(Des);

            string GenJwtToken = token.WriteToken(tokenHandler).ToString();

            //Genrate Refresh token
          
            return GenJwtToken;
        }

    }

    public class InnerToken
    {
        public bool isValid; 
        public string Token;
        public IEnumerable<string> Error;
    }

}
