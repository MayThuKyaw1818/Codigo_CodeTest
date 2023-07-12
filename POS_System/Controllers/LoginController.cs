using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using POS_System.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace POS_System.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetToken")]
        [AllowAnonymous]
        public LoginResponseModel GenerateToken()
        {
            LoginResponseModel responseModel = new LoginResponseModel();

            try
            {
                var issuer = _configuration["JwtBearer:Issuer"];
                var audience = _configuration["JwtBearer:Audience"];
                var key = Encoding.ASCII.GetBytes(_configuration["JwtBearer:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Expires = DateTime.Now.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                //var stringToken = tokenHandler.WriteToken(token);

                responseModel.Token = jwtToken;
                responseModel.RespCode = "000";
                responseModel.RespDescription = "Success"; 
            }
            catch(Exception e)
            {
                responseModel.RespCode = "999";
                responseModel.RespDescription = e.Message;
            }

            return responseModel;
        }
    }
}
