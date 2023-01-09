using ECommerce.Business.Security;
using ECommerce.Shared.CriteriaObjects.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Giriş işlemi 
        /// </summary>
        /// <param name="userService"></param>
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        // POST api/v1/Giris
        /// <summary>
        /// Giriş servisi 
        /// </summary>
        /// <param name="co"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginCO co)
        {
            var giris = await _userService.GetUserLoginAsync(co);
            return Ok(giris);
        }
    }
}
