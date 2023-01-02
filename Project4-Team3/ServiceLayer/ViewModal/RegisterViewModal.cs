using System.ComponentModel.DataAnnotations;
using System;

namespace Project4_Nhom3.Models
{
    public class RegisterViewModal
    {
        [Required(ErrorMessage = "Bạn cần nhập tên")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập số điện thoại")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        public string Password { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
