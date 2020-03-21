using Sigo.WebApi.DataEntities;

namespace Sigo.WebApi.Services
{
    /// <summary>
    /// 提供与账户交互的相关功能
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="pwd">密码</param>
        /// <returns><see cref="UserEntity"/></returns>
        UserEntity Login(string userId, string pwd);

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="userId">用户Id</param>
        void Logout(string userId);
    }
}
