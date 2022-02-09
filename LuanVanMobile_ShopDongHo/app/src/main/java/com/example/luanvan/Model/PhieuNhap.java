package com.example.luanvan.Model;

import java.io.Serializable;
import java.math.BigInteger;

public class PhieuNhap implements Serializable {
    Integer IDPhieuNhap;
    String maPN;
    String tenNV;
    String tenHang;
    String ngayLap;
    BigInteger tongTien;
    boolean tinhTrang;
    boolean trangThai;

    public Integer getIDPhieuNhap() {
        return IDPhieuNhap;
    }

    public void setIDPhieuNhap(Integer IDPhieuNhap) {
        this.IDPhieuNhap = IDPhieuNhap;
    }

    public String getMaPN() {
        return maPN;
    }

    public void setMaPN(String maPN) {
        this.maPN = maPN;
    }

    public String getTenNV() {
        return tenNV;
    }

    public void setTenNV(String tenNV) {
        this.tenNV = tenNV;
    }

    public String getTenHang() {
        return tenHang;
    }

    public void setTenHang(String tenHang) {
        this.tenHang = tenHang;
    }

    public String getNgayLap() {
        return ngayLap;
    }

    public void setNgayLap(String ngayLap) {
        this.ngayLap = ngayLap;
    }

    public BigInteger getTongTien() {
        return tongTien;
    }

    public void setTongTien(BigInteger tongTien) {
        this.tongTien = tongTien;
    }

    public boolean isTinhTrang() {
        return tinhTrang;
    }

    public void setTinhTrang(boolean tinhTrang) {
        this.tinhTrang = tinhTrang;
    }

    public boolean isTrangThai() {
        return trangThai;
    }

    public void setTrangThai(boolean trangThai) {
        this.trangThai = trangThai;
    }

    public PhieuNhap(Integer IDPhieuNhap, String maPN, String tenNV, String tenHang, String ngayLap, BigInteger tongTien, boolean tinhTrang, boolean trangThai) {
        this.IDPhieuNhap = IDPhieuNhap;
        this.maPN = maPN;
        this.tenNV = tenNV;
        this.tenHang = tenHang;
        this.ngayLap = ngayLap;
        this.tongTien = tongTien;
        this.tinhTrang = tinhTrang;
        this.trangThai = trangThai;
    }
}
