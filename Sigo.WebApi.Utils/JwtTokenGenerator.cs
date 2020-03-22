using Sigo.WebApi.DataEntities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sigo.WebApi.Utils
{
    /// <summary>
    /// Token生成器
    /// </summary>
    public class TokenGenerator
    {
        /// <summary>
        /// 生成JwtToken
        /// </summary>
        /// <param name="cnfiguration">配置信息</param>
        /// <param name="userEntity">用户信息</param>
        /// <returns>JwtToken</returns>
        public static string CreateJwtToken(IConfiguration cnfiguration, UserEntity userEntity)
        {
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Name, userEntity.UserId),
                    new Claim(ClaimTypes.GivenName, userEntity.UserName),
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cnfiguration.GetValue<string>("JwtSetting:SecurityKey")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: cnfiguration.GetValue<string>("JwtSetting:Issuer"),
                audience: cnfiguration.GetValue<string>("JwtSetting:Audience"),
                signingCredentials: creds,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(cnfiguration.GetValue<int>("JwtSetting:ExpireSeconds")));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
