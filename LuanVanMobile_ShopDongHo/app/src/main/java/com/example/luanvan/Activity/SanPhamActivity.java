package com.example.luanvan.Activity;

import android.app.SearchManager;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ProgressBar;

import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.SearchView;
import androidx.core.widget.NestedScrollView;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.luanvan.API.SanPhamService;
import com.example.luanvan.Adapter.SanPhamAdapter;
import com.example.luanvan.Model.SanPham;
import com.example.luanvan.R;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class SanPhamActivity extends AppCompatActivity {

    RecyclerView recyclerView;
    NestedScrollView nestedScrollView;
    ProgressBar progressBar;
    List<SanPham> mSanPham;
    SanPhamAdapter adapter;
    SearchView searchView;
    int page = 1, limit = 50;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_san_pham);

        nestedScrollView = findViewById(R.id.nestedScroll_SanPham);
        recyclerView = findViewById(R.id.recyclerView_SanPham);
        progressBar = findViewById(R.id.progressbarLoadingSanPham);

        recyclerView.setHasFixedSize(true);
        recyclerView.setLayoutManager(new GridLayoutManager(SanPhamActivity.this,3));


        getSupportActionBar().setTitle("Danh sách sản phẩm");
        getSupportActionBar().setBackgroundDrawable(getResources().getDrawable(R.drawable.custom_actionbar));
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        mSanPham = new ArrayList<>();
        //getListSanPham();
        adapter = new SanPhamAdapter(mSanPham, getApplicationContext());
        recyclerView.setAdapter(adapter);

        getData(page, limit);

        /*DividerItemDecoration dividerItemDecoration = new DividerItemDecoration(SanPhamActivity.this, DividerItemDecoration.VERTICAL);
        recyclerView.addItemDecoration(dividerItemDecoration);*/

        nestedScrollView.setOnScrollChangeListener(new NestedScrollView.OnScrollChangeListener() {
            @Override
            public void onScrollChange(NestedScrollView v, int scrollX, int scrollY, int oldScrollX, int oldScrollY) {
                if(scrollY == v.getChildAt(0).getMeasuredHeight() - v.getMeasuredHeight())
                {
                    page++;
                    progressBar.setVisibility(View.VISIBLE);
                    getData(page, limit);
                }
            }
        });


    }

    private void getData(int page, int limit) {
        SanPhamService.sanPhamService.getListSanPhamTheoTrang(page, limit).enqueue(new Callback<List<SanPham>>() {
            @Override
            public void onResponse(Call<List<SanPham>> call, Response<List<SanPham>> response) {
                if(response.isSuccessful() && response.body() != null)
                {
                    mSanPham.addAll(response.body());
                    adapter = new SanPhamAdapter(mSanPham, getApplicationContext());
                    recyclerView.setAdapter(adapter);
                    adapter.notifyDataSetChanged();
                    progressBar.setVisibility(View.GONE);
                }
            }

            @Override
            public void onFailure(Call<List<SanPham>> call, Throwable t) {
                progressBar.setVisibility(View.GONE);
                //Toast.makeText(SanPhamActivity.this, "Gọi API sản phẩm thất bại", Toast.LENGTH_LONG).show();
            }
        });
    }

    /*private void getListSanPhamTimKiem(int page, int limit, String tukhoa) {
        SanPhamService.sanPhamService.getListSanPhamTimKiem(page, limit, tukhoa).enqueue(new Callback<List<SanPham>>() {
            @Override
            public void onResponse(Call<List<SanPham>> call, Response<List<SanPham>> response) {
                if(response.isSuccessful() && response.body() != null)
                {
                    mSanPham.clear();
                    mSanPham.addAll(response.body());
                    adapter = new SanPhamAdapter(mSanPham, getApplicationContext());
                    recyclerView.setAdapter(adapter);
                    adapter.notifyDataSetChanged();
                    progressBar.setVisibility(View.GONE);
                }
            }

            @Override
            public void onFailure(Call<List<SanPham>> call, Throwable t) {
                progressBar.setVisibility(View.GONE);
                Toast.makeText(SanPhamActivity.this, "Gọi API sản phẩm thất bại", Toast.LENGTH_LONG).show();
            }
        });
    }*/

    @Override
    public boolean onSupportNavigateUp() {
        Intent intent = new Intent(SanPhamActivity.this, MainActivity.class);
        finish();
        return super.onSupportNavigateUp();
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.search_sanpham, menu);
        SearchManager searchManager = (SearchManager) getSystemService(SEARCH_SERVICE);
        searchView = (SearchView) menu.findItem(R.id.searchSanPham).getActionView();
        searchView.setQueryHint("Nhập mã hoặc tên sản phẩm...");
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
    }
}