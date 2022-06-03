package com.technoio.api;

import com.technoio.models.Categorie;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;

public interface CategorieAPI {

    @GET("/api/categories")
    Call<List<Categorie>> getCategories();
}
