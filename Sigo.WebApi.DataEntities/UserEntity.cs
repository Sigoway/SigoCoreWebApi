using System.Collections.Generic;
using System.Security.Claims;

namespace Sigo.WebApi.DataEntities
{
    /// <summary>
    /// 用户信息实体类
    /// </summary>
    public class UserEntity: BaseEntity
    {
        //TODO 封装具体业务属性

        public string UserId { get; set; }

        public string UserName { get; set; }

        public IList<string> PermissonList { get; internal set; }

        public bool IsEmpty => string.IsNullOrEmpty(UserId);
    }
}
