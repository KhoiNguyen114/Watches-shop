package com.example.luanvan.API;

import com.example.luanvan.Model.ChiTietPN;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.List;

import retrofit2.Call;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;
import retrofit2.http.GET;
import retrofit2.http.Query;

public interface ChiTietPhieuNhapService {
    Gson gson = new GsonBuilder()
            .setDateFormat("yyyy-MM-dd HH:mm:ss")
            .create();

    ChiTietPhieuNhapService chiTietPhieuNhapService = new Retrofit.Builder()
            .baseUrl(LinkAPI.BASE_URL)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build()
            .create(ChiTietPhieuNhapService.class);

    @GET("api/GetChiTietPhieuNhap")
    Call<List<ChiTietPN>> GetChiTietPhieuNhap(@Query("id") int id);
}
