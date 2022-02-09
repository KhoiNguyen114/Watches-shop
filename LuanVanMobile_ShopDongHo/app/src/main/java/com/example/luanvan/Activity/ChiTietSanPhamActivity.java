package com.example.luanvan.Activity;

import android.content.Intent;
import android.os.Bundle;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatActivity;

import com.bumptech.glide.Glide;
import com.bumptech.glide.load.engine.DiskCacheStrategy;
import com.example.luanvan.API.DanhMucSPService;
import com.example.luanvan.API.HangService;
import com.example.luanvan.API.LinkAPI;
import com.example.luanvan.Model.DanhMucSP;
import com.example.luanvan.Model.Hang;
import com.example.luanvan.Model.SanPham;
import com.example.luanvan.R;

import java.text.DecimalFormat;
import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class ChiTietSanPhamActivity extends AppCompatActivity {

    TextView textViewMaSP, textViewTenSP, textViewDonGia, textViewKT_ND_LD, textViewXuatSu,
            textViewThongTin, textViewHang, textViewDMSP, textViewThoiHanBH, textViewSoLuong;
    ImageView imageViewSP;
    SanPham sanPham;
    List<Hang> hangs;
    List<DanhMucSP> danhMucSPs;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_chi_tiet_san_pham);

        textViewMaSP = findViewById(R.id.textViewMaSP);
        textViewTenSP = findViewById(R.id.textViewTenSP_ChiTietSP);
        textViewDonGia = findViewById(R.id.textViewGiaSP_ChiTietSP);
        textViewKT_ND_LD = findViewById(R.id.textViewLD_NL_KTSP);
        textViewXuatSu = findViewById(R.id.textViewXuatSuSP);
        textViewThongTin = findViewById(R.id.textViewThongTinSP);
        textViewHang = findViewById(R.id.textViewHangSP_ChiTietSP);
        imageViewSP = findViewById(R.id.imageViewSanPham);
        textViewDMSP = findViewById(R.id.textViewDMSP_ChiTietSP);
        textViewThoiHanBH = findViewById(R.id.textViewThoiHanBaoHanh);
        textViewSoLuong = findViewById(R.id.textViewSoLuongSP);

        sanPham = (SanPham) getIntent().getSerializableExtra("ChiTietSP");
        hangs = new ArrayList<>();
        danhMucSPs = new ArrayList<>();

        getDataHang();
        getDataDanhMucSP();

        textViewMaSP.setText(textViewMaSP.getText() + sanPham.getMASP());
        textViewTenSP.setText(textViewTenSP.getText() + sanPham.getTENSP());
        textViewSoLuong.setText(textViewSoLuong.getText() + "" + sanPham.getSOLUONG());
        textViewThoiHanBH.setText(textViewThoiHanBH.getText() + "" + sanPham.getThoiHanBH() + " năm");

        String url = LinkAPI.BASE_URL + "Content/Images/SanPham/" + sanPham.getHINHANH();
        Glide.with(ChiTietSanPhamActivity.this).load(url)
                .diskCacheStrategy(DiskCacheStrategy.ALL)
                .into(imageViewSP);
        textViewXuatSu.setText(textViewXuatSu.getText() + sanPham.getXUATSU());

        DecimalFormat decimalFormat = new DecimalFormat("#,###");
        String donGia = decimalFormat.format(sanPham.getDONGIA());
        textViewDonGia.setText(donGia);
        textViewThongTin.setText(sanPham.getCHITIETSP());
        textViewKT_ND_LD.setText(sanPham.getLOAIDAY() + " - " + sanPham.getNANGLUONG() + " - " + sanPham.getKICHTHUOC());


        getSupportActionBar().setTitle("Chi tiết sản phẩm");
        getSupportActionBar().setBackgroundDrawable(getResources().getDrawable(R.drawable.custom_actionbar));
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
    }

    @Override
    public boolean onSupportNavigateUp() {
        Intent intent = new Intent(ChiTietSanPhamActivity.this, SanPhamActivity.class);
        finish();
        return super.onSupportNavigateUp();
    }

    private void getDataHang() {
        HangService.hangService.getListHang().enqueue(new Callback<List<Hang>>() {
            @Override
            public void onResponse(Call<List<Hang>> call, Response<List<Hang>> response) {
                if(response.isSuccessful() && response.body() != null)
                {
                    hangs = response.body();
                    TraVeTenHang(sanPham.getMAHANG());
                }
            }

            @Override
            public void onFailure(Call<List<Hang>> call, Throwable t) {
                //Toast.makeText(ChiTietSanPhamActivity.this, "Gọi API hãng thất bại", Toast.LENGTH_LONG).show();
            }
        });
    }

    private void getDataDanhMucSP() {
        DanhMucSPService.danhMucSpService.getListDanhMucSP().enqueue(new Callback<List<DanhMucSP>>() {
            @Override
            public void onResponse(Call<List<DanhMucSP>> call, Response<List<DanhMucSP>> response) {
                if(response.isSuccessful() && response.body() != null)
                {
                    danhMucSPs.addAll(response.body());
                    TraVeTenDanhMuc(sanPham.getMADM());
                }
            }

            @Override
            public void onFailure(Call<List<DanhMucSP>> call, Throwable t) {
                //Toast.makeText(ChiTietSanPhamActivity.this, "Gọi API danh mục sản phẩm thất bại", Toast.LENGTH_LONG).show();
            }
        });
    }

    private void TraVeTenHang(String maHang)
    {
        for(Hang h: hangs)
        {
            if(h.getMAHANG().equals(maHang))
            {
                textViewHang.setText(textViewHang.getText() + h.getTENHANG());
                break;
            }
        }
    }

    private void TraVeTenDanhMuc(String maDM)
    {
        for(DanhMucSP dmSP: danhMucSPs)
        {
            if(dmSP.getMADM().equals(maDM))
            {
                textViewDMSP.setText(textViewDMSP.getText() + dmSP.getTENDM());
                break;
            }
        }
    }
}