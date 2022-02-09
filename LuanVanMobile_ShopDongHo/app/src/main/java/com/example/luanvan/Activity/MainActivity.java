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

public class MainActivity extends AppCompatActivity {
    BottomNavigationView bottomNavigationView;
    Fragment fragment;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        bottomNavigationView = findViewById(R.id.bottomNavigationView);
        getSupportActionBar().hide();
        loadFragment(new HomeFragment());

        BottomNavigationView.OnNavigationItemSelectedListener onNavigationItemSelectedListener = new BottomNavigationView.OnNavigationItemSelectedListener() {
            @Override
            public boolean onNavigationItemSelected(@NonNull MenuItem item) {
                switch (item.getItemId())
                {
                    case R.id.navigation_home:
                        //getSupportActionBar().setTitle("Home");
                        getSupportActionBar().hide();
                        fragment = new HomeFragment();
                        loadFragment(fragment);
                        return true;
                    case R.id.navigation_dashboard:
                        //getSupportActionBar().setTitle("Review");
                        fragment = new DashboardFragment();
                        getSupportActionBar().hide();
                        loadFragment(fragment);
                        return true;
                    case R.id.navigation_account:
                        //getSupportActionBar().setTitle("Me");
                        getSupportActionBar().hide();
                        fragment = new AccountFragment();
                        loadFragment(fragment);
                        return true;
                }
                return false;
            }
        };
        bottomNavigationView.setOnNavigationItemSelectedListener(onNavigationItemSelectedListener);


    }

    public void loadFragment(Fragment fragment)
    {
        FragmentTransaction fragmentTransaction = getSupportFragmentManager().beginTransaction();
        fragmentTransaction.replace(R.id.nav_main,fragment);
        fragmentTransaction.addToBackStack(null);
        fragmentTransaction.commit();
    }
}