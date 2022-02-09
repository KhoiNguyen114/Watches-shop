package com.example.luanvan.API;

import com.example.luanvan.Model.SanPham;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.List;

import retrofit2.Call;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;
import retrofit2.http.GET;
import retrofit2.http.Query;

public interface SanPhamService {
    Gson gson = new GsonBuilder()
            .setDateFormat("yyyy-MM-dd HH:mm:ss")
            .create();

    SanPhamService sanPhamService = new Retrofit.Builder()
            .baseUrl(LinkAPI.BASE_URL)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build()
            .create(SanPhamService.class);

    @GET("api/GetAllSanPham")
    Call<List<SanPham>> getListSanPham();

    @GET("api/GetAllSanPham")
    Call<List<SanPham>> getListSanPhamTheoTrang(@Query("page") int page,
                                                @Query("limit") int limit);

    @GET("api/GetAllSanPhamTimKiem")
    Call<List<SanPham>> getListSanPhamTimKiem(@Query("page") int page,
                                              @Query("limit") int limit,
                                              @Query("tukhoa") String tuKhoa);
}
