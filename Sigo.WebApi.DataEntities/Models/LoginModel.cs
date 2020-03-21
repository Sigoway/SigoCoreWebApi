using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sigo.WebApi.Models
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="用户名不能为空！")]
        public string UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "密码不能为空！")]
        [DataType(DataType.Password)]
        public string pwd { get; set; }
    }
}
