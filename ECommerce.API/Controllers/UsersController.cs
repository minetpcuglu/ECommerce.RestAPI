using ECommerce.Business.Security;
using ECommerce.Shared.CriteriaObjects.Security;
using ECommerce.Shared.Request.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Yeni Kullanıcı ekleme servisi
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] UserRequest request)
        {
            var add = await _userService.AddUserAsync(request);
            return Ok(add);
        }

        /// <summary>
        /// Sayfalamalı olarak kullanıcılar listesini döner.
        /// </summary>
        /// <param name="co"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUsersPagingAsync([FromQuery] UserCO co)
        {
            var list = await _userService.GetUserPagingAsync(co);
            return Ok(list);
        }
    }
}
