package com.example.luanvan.API;

import com.example.luanvan.Model.PhieuNhap;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.List;

import retrofit2.Call;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;
import retrofit2.http.GET;
import retrofit2.http.Query;

public interface PhieuNhapService {
    Gson gson = new GsonBuilder()
            .setDateFormat("yyyy-MM-dd HH:mm:ss")
            .create();

    PhieuNhapService phieuNhapService = new Retrofit.Builder()
            .baseUrl(LinkAPI.BASE_URL)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build()
            .create(PhieuNhapService.class);

    @GET("api/GetAllPhieuNhap")
    Call<List<PhieuNhap>> getListPhieuNhapTheoTrang(@Query("page") int page,
                                                    @Query("limit") int limit);

    @GET("api/GetAllPhieuNhap")
    Call<List<PhieuNhap>> getListPhieuNhap();

    @GET("api/GetAllPhieuNhapChuaXuLy")
    Call<List<PhieuNhap>> getListPhieuNhapChuaXuLy(@Query("page") int page,
                                                   @Query("limit") int limit);

    @GET("api/GetAllPhieuNhapDaXuLy")
    Call<List<PhieuNhap>> getListPhieuNhapDaXuLy(@Query("page") int page,
                                                 @Query("limit") int limit);
}
