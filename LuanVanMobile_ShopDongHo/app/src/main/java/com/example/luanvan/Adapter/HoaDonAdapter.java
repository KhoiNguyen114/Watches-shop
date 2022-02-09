package com.example.luanvan.Adapter;

import android.content.Context;
import android.content.Intent;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Filter;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.luanvan.Activity.ChitietHDActivity;
import com.example.luanvan.Model.HoaDon;
import com.example.luanvan.Model.SanPham;
import com.example.luanvan.Model.hd;
import com.example.luanvan.R;

import java.text.DecimalFormat;
import java.util.List;

public class HoaDonAdapter  extends RecyclerView.Adapter<HoaDonAdapter.HoaDonViewHolder>{

     List<hd> lsHD;
     Context mContext;

    public HoaDonAdapter(List<hd> lsHD, Context context){
        this.lsHD = lsHD;
        this.mContext = context;
    }

    @NonNull
    @Override
    public HoaDonViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.layout1donghoadon,parent,false);
        return new HoaDonViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull HoaDonViewHolder holder, int position) {
        DecimalFormat decimalFormat = new DecimalFormat("#,###");
        holder.maHoaDon.setText( lsHD.get(position).maHoaDon);
        holder.ngayLap.setText( lsHD.get(position).ngayLap);
        holder.tinhTrang.setText( lsHD.get(position).tinhTrang);
        holder.hinhThuc.setText( lsHD.get(position).hinhThuc);
        holder.tongTien.setText( decimalFormat.format(lsHD.get(position).tongTien));
        hd thisHD = lsHD.get(position);
        holder.itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Log.e("ct","Ma Hoa Don : " + thisHD.idHoaDon);
                Intent i = new Intent(mContext,ChitietHDActivity.class);
                i.putExtra("idHD",thisHD.idHoaDon);
                view.getContext().startActivity(i);
            }
        });
    }


    @Override
    public int getItemCount() {
        return lsHD.size();
    }

    public class HoaDonViewHolder extends RecyclerView.ViewHolder{
        TextView maHoaDon,tinhTrang,ngayLap,hinhThuc,tongTien;

        public HoaDonViewHolder(@NonNull View itemView) {
            super(itemView);
            maHoaDon = itemView.findViewById(R.id.maDON);
            tinhTrang = itemView.findViewById(R.id.tinhTrang);
            ngayLap = itemView.findViewById(R.id.ngayLap);
            hinhThuc = itemView.findViewById(R.id.hinhThuc);
            tongTien = itemView.findViewById(R.id.tongTien);
        }
    }
}
