package com.example.luanvan.Model;

import java.util.Date;

public class TinTuc {
    Integer idTinTuc;
    String tieuDe;
    String tomTat;
    String noiDung;
    String hinhDaiDien;
    Date ngayDang;
    String maNV;
    boolean trangThai;

    public Integer getIdTinTuc() {
        return idTinTuc;
    }

    public void setIdTinTuc(Integer idTinTuc) {
        this.idTinTuc = idTinTuc;
    }

    public String getTieuDe() {
        return tieuDe;
    }

    public void setTieuDe(String tieuDe) {
        this.tieuDe = tieuDe;
    }

    public String getTomTat() {
        return tomTat;
    }

    public void setTomTat(String tomTat) {
        this.tomTat = tomTat;
    }

    public String getNoiDung() {
        return noiDung;
    }

    public void setNoiDung(String noiDung) {
        this.noiDung = noiDung;
    }

    public String getHinhDaiDien() {
        return hinhDaiDien;
    }

    public void setHinhDaiDien(String hinhDaiDien) {
        this.hinhDaiDien = hinhDaiDien;
    }

    public Date getNgayDang() {
        return ngayDang;
    }

    public void setNgayDang(Date ngayDang) {
        this.ngayDang = ngayDang;
    }

    public String getMaNV() {
        return maNV;
    }

    public void setMaNV(String maNV) {
        this.maNV = maNV;
    }

    public boolean isTrangThai() {
        return trangThai;
    }

    public void setTrangThai(boolean trangThai) {
        this.trangThai = trangThai;
    }

    public TinTuc() {
    }

    public TinTuc(Integer idTinTuc, String tieuDe, String tomTat, String noiDung, String hinhDaiDien, Date ngayDang, String maNV, boolean trangThai) {
        this.idTinTuc = idTinTuc;
        this.tieuDe = tieuDe;
        this.tomTat = tomTat;
        this.noiDung = noiDung;
        this.hinhDaiDien = hinhDaiDien;
        this.ngayDang = ngayDang;
        this.maNV = maNV;
        this.trangThai = trangThai;
    }
}
