using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ekutuphane.webui.Models
{
    public class ResetPasswordModel
    {
        public string UserId { get; set; }
        public string Token {get ; set;}
        [DataType(DataType.Password)]
        [Required]
        public string Password {get ; set;}
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Required]
        public string RePassword {get ; set;}
    }
}