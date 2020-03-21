using Sigo.WebApi.DataEntities;
using Sigo.WebApi.Filters;
using Sigo.WebApi.Models;
using Sigo.WebApi.Services;
using Sigo.WebApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Sigo.WebApi.Controllers
{
    /// <summary>
    /// 提供与账户交互的相关功能
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [AccountActionFilter]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// <see cref="IAccountService"/>
        /// </summary>
        private readonly IAccountService _accountService;

        /// <summary>
        /// <see cref="IConfiguration"/>
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 构造<see cref="AccountController"/>对象
        /// </summary>
        /// <param name="accountService">账户服务</param>
        /// <param name="configuration">配置信息</param>
        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        // POST: api/Account
        [AllowAnonymous]
        [HttpPost("login")]
        public LoginResultEntity Login([FromBody] LoginModel loginInfo)
        {
            var jwtToken = string.Empty;
            var userEntity = _accountService.Login(loginInfo.UserId, loginInfo.pwd);
            if (!userEntity.IsEmpty)
            {
                #region Session
                //HttpContext.Session.SetString(loginInfo.UserId, HttpContext.Session.Id);
                #endregion

                #region Cookie
                //var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //var authProperties = new AuthenticationProperties
                //{
                //    //AllowRefresh = <bool>,
                //    // Refreshing the authentication session should be allowed.

                //    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                //    // The time at which the authentication ticket expires. A 
                //    // value set here overrides the ExpireTimeSpan option of 
                //    // CookieAuthenticationOptions set with AddCookie.

                //    //IsPersistent = true,
                //    // Whether the authentication session is persisted across 
                //    // multiple requests. When used with cookies, controls
                //    // whether the cookie's lifetime is absolute (matching the
                //    // lifetime of the authentication ticket) or session-based.

                //    //IssuedUtc = <DateTimeOffset>,
                //    // The time at which the authentication ticket was issued.

                //    //RedirectUri = <string>
                //    // The full path or absolute URI to be used as an http 
                //    // redirect response value.
                //};

                //HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties); 
                #endregion
            }

            jwtToken = TokenGenerator.CreateJwtToken(_configuration, userEntity);
            return new LoginResultEntity() { Token = jwtToken, UserInfo = userEntity };
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="userId">用户Id</param>
        [HttpPost("logout/{userId}")]
        public void Logout(string userId)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                _accountService.Logout(userId);
            }
            //HttpContext.Session.Remove(userId);
        }
    }
}
