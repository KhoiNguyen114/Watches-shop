package com.example.luanvan.Model;

public class Permission {
    Integer idPermission;
    Integer idGroup;
    boolean permission_sale;
    boolean permission_editUser;
    boolean permission_report;
    boolean permission_stockWare;
    boolean permission_editSaleOff;

    public Integer getIdPermission() {
        return idPermission;
    }

    public void setIdPermission(Integer idPermission) {
        this.idPermission = idPermission;
    }

    public Integer getIdGroup() {
        return idGroup;
    }

    public void setIdGroup(Integer idGroup) {
        this.idGroup = idGroup;
    }

    public boolean getPermission_sale() {
        return permission_sale;
    }

    public void setPermission_sale(boolean permission_sale) {
        this.permission_sale = permission_sale;
    }

    public boolean getPermission_editUser() {
        return permission_editUser;
    }

    public void setPermission_editUser(boolean permission_editUser) {
        this.permission_editUser = permission_editUser;
    }

    public boolean getPermission_report() {
        return permission_report;
    }

    public void setPermission_report(boolean permission_report) {
        this.permission_report = permission_report;
    }

    public boolean getPermission_stockWare() {
        return permission_stockWare;
    }

    public void setPermission_stockWare(boolean permission_stockWare) {
        this.permission_stockWare = permission_stockWare;
    }

    public boolean getPermission_editSaleOff() {
        return permission_editSaleOff;
    }

    public void setPermission_editSaleOff(boolean permission_editSaleOff) {
        this.permission_editSaleOff = permission_editSaleOff;
    }

    public Permission() {
    }

    public Permission(Integer idPermission, Integer idGroup, boolean permission_sale, boolean permission_editUser, boolean permission_report, boolean permission_stockWare, boolean permission_editSaleOff) {
        this.idPermission = idPermission;
        this.idGroup = idGroup;
        this.permission_sale = permission_sale;
        this.permission_editUser = permission_editUser;
        this.permission_report = permission_report;
        this.permission_stockWare = permission_stockWare;
        this.permission_editSaleOff = permission_editSaleOff;
    }
}
