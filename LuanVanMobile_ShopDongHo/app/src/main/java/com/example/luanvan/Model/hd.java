package com.example.luanvan.Model;

public class hd {
    public Integer idHoaDon;
    public String maHoaDon;
    public String ngayLap;
    public String tinhTrang;
    public String hinhThuc;
    public Long tongTien;

    public hd(){

    }
    public hd(Integer idHoaDon,String maHoaDon, String ngayLap, String tinhTrang, String hinhThuc, Long tongTien)
    {
        this.idHoaDon = idHoaDon;
        this.maHoaDon = maHoaDon;
        this.ngayLap = ngayLap;
        this.tinhTrang = tinhTrang;
        this.hinhThuc = hinhThuc;
        this.tongTien = tongTien;
    }
}
