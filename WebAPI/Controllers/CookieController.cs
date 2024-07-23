//using Core.Utilities.Results;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;

//namespace WebAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CookieController : ControllerBase
//    {

//        [HttpGet("show-cookies")]
//        public IActionResult ShowCookies()
//        {
//            bool auth = false;
//            var cookies = Request.Cookies;
//            var cookieDict = new Dictionary<string, string>();

//            foreach (var cookie in cookies)
//            {
//                cookieDict[cookie.Key] = cookie.Value;
//            }

//            //if(cookieDict !=  null)
//            //{
//            //    var BadResult = new {
//            //        message = "There is no cookie",
//            //    };
//            //}
//            //else
//            //{

//            //}
//            var result = new
//            {
//                data = cookieDict,
//                message = "Cookie is set",
//            };
//            auth = true;

//            //var whichone =  auth ? 

//            var json = JsonConvert.SerializeObject(result, Formatting.Indented);
//            return Content(json, "application/json");
//            //var cookieList = new List<string>();

//            //foreach (var cookie in cookies)
//            //{
//            //    cookieList.Add($"{cookie.Key}: {cookie.Value}");
//            //}
//            //return Content(string.Join(";", cookieList));

//        }
//    }
//}