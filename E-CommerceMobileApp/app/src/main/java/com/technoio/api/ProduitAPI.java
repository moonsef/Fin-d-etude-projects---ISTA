package com.technoio.api;

import com.technoio.models.Produit;

import java.util.HashMap;
import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Path;

public interface ProduitAPI {

    @GET("/api/produits/categorie/{name}")
    Call<List<Produit>> getProduitsByCategorie(@Path("name") String name);

    @GET("/api/produits/latest")
    Call<List<Produit>> getAllProduits();

    @POST("/api/produits/produit")
    Call<Produit> getProduitByName(@Body HashMap<String, Object> body);
}
