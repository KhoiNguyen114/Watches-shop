package com.example.luanvan.Model;

import java.math.BigInteger;
import java.util.List;

public class ChiTietHD {
    Integer idHoaDon;
    String maHoaDon;
    String ngayLap;
    String tinhTrang;
    String hinhThuc;
    long tongTien;
    List<sp> dsSP;
    String tenKH;
    String diaChi;
    String soDT;
    String gioiTinh;
    String soCMND;

    public ChiTietHD(){

    }

    public ChiTietHD(Integer idHoaDon,String maHoaDon,String ngayLap,String tinhTrang,String hinhThuc, long tongTien, List<sp> dsSP
    ,String tenKH,String diaChi,String soDT, String gioiTinh,String soCMND)
    {
        this.idHoaDon = idHoaDon;
        this.maHoaDon = maHoaDon;
        this.ngayLap = ngayLap;
        this.tinhTrang = tinhTrang;
        this.hinhThuc = hinhThuc;
        this.tongTien = tongTien;
        this.dsSP = dsSP;
        this.tenKH = tenKH;
        this.diaChi=diaChi;
        this.soDT = soDT;
        this.gioiTinh = gioiTinh;
        this.soCMND = soCMND;
    }

    public Integer getIdHoaDon(){
        return this.idHoaDon;
    }
    public String getTenKH(){
        return this.tenKH;
    }
    public String getDiaChi(){
        return this.diaChi;
    }
    public String getSoDT(){
        return this.soDT;
    }
    public String getGioiTinh(){
        return this.gioiTinh;
    }
    public String getSoCMND(){
        return this.soCMND;
    }
    public String getMaHoaDon(){
        return this.maHoaDon;
    }
    public String getNgayLap(){
        return this.ngayLap;
    }
    public String getTinhTrang(){
        return this.tinhTrang;
    }
    public String getHinhThuc(){
        return this.hinhThuc;
    }
    public Long getTongTien(){
        return this.tongTien;
    }
    public List<sp> getDsSP(){
        return this.dsSP;
    }

}
