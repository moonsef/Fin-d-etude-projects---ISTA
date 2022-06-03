using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_CommerceApi.Models
{
    public class Avis
    {
        public int ID { get; set; }
        public string Text { get; set; }

        
        [Range(0,5, ErrorMessage = "Avis etoire doit être comprise entre 0 et 5")]
        public int AvisEtoile { get; set; }


        [Required(ErrorMessage ="Le nom de produit est obligatoire")]
        public string ProduitNom { get; set; }

        public string Date { get; set; }

        public Client Client { get; set; }

        public string PhoneUuid { get; set; }
    }
}
