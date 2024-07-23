//using Azure;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Core.Utilities.Security.JWT
//{
//    public class CookieResponse
//    {
//        public void AddCookieToResponse(Authentication authModel)
//        {
//            var cookieOptions = new CookieOptions()
//            {
//                HttpOnly = true,
//                Secure = true,
//                SameSite = SameSiteMode.Strict,
//                Expires = DateTime.UtcNow.AddMinutes(Globals.AUTH_TOKEN_EXPIRED_MINUTES)
//            };
//            Response.Cookies.Append("X-Access-Token", authModel.UserToken, cookieOptions);
//        }
//    }
//}
