package com.example.luanvan.Activity;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.example.luanvan.API.HoaDonApi;
import com.example.luanvan.Adapter.ChitietHDAdapter;
import com.example.luanvan.Model.ChiTietHD;
import com.example.luanvan.Model.hd;
import com.example.luanvan.Model.sp;
import com.example.luanvan.R;

import java.text.DecimalFormat;
import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class ChitietHDActivity extends AppCompatActivity {
    static Context mContext;
    Integer idHoaDon;
    TextView tenKH,gioiTinh,soDT,soCMND,diaChi,maHD,ngayLap,hinhThuc,tinhTrang,tongTien;
    RecyclerView mRecyclerView;
    List<sp> lsSP;
    ChitietHDAdapter adapter;
    static ChiTietHD chitiet = new ChiTietHD();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_chitiet_hdactivity);
        mContext = getApplicationContext();
        tenKH = findViewById(R.id.tenKH);
        gioiTinh = findViewById(R.id.gioiTinh);
        soDT = findViewById(R.id.SDT);
        soCMND = findViewById(R.id.SoCMND);
        diaChi = findViewById(R.id.diaChi);
        maHD = findViewById(R.id.OrderId);
        ngayLap = findViewById(R.id.OrderNgayLap);
        hinhThuc = findViewById(R.id.OrderHinhThuc);
        tinhTrang = findViewById(R.id.OrderTinhTrang);
        tongTien = findViewById(R.id.OrderTongTien);

        Bundle extras = getIntent().getExtras();
        if(extras != null)
        {
            idHoaDon = extras.getInt("idHD");
        }
        Log.e("Test","ID Hoa don : " + idHoaDon);

        getSupportActionBar().setTitle("Chi tiết hóa đơn");
        getSupportActionBar().setBackgroundDrawable(getResources().getDrawable(R.drawable.custom_actionbar));
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        lsSP = new ArrayList<>();
        getChiTiet();

        Log.e("Check","Hóa đơn ngoài api : " + chitiet.getTenKH());

        adapter = new ChitietHDAdapter(mContext,lsSP);
        mRecyclerView = findViewById(R.id.lstSP);
        mRecyclerView.setHasFixedSize(true);
        mRecyclerView.setNestedScrollingEnabled(false);
        mRecyclerView.setLayoutManager(new LinearLayoutManager(ChitietHDActivity.this,LinearLayoutManager.VERTICAL,false));
        mRecyclerView.setItemViewCacheSize(40);
        mRecyclerView.setAdapter(adapter);

    }
    @Override
    public boolean onSupportNavigateUp() {
        Intent intent = new Intent(ChitietHDActivity.this, HoaDonActivity.class);
        finish();
        return super.onSupportNavigateUp();
    }

    public void getChiTiet()
    {
        HoaDonApi.HoaDonService.getChitietHD(idHoaDon).enqueue(new Callback<ChiTietHD>() {
            @Override
            public void onResponse(Call<ChiTietHD> call, Response<ChiTietHD> response) {
                if(response.isSuccessful() && response.body() != null)
                {
                    ChiTietHD newct = response.body();
                    chitiet = newct;
                    tenKH.setText(chitiet.getTenKH());
                    tenKH.setText(chitiet.getTenKH());
                    gioiTinh.setText(chitiet.getGioiTinh());
                    soDT.setText(chitiet.getSoDT());
                    soCMND.setText(chitiet.getSoCMND());
                    diaChi.setText(chitiet.getDiaChi());
                    maHD.setText(chitiet.getMaHoaDon());
                    ngayLap.setText(chitiet.getNgayLap());
                    tinhTrang.setText(chitiet.getTinhTrang());
                    hinhThuc.setText(chitiet.getHinhThuc());
                    DecimalFormat decimalFormat = new DecimalFormat("#,###");
                    tongTien.setText(decimalFormat.format(chitiet.getTongTien()));
                    lsSP.addAll(chitiet.getDsSP());
                    Log.e("Check SP","Check san pham : " + lsSP.get(0).getTenSP());
                    adapter.notifyDataSetChanged();
                }
            }

            @Override
            public void onFailure(Call<ChiTietHD> call, Throwable t) {
                Toast.makeText(mContext, "Gọi API hóa đơn thất bại", Toast.LENGTH_LONG).show();
                Log.e("fail",t.toString());
            }
        });
    }
}