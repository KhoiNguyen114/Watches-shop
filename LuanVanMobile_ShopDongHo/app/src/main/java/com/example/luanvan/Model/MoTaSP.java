package com.example.luanvan.Model;

public class MoTaSP {
    String maMoTa;
    String maSP;
    String url_VID_Review;
    String url_VID_AMZ;
    String url_VID_360;
    String url_BaiViet;

    public String getMaMoTa() {
        return maMoTa;
    }

    public void setMaMoTa(String maMoTa) {
        this.maMoTa = maMoTa;
    }

    public String getMaSP() {
        return maSP;
    }

    public void setMaSP(String maSP) {
        this.maSP = maSP;
    }

    public String getUrl_VID_Review() {
        return url_VID_Review;
    }

    public void setUrl_VID_Review(String url_VID_Review) {
        this.url_VID_Review = url_VID_Review;
    }

    public String getUrl_VID_AMZ() {
        return url_VID_AMZ;
    }

    public void setUrl_VID_AMZ(String url_VID_AMZ) {
        this.url_VID_AMZ = url_VID_AMZ;
    }

    public String getUrl_VID_360() {
        return url_VID_360;
    }

    public void setUrl_VID_360(String url_VID_360) {
        this.url_VID_360 = url_VID_360;
    }

    public String getUrl_BaiViet() {
        return url_BaiViet;
    }

    public void setUrl_BaiViet(String url_BaiViet) {
        this.url_BaiViet = url_BaiViet;
    }

    public MoTaSP() {
    }

    public MoTaSP(String maMoTa, String maSP, String url_VID_Review, String url_VID_AMZ, String url_VID_360, String url_BaiViet) {
        this.maMoTa = maMoTa;
        this.maSP = maSP;
        this.url_VID_Review = url_VID_Review;
        this.url_VID_AMZ = url_VID_AMZ;
        this.url_VID_360 = url_VID_360;
        this.url_BaiViet = url_BaiViet;
    }
}
