package com.example.luanvan.Activity;

import android.content.Intent;
import android.os.Bundle;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.DividerItemDecoration;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.luanvan.API.ChiTietPhieuNhapService;
import com.example.luanvan.Adapter.ChiTietPhieuNhapAdapter;
import com.example.luanvan.Model.ChiTietPN;
import com.example.luanvan.Model.PhieuNhap;
import com.example.luanvan.R;

import java.text.DecimalFormat;
import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class ChiTietPhieuNhapActivity extends AppCompatActivity {

    TextView textViewMaPN, textViewTenNV, textViewTenHang, textViewNgayLap, textViewTongTien, textViewTinhTrang;
    RecyclerView recyclerView;
    List<ChiTietPN> chiTietPNS;
    PhieuNhap phieuNhap;
    ChiTietPhieuNhapAdapter chiTietPhieuNhapAdapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_chi_tiet_phieu_nhap);

        MappingLayout();

        phieuNhap = (PhieuNhap) getIntent().getSerializableExtra("ChiTietPhieuNhap");
        chiTietPNS = new ArrayList<>();
        chiTietPhieuNhapAdapter = new ChiTietPhieuNhapAdapter(chiTietPNS, getApplicationContext());
        recyclerView.setAdapter(chiTietPhieuNhapAdapter);

        recyclerView.setHasFixedSize(true);
        recyclerView.setLayoutManager(new LinearLayoutManager(ChiTietPhieuNhapActivity.this));

        getDataCTPN();
        BindingData();

        DividerItemDecoration dividerItemDecoration = new DividerItemDecoration(ChiTietPhieuNhapActivity.this, DividerItemDecoration.VERTICAL);
        recyclerView.addItemDecoration(dividerItemDecoration);

        getSupportActionBar().setTitle("Chi tiết phiếu nhập hàng");
        getSupportActionBar().setBackgroundDrawable(getResources().getDrawable(R.drawable.custom_actionbar));
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
    }

    @Override
    public boolean onSupportNavigateUp() {
        Intent intent = new Intent(ChiTietPhieuNhapActivity.this, NhapHangActivity.class);
        finish();
        return super.onSupportNavigateUp();
    }

    private void MappingLayout()
    {
        recyclerView = findViewById(R.id.recyclerView_ChiTietPhieuNhapHang);
        textViewMaPN = findViewById(R.id.textViewMaPN_ChiTietPN);
        textViewTenNV = findViewById(R.id.textViewTenNV_ChiTietPN);
        textViewTenHang = findViewById(R.id.textViewTenHang_ChiTietPN);
        textViewNgayLap = findViewById(R.id.textViewNgayLap_ChiTietPN);
        textViewTinhTrang = findViewById(R.id.textViewTinhTrang_ChiTietPN);
        textViewTongTien = findViewById(R.id.textViewTongTien_ChiTietPN);
    }

    private void getDataCTPN()
    {
        ChiTietPhieuNhapService.chiTietPhieuNhapService.GetChiTietPhieuNhap(phieuNhap.getIDPhieuNhap())
                .enqueue(new Callback<List<ChiTietPN>>() {
                    @Override
                    public void onResponse(Call<List<ChiTietPN>> call, Response<List<ChiTietPN>> response) {
                        chiTietPNS.addAll(response.body());
                        chiTietPhieuNhapAdapter = new ChiTietPhieuNhapAdapter(chiTietPNS, getApplicationContext());
                        recyclerView.setAdapter(chiTietPhieuNhapAdapter);
                        chiTietPhieuNhapAdapter.notifyDataSetChanged();
                    }

                    @Override
                    public void onFailure(Call<List<ChiTietPN>> call, Throwable t) {
                        Toast.makeText(ChiTietPhieuNhapActivity.this, "Gọi API chi tiết phiếu nhập thất bại", Toast.LENGTH_LONG).show();
                    }
                });
    }

    private void BindingData()
    {
        textViewMaPN.setText(textViewMaPN.getText() + phieuNhap.getMaPN());
        textViewTenHang.setText(textViewTenHang.getText() + phieuNhap.getTenHang());
        textViewTenNV.setText(textViewTenNV.getText() + phieuNhap.getTenNV());
        textViewNgayLap.setText(textViewNgayLap.getText() + phieuNhap.getNgayLap());

        if(phieuNhap.isTinhTrang())
        {
            textViewTinhTrang.setText(textViewTinhTrang.getText() + "Đã xử lý");
        }
        else
        {
            textViewTinhTrang.setText(textViewTinhTrang.getText() + "Chưa xử lý");
        }
        DecimalFormat decimalFormat = new DecimalFormat("#,###");
        String tongTien = decimalFormat.format(phieuNhap.getTongTien());
        textViewTongTien.setText(textViewTongTien.getText() + tongTien);
    }
}