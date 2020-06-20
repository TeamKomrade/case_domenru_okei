using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaseDomenru.Models
{
    public class ValidationModel
    {
        [Required]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "От 3 до 255 символов")]
        public string Input { get; set; }

        public bool? isValidEmail { get; set; } = null;
    }
}
