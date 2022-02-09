package com.example.luanvan.Model;

public class sp {
    String tenSP;
    String hinhAnh;
    Integer quant;

    public sp(String tenSP,String hinhAnh,Integer soLuong)
    {
        this.tenSP = tenSP;
        this.hinhAnh= hinhAnh;
        this.quant= soLuong;
    }

    public String getHinhAnh(){return this.hinhAnh;}

    public String getTenSP(){return this.tenSP;}

    public Integer getSoLuong(){return this.quant;}

}
