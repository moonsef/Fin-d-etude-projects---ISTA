package com.technoio.models;


import java.util.List;

public class Categorie {

    public int ID;
    public String Nom;
    public List<Produit> produitList;

    public Categorie(int ID, String nom) {
        this.ID = ID;
        this.Nom = nom;
    }

}
