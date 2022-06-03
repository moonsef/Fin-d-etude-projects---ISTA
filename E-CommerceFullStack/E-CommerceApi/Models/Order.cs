using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace E_CommerceApi.Models
{
    public class Order
    {
        public int ID { get; set; }

        [Required(ErrorMessage="Produit nom est obligatoire")]
        public string ProduitName { get; set; }

        [Required(ErrorMessage="Client uuid nom est obligatoire")]
        public string ClientUuid { get; set; }

        
        [Required(ErrorMessage="Quantity est obligatoire")]
        public int Quantity { get; set; }



    }
}
