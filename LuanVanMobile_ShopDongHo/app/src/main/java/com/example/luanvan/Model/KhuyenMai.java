package com.example.luanvan.Model;

import java.util.Date;

public class KhuyenMai {
    String maKM;
    Date thoiGianBD;
    Date thoiGianKT;
    Integer khuyenMai;
    String tinhTrang;
    boolean trangThai;

    public String getMaKM() {
        return maKM;
    }

    public void setMaKM(String maKM) {
        this.maKM = maKM;
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

    public Integer getKhuyenMai() {
        return khuyenMai;
    }

    public void setKhuyenMai(Integer khuyenMai) {
        this.khuyenMai = khuyenMai;
    }

    public String getTinhTrang() {
        return tinhTrang;
    }

    public void setTinhTrang(String tinhTrang) {
        this.tinhTrang = tinhTrang;
    }

    public boolean getTrangThai() {
        return trangThai;
    }

    public void setTrangThai(boolean trangThai) {
        this.trangThai = trangThai;
    }

    public KhuyenMai() {
    }

    public KhuyenMai(String maKM, Date thoiGianBD, Date thoiGianKT, Integer khuyenMai, String tinhTrang, boolean trangThai) {
        this.maKM = maKM;
        this.thoiGianBD = thoiGianBD;
        this.thoiGianKT = thoiGianKT;
        this.khuyenMai = khuyenMai;
        this.tinhTrang = tinhTrang;
        this.trangThai = trangThai;
    }
}
