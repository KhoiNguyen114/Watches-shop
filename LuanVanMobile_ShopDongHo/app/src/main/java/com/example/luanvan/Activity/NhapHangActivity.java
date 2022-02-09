package com.example.luanvan.Activity;

import android.app.SearchManager;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ProgressBar;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.SearchView;
import androidx.core.widget.NestedScrollView;
import androidx.recyclerview.widget.DividerItemDecoration;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.luanvan.API.PhieuNhapService;
import com.example.luanvan.Adapter.PhieuNhapAdapter;
import com.example.luanvan.Model.PhieuNhap;
import com.example.luanvan.R;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class NhapHangActivity extends AppCompatActivity {

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
        setContentView(R.layout.activity_nhap_hang);

        nestedScrollView = findViewById(R.id.nestedScroll_NhapHang);
        recyclerView = findViewById(R.id.recyclerView_NhapHang);
        progressBar = findViewById(R.id.progressbarLoadingNhapHang);

        recyclerView.setHasFixedSize(true);
        recyclerView.setLayoutManager(new LinearLayoutManager(NhapHangActivity.this));

        mPhieuNhaps = new ArrayList<>();
        adapter = new PhieuNhapAdapter(mPhieuNhaps, getApplicationContext());
        recyclerView.setAdapter(adapter);

        getData(page, limit);

        DividerItemDecoration dividerItemDecoration = new DividerItemDecoration(NhapHangActivity.this, DividerItemDecoration.VERTICAL);
        recyclerView.addItemDecoration(dividerItemDecoration);

        getSupportActionBar().setTitle("Danh sách phiếu nhập hàng");
        getSupportActionBar().setBackgroundDrawable(getResources().getDrawable(R.drawable.custom_actionbar));
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
    }

    @Override
    public boolean onSupportNavigateUp() {
        Intent intent = new Intent(NhapHangActivity.this, MainActivity.class);
        finish();
        return super.onSupportNavigateUp();
    }

    private void getData(int page, int limit) {
       PhieuNhapService.phieuNhapService.getListPhieuNhapTheoTrang(page, limit).enqueue(new Callback<List<PhieuNhap>>() {
           @Override
           public void onResponse(Call<List<PhieuNhap>> call, Response<List<PhieuNhap>> response) {
               if(response.isSuccessful() && response.body() != null)
               {
                   mPhieuNhaps.addAll(response.body());
                   Log.d("listHang", mPhieuNhaps.size() + "");
                   adapter = new PhieuNhapAdapter(mPhieuNhaps, getApplicationContext());
                   recyclerView.setAdapter(adapter);
                   adapter.notifyDataSetChanged();
                   progressBar.setVisibility(View.GONE);
               }
           }

           @Override
           public void onFailure(Call<List<PhieuNhap>> call, Throwable t) {
               progressBar.setVisibility(View.GONE);
               Toast.makeText(NhapHangActivity.this, "Gọi API hãng thất bại", Toast.LENGTH_LONG).show();
           }
       });
    }

    /*@Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.search_sanpham, menu);
        SearchManager searchManager = (SearchManager) getSystemService(SEARCH_SERVICE);
        searchView = (SearchView) menu.findItem(R.id.searchSanPham).getActionView();
        searchView.setQueryHint("Nhập mã phiếu nhập...");
        searchView.setSearchableInfo(searchManager.getSearchableInfo(getComponentName()));
        searchView.setMaxWidth(Integer.MAX_VALUE);
        searchView.setOnQueryTextListener(new SearchView.OnQueryTextListener() {
            @Override
            public boolean onQueryTextSubmit(String query) {
                adapter.getFilter().filter(query);
                return false;
            }

            @Override
            public boolean onQueryTextChange(String newText) {
                adapter.getFilter().filter(newText);
                return false;
            }
        });
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        return super.onOptionsItemSelected(item);
    }*/
}