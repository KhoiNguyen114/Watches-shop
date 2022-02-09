package com.example.luanvan.API;

import com.example.luanvan.Model.ThongKe;
import com.example.luanvan.Model.hd;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.List;

import retrofit2.Call;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;
import retrofit2.http.GET;
import retrofit2.http.Query;

public interface ThongKeAPI {
    Gson gson = new GsonBuilder()
            .setDateFormat("yyyy-MM-dd HH:mm:ss")
            .create();
    ThongKeAPI ThongKeService= new Retrofit.Builder()
            .baseUrl(LinkAPI.BASE_URL1)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build()
            .create(ThongKeAPI.class);
    @GET("api/ThongKeAPI/getThongKe")
    Call<List<ThongKe>> getListThongke(@Query("year") int year);
}
