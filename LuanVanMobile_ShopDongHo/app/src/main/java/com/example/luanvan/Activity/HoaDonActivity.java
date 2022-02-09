package com.example.luanvan.Activity;

import androidx.appcompat.app.ActionBar;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.widget.Toast;

import com.example.luanvan.API.HoaDonApi;
import com.example.luanvan.Adapter.HoaDonAdapter;
import com.example.luanvan.Model.hd;
import com.example.luanvan.R;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class HoaDonActivity extends AppCompatActivity {
    Integer type;
    ActionBar actionBar;
    List<hd> lsHD;
    HoaDonAdapter adapter;
    Context context;
    RecyclerView recyclerView;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        context = this.getApplicationContext();
        setContentView(R.layout.activity_hoa_don);
        Bundle extras = getIntent().getExtras();
        if(extras != null)
        {
            type = extras.getInt("type");

        }
        Log.e("Test ","Test type : " + type);
        getSupportActionBar().setTitle("Danh sách hóa đơn");
        getSupportActionBar().setBackgroundDrawable(getResources().getDrawable(R.drawable.custom_actionbar));
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        lsHD = new ArrayList<>();
        getHoaDons();
        adapter = new HoaDonAdapter(lsHD,context);

        recyclerView = findViewById(R.id.recHoadon);
        recyclerView.setHasFixedSize(true);
        recyclerView.setNestedScrollingEnabled(false);
        recyclerView.setLayoutManager(new LinearLayoutManager(HoaDonActivity.this,LinearLayoutManager.VERTICAL,false));
        recyclerView.setItemViewCacheSize(40);
        recyclerView.setAdapter(adapter);
    }
    @Override
    public boolean onSupportNavigateUp() {
        Intent intent = new Intent(HoaDonActivity.this, MainActivity.class);
        finish();
        return super.onSupportNavigateUp();
    }
    public void getHoaDons(){
        HoaDonApi.HoaDonService.getListHD(type).enqueue(new Callback<List<hd>>() {
            @Override
            public void onResponse(Call<List<hd>> call, Response<List<hd>> response) {
                if(response.isSuccessful() && response.body() != null)
                {
                    lsHD.addAll(response.body());
                    adapter.notifyDataSetChanged();
                }
            }

            @Override
            public void onFailure(Call<List<hd>> call, Throwable t) {
                Toast.makeText(context, "Gọi API hóa đơn thất bại", Toast.LENGTH_LONG).show();
                Log.e("fail",t.toString());
            }
        });
    }
}