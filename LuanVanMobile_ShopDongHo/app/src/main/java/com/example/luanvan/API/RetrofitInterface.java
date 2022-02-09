package com.example.luanvan.API;

import com.example.luanvan.Model.Test;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;

public interface RetrofitInterface {
    @GET("/photos")
    Call<List<Test>> getAllPhotos();

}
