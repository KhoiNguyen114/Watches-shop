package com.example.luanvan.Activity;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;

import android.os.Bundle;
import android.view.MenuItem;
import android.widget.EditText;
import android.widget.LinearLayout;

import com.example.luanvan.Fragment.AccountFragment;
import com.example.luanvan.Fragment.DashboardFragment;
import com.example.luanvan.Fragment.HomeFragment;
import com.example.luanvan.R;
import com.google.android.material.bottomnavigation.BottomNavigationView;

public class RpActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_rp);
    }
}