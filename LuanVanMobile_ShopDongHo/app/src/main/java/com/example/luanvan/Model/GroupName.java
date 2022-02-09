package com.example.luanvan.Model;

public class GroupName {
    Integer idGroup;
    String name;
    boolean trangThai;

    public Integer getIdGroup() {
        return idGroup;
    }

    public void setIdGroup(Integer idGroup) {
        this.idGroup = idGroup;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public boolean getTrangThai() {
        return trangThai;
    }

    public void setTrangThai(boolean trangThai) {
        this.trangThai = trangThai;
    }

    public GroupName() {
    }

    public GroupName(Integer idGroup, String name, boolean trangThai) {
        this.idGroup = idGroup;
        this.name = name;
        this.trangThai = trangThai;
    }
}
