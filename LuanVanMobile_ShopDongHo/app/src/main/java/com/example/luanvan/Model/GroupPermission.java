package com.example.luanvan.Model;

public class GroupPermission {
    Integer id;
    String tenDN;
    Integer idGroup;
    boolean trangThai;

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getTenDN() {
        return tenDN;
    }

    public void setTenDN(String tenDN) {
        this.tenDN = tenDN;
    }

    public Integer getIdGroup() {
        return idGroup;
    }

    public void setIdGroup(Integer idGroup) {
        this.idGroup = idGroup;
    }

    public boolean getTrangThai() {
        return trangThai;
    }

    public void setTrangThai(boolean trangThai) {
        this.trangThai = trangThai;
    }

    public GroupPermission() {
    }

    public GroupPermission(Integer id, String tenDN, Integer idGroup, boolean trangThai) {
        this.id = id;
        this.tenDN = tenDN;
        this.idGroup = idGroup;
        this.trangThai = trangThai;
    }
}
