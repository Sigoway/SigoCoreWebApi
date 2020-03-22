using Sigo.WebApi.DataEntities;
using Sigo.WebApi.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sigo.WebApi.Services.Impl
{
    /// <summary>
    /// 提供与账户交互的相关功能
    /// </summary>
    public class AccountService : IAccountService
    {
#if DEBUG
        //测试代码
        internal IList<UserEntity> UserList { get; private set; }
#endif

        /// <summary>
        /// <see cref="IDataProvider"/>
        /// </summary>
        private readonly IDataProvider _dataProvider;

        /// <summary>
        /// 构造<see cref="AccountService"/>对象
        /// </summary>
        /// <param name="dataProvider">数据驱动</param>
        public AccountService(ISqlServerDataProvider dataProvider)
        {
            _dataProvider = dataProvider;

#if DEBUG
            //测试代码
            UserList = new List<UserEntity>()
            {
                new UserEntity()
                {
                    UserId="t001",
                    UserName = "张三"
                },
                new UserEntity()
                {
                    UserId="t002",
                    UserName = "李四"
                }
            };
#endif
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="pwd">密码</param>
        /// <returns><see cref="UserEntity"/></returns>
        public UserEntity Login(string userId, string pwd)
        {
#if DEBUG
            var user = UserList.FirstOrDefault(t => t.UserId == userId);
            if (user == null)
            {
                throw new ApplicationException("该用户不存在！");
            }

            if (pwd != "123")
            {
                throw new ApplicationException("密码不正确！");
            }
            return user;
#endif
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="userId">用户Id</param>
        public void Logout(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
