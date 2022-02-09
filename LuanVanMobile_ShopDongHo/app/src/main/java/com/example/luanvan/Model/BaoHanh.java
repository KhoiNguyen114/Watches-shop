package com.example.luanvan.Model;

import java.util.Date;

public class BaoHanh {
    Integer idHoaDon;
    String maSP;
    Date thoiGianBD;
    Date thoiGianKT;
    boolean tinhTrang;

    public Integer getIdHoaDon() {
        return idHoaDon;
    }

    public void setIdHoaDon(Integer idHoaDon) {
        this.idHoaDon = idHoaDon;
    }

    public String getMaSP() {
        return maSP;
    }

    public void setMaSP(String maSP) {
        this.maSP = maSP;
    }

    public Date getThoiGianBD() {
        return thoiGianBD;
    }

    public void setThoiGianBD(Date thoiGianBD) {
        this.thoiGianBD = thoiGianBD;
    }

    public Date getThoiGianKT() {
        return thoiGianKT;
    }

    public void setThoiGianKT(Date thoiGianKT) {
        this.thoiGianKT = thoiGianKT;
    }

    public boolean isTinhTrang() {
        return tinhTrang;
    }

    public void setTinhTrang(boolean tinhTrang) {
        this.tinhTrang = tinhTrang;
    }

    public BaoHanh(Integer idHoaDon, String maSP, Date thoiGianBD, Date thoiGianKT, boolean tinhTrang) {
        this.idHoaDon = idHoaDon;
        this.maSP = maSP;
        this.thoiGianBD = thoiGianBD;
        this.thoiGianKT = thoiGianKT;
        this.tinhTrang = tinhTrang;
    }
}
