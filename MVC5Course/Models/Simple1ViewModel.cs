using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class Simple1ViewModel : IValidatableObject
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public int Age { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
            //throw new NotImplementedException();
        }
    }
}