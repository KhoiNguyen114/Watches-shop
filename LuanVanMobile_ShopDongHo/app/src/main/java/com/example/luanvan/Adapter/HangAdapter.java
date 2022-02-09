package com.example.luanvan.Adapter;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;
import com.bumptech.glide.load.engine.DiskCacheStrategy;
import com.example.luanvan.API.LinkAPI;
import com.example.luanvan.Activity.ChiTietHangActivity;
import com.example.luanvan.Model.Hang;
import com.example.luanvan.R;

import java.util.List;

public class HangAdapter extends RecyclerView.Adapter<HangAdapter.HangViewHolder> {
    List<Hang> hangs;
    Context context;

    public HangAdapter(List<Hang> hangs, Context context) {
        this.hangs = hangs;
        this.context = context;
    }

    @NonNull
    @Override
    public HangViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.custom_hang,parent,false);

        return new HangViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull HangViewHolder holder, int position) {
        holder.textViewTenHang.setText(hangs.get(position).getTENHANG());
        holder.textViewQuocGia.setText(hangs.get(position).getQUOCGIA());

        String url = LinkAPI.BASE_URL + "Content/Images/Brand_Logo/" + hangs.get(position).getLOGO();
        Glide.with(context).load(url)
                .diskCacheStrategy(DiskCacheStrategy.ALL)
                .into(holder.imageViewLogoHang);

        Hang hang = hangs.get(position);
        holder.itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(context, ChiTietHangActivity.class);
                intent.putExtra("ChiTietHang", hang);
                v.getContext().startActivity(intent);
            }
        });
    }

    @Override
    public int getItemCount() {
        return hangs.size();
    }


    public class HangViewHolder extends RecyclerView.ViewHolder{

        ImageView imageViewLogoHang;
        TextView textViewTenHang, textViewQuocGia;
        public HangViewHolder(@NonNull View itemView) {
            super(itemView);
            imageViewLogoHang = itemView.findViewById(R.id.imageViewLogoHang);
            textViewTenHang = itemView.findViewById(R.id.textViewTenHang);
            textViewQuocGia = itemView.findViewById(R.id.textViewQuocGiaHang);
        }
    }
}
