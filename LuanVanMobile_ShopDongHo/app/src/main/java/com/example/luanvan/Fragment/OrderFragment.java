package com.example.luanvan.Fragment;

import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;

import com.example.luanvan.API.HoaDonApi;
import com.example.luanvan.API.SanPhamService;
    
import com.example.luanvan.Activity.HangActivity;
import com.example.luanvan.Activity.HoaDonActivity;
import com.example.luanvan.R;

/**
 * A simple {@link Fragment} subclass.
 * Use the {@link OrderFragment#newInstance} factory method to
 * create an instance of this fragment.
 */
public class OrderFragment extends Fragment {

    // TODO: Rename parameter arguments, choose names that match
    // the fragment initialization parameters, e.g. ARG_ITEM_NUMBER
    private static final String ARG_PARAM1 = "param1";
    private static final String ARG_PARAM2 = "param2";

    // TODO: Rename and change types of parameters
    private String mParam1;
    private String mParam2;


    public OrderFragment() {
        // Required empty public constructor
    }

    /**
     * Use this factory method to create a new instance of
     * this fragment using the provided parameters.
     *
     * @param param1 Parameter 1.
     * @param param2 Parameter 2.
     * @return A new instance of fragment OrderFragment.
     */
    // TODO: Rename and change types and number of parameters
    public static OrderFragment newInstance(String param1, String param2) {
        OrderFragment fragment = new OrderFragment();
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
        View view = inflater.inflate(R.layout.fragment_order,container,false);
        // Inflate the layout for this fragment
        return view;
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        Button btnAllHoaDon,btnHoaDonComplete,btnHoaDonProcess,btnHoaDonTransport,btnHoaDonCanceled;
        btnAllHoaDon = view.findViewById(R.id.btnAllHoaDon);
        btnHoaDonComplete = view.findViewById(R.id.btnHoaDonComplete);
        btnHoaDonProcess = view.findViewById(R.id.btnHoaDonInProcess);
        btnHoaDonTransport = view.findViewById(R.id.btnHoaDon_transport);
        btnHoaDonCanceled = view.findViewById(R.id.btnHoaDonCanceled);

        btnAllHoaDon.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Integer type = 0;
                Intent intent = new Intent(getActivity(), HoaDonActivity.class);
                intent.putExtra("type",type);
                startActivity(intent);
            }
        });
        btnHoaDonComplete.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Integer type = 3;
                Intent intent = new Intent(getActivity(), HoaDonActivity.class);
                intent.putExtra("type",type);
                startActivity(intent);
            }
        });
        btnHoaDonProcess.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Integer type = 1;
                Intent intent = new Intent(getActivity(), HoaDonActivity.class);
                intent.putExtra("type",type);
                startActivity(intent);
            }
        });
        btnHoaDonTransport.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Integer type = 2;
                Intent intent = new Intent(getActivity(), HoaDonActivity.class);
                intent.putExtra("type",type);
                startActivity(intent);
            }
        });
        btnHoaDonCanceled.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Integer type = 4;
                Intent intent = new Intent(getActivity(), HoaDonActivity.class);
                intent.putExtra("type",type);
                startActivity(intent);
            }
        });
        super.onViewCreated(view, savedInstanceState);
    }


}