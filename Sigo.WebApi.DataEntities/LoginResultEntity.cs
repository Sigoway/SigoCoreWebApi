using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sigo.WebApi.DataEntities
{
    public class LoginResultEntity
    {
        public string Token { get; set; }

        public UserEntity UserInfo { get; set; }
    }
}
