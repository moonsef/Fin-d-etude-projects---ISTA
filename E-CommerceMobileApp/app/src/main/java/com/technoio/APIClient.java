package com.technoio;

import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class APIClient {
    private static Retrofit retrofit;
    private static final String BASE_URL = "http://192.168.0.103:45455";

    static Retrofit getClient(){
        retrofit = new Retrofit.Builder()
            .baseUrl(BASE_URL)
            .addConverterFactory(GsonConverterFactory.create())
            .build();

        return retrofit;
    }
}
