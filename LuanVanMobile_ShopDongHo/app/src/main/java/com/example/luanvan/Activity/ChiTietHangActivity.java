package com.example.luanvan.Activity;

import android.content.Intent;
import android.os.Bundle;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatActivity;

import com.bumptech.glide.Glide;
import com.bumptech.glide.load.engine.DiskCacheStrategy;
import com.example.luanvan.API.LinkAPI;
import com.example.luanvan.Model.Hang;
import com.example.luanvan.R;

public class ChiTietHangActivity extends AppCompatActivity {

    TextView textViewMaHang, textViewTenHang, textViewThongTin;
    ImageView imageViewLogoHang;
    Hang hang;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_chi_tiet_hang);

        textViewMaHang = findViewById(R.id.textViewMaHang_ChiTietHang);
        textViewTenHang = findViewById(R.id.textViewTenHang_ChiTietHang);
        textViewThongTin = findViewById(R.id.textViewThongTinHang_ChiTietHang);
        imageViewLogoHang = findViewById(R.id.imageViewChiTietHang);

        hang = (Hang) getIntent().getSerializableExtra("ChiTietHang");

        String url = LinkAPI.BASE_URL + "Content/Images/Brand_Logo/" + hang.getLOGO();
        Glide.with(ChiTietHangActivity.this).load(url)
                .diskCacheStrategy(DiskCacheStrategy.ALL)
                .into(imageViewLogoHang);

        textViewMaHang.setText(textViewMaHang.getText() + hang.getMAHANG());
        textViewTenHang.setText(textViewTenHang.getText() + hang.getTENHANG());
        textViewThongTin.setText(hang.getTHONGTIN());

        getSupportActionBar().setTitle("Chi tiết hãng");
        getSupportActionBar().setBackgroundDrawable(getResources().getDrawable(R.drawable.custom_actionbar));
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
    }

    @Override
    public boolean onSupportNavigateUp() {
        Intent intent = new Intent(ChiTietHangActivity.this, HangActivity.class);
        finish();
        return super.onSupportNavigateUp();
    }
}