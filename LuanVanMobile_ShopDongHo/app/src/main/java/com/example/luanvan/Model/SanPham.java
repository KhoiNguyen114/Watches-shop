package com.example.luanvan.Model;

import java.io.Serializable;
import java.math.BigInteger;

public class SanPham implements Serializable {
    String MASP;
    String TENSP;
    BigInteger DONGIA;
    String HINHANH;
    String CHITIETSP;
    Integer SOLUONG;
    String MAHANG;
    String MABAOHANH;
    Integer ThoiHanBH;
    String MADM;
    String DONGSP;
    String NANGLUONG;
    String KICHTHUOC;
    String LOAIDAY;
    String XUATSU;
    String MOTASP;
    boolean TRANGTHAI;

    public String getMASP() {
        return MASP;
    }

    public void setMASP(String MASP) {
        this.MASP = MASP;
    }

    public String getTENSP() {
        return TENSP;
    }

    public void setTENSP(String TENSP) {
        this.TENSP = TENSP;
    }

    public BigInteger getDONGIA() {
        return DONGIA;
    }

    public void setDONGIA(BigInteger DONGIA) {
        this.DONGIA = DONGIA;
    }

    public String getHINHANH() {
        return HINHANH;
    }

    public void setHINHANH(String HINHANH) {
        this.HINHANH = HINHANH;
    }

    public String getCHITIETSP() {
        return CHITIETSP;
    }

    public void setCHITIETSP(String CHITIETSP) {
        this.CHITIETSP = CHITIETSP;
    }

    public Integer getSOLUONG() {
        return SOLUONG;
    }

    public void setSOLUONG(Integer SOLUONG) {
        this.SOLUONG = SOLUONG;
    }

    public String getMAHANG() {
        return MAHANG;
    }

    public void setMAHANG(String MAHANG) {
        this.MAHANG = MAHANG;
    }

    public String getMABAOHANH() {
        return MABAOHANH;
    }

    public void setMABAOHANH(String MABAOHANH) {
        this.MABAOHANH = MABAOHANH;
    }

    public Integer getThoiHanBH() {
        return ThoiHanBH;
    }

    public void setThoiHanBH(Integer thoiHanBH) {
        ThoiHanBH = thoiHanBH;
    }

    public String getMADM() {
        return MADM;
    }

    public void setMADM(String MADM) {
        this.MADM = MADM;
    }

    public String getDONGSP() {
        return DONGSP;
    }

    public void setDONGSP(String DONGSP) {
        this.DONGSP = DONGSP;
    }

    public String getNANGLUONG() {
        return NANGLUONG;
    }

    public void setNANGLUONG(String NANGLUONG) {
        this.NANGLUONG = NANGLUONG;
    }

    public String getKICHTHUOC() {
        return KICHTHUOC;
    }

    public void setKICHTHUOC(String KICHTHUOC) {
        this.KICHTHUOC = KICHTHUOC;
    }

    public String getLOAIDAY() {
        return LOAIDAY;
    }

    public void setLOAIDAY(String LOAIDAY) {
        this.LOAIDAY = LOAIDAY;
    }

    public String getXUATSU() {
        return XUATSU;
    }

    public void setXUATSU(String XUATSU) {
        this.XUATSU = XUATSU;
    }

    public String getMOTASP() {
        return MOTASP;
    }

    public void setMOTASP(String MOTASP) {
        this.MOTASP = MOTASP;
    }

    public boolean isTRANGTHAI() {
        return TRANGTHAI;
    }

    public void setTRANGTHAI(boolean TRANGTHAI) {
        this.TRANGTHAI = TRANGTHAI;
    }

    public SanPham(String MASP, String TENSP, BigInteger DONGIA, String HINHANH, String CHITIETSP, Integer SOLUONG, String MAHANG, String MABAOHANH, Integer thoiHanBH, String MADM, String DONGSP, String NANGLUONG, String KICHTHUOC, String LOAIDAY, String XUATSU, String MOTASP, boolean TRANGTHAI) {
        this.MASP = MASP;
        this.TENSP = TENSP;
        this.DONGIA = DONGIA;
        this.HINHANH = HINHANH;
        this.CHITIETSP = CHITIETSP;
        this.SOLUONG = SOLUONG;
        this.MAHANG = MAHANG;
        this.MABAOHANH = MABAOHANH;
        ThoiHanBH = thoiHanBH;
        this.MADM = MADM;
        this.DONGSP = DONGSP;
        this.NANGLUONG = NANGLUONG;
        this.KICHTHUOC = KICHTHUOC;
        this.LOAIDAY = LOAIDAY;
        this.XUATSU = XUATSU;
        this.MOTASP = MOTASP;
        this.TRANGTHAI = TRANGTHAI;
    }
}
