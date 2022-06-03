package com.technoio.models;

public class Avis {
    public int ID;
    public String Text;
    public int AvisEtoile;
    public String Date;
    public Client Client;

    public Avis(int ID, String text, int avisEtoile, String date, com.technoio.models.Client client) {
        this.ID = ID;
        Text = text;
        AvisEtoile = avisEtoile;
        Date = date;
        Client = client;
    }
}
