package com.example.luanvan.Model;

import java.io.Serializable;

public class Hang implements Serializable {
    String MAHANG;
    String TENHANG;
    String THONGTIN;
    String URL;
    String BANNER;
    String LOGO;
    String QUOCGIA;
    boolean TRANGTHAI;

    public String getMAHANG() {
        return MAHANG;
    }

    public void setMAHANG(String MAHANG) {
        this.MAHANG = MAHANG;
    }

    public String getTENHANG() {
        return TENHANG;
    }

    public void setTENHANG(String TENHANG) {
        this.TENHANG = TENHANG;
    }

    public String getTHONGTIN() {
        return THONGTIN;
    }

    public void setTHONGTIN(String THONGTIN) {
        this.THONGTIN = THONGTIN;
    }

    public String getURL() {
        return URL;
    }

    public void setURL(String URL) {
        this.URL = URL;
    }

    public String getBANNER() {
        return BANNER;
    }

    public void setBANNER(String BANNER) {
        this.BANNER = BANNER;
    }

    public String getLOGO() {
        return LOGO;
    }

    public void setLOGO(String LOGO) {
        this.LOGO = LOGO;
    }

    public String getQUOCGIA() {
        return QUOCGIA;
    }

    public void setQUOCGIA(String QUOCGIA) {
        this.QUOCGIA = QUOCGIA;
    }

    public boolean isTRANGTHAI() {
        return TRANGTHAI;
    }

    public void setTRANGTHAI(boolean TRANGTHAI) {
        this.TRANGTHAI = TRANGTHAI;
    }

    public Hang(String MAHANG, String TENHANG, String THONGTIN, String URL, String BANNER, String LOGO, String QUOCGIA, boolean TRANGTHAI) {
        this.MAHANG = MAHANG;
        this.TENHANG = TENHANG;
        this.THONGTIN = THONGTIN;
        this.URL = URL;
        this.BANNER = BANNER;
        this.LOGO = LOGO;
        this.QUOCGIA = QUOCGIA;
        this.TRANGTHAI = TRANGTHAI;
    }
}
