package com.technoio.models;

public class Client {
    public int ID;
    public String Nom;
    public String Prenom;
    public String Email;
    public String Telephone;
    public String Adresse;

    public Client(int ID, String nom, String prenom, String email, String telephone, String adresse) {
        this.ID = ID;
        Nom = nom;
        Prenom = prenom;
        Email = email;
        Telephone = telephone;
        Adresse = adresse;
    }
}
