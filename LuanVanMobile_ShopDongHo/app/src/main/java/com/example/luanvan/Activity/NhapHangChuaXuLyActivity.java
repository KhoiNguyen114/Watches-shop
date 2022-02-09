package com.example.luanvan.Activity;

import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.SearchView;
import androidx.core.widget.NestedScrollView;
import androidx.recyclerview.widget.DividerItemDecoration;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.ProgressBar;
import android.widget.Toast;

import com.example.luanvan.API.PhieuNhapService;
import com.example.luanvan.Adapter.PhieuNhapAdapter;
import com.example.luanvan.Model.PhieuNhap;
import com.example.luanvan.R;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class NhapHangChuaXuLyActivity extends AppCompatActivity {

    RecyclerView recyclerView;
    NestedScrollView nestedScrollView;
    ProgressBar progressBar;
    List<PhieuNhap> mPhieuNhaps;
    PhieuNhapAdapter adapter;
    SearchView searchView;
    int page = 1, limit = 500;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_nhap_hang_chua_xu_ly);

        nestedScrollView = findViewById(R.id.nestedScroll_NhapHangChuaXuLy);
        recyclerView = findViewById(R.id.recyclerView_NhapHangChuaXuLy);
        progressBar = findViewById(R.id.progressbarLoading_NhapHangChuaXuLy);

        recyclerView.setHasFixedSize(true);
        recyclerView.setLayoutManager(new LinearLayoutManager(NhapHangChuaXuLyActivity.this));

        mPhieuNhaps = new ArrayList<>();
        adapter = new PhieuNhapAdapter(mPhieuNhaps, getApplicationContext());
        recyclerView.setAdapter(adapter);

        getData(page, limit);

        DividerItemDecoration dividerItemDecoration = new DividerItemDecoration(NhapHangChuaXuLyActivity.this, DividerItemDecoration.VERTICAL);
        recyclerView.addItemDecoration(dividerItemDecoration);

        getSupportActionBar().setTitle("Danh sách chưa xử lý");
        getSupportActionBar().setBackgroundDrawable(getResources().getDrawable(R.drawable.custom_actionbar));
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
    }

    @Override
    public boolean onSupportNavigateUp() {
        Intent intent = new Intent(NhapHangChuaXuLyActivity.this, MainActivity.class);
        finish();
        return super.onSupportNavigateUp();
    }

    private void getData(int page, int limit) {
        PhieuNhapService.phieuNhapService.getListPhieuNhapChuaXuLy(page, limit).enqueue(new Callback<List<PhieuNhap>>() {
            @Override
            public void onResponse(Call<List<PhieuNhap>> call, Response<List<PhieuNhap>> response) {
                if(response.isSuccessful() && response.body() != null)
                {
                    mPhieuNhaps.addAll(response.body());
                    Log.d("listPNChuaXuLy", mPhieuNhaps.size() + "");
                    adapter = new PhieuNhapAdapter(mPhieuNhaps, getApplicationContext());
                    recyclerView.setAdapter(adapter);
                    adapter.notifyDataSetChanged();
                    progressBar.setVisibility(View.GONE);
                }
            }

            @Override
            public void onFailure(Call<List<PhieuNhap>> call, Throwable t) {
                progressBar.setVisibility(View.GONE);
                Toast.makeText(NhapHangChuaXuLyActivity.this, "Gọi API nhập hàng chưa xử lý thất bại", Toast.LENGTH_LONG).show();
            }
        });
    }
}