function nhapTable() {
    var a = Intl.NumberFormat('en-US');
    $('#tableChiTietPN').append('<tr style="border-bottom: 1px solid">'
        + '<td>'
        + 'Đồng hồ 002'
        + '</td>'
        + '<td>'
        + '15'
        + '</td>'
        + '<td>'
        + a.format('15000000')
        + '</td>'
        + '<td>'
        + a.format('2500000')
        + '</td>'
        + '</tr>');

}

$('#btnTaoPhieuNhap').on('click', function (e) {
    var maHang = $('#maHang').val();

    //var dieuKien1 = true;

    //if (maPN == '') {
    //    document.getElementById('maPNVL').innerHTML = "Mã phiếu nhập không được bỏ trống";
    //    $('#maPN').css('border-color', 'red');
    //    dieuKien1 = false;
    //    e.preventDefault();
    //}
    //else {
    //    document.getElementById('maPNVL').innerHTML = "";
    //    $('#maPN').css('border-color', '');
    //    dieuKien1 = true;
    //    if (maPN.length > 20) {
    //        document.getElementById('maPNVL').innerHTML = "Mã phiếu nhập không được vượt quá 20 kí tự";
    //        $('#maPN').css('border-color', 'red');
    //        dieuKien1 = false;
    //        e.preventDefault();
    //    }
    //    else {
    //        document.getElementById('maPNVL').innerHTML = "";
    //        $('#maPN').css('border-color', '');
    //        dieuKien1 = true;
    //    }
    //}

    $.ajax({
        url: "/NhapHang/TaoPN",
        type: "POST",
        data: JSON.stringify({
            'pn.tenHang': maHang,
        }),
        dataType: "json",
        contentType: "application/json; charset = utf-8",
        success: function (result) {
            if (result.kq) {
                $.notify(result.message, "success");
                var timeout = window.setTimeout(function () {
                    $('#maPN').val(result.mapn);
                    $('#listbtnCTPN').css('visibility', 'visible');
                    $('#listTableCTPN').css('visibility', 'visible');
                }, 500);

            }
            else {
                $.notify(result.message, "error");
            }
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    })

});

$('#btnLamMoi').on('click', function (e) {
    window.location.reload();
})

function getSPPhieuNhap() {
    var maSP = $('#tenSPCTPN').val();
    $.ajax({
        url: "/NhapHang/GetSanPhamPN",
        type: "GET",
        data: { maSP: maSP },
        dataType: "Json",
        contentType: "application/json; charset = utf-8",
        success: function (res) {
            $('#maSPThemCTPN').val(res[0].MASP);
            $('#donGiaSPThemCTPN').val(res[0].DONGIA);
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    })
}

$('#btnNhapSPDaCo').on('click', function (e) {
    var valSL = $('#soLuongSPThemCTPN').val();
    var soLuong = parseInt(valSL) || 0;

    var tenSP = $('#tenSPCTPN').val();
    var maSP = $('#maSPThemCTPN').val();

    var valdonGia = $('#donGiaSPThemCTPN').val();
    var donGia = parseInt(valdonGia) || 0;

    var valchietKhau = $('#chietKhauSPThemCTPN').val();
    var chietKhau = parseInt(valchietKhau) || 0;

    var dieuKien1 = true;
    var dieuKien2 = true;
    var dieuKien3 = true;
    var dieuKien4 = true;

    if (soLuong == 0 || soLuong < 0) {
        document.getElementById('soLuongSPThemCTPNVL').innerHTML = "Số lượng phải là số nguyên lớn hơn 0";
        $('#soLuongSPThemCTPN').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('soLuongSPThemCTPNVL').innerHTML = "";
        $('#soLuongSPThemCTPN').css('border-color', '');
        dieuKien1 = true;
    }

    if (donGia == 0 || donGia < 0) {
        document.getElementById('donGiaSPThemCTPNVL').innerHTML = "Đơn giá phải là số nguyên lớn hơn 0";
        $('#donGiaSPThemCTPN').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('donGiaSPThemCTPNVL').innerHTML = "";
        $('#donGiaSPThemCTPN').css('border-color', '');
        dieuKien2 = true;
    }

    if (maSP == '') {
        document.getElementById('maSPThemCTPNVL').innerHTML = "Mã sản phẩm không được bỏ trống";
        $('#maSPThemCTPN').css('border-color', 'red');
        dieuKien3 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('maSPThemCTPNVL').innerHTML = "";
        $('#maSPThemCTPN').css('border-color', '');
        dieuKien3 = true;
    }

    if (chietKhau == '') {
        document.getElementById('chietKhauSPThemCTPNVL').innerHTML = "Chiết khấu không được bỏ trống";
        $('#chietKhauSPThemCTPN').css('border-color', 'red');
        dieuKien4 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('chietKhauSPThemCTPNVL').innerHTML = "";
        $('#chietKhauSPThemCTPN').css('border-color', '');
        dieuKien4 = true;
    }

    if (dieuKien1 && dieuKien2 && dieuKien3 && dieuKien4) {
        var maPN = $('#maPN').val();
        $.ajax({
            url: "/NhapHang/ThemCTPNSPDaCo",
            type: "POST",
            data: JSON.stringify({
                maPN: maPN,
                soLuong: soLuong,
                maSP: maSP,
                chietKhau: chietKhau,
            }),
            dataType: "json",
            contentType: "application/json; charset = utf-8",
            success: function (res) {
                if (res.kq) {
                    $.notify(res.message, "success");
                    $('#myModalAddDaCo').modal('hide');
                    var a = Intl.NumberFormat('en-US');
                    $('#tableChiTietPN').append('<tr style="border-bottom: 1px solid" id="PN' + maSP + '">'
                        + '<td>'
                        + tenSP
                        + '</td>'
                        + '<td>'
                        + soLuong
                        + '</td>'
                        + '<td>'
                        + a.format(donGia)
                        + '</td>'
                        + '<td>'
                        + a.format((soLuong * donGia) - (soLuong * donGia * chietKhau) / 100)
                        + '</td>'
                        + '<td>'
                        + '<button class="btn btn-danger" id="' + maSP + '" onclick="GetIDSanPhamDelete(this)" data-bs-toggle="modal" data-bs-target="#myModalDeleteSanPhamNhap"><i class="fas fa-trash"></i> Xóa</button>'
                        + '</td>'
                        + '</tr>');
                    $('#tongTienPN').val(res.tt);
                }
                else {
                    $.notify(res.message, "error");
                }
            },
            error: function (e) {
                $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
            }
        })
    }
})

$('#btnThem').on('click', function (e) {
    var masp = $('#maSPThem').val();
    var tensp = $('#tenSPThem').val();
    var chiTietSP = $('#chiTietSPThem').val();

    var valDG = $('#donGiaSPThem').val();
    var donGia = parseInt(valDG) || 0;

    var hinhAnh = $('#hinhAnhSPThem').get(0).files.length;
    var hinh = $('#hinhAnhSPThem').val();

    var valSL = $('#soLuongSPThem').val();
    var soLuong = parseInt(valSL) || 0;

    var maHang = $('#maHangSPThem').val();
    var maNDBH = $('#maNoiDungSPThem').val();
    var maDMSP = $('#maDanhMucSPThem').val();

    var thoiHanBH = $('#ThoiGianBHSPThem').val();

    var phienBan = $('#phienBanSPThem').val();

    var valKT = $('#kichThuocSPThem').val();
    var parsevalKT = parseFloat(valKT) || 0;
    var kichThuoc = parsevalKT.toFixed(1);

    var nangLuong = $('#nangLuongSPThem').val();
    var loaiDay = $('#loaiDaySPThem').val();
    var xuatSu = $('#xuatSuSPThem').val();

    var moTaSP = $('#txtMoTaSPThem').summernote('code');
    var moTaSPKT = $('#txtMoTaSPThem').val();

    var valchietKhau = $('#chietKhauSPThemMoiCTPN').val();
    var chietKhau = parseInt(valchietKhau) || 0;

    var dieuKien1 = true;
    var dieuKien2 = true;
    var dieuKien3 = true;
    var dieuKien4 = true;
    var dieuKien5 = true;
    var dieuKien6 = true;
    var dieuKien7 = true;
    var dieuKien8 = true;
    var dieuKien9 = true;
    var dieuKien10 = true;
    var dieuKien11 = true;

    if (masp == '') {
        document.getElementById('maSPThemVL').innerHTML = "Mã sản phẩm không được bỏ trống";
        $('#maSPThem').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('maSPThemVL').innerHTML = "";
        $('#maSPThem').css('border-color', '');
        dieuKien1 = true;
        if (masp.length > 100) {
            document.getElementById('maSPThemVL').innerHTML = "Mã sản phẩm không được vượt quá 100 kí tự";
            $('#maSPThem').css('border-color', 'red');
            dieuKien1 = false;
            e.preventDefault();
        }
        else {
            document.getElementById('maSPThemVL').innerHTML = "";
            $('#maSPThem').css('border-color', '');
            dieuKien1 = true;
        }
    }

    if (tensp == '') {
        document.getElementById('tenSPThemVL').innerHTML = "Tên sản phẩm không được bỏ trống";
        $('#tenSPThem').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tenSPThemVL').innerHTML = "";
        $('#tenSPThem').css('border-color', '');
        dieuKien2 = true;
    }
    if (chiTietSP == '') {
        document.getElementById('chiTietSPThemVL').innerHTML = "Chi tiết sản phẩm không được bỏ trống";
        $('#chiTietSPThem').css('border-color', 'red');
        dieuKien3 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('chiTietSPThemVL').innerHTML = "";
        $('#chiTietSPThem').css('border-color', '');
        dieuKien3 = true;
    }
    if (donGia == 0 || donGia < 0) {
        document.getElementById('donGiaSPThemVL').innerHTML = "Đơn giá phải là số nguyên lớn hơn 0";
        $('#donGiaSPThem').css('border-color', 'red');
        dieuKien4 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('donGiaSPThemVL').innerHTML = "";
        $('#donGiaSPThem').css('border-color', '');
        dieuKien4 = true;
    }
    if (chiTietSP == '') {
        document.getElementById('chiTietSPThemVL').innerHTML = "Chi tiết sản phẩm không được bỏ trống";
        $('#chiTietSPThem').css('border-color', 'red');
        dieuKien5 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('chiTietSPThemVL').innerHTML = "";
        $('#chiTietSPThem').css('border-color', '');
        dieuKien5 = true;
    }

    if (hinhAnh == 0) {
        document.getElementById('hinhAnhSPThemVL').innerHTML = "Vui lòng chọn hình ảnh cho sản phẩm";
        dieuKien6 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('hinhAnhSPThemVL').innerHTML = "";
        dieuKien6 = true;
    }

    if (soLuong == 0 || soLuong < 0) {
        document.getElementById('soLuongSPThemVL').innerHTML = "Số lượng phải là số nguyên lớn hơn 0";
        $('#soLuongSPThem').css('border-color', 'red');
        dieuKien7 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('soLuongSPThemVL').innerHTML = "";
        $('#soLuongSPThem').css('border-color', '');
        dieuKien7 = true;
    }

    if (thoiHanBH == 0 || thoiHanBH < 0) {
        document.getElementById('ThoiGianBHSPThemVL').innerHTML = "Số lượng phải là số nguyên lớn hơn 0";
        $('#ThoiGianBHSPThem').css('border-color', 'red');
        dieuKien8 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('ThoiGianBHSPThemVL').innerHTML = "";
        $('#ThoiGianBHSPThem').css('border-color', '');
        dieuKien8 = true;
    }

    if (kichThuoc == 0 || kichThuoc < 0) {
        document.getElementById('kichThuocSPThemVL').innerHTML = "Kích thước phải là số lớn hơn 0";
        $('#kichThuocSPThem').css('border-color', 'red');
        dieuKien9 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('kichThuocSPThemVL').innerHTML = "";
        $('#kichThuocSPThem').css('border-color', '');
        dieuKien9 = true;
    }

    if (moTaSPKT == '') {
        document.getElementById('txtMoTaSPThemVL').innerHTML = "Mô tả sản phẩm không được bỏ trống";
        $('#txtMoTaSPThem').css('border-color', 'red');
        dieuKien10 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('txtMoTaSPThemVL').innerHTML = "";
        $('#txtMoTaSPThem').css('border-color', '');
        dieuKien10 = true;
    }

    if (chietKhau == '') {
        document.getElementById('chietKhauSPThemMoiCTPNVL').innerHTML = "Chiết khấu không được bỏ trống";
        $('#chietKhauSPThemMoiCTPN').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('chietKhauSPThemMoiCTPNVL').innerHTML = "";
        $('#chietKhauSPThemMoiCTPN').css('border-color', '');
        dieuKien2 = true;
    }

    if (dieuKien1 && dieuKien2 && dieuKien3 && dieuKien4 && dieuKien5 && dieuKien6 && dieuKien7 && dieuKien8 && dieuKien9 && dieuKien10 && dieuKien11) {
        var maPN = $('#maPN').val();
        $.ajax({
            url: "/QuanLy/ThemSanPham",
            type: "POST",
            data: JSON.stringify({
                MASP: masp,
                TENSP: tensp,
                CHITIETSP: chiTietSP,
                DONGIA: donGia,
                HINHANH: hinh,
                SOLUONG: soLuong,
                MADM: maDMSP,
                MABAOHANH: maNDBH,
                ThoiHanBH: thoiHanBH,
                DONGSP: phienBan,
                MAHANG: maHang,
                NANGLUONG: nangLuong,
                KICHTHUOC: kichThuoc,
                LOAIDAY: loaiDay,
                XUATSU: xuatSu,
                MOTASP: moTaSP,
                maPN: maPN,
                chietKhau: chietKhau,
            }),
            dataType: "Json",
            contentType: "application/json; charset = utf-8",
            success: function (res) {
                if (res.kq) {
                    loadAnh('hinhAnhSPThem');
                    $.notify(res.message, "success");
                    $('#myModalAdd').modal('hide');
                    var a = Intl.NumberFormat('en-US');
                    $('#tableChiTietPN').append('<tr style="border-bottom: 1px solid" id="PN' + masp + '">'
                        + '<td>'
                        + tensp
                        + '</td>'
                        + '<td>'
                        + soLuong
                        + '</td>'
                        + '<td>'
                        + a.format(donGia)
                        + '</td>'
                        + '<td>'
                        + a.format((soLuong * donGia) - (soLuong * donGia * chietKhau) /100)
                        + '</td>'
                        + '<td>'
                        + '<button class="btn btn-danger" id="' + masp + '" onclick="GetIDSanPhamDelete(this)"  data-bs-toggle="modal" data-bs-target="#myModalDeleteSanPhamNhap"><i class="fas fa-trash"></i> Xóa</button>'
                        + '</td>'
                        + '</tr>');
                    $('#tongTienPN').val(res.tt);
                }
                else {
                    $.notify(res.message, "error");
                }
            },
            error: function (e) {
                $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
            }
        })
    }
});

function GetIDSanPhamDelete(e) {
    var maNV = $(e).attr('id');
    $('#idSanPhamNhapDelete').val(maNV);
}

$('#btnXoaSanPhamNhap').on('click', function (e) {
    var maPN = $('#maPN').val();
    var maSP = $('#idSanPhamNhapDelete').val();
    console.log(maSP);
    $.ajax({
        url: "/NhapHang/XoaCTPN",
        type: "GET",
        data: {
            maSP: maSP,
            maPN: maPN,
        },
        dataType: "json",
        contentType: "application/json; charset = utf-8",
        success: function (res) {
            if (res.kq) {
                $('#PN' + maSP).remove();
                $.notify(res.message, "success");
                $('#myModalDeleteSanPhamNhap').modal('hide');
            }
            else {
                $.notify(res.message, "error");
            }
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        },
    })
})


$('#btnCapNhatTinhTrang').on('click', function (e) {
    var maPN = $('#maPNTrinhTrang').val();
    var tinhTrang = $('#tinhTrangPN').val();
    $.ajax({
        url: "/NhapHang/CapNhatTinhTrang",
        type: "GET",
        data: {
            maPN: maPN,
            tinhTrang: tinhTrang,
        },
        dataType: "json",
        contentType: "application/json; charset = utf-8",
        success: function (res) {
            if (res.kq) {
                $.notify(res.message, "success");
                var timeout = window.setTimeout(function () {
                    var url = window.location.origin + "/NhapHang/QuanLyPhieuNhap";
                    window.location = url;
                }, 1000);
            }
            else {
                $.notify(res.message, "error");
            }
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    })
})
