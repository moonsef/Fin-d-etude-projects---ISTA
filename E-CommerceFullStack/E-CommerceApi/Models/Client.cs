using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace E_CommerceApi.Models
{
    public class Client
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Nom est obligatoire")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Prenom est obligatoire")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Nom est obligatoire")]
        [EmailAddress(ErrorMessage = "Email n'est pas valide")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Telephone est obligatoire")]
        [RegularExpression(@"(\+212|0)([ \-_/]*)(\d[ \-_/]*){9}", ErrorMessage = "Telephone n'est pas valide")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Adresse est obligatoire")]
        public string Adresse { get; set; }
        [MinLength(6, ErrorMessage = "Le mot de passe doit comporter au moins 6 caractères")]
        public string Password { get; set; }

        public bool Phone { get; set; } = false;
    }
}
