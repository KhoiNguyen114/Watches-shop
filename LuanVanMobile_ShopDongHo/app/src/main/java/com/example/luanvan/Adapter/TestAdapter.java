package com.example.luanvan.Adapter;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.luanvan.Model.Test;
import com.example.luanvan.R;
import com.jakewharton.picasso.OkHttp3Downloader;
import com.squareup.picasso.Picasso;

import java.util.List;

public class TestAdapter extends RecyclerView.Adapter<TestAdapter.RecyclerHolder>{

    List<Test> testList;
    Context context;

    public TestAdapter(List<Test> testList, Context context) {
        this.testList = testList;
        this.context = context;
    }

    @NonNull
    @Override
    public RecyclerHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.custome_test, parent, false);
        return new RecyclerHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull RecyclerHolder holder, int position) {
        holder.textView.setText(testList.get(position).getTitle());
        Picasso.Builder builder = new Picasso.Builder(context);
        builder.downloader(new OkHttp3Downloader(context));
        builder.build().load(testList.get(position).getUrl()).placeholder(R.drawable.ic_launcher_background).error(R.drawable.ic_launcher_foreground).into(holder.imageView);

    }

    @Override
    public int getItemCount() {
        return testList.size();
    }

    public class RecyclerHolder extends RecyclerView.ViewHolder {
        TextView textView;
        ImageView imageView;

        public RecyclerHolder(@NonNull View itemView) {
            super(itemView);
            textView = itemView.findViewById(R.id.textView_API);
            imageView = itemView.findViewById(R.id.image_API);
        }
    }
}

