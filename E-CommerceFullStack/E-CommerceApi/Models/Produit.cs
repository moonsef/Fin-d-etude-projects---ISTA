using System;
using System.Collections.Generic;

namespace E_CommerceApi.Models
{
    public class Produit
    {

        public int ID { get; set; }

        public string Nom { get; set; }

        public string Prix { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public string Etat { get; set; }

        public string Statut { get; set; }

        public DateTime Produit_date { get; set; }

        public List<ProduitPhoto> Photos { get; set; }

        public int Promo { get; set; }

        public Marque Marque { get; set; }
        public Categorie Categorie { get; set; }

        public List<Avis> Avis { get; set; }

        public int Rating { get; set; }

        public int TotalRating {get;set;}
    

    }

    public class ProduitPhoto
    {
        public int ID { get; set; }
        public byte[] Photo { get; set; }
    }

}
