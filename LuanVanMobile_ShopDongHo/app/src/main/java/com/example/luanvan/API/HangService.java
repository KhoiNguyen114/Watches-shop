package com.example.luanvan.API;

import com.example.luanvan.Model.Hang;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.List;

import retrofit2.Call;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;
import retrofit2.http.GET;
import retrofit2.http.Query;

public interface HangService {
    Gson gson = new GsonBuilder()
            .setDateFormat("yyyy-MM-dd HH:mm:ss")
            .create();

    HangService hangService = new Retrofit.Builder()
            .baseUrl(LinkAPI.BASE_URL)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build()
            .create(HangService.class);

    @GET("api/GetAllHang")
    Call<List<Hang>> getListHangTheoTrang(@Query("page") int page,
                                          @Query("limit") int limit);

    @GET("api/GetAllHang")
    Call<List<Hang>> getListHang();
}
