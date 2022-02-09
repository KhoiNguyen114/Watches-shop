package com.example.luanvan.Model;

import java.math.BigInteger;
import java.util.Date;

public class HoaDon {
    Integer idHoaDon;
    String maHD;
    Integer idKhachHang;
    String maNV;
    Integer maHinhThucThanhToan;
    Date ngayLap;
    BigInteger tongTien;
    Integer tinhTrang;
    String ghiChu;
    boolean trangThai;

    public Integer getIdHoaDon() {
        return idHoaDon;
    }

    public void setIdHoaDon(Integer idHoaDon) {
        this.idHoaDon = idHoaDon;
    }

    public String getMaHD() {
        return maHD;
    }

    public void setMaHD(String maHD) {
        this.maHD = maHD;
    }

    public Integer getIdKhachHang() {
        return idKhachHang;
    }

    public void setIdKhachHang(Integer idKhachHang) {
        this.idKhachHang = idKhachHang;
    }

    public String getMaNV() {
        return maNV;
    }

    public void setMaNV(String maNV) {
        this.maNV = maNV;
    }

    public Integer getMaHinhThucThanhToan() {
        return maHinhThucThanhToan;
    }

    public void setMaHinhThucThanhToan(Integer maHinhThucThanhToan) {
        this.maHinhThucThanhToan = maHinhThucThanhToan;
    }

    public Date getNgayLap() {
        return ngayLap;
    }

    public void setNgayLap(Date ngayLap) {
        this.ngayLap = ngayLap;
    }

    public BigInteger getTongTien() {
        return tongTien;
    }

    public void setTongTien(BigInteger tongTien) {
        this.tongTien = tongTien;
    }

    public Integer getTinhTrang() {
        return tinhTrang;
    }

    public void setTinhTrang(Integer tinhTrang) {
        this.tinhTrang = tinhTrang;
    }

    public String getGhiChu() {
        return ghiChu;
    }

    public void setGhiChu(String ghiChu) {
        this.ghiChu = ghiChu;
    }

    public boolean isTrangThai() {
        return trangThai;
    }

    public void setTrangThai(boolean trangThai) {
        this.trangThai = trangThai;
    }

    public HoaDon() {
    }

    public HoaDon(Integer idHoaDon, String maHD, Integer idKhachHang, String maNV, Integer maHinhThucThanhToan, Date ngayLap, BigInteger tongTien, Integer tinhTrang, String ghiChu, boolean trangThai) {
        this.idHoaDon = idHoaDon;
        this.maHD = maHD;
        this.idKhachHang = idKhachHang;
        this.maNV = maNV;
        this.maHinhThucThanhToan = maHinhThucThanhToan;
        this.ngayLap = ngayLap;
        this.tongTien = tongTien;
        this.tinhTrang = tinhTrang;
        this.ghiChu = ghiChu;
        this.trangThai = trangThai;
    }
}
