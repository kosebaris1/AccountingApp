using Accounting.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Accounting.API.Controllers
{
   
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public ActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == 204)
            {
                return new ObjectResult(null)
                {
                    StatusCode = response.StatusCode
                };
            }
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

        [NonAction]
        public int GetUserFromToken()
        {
            string requestHeader = Request.Headers["Authorization"];
            string jwt = requestHeader?.Replace("Bearer ", "");
            var handler=new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadToken(jwt) as JwtSecurityToken;
            string userId= jwtSecurityToken.Claims.First(claim => claim.Type == "sub")?.Value;
            int id = Int32.Parse(userId);
            return id == 0 ? 0 : id;  

        }
    }
}
