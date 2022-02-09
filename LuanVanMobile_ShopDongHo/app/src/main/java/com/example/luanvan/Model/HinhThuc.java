package com.example.luanvan.Model;

public class HinhThuc {
    Integer maHinhThuc;
    String tenHinhThuc;

    public Integer getMaHinhThuc() {
        return maHinhThuc;
    }

    public void setMaHinhThuc(Integer maHinhThuc) {
        this.maHinhThuc = maHinhThuc;
    }

    public String getTenHinhThuc() {
        return tenHinhThuc;
    }

    public void setTenHinhThuc(String tenHinhThuc) {
        this.tenHinhThuc = tenHinhThuc;
    }

    public HinhThuc() {
    }

    public HinhThuc(Integer maHinhThuc, String tenHinhThuc) {
        this.maHinhThuc = maHinhThuc;
        this.tenHinhThuc = tenHinhThuc;
    }
}
