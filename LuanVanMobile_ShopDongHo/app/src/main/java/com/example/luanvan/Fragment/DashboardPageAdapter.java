package com.example.luanvan.Fragment;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentActivity;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentStatePagerAdapter;
import androidx.lifecycle.Lifecycle;


public  class DashboardPageAdapter extends FragmentStatePagerAdapter {


    public DashboardPageAdapter(@NonNull FragmentManager fm, int behavior) {
        super(fm, behavior);
    }

    @NonNull
    @Override
    public Fragment getItem(int position) {
        switch (position){
            case 0 : return new StockFragment();
            case 1 : return new OrderFragment();
            case 2: return new RpFragment();
            default: return new StockFragment();
        }
    }

    @Override
    public int getCount() {
        return 3;
    }

    @Nullable
    @Override
    public CharSequence getPageTitle(int position) {
        String title = "";
        switch(position){
            case 0 :
                title = "Quản lý kho";
                break;
            case  1 :
                title = "Quản lý đơn hàng";
                break;
            case 2:
                title = "Thống kê và báo cáo";
                break;
            default:
                title = "Quản lý kho";
                break;
        }

        return title;
    }
}
