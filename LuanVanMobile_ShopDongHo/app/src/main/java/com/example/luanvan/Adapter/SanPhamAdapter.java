package com.example.luanvan.Adapter;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Filter;
import android.widget.Filterable;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;
import com.bumptech.glide.load.engine.DiskCacheStrategy;
import com.example.luanvan.API.LinkAPI;
import com.example.luanvan.Activity.ChiTietSanPhamActivity;
import com.example.luanvan.Model.SanPham;
import com.example.luanvan.R;

import java.text.DecimalFormat;
import java.util.ArrayList;
import java.util.List;

public class SanPhamAdapter extends RecyclerView.Adapter<SanPhamAdapter.SanPhamViewHolder> implements Filterable {

    List<SanPham> sanPhams;
    List<SanPham> sanPhamsFull;
    Context context;

    public SanPhamAdapter(List<SanPham> sanPhams, Context context) {
        this.sanPhams = sanPhams;
        this.context = context;
        this.sanPhamsFull = sanPhams;
    }

    @NonNull
    @Override
    public SanPhamViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int position) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.custom_san_pham,parent,false);

        return new SanPhamViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull SanPhamViewHolder holder, int position) {
        DecimalFormat decimalFormat = new DecimalFormat("#,###");
        String donGia = decimalFormat.format(sanPhams.get(position).getDONGIA());
        holder.textViewTenSP.setText(sanPhams.get(position).getTENSP());
        holder.textViewDonGiaSP.setText(donGia);

        String url = LinkAPI.BASE_URL + "Content/Images/SanPham/" + sanPhams.get(position).getHINHANH();
        Glide.with(context).load(url)
                .diskCacheStrategy(DiskCacheStrategy.ALL)
                .into(holder.imageView);

        SanPham sp = sanPhams.get(position);

        /*Picasso.Builder builder = new Picasso.Builder(context);
        builder.downloader(new OkHttp3Downloader(context));
        String url = LinkAPI.BASE_URL + "Content/Images/SanPham/" + sanPhams.get(position).getHINHANH();
        builder.build().load(url).fit().centerCrop().into(holder.imageView);*/

        holder.itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(context, ChiTietSanPhamActivity.class);
                intent.putExtra("ChiTietSP", sp);
                v.getContext().startActivity(intent);
            }
        });
    }

    @Override
    public int getItemCount() {
        return sanPhams.size();
    }

    @Override
    public Filter getFilter() {
        return new Filter() {
            @Override
            protected FilterResults performFiltering(CharSequence constraint) {
                String tukhoa = constraint.toString();
                if(tukhoa.isEmpty())
                {
                    sanPhams = sanPhamsFull;
                }
                else
                {
                    List<SanPham> ds = new ArrayList<>();
                    for(SanPham item: sanPhamsFull)
                    {
                        if(item.getTENSP().toLowerCase().contains(tukhoa.toLowerCase())
                                || item.getMASP().equals(tukhoa))
                        {
                            ds.add(item);
                        }
                    }
                    sanPhams = ds;
                }
                FilterResults results = new FilterResults();
                results.values = sanPhams;
                return results;
            }

            @Override
            protected void publishResults(CharSequence constraint, FilterResults results) {
                sanPhams = (List<SanPham>)results.values;
                notifyDataSetChanged();
            }
        };
    }

    public class SanPhamViewHolder extends RecyclerView.ViewHolder {
        ImageView imageView;
        TextView textViewTenSP, textViewDonGiaSP;

        public SanPhamViewHolder(@NonNull View itemView) {
            super(itemView);
            imageView = itemView.findViewById(R.id.imageViewHinhAnhSP);
            textViewTenSP = itemView.findViewById(R.id.textViewTenSP);
            textViewDonGiaSP = itemView.findViewById(R.id.textViewGiaSP);
        }

    }

   /* @Override
    public long getItemId(int position) {
        return sanPhams.get(position).hashCode();
    }*/
}
