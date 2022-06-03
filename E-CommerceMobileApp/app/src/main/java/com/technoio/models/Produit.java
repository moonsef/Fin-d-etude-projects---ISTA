package com.technoio.models;



import java.util.List;

public class Produit {
    public  int ID;
    public String Nom;
    public String Description;
    public String Prix;
    public int Quantity;
    public String Etat;
    public String Statut;
    public List<Photo> Photos;
    public List<Avis> Avis;
    public Categorie Categorie;
    public Marque Marque;
    public String Produit_date;
    public int Promo;
    public int Rating;
    public int TotalRating;

    public Produit(int ID, String nom, String description, String prix, int quantity, String etat, String statut, List<Photo> photos, List<com.technoio.models.Avis> avis, com.technoio.models.Categorie categorie, com.technoio.models.Marque marque, String produit_date, int promo, int rating, int totalRating) {
        this.ID = ID;
        Nom = nom;
        Description = description;
        Prix = prix;
        Quantity = quantity;
        Etat = etat;
        Statut = statut;
        Photos = photos;
        Avis = avis;
        Categorie = categorie;
        Marque = marque;
        Produit_date = produit_date;
        Promo = promo;
        Rating = rating;
        TotalRating = totalRating;
    }
}
