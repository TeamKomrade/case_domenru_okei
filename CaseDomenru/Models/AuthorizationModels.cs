using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseDomenru.Models
{
    public class AuthorizationModel
    {
        public LoginModel LoginModel { get; set; }
        public RegistrationModel RegistrationModel { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string Email { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string Password { get; set; }
        
    }

    public class RegistrationModel
    {
        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string Email { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string Password { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string UniqueKey { get; set; }
    }
}
