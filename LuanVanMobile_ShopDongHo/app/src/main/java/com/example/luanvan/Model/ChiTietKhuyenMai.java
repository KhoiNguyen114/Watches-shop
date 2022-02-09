package com.example.luanvan.Model;

public class ChiTietKhuyenMai {
    String maKM;
    String maSP;

    public String getMaKM() {
        return maKM;
    }

    public void setMaKM(String maKM) {
        this.maKM = maKM;
    }

    public String getMaSP() {
        return maSP;
    }

    public void setMaSP(String maSP) {
        this.maSP = maSP;
    }

    public ChiTietKhuyenMai(String maKM, String maSP) {
        this.maKM = maKM;
        this.maSP = maSP;
    }

    public ChiTietKhuyenMai() {
    }
}
