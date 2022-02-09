package com.example.luanvan.Fragment;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;

import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.Spinner;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;
import android.widget.Toast;

import com.example.luanvan.API.HoaDonApi;
import com.example.luanvan.API.ThongKeAPI;
import com.example.luanvan.Activity.HoaDonActivity;
import com.example.luanvan.Model.ThongKe;
import com.example.luanvan.Model.hd;
import com.example.luanvan.R;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * A simple {@link Fragment} subclass.
 * Use the {@link RpFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class RpFragment extends Fragment {

    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;

    public RpFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment RpFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static RpFragment newInstance(String param1, String param2) {
        RpFragment fragment = new RpFragment();
        Bundle args = new Bundle();
        args.putString(ARG_PARAM1, param1);
        args.putString(ARG_PARAM2, param2);
        fragment.setArguments(args);
        return fragment;
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {
            mParam1 = getArguments().getString(ARG_PARAM1);
            mParam2 = getArguments().getString(ARG_PARAM2);
        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        return inflater.inflate(R.layout.fragment_rp, container, false);
    }
    TableLayout mTable;
    Spinner mSpinner;
    Button mButton_rp;
    int selectedYear = 0;
    List<ThongKe> lsTK;
    Context context;
    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        mSpinner = view.findViewById(R.id.year_dropdown);
        mTable = view.findViewById(R.id.tableThongKe);
        mButton_rp = view.findViewById(R.id.report);
        lsTK = new ArrayList<>();
        context = getContext();
        ArrayList<String> years = new ArrayList<String>();
        Date rootDay = new Date();
        int year = Calendar.getInstance().get(Calendar.YEAR);
        for(int i = 2015;i< year;i++)
        {
            years.add(""+i);
        }
        years.add(""+year);
        ArrayAdapter<String> adapter= new ArrayAdapter<String>(getContext(),android.R.layout.simple_spinner_dropdown_item, years);
        mSpinner.setAdapter(adapter);
        Log.e("checkYear","Year : " + year);


        mButton_rp.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                selectedYear=  Integer.parseInt(mSpinner.getSelectedItem().toString());
                Log.e("checkSelectedYear","Selected Year : " + selectedYear);
                getThongKes();
            }
        });
    }
    public void getThongKes(){
        ThongKeAPI.ThongKeService.getListThongke(selectedYear).enqueue(new Callback<List<ThongKe>>() {
            @Override
            public void onResponse(Call<List<ThongKe>> call, Response<List<ThongKe>> response) {
                if(response.isSuccessful() && response.body() != null)
                {
                    lsTK.addAll(response.body());
                    Log.e("TestAPI","Lay phan tu dau : "+lsTK.get(0).getMaHang());
                    Log.e("Test APi2 ","So luong phan tu trong arrayList : " + lsTK.size());
                    Log.e("Test API3","So luong ban ra : " + lsTK.get(0).getsoLuongNhap());
                    for(int i =0;i< lsTK.size();i++)
                    {
                        TableRow row = new TableRow(context);
                        TableRow.LayoutParams lp = new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT);
                        row.setLayoutParams(lp);
                        TextView tenSP = new TextView(context);
                        tenSP.setText(lsTK.get(i).getTenSP());
                        TextView soLuongNhap = new TextView(context);
                        soLuongNhap.setText(""+lsTK.get(i).getsoLuongNhap());
                        TextView soLuongBan = new TextView(context);
                        soLuongBan.setText(""+lsTK.get(i).getsoLuongBanRa());
                        TextView tonKho = new TextView(context);
                        tonKho.setText(""+lsTK.get(i).gettonKho());
                        row.addView(tenSP);
                        row.addView(soLuongNhap);
                        row.addView(soLuongBan);
                        row.addView(tonKho);
                        mTable.addView(row);
                    }
                }
            }

            @Override
            public void onFailure(Call<List<ThongKe>> call, Throwable t) {
                Toast.makeText(context, "Gọi API hóa đơn thất bại", Toast.LENGTH_LONG).show();
                Log.e("fail",t.toString());
            }
        });
    }
}