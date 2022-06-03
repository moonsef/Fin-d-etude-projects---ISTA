using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_CommerceApi.Models
{
    public class Feedback
    {
        [Required(ErrorMessage ="Fullname est obligatoire")]
        [RegularExpression(@"(^[A-Za-z]{3,16})([ ]{0,1})([A-Za-z]{3,16})?([ ]{0,1})?([A-Za-z]{3,16})?([ ]{0,1})?([A-Za-z]{3,16})", ErrorMessage ="FullName est incorrect format")]
        public string FullName { get; set; }

        [Required(ErrorMessage ="Email est obligatoire")]
        [EmailAddress(ErrorMessage ="Email est incorrect format")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Subject est obligatoire")]
        public string Subject { get; set; }

        [Required(ErrorMessage ="Message est obligatoire")]
        public string Message { get; set; }
    }
}
