package com.example.luanvan.Activity;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.ProgressBar;

import androidx.appcompat.app.AppCompatActivity;
import androidx.core.widget.NestedScrollView;
import androidx.recyclerview.widget.DividerItemDecoration;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.luanvan.API.HangService;
import com.example.luanvan.Adapter.HangAdapter;
import com.example.luanvan.Model.Hang;
import com.example.luanvan.R;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class HangActivity extends AppCompatActivity {

    RecyclerView recyclerView;
    NestedScrollView nestedScrollView;
    ProgressBar progressBar;
    List<Hang> hangs;
    HangAdapter adapter;
    int page = 1, limit = 15;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_hang);

        MappingLayout();

        recyclerView.setHasFixedSize(true);
        recyclerView.setLayoutManager(new LinearLayoutManager(HangActivity.this));


        getSupportActionBar().setTitle("Danh sách hãng");
        getSupportActionBar().setBackgroundDrawable(getResources().getDrawable(R.drawable.custom_actionbar));
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        hangs = new ArrayList<>();
        adapter = new HangAdapter(hangs, getApplicationContext());
        recyclerView.setAdapter(adapter);

        getData(page, limit);

        DividerItemDecoration dividerItemDecoration = new DividerItemDecoration(HangActivity.this, DividerItemDecoration.VERTICAL);
        recyclerView.addItemDecoration(dividerItemDecoration);

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

    private void MappingLayout()
    {
        nestedScrollView = findViewById(R.id.nestedScroll_Hang);
        recyclerView = findViewById(R.id.recyclerView_Hang);
        progressBar = findViewById(R.id.progressbarLoadingHang);
    }

    private void getData(int page, int limit) {
        HangService.hangService.getListHangTheoTrang(page, limit).enqueue(new Callback<List<Hang>>() {
            @Override
            public void onResponse(Call<List<Hang>> call, Response<List<Hang>> response) {
                if(response.isSuccessful() && response.body() != null)
                {
                    hangs.addAll(response.body());
                    Log.d("listHang", hangs.size() + "");
                    adapter = new HangAdapter(hangs, getApplicationContext());
                    recyclerView.setAdapter(adapter);
                    adapter.notifyDataSetChanged();
                    progressBar.setVisibility(View.GONE);
                }
            }

            @Override
            public void onFailure(Call<List<Hang>> call, Throwable t) {
                progressBar.setVisibility(View.GONE);
                //Toast.makeText(HangActivity.this, "Gọi API hãng thất bại", Toast.LENGTH_LONG).show();
            }
        });
    }

    @Override
    public boolean onSupportNavigateUp() {
        Intent intent = new Intent(HangActivity.this, MainActivity.class);
        finish();
        return super.onSupportNavigateUp();
    }
}