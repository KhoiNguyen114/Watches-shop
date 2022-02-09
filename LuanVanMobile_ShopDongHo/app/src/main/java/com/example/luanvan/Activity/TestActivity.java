package com.example.luanvan.Activity;

import android.os.Bundle;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.luanvan.API.RetrofitInterface;
import com.example.luanvan.Adapter.TestAdapter;
import com.example.luanvan.Model.RetrofitClient;
import com.example.luanvan.Model.Test;
import com.example.luanvan.R;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class TestActivity extends AppCompatActivity {

    RecyclerView recyclerView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_test);

        recyclerView = findViewById(R.id.recyclerView_API);
        recyclerView.setLayoutManager(new LinearLayoutManager(TestActivity.this));
        RetrofitInterface retrofitClient = RetrofitClient.getClient().create(RetrofitInterface.class);
        Call<List<Test>> listCall = retrofitClient.getAllPhotos();
        listCall.enqueue(new Callback<List<Test>>() {
            @Override
            public void onResponse(Call<List<Test>> call, Response<List<Test>> response) {
                parseData(response.body());
            }

            @Override
            public void onFailure(Call<List<Test>> call, Throwable t) {

            }
        });
    }

    private void parseData(List<Test> ds)
    {
        TestAdapter testAdapter = new TestAdapter(ds, getApplicationContext());
        recyclerView.setAdapter(testAdapter);
    }
}