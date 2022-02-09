package com.example.luanvan.Model;

public class TaiKhoan {
    String TENDN;

    String MATKHAU;

    String MALOAI;

    /*@SerializedName("Email")
    String Email;

    @SerializedName("token")
    String token;

    @SerializedName("key_tfa")
    String key_tfa;

    @SerializedName("date_send")
    Date date_send;

    @SerializedName("userConfirm")
    String userConfirm;*/

    boolean TRANGTHAI;

    public String getTENDN() {
        return TENDN;
    }

    public void setTENDN(String TENDN) {
        this.TENDN = TENDN;
    }

    public String getMATKHAU() {
        return MATKHAU;
    }

    public void setMATKHAU(String MATKHAU) {
        this.MATKHAU = MATKHAU;
    }

    public String getMALOAI() {
        return MALOAI;
    }

    public void setMALOAI(String MALOAI) {
        this.MALOAI = MALOAI;
    }

    /*public String getEmail() {
        return Email;
    }

    public void setEmail(String email) {
        Email = email;
    }

    public String getToken() {
        return token;
    }

    public void setToken(String token) {
        this.token = token;
    }

    public String getKey_tfa() {
        return key_tfa;
    }

    public void setKey_tfa(String key_tfa) {
        this.key_tfa = key_tfa;
    }

    public Date getDate_send() {
        return date_send;
    }

    public void setDate_send(Date date_send) {
        this.date_send = date_send;
    }

    public String getUserConfirm() {
        return userConfirm;
    }

    public void setUserConfirm(String userConfirm) {
        this.userConfirm = userConfirm;
    }
*/
    public boolean isTRANGTHAI() {
        return TRANGTHAI;
    }

    public void setTRANGTHAI(boolean TRANGTHAI) {
        this.TRANGTHAI = TRANGTHAI;
    }

    /*public TaiKhoan(String TENDN, String MATKHAU, String MALOAI, String email, String token, String key_tfa, Date date_send, String userConfirm, boolean TRANGTHAI) {
        this.TENDN = TENDN;
        this.MATKHAU = MATKHAU;
        this.MALOAI = MALOAI;
        this.Email = email;
        this.token = token;
        this.key_tfa = key_tfa;
        this.date_send = date_send;
        this.userConfirm = userConfirm;
        this.TRANGTHAI = TRANGTHAI;
    }*/

    public TaiKhoan(String TENDN, String MATKHAU, String MALOAI, boolean TRANGTHAI) {
        this.TENDN = TENDN;
        this.MATKHAU = MATKHAU;
        this.MALOAI = MALOAI;
        this.TRANGTHAI = TRANGTHAI;
    }
}
