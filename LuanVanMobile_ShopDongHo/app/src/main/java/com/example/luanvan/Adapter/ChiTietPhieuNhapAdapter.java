package com.example.luanvan.Adapter;

import android.content.Context;
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
import com.example.luanvan.Model.ChiTietPN;
import com.example.luanvan.R;

import java.text.DecimalFormat;
import java.util.List;

public class ChiTietPhieuNhapAdapter extends RecyclerView.Adapter<ChiTietPhieuNhapAdapter.ChiTietPhieuNhapViewHolder> {
    List<ChiTietPN> chiTietPNS;
    Context context;

    public ChiTietPhieuNhapAdapter(List<ChiTietPN> chiTietPNS, Context context) {
        this.chiTietPNS = chiTietPNS;
        this.context = context;
    }

    @NonNull
    @Override
    public ChiTietPhieuNhapViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.custom_chitietphieunhap, parent, false);
        return new ChiTietPhieuNhapAdapter.ChiTietPhieuNhapViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull ChiTietPhieuNhapViewHolder holder, int position) {
        holder.textViewTenSP.setText(holder.textViewTenSP.getText() + chiTietPNS.get(position).getTenSP());
        holder.textViewSoLuong.setText("Số lượng: " + chiTietPNS.get(position).getSoLuong());
        DecimalFormat decimalFormat = new DecimalFormat("#,###");
        String donGia = decimalFormat.format(chiTietPNS.get(position).getDonGia());
        String tongTien = decimalFormat.format(chiTietPNS.get(position).getThanhTien());
        holder.textViewDonGia.setText(holder.textViewDonGia.getText() + donGia + "");
        holder.textViewTongTien.setText(holder.textViewTongTien.getText() + tongTien + "");

        String url = LinkAPI.BASE_URL + "Content/Images/SanPham/" + chiTietPNS.get(position).getHinhAnh();
        Glide.with(context).load(url)
                .diskCacheStrategy(DiskCacheStrategy.ALL)
                .into(holder.imageViewHinhSP);
    }

    @Override
    public int getItemCount() {
        return chiTietPNS.size();
    }


    public class ChiTietPhieuNhapViewHolder extends RecyclerView.ViewHolder{

        TextView textViewTenSP, textViewSoLuong, textViewDonGia, textViewTongTien;
        ImageView imageViewHinhSP;

        public ChiTietPhieuNhapViewHolder(@NonNull View itemView) {
            super(itemView);
            textViewTenSP = itemView.findViewById(R.id.textViewTenSP_ThongTinCTPN);
            textViewSoLuong = itemView.findViewById(R.id.textViewSoLuong_CTPN);
            textViewDonGia = itemView.findViewById(R.id.textViewDonGia_CTPN);
            textViewTongTien = itemView.findViewById(R.id.textViewTongTienPN);
            imageViewHinhSP = itemView.findViewById(R.id.imageViewSP_ChiTietPN);
        }
    }
}
