using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceApi.Models
{
    public class Categorie
    {
        public int ID { get; set; }
        public string Nom { get; set; }

        public List<Produit> Produits { get; set; }
    }

}
