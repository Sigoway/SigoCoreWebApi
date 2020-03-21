using System.Collections.Generic;

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

        public string DisplayName { get; set; }

        public string EmployeeNumber { get; set; }

        public string UserType { get; set; }

        public string DeptCode { get; set; }

        public string DeptName { get; set; }

        public IList<string> PermissonList { get; internal set; }

        public bool IsEmpty => string.IsNullOrEmpty(UserId);
    }
}
