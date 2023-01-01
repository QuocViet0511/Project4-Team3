using System.ComponentModel.DataAnnotations;

namespace Project4_Nhom3.Areas.Admin.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Vui lòng nhập user name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
