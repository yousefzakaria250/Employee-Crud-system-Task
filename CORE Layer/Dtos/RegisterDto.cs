using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE_Layer.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string Name { set; get; }
        [Required]
        public string Email { set; get; }
        [Required]
        public string UserRole { get; set; }
        [Required]
        public string Password { set; get; }
        [Required]
        public string ConfirmPassword { set; get; }
    }
}
