package com.example.luanvan.Model;

import java.util.Date;

public class BinhLuan {
    Integer idBinhLuan;
    String maSP;
    Integer idTinTuc;
    String tenDN;
    Date ngayTao;
    String noiDung;
    boolean isTinTuc;
    boolean trangThai;

    public Integer getIdBinhLuan() {
        return idBinhLuan;
    }

    public void setIdBinhLuan(Integer idBinhLuan) {
        this.idBinhLuan = idBinhLuan;
    }

    public String getMaSP() {
        return maSP;
    }

    public void setMaSP(String maSP) {
        this.maSP = maSP;
    }

    public Integer getIdTinTuc() {
        return idTinTuc;
    }

    public void setIdTinTuc(Integer idTinTuc) {
        this.idTinTuc = idTinTuc;
    }

    public String getTenDN() {
        return tenDN;
    }

    public void setTenDN(String tenDN) {
        this.tenDN = tenDN;
    }

    public Date getNgayTao() {
        return ngayTao;
    }

    public void setNgayTao(Date ngayTao) {
        this.ngayTao = ngayTao;
    }

    public String getNoiDung() {
        return noiDung;
    }

    public void setNoiDung(String noiDung) {
        this.noiDung = noiDung;
    }

    public boolean isTinTuc() {
        return isTinTuc;
    }

    public void setTinTuc(boolean tinTuc) {
        isTinTuc = tinTuc;
    }

    public boolean isTrangThai() {
        return trangThai;
    }

    public void setTrangThai(boolean trangThai) {
        this.trangThai = trangThai;
    }

    public BinhLuan() {
    }

    public BinhLuan(Integer idBinhLuan, String maSP, Integer idTinTuc, String tenDN, Date ngayTao, String noiDung, boolean isTinTuc, boolean trangThai) {
        this.idBinhLuan = idBinhLuan;
        this.maSP = maSP;
        this.idTinTuc = idTinTuc;
        this.tenDN = tenDN;
        this.ngayTao = ngayTao;
        this.noiDung = noiDung;
        this.isTinTuc = isTinTuc;
        this.trangThai = trangThai;
    }
}
