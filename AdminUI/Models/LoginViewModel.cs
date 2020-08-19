using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminUI.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name ="账号")]
        public string PhoneNum { get; set; }
        [Required]
        [Display(Name = "密码")]
        public string Password { get; set; }
        [Required(ErrorMessage ="验证码必填")]
        public string Captcha { get; set; }

    }
}