package com.example.luanvan.Model;

import java.math.BigInteger;
import java.util.Date;

public class ThongKe {
    String maHang;
    String maSP;
    String tenSP;
    Integer soLuongBanRa;
    Integer soLuongNhap;
    Integer doanhSo;
    Integer tonKho;

    public void setMaHang(String maHang){
        this.maHang = maHang;
    }

    public String getMaHang(){
        return this.maHang;
    }

    public void setMaSP(String maSP){
        this.maSP = maSP;
    }

    public String getMaSP(){
        return  this.maSP;
    }

    public void setTenSP(String tenSP){
        this.tenSP = tenSP;
    }

    public String getTenSP(){
        return this.tenSP;
    }

    public void setsoLuongBanRa(Integer soLuongBanRa)
    {
        this.soLuongBanRa = soLuongBanRa;
    }
    public Integer getsoLuongBanRa() {
        return this.soLuongBanRa;
    }

    public void setDoanhSo(Integer doanhSo)
    {
        this.doanhSo = doanhSo;
    }
    public Integer getDoanhSo() {
        return this.doanhSo;
    }

    public void setsoLuongNhap(Integer soLuongNhap)
    {
        this.soLuongNhap = soLuongNhap;
    }
    public Integer getsoLuongNhap() {
        return this.soLuongNhap;
    }

    public void settonKho(Integer tonKho)
    {
        this.tonKho = tonKho;
    }
    public Integer gettonKho() {
        return this.tonKho;
    }


    public ThongKe(String maHang,String maSP,String tenSP,Integer soLuongBanRa,Integer soLuongNhap, Integer doanhSo, Integer tonKho) {
        this.maHang = maHang;
        this.maSP = maSP;
        this.tenSP = tenSP;
        this.soLuongBanRa = soLuongBanRa;
        this.doanhSo = doanhSo;
        this.soLuongNhap = soLuongNhap;
        this.tonKho = tonKho;
    }

}
