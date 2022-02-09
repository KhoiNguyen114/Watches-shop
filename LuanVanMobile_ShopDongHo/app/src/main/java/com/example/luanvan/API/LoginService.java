package com.example.luanvan.API;

import com.example.luanvan.Model.TaiKhoan;
import com.example.luanvan.Model.ThongTinTaiKhoan;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.List;

import retrofit2.Call;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;
import retrofit2.http.GET;
import retrofit2.http.Query;

public interface LoginService {
    Gson gson = new GsonBuilder()
            .setDateFormat("yyyy-MM-dd HH:mm:ss")
            .create();

    LoginService loginService = new Retrofit.Builder()
            .baseUrl(LinkAPI.BASE_URL)
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build()
            .create(LoginService.class);

    @GET("api/GetAllTaiKhoan")
    Call<List<TaiKhoan>> getListTaiKhoan();

    @GET("api/GetThongTinNhanVien")
    Call<ThongTinTaiKhoan> getThongTinNhanVien(@Query("tenDN") String tenDN);
}
