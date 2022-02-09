package com.example.luanvan.Adapter;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Filter;
import android.widget.Filterable;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.luanvan.Activity.ChiTietPhieuNhapActivity;
import com.example.luanvan.Model.PhieuNhap;
import com.example.luanvan.R;

import java.text.DecimalFormat;
import java.util.ArrayList;
import java.util.List;

public class PhieuNhapAdapter extends RecyclerView.Adapter<PhieuNhapAdapter.PhieuNhapViewHolder> implements Filterable {
    List<PhieuNhap> phieuNhaps;
    List<PhieuNhap> phieuNhapFull;
    Context context;

    public PhieuNhapAdapter(List<PhieuNhap> PhieuNhaps, Context context) {
        this.phieuNhaps = PhieuNhaps;
        this.phieuNhapFull = PhieuNhaps;
        this.context = context;
    }

    @NonNull
    @Override
    public PhieuNhapAdapter.PhieuNhapViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.custom_nhap_hang, parent, false);

        return new PhieuNhapAdapter.PhieuNhapViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull PhieuNhapAdapter.PhieuNhapViewHolder holder, int position) {
        holder.textViewMaPN.setText(phieuNhaps.get(position).getMaPN());
        holder.textViewNgayLap.setText(phieuNhaps.get(position).getNgayLap() + "");
        DecimalFormat decimalFormat = new DecimalFormat("#,###");
        String donGia = decimalFormat.format(phieuNhaps.get(position).getTongTien());
        holder.textViewTongTienPN.setText(donGia);
        holder.textViewTenHangPN.setText(phieuNhaps.get(position).getTenHang());

        PhieuNhap PhieuNhap = phieuNhaps.get(position);
        holder.itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(context, ChiTietPhieuNhapActivity.class);
                intent.putExtra("ChiTietPhieuNhap", PhieuNhap);
                v.getContext().startActivity(intent);
            }
        });
    }

    @Override
    public int getItemCount() {
        return phieuNhaps.size();
    }

    @Override
    public Filter getFilter() {
        return new Filter() {
            @Override
            protected FilterResults performFiltering(CharSequence constraint) {
                String tukhoa = constraint.toString();
                if (tukhoa.isEmpty()) {
                    phieuNhaps = phieuNhapFull;
                } else {
                    List<PhieuNhap> ds = new ArrayList<>();
                    for (PhieuNhap item : phieuNhapFull) {
                        if (item.getMaPN().equals(tukhoa)) {
                            ds.add(item);
                        }
                    }
                    phieuNhaps = ds;
                }
                FilterResults results = new FilterResults();
                results.values = phieuNhaps;
                return results;
            }

            @Override
            protected void publishResults(CharSequence constraint, FilterResults results) {
                phieuNhaps = (List<PhieuNhap>) results.values;
                notifyDataSetChanged();
            }
        };
    }


    public class PhieuNhapViewHolder extends RecyclerView.ViewHolder {

        TextView textViewMaPN, textViewNgayLap, textViewTongTienPN, textViewTenHangPN;

        public PhieuNhapViewHolder(@NonNull View itemView) {
            super(itemView);
            textViewMaPN = itemView.findViewById(R.id.textViewMaPhieuNhap);
            textViewNgayLap = itemView.findViewById(R.id.textViewNgayLapPN);
            textViewTongTienPN = itemView.findViewById(R.id.textViewTongTienPN);
            textViewTenHangPN = itemView.findViewById(R.id.textViewTenHangPN);
        }
    }
}
