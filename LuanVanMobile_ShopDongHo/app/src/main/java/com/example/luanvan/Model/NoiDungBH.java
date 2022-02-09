package com.example.luanvan.Model;

public class NoiDungBH {
    String maNoiDung;
    String noiDung;
    Float soNamBH;
    boolean trangThai;

    public String getMaNoiDung() {
        return maNoiDung;
    }

    public void setMaNoiDung(String maNoiDung) {
        this.maNoiDung = maNoiDung;
    }

    public String getNoiDung() {
        return noiDung;
    }

    public void setNoiDung(String noiDung) {
        this.noiDung = noiDung;
    }

    public Float getSoNamBH() {
        return soNamBH;
    }

    public void setSoNamBH(Float soNamBH) {
        this.soNamBH = soNamBH;
    }

    public boolean getTrangThai() {
        return trangThai;
    }

    public void setTrangThai(boolean trangThai) {
        this.trangThai = trangThai;
    }

    public NoiDungBH() {
    }

    public NoiDungBH(String maNoiDung, String noiDung, Float soNamBH, boolean trangThai) {
        this.maNoiDung = maNoiDung;
        this.noiDung = noiDung;
        this.soNamBH = soNamBH;
        this.trangThai = trangThai;
    }
}
