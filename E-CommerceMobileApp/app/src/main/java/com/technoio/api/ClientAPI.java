package com.technoio.api;

import java.util.HashMap;
import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

public interface ClientAPI {
    @POST("/api/auth/login")
    Call<String> loginClient(@Body HashMap<String, Object> body);

    @POST("/api/auth/register")
    Call<String> registerClient(@Body HashMap<String, Object> body);

    @POST("/api/orders/create")
    Call<Void> postOrder(@Body List<HashMap<String, Object>> body);
}
