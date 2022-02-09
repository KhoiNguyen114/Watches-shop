package com.example.luanvan.Model;

public class DanhMucSP {
    String MADM;
    String TENDM;
    boolean TRANGTHAI;

    public String getMADM() {
        return MADM;
    }

    public void setMADM(String MADM) {
        this.MADM = MADM;
    }

    public String getTENDM() {
        return TENDM;
    }

    public void setTENDM(String TENDM) {
        this.TENDM = TENDM;
    }

    public boolean isTRANGTHAI() {
        return TRANGTHAI;
    }

    public void setTRANGTHAI(boolean TRANGTHAI) {
        this.TRANGTHAI = TRANGTHAI;
    }

    public DanhMucSP(String MADM, String TENDM, boolean TRANGTHAI) {
        this.MADM = MADM;
        this.TENDM = TENDM;
        this.TRANGTHAI = TRANGTHAI;
    }
}
