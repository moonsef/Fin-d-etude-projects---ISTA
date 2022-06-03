using E_CommerceApi.Helpers;
using E_CommerceApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Http;
using System.ComponentModel.DataAnnotations;


namespace E_CommerceApi.Controllers
{
    [RoutePrefix("api/produits")]
    public class ProduitController : ApiController
    {

        [Route("")]
        public IHttpActionResult GetCategoriesAndThereProduits()
        {

            var dt_categories = DataAccess.GetData(new SqlCommand("select id,nom from categories"), out string cate_error);
            if (cate_error != string.Empty)
                return Ok(cate_error);

            var categories = new List<Categorie>();
            foreach(DataRow cate_row in dt_categories.Rows)
            {
                var query = @"select p.*, r.reduction_montant, rp.date_debut, rp.date_fin,
                        (select count(*) from produit_ratings where produit_id=p.id) as 'total_avis',
                        (select avg(rating) from produit_ratings where produit_id=p.id) as 'rating',
                        (select top 1 photo from produit_photos pp where pp.produit_id=p.id) as 'photo'
                        from reduction_produits rp full join produits p on rp.produit_id=p.id left join reductions r on r.id=rp.reduction_id 
                        where p.categorie_id={0}";

                var dt_produits = DataAccess.GetData(new SqlCommand(string.Format(query, cate_row["id"].ToString())), out string pro_error);


                if(pro_error != string.Empty)
                    return BadRequest(pro_error);

                var produits = new List<Produit>();

                foreach(DataRow prod_row in dt_produits.Rows)
                {

                    produits.Add(new Produit
                    {
                        ID = int.Parse(prod_row["id"].ToString()),
                        Nom = prod_row["nom"].ToString(),
                        Description = prod_row["description"].ToString(),
                        Etat = prod_row["etat"].ToString(),
                        Prix = prod_row["prix"].ToString(),
                        Quantity = int.Parse(prod_row["quantity"].ToString()),
                        Statut = prod_row["statut"].ToString(),
                        Produit_date = DateTime.Parse(prod_row["produit_date"].ToString()),
                        Promo = ValidPromo(prod_row) ? int.Parse(prod_row["reduction_montant"].ToString()) : 0,
                        Photos = new List<ProduitPhoto> { new ProduitPhoto { Photo = (byte[])prod_row["photo"] } },
                        Rating = prod_row["rating"].ToString() != string.Empty ? int.Parse(prod_row["rating"].ToString()) : 0,
                        TotalRating = prod_row["total_avis"].ToString() != string.Empty ? int.Parse(prod_row["total_avis"].ToString()) : 0

                    });
                }

                categories.Add(new Categorie
                {
                    ID = int.Parse(cate_row["id"].ToString()),
                    Nom = cate_row["nom"].ToString(),
                    Produits = produits
                });
            }
                
            return Ok(categories);
        }

        [Route("latest")]
        public IHttpActionResult GetLatestProduits()
        {

            var query = @"select p.*, r.reduction_montant, rp.date_debut, rp.date_fin,
                        (select count(*) from produit_ratings where produit_id=p.id) as 'total_avis',
                        (select avg(rating) from produit_ratings where produit_id=p.id) as 'rating',
                        (select top 1 photo from produit_photos pp where pp.produit_id=p.id) as 'photo'
                        from reduction_produits rp full join produits p on rp.produit_id=p.id left join reductions r on r.id=rp.reduction_id 
                        order by p.quantity desc;";

            
            var dt = DataAccess.GetData(new SqlCommand(query), out string message);

            if (message != string.Empty)
                return BadRequest(message);


            List<Produit> produits = new List<Produit>();
            foreach(DataRow row in dt.Rows)
            {
                
 
                produits.Add(new Produit {
                    ID = int.Parse(row["id"].ToString()),
                    Nom = row["nom"].ToString(),
                    Description = row["description"].ToString(),
                    Etat = row["etat"].ToString(),
                    Prix = row["prix"].ToString(),
                    Quantity = int.Parse(row["quantity"].ToString()),
                    Statut = row["statut"].ToString(),
                    Produit_date = DateTime.Parse(row["produit_date"].ToString()),
                    Promo = ValidPromo(row) ? int.Parse(row["reduction_montant"].ToString()) : 0,
                    Photos = new List<ProduitPhoto> { new ProduitPhoto { Photo = (byte[])row["photo"] } },
                    Rating = row["rating"].ToString() != string.Empty ? int.Parse(row["rating"].ToString()) : 0,
                    TotalRating = row["total_avis"].ToString() != string.Empty ? int.Parse(row["total_avis"].ToString()) : 0
                });
            }

            return Ok(produits);
            
        }


        public class Data
        {
            [Required(ErrorMessage ="Nom est obligatoire")]
            public string Nom { get; set; }
        }

