package com.example.luanvan.API;

import com.example.luanvan.Model.ChiTietHD;
import com.example.luanvan.Model.TaiKhoan;
import com.example.luanvan.Model.Test;
import com.example.luanvan.Model.hd;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.List;

import retrofit2.Call;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;
import retrofit2.http.GET;
import retrofit2.http.Query;

public interface HoaDonApi {
    Gson gson = new GsonBuilder()
            .setDateFormat("yyyy-MM-dd HH:mm:ss")
            .create();
    HoaDonApi HoaDonService = new Retrofit.Builder()
            .baseUrl(LinkAPI.BASE_URL1)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build()
            .create(HoaDonApi.class);
    @GET("api/HoaDonAPI/getHoaDons")
    Call<List<hd>> getListHD(@Query("type") int type);

    @GET("api/HoaDonAPI/getChitietHD")
    Call<ChiTietHD> getChitietHD(@Query("idHD") int idHD);
}
