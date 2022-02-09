package com.example.luanvan.Model;

import java.math.BigInteger;

public class ChiTietPN {
    String tenSP ;
    String hinhAnh;
    Integer soLuong;
    BigInteger donGia;
    BigInteger thanhTien;

    public String getTenSP() {
        return tenSP;
    }

    public void setTenSP(String tenSP) {
        this.tenSP = tenSP;
    }

    public String getHinhAnh() {
        return hinhAnh;
    }

    public void setHinhAnh(String hinhAnh) {
        this.hinhAnh = hinhAnh;
    }

    public Integer getSoLuong() {
        return soLuong;
    }

    public void setSoLuong(Integer soLuong) {
        this.soLuong = soLuong;
    }

    public BigInteger getDonGia() {
        return donGia;
    }

    public void setDonGia(BigInteger donGia) {
        this.donGia = donGia;
    }

    public BigInteger getThanhTien() {
        return thanhTien;
    }

    public void setThanhTien(BigInteger thanhTien) {
        this.thanhTien = thanhTien;
    }

    public ChiTietPN(String tenSP, String hinhAnh, Integer soLuong, BigInteger donGia, BigInteger thanhTien) {
        this.tenSP = tenSP;
        this.hinhAnh = hinhAnh;
        this.soLuong = soLuong;
        this.donGia = donGia;
        this.thanhTien = thanhTien;
    }
}