        [Route("produit")]
        public IHttpActionResult PostProduitByName([FromBody] Data data)
        {
            var query = @"select top 1 p.id, p.nom, c.nom as 'categorie', p.description,p.prix, p.etat, p.statut, p.quantity,
                        r.reduction_montant,
                        rp.date_debut, rp.date_fin,
                        (select count(*) from produit_ratings where produit_id=p.id) as 'total_avis',
                        (select avg(rating) from produit_ratings where produit_id=p.id) as 'rating',
                        p.produit_date, m.nom as 'marque_nom', m.photo as 'marque_photo'
                        from produits p
                        join marques m on  m.id=p.marque_id
                        join categories c on c.id=p.categorie_id
                        left join reduction_produits rp on rp.produit_id=p.id
                        left join reductions r on r.id=rp.reduction_id
                        where p.nom like @nom";

            var cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@nom", string.Format("%{0}%", data.Nom.ToString())) ;
            var dt = DataAccess.GetData(cmd, out string message);
            if (message != string.Empty)
                return BadRequest(message);

            if (dt.Rows.Count == 0)
                return BadRequest("Le produit n'existe pas");

            var row = dt.Rows[0];
            var produit = new Produit
            {
                ID = int.Parse(row["id"].ToString()),
                Nom = row["nom"].ToString(),
                Description = row["description"].ToString(),
                Etat = row["etat"].ToString(),
                Prix = row["prix"].ToString(),
                Quantity = int.Parse(row["quantity"].ToString()),
                Statut = row["statut"].ToString(),
                Categorie= new Categorie { Nom=row["categorie"].ToString()},
                Produit_date = DateTime.Parse(row["produit_date"].ToString()),
                Promo = ValidPromo(row) ? int.Parse(row["reduction_montant"].ToString()) : 0,
                Marque = new Marque { Nom= row["marque_nom"].ToString() , Photo= (byte[])row["marque_photo"]},
                Rating = row["rating"].ToString() !=string.Empty ? int.Parse(row["rating"].ToString()) : 0,
                TotalRating = row["total_avis"].ToString() != string.Empty ? int.Parse(row["total_avis"].ToString()) : 0
            };


            var photos = DataAccess.GetData(new SqlCommand("select id,photo from produit_photos where produit_id=" + produit.ID), out string err);
            var avis = DataAccess.GetData(new SqlCommand("select pr.*, c.nom, c.prenom from produit_ratings pr join clients c on c.id=pr.client_id where pr.produit_id=" + produit.ID), out string err2);


            if (err != string.Empty)
                return BadRequest(err);

            if (err2 != string.Empty)
                return BadRequest(err2);


            var photo_list = new List<ProduitPhoto>();
            var avis_list = new List<Avis>();

            foreach(DataRow photo_row in photos.Rows)
            {
                photo_list.Add(new ProduitPhoto { ID = int.Parse(photo_row["id"].ToString()), Photo = (byte[])photo_row["photo"] }); ;
            }

            foreach(DataRow avis_row in avis.Rows)
            {
                avis_list.Add(new Avis
                {
                    ID= int.Parse(avis_row["id"].ToString()),
                    Text= avis_row["text"].ToString(),
                    AvisEtoile= int.Parse(avis_row["rating"].ToString()),
                    Date= avis_row["date"].ToString(),
                    Client = new Client { Nom=avis_row["nom"].ToString(), Prenom = avis_row["prenom"].ToString()}
                });
            }
            produit.Avis = avis_list;
            produit.Photos = photo_list;

            return Ok(produit);
        }


        [Route("categorie/{name}")]
        public IHttpActionResult GetProduitsByCategorie (string name)
        {
            var cmd = new SqlCommand("select id from categories where nom=@nom");
            cmd.Parameters.AddWithValue("@nom", name);
            var dt = DataAccess.GetData(cmd, out string err);
            if (err != string.Empty)
                return BadRequest(err);

            if (dt.Rows.Count == 0)
                return BadRequest("Cette categorie n'exist pas");

            var query = string.Format(@"select TOP 30 p.*, r.reduction_montant, rp.date_debut, rp.date_fin,
                        (select count(*) from produit_ratings where produit_id=p.id) as 'total_avis',
                        (select avg(rating) from produit_ratings where produit_id=p.id) as 'rating',
                        (select top 1 photo from produit_photos pp where pp.produit_id=p.id) as 'photo'
                        from reduction_produits rp full join produits p on rp.produit_id=p.id left join reductions r on r.id=rp.reduction_id 
                        where p.categorie_id={0}
                        order by p.quantity desc;", int.Parse(dt.Rows[0]["id"].ToString()));


            dt = DataAccess.GetData(new SqlCommand(query), out string message);

            if (message != string.Empty)
                return BadRequest(message);


            List<Produit> produits = new List<Produit>();
            foreach (DataRow row in dt.Rows)
            {


                produits.Add(new Produit
                {
                    ID = int.Parse(row["id"].ToString()),
                    Nom = row["nom"].ToString(),
                    Description = row["description"].ToString(),
                    Etat = row["etat"].ToString(),
                    Prix = row["prix"].ToString(),
                    Quantity = int.Parse(row["quantity"].ToString()),
                    Statut = row["statut"].ToString(),
                    Produit_date = DateTime.Parse(row["produit_date"].ToString()),
                    Promo = ValidPromo(row) ? int.Parse(row["reduction_montant"].ToString()) : 0,
                    Photos = new List<ProduitPhoto> { new ProduitPhoto { Photo = (byte[])row["photo"] } },
                    Rating = row["rating"].ToString() != string.Empty ? int.Parse(row["rating"].ToString()) : 0,
                    TotalRating = row["total_avis"].ToString() != string.Empty ? int.Parse(row["total_avis"].ToString()) : 0
                });
            }

            return Ok(produits);


        }
        private bool ValidPromo(DataRow row)
        {
            bool valid = false;
            if (row["date_debut"].ToString() != string.Empty && row["date_fin"].ToString() != string.Empty)
            {
                var date_now = DateTime.Now;
                var redu_debut = DateTime.Parse(row["date_debut"].ToString());
                var redu_fin = DateTime.Parse(row["date_fin"].ToString());

                valid = date_now > redu_debut && date_now < redu_fin;
            }
            

            return valid;
        }
    }
}
