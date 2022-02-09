package com.example.luanvan.Adapter;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.luanvan.API.LinkAPI;
import com.example.luanvan.Model.ChiTietHD;
import com.example.luanvan.Model.sp;
import com.example.luanvan.R;
import com.jakewharton.picasso.OkHttp3Downloader;
import com.squareup.picasso.Picasso;

import java.text.DecimalFormat;
import java.util.List;

public class ChitietHDAdapter extends RecyclerView.Adapter<ChitietHDAdapter.ChitieteHDAdapterViewHolder> {
    Context mContext;
    List<sp> lsSP;

    public ChitietHDAdapter(Context context,List<sp> lsSP)
    {
        this.mContext = context;
        this.lsSP = lsSP;
    }

    @NonNull
    @Override
    public ChitieteHDAdapterViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View mView = LayoutInflater.from(parent.getContext()).inflate(R.layout.custom_chitiethd_sanpham,parent,false);
        return new ChitieteHDAdapterViewHolder(mView);
    }

    @Override
    public void onBindViewHolder(@NonNull ChitieteHDAdapterViewHolder holder, int position) {
        holder.tenSP.setText(lsSP.get(position).getTenSP());
        Log.e("Check in bind","Check : " + lsSP.get(position).getTenSP());
        Log.e("Check so luong in bind","check : " + lsSP.get(position).getSoLuong());
        holder.soLuong.setText(lsSP.get(position).getSoLuong().toString());
        Picasso.Builder builder = new Picasso.Builder(mContext);
        builder.downloader(new OkHttp3Downloader(mContext));
        String url = LinkAPI.BASE_URL + "Content/Images/SanPham/" + lsSP.get(position).getHinhAnh();
        builder.build().load(url).fit().centerCrop().into((ImageView) holder.hinhAnh);

    }

    @Override
    public int getItemCount() {
        return lsSP.size();
    }

    public class ChitieteHDAdapterViewHolder extends RecyclerView.ViewHolder {
        TextView tenSP,soLuong;
        ImageView hinhAnh;
        public ChitieteHDAdapterViewHolder(@NonNull View itemView) {
            super(itemView);
            tenSP = itemView.findViewById(R.id.ctTenSP);
            soLuong = itemView.findViewById(R.id.ctSoLuong);
            hinhAnh = itemView.findViewById(R.id.ctImageSP);
        }
    }

}
