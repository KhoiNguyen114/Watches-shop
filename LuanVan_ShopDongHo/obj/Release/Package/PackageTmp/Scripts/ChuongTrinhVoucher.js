$(document).ready(function () {
    $('#idKHVoucher').select2({
        dropdownParent: $('#myModalGuiMailVoucher .modal-content')
    });

})
$('#btnThemCTVoucher').on('click', function (e) {
    var maCTVoucher = $('#maCTVoucherThem').val();
    var ngaybd = $('#ngayBDCTVoucherThem').val();
    var ngaykt = $('#ngayKTCTVoucherThem').val();
    var ghiChu = $('#ghiChuCTVoucherThem').val();

    var valSoLuong = $('#soLuongVoucherThem').val();
    var soLuong = parseInt(valSoLuong) || 0;

    var valGiamGia = $('#giaCTVoucherThem').val();
    var giamGia = parseInt(valGiamGia) || 0;

    var dieuKien1 = true;
    var dieuKien2 = true;
    var dieuKien3 = true;
    var dieuKien4 = true;
    var dieuKien5 = true;
    var dieuKien6 = true;

    if (maCTVoucher == '') {
        document.getElementById('maCTVoucherThemVL').innerHTML = "Mã chương trình voucher không được bỏ trống";
        $('#maCTVoucherThem').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('maCTVoucherThemVL').innerHTML = "";
        $('#maCTVoucherThem').css('border-color', '');
        dieuKien1 = true;
        if (maCTVoucher.length > 50) {
            document.getElementById('maCTVoucherThemVL').innerHTML = "Mã chương trình voucher không được vượt quá 50 kí tự";
            $('#maCTVoucherThem').css('border-color', 'red');
            dieuKien1 = false;
            e.preventDefault();
        }
        else {
            document.getElementById('maCTVoucherThemVL').innerHTML = "";
            $('#maCTVoucherThem').css('border-color', '');
            dieuKien1 = true;
        }
    }

    if (ngaybd == '') {
        document.getElementById('ngayBDCTVoucherThemVL').innerHTML = "Ngày tạo không được bỏ trống";
        $('#ngayBDCTVoucherThem').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('ngayBDCTVoucherThemVL').innerHTML = "";
        $('#ngayBDCTVoucherThem').css('border-color', '');
        dieuKien2 = true;
    }

    if (ngaykt == '') {
        document.getElementById('ngayKTCTVoucherThemVL').innerHTML = "Ngày kết thúc không được bỏ trống";
        $('#ngayKTCTVoucherThem').css('border-color', 'red');
        dieuKien3 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('ngayKTCTVoucherThemVL').innerHTML = "";
        $('#ngayKTCTVoucherThem').css('border-color', '');
        dieuKien3 = true;
    }

    if (giamGia == 0 || giamGia < 0 || giamGia > 100) {
        document.getElementById('giaCTVoucherThemVL').innerHTML = "Giảm giá voucher phải là số nguyên lớn hơn 0 và nhỏ hơn 100";
        $('#giaCTVoucherThem').css('border-color', 'red');
        dieuKien4 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('giaCTVoucherThemVL').innerHTML = "";
        $('#giaCTVoucherThem').css('border-color', '');
        dieuKien4 = true;
    }

    if (ghiChu == '') {
        document.getElementById('ghiChuCTVoucherThemVL').innerHTML = "Ghi chú không được bỏ trống";
        $('#ghiChuCTVoucherThem').css('border-color', 'red');
        dieuKien5 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('ghiChuCTVoucherThemVL').innerHTML = "";
        $('#ghiChuCTVoucherThem').css('border-color', '');
        dieuKien5 = true;
    }

    if (soLuong == 0 || soLuong < 0) {
        document.getElementById('soLuongVoucherThemVL').innerHTML = "Số lượng voucher phải là số nguyên lớn hơn 0";
        $('#soLuongVoucherThem').css('border-color', 'red');
        dieuKien6 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('soLuongVoucherThemVL').innerHTML = "";
        $('#soLuongVoucherThem').css('border-color', '');
        dieuKien6 = true;
    }

    if (dieuKien1 && dieuKien2 && dieuKien3 && dieuKien4 && dieuKien5 && dieuKien6) {

        $.ajax({
            url: "/Voucher/ThemCTVoucher",
            type: "Post",
            data: JSON.stringify({
                MACHUONGTRINH: maCTVoucher,
                NGAYTAO: ngaybd,
                NGAYKETTHUC: ngaykt,
                GIAMGIA: giamGia,
                SOLUONG: soLuong,
                GHICHU: ghiChu,
            }),
            dataType: "Json",
            contentType: "application/json; charset = utf-8",
            success: function (res) {
                if (res.kq) {
                    $.notify(res.message, "success");
                    var timeout = window.setTimeout(function () {
                        window.location.reload();
                    }, 500);
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

$('#btnCapNhatCTVoucher').on('click', function (e) {
    var maCTVoucher = $('#maCTVoucherSua').val();
    var ngaybd = $('#ngayBDCTVoucherSua').val();
    var ngaykt = $('#ngayKTCTVoucherSua').val();
    var ghiChu = $('#ghiChuCTVoucherSua').val();

    var valGiamGia = $('#giaCTVoucherSua').val();
    var giamGia = parseInt(valGiamGia) || 0;

    var dieuKien1 = true;
    var dieuKien2 = true;
    var dieuKien3 = true;
    var dieuKien4 = true;
    var dieuKien5 = true;

    if (maCTVoucher == '') {
        document.getElementById('maCTVoucherSuaVL').innerHTML = "Mã chương trình voucher không được bỏ trống";
        $('#maCTVoucherSua').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('maCTVoucherSuaVL').innerHTML = "";
        $('#maCTVoucherSua').css('border-color', '');
        dieuKien1 = true;
        if (maCTVoucher.length > 50) {
            document.getElementById('maCTVoucherSuaVL').innerHTML = "Mã chương trình voucher không được vượt quá 50 kí tự";
            $('#maCTVoucherSua').css('border-color', 'red');
            dieuKien1 = false;
            e.preventDefault();
        }
        else {
            document.getElementById('maCTVoucherSuaVL').innerHTML = "";
            $('#maCTVoucherSua').css('border-color', '');
            dieuKien1 = true;
        }
    }

    if (ngaybd == '') {
        document.getElementById('ngayBDCTVoucherSuaVL').innerHTML = "Ngày tạo không được bỏ trống";
        $('#ngayBDCTVoucherSua').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('ngayBDCTVoucherSuaVL').innerHTML = "";
        $('#ngayBDCTVoucherSua').css('border-color', '');
        dieuKien2 = true;
    }

    if (ngaykt == '') {
        document.getElementById('ngayKTCTVoucherSuaVL').innerHTML = "Ngày kết thúc không được bỏ trống";
        $('#ngayKTCTVoucherSua').css('border-color', 'red');
        dieuKien3 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('ngayKTCTVoucherSuaVL').innerHTML = "";
        $('#ngayKTCTVoucherSua').css('border-color', '');
        dieuKien3 = true;
    }

    if (giamGia == 0 || giamGia < 0 || giamGia > 100) {
        document.getElementById('giaCTVoucherSuaVL').innerHTML = "Giảm giá voucher phải là số nguyên lớn hơn 0 và nhỏ hơn 100";
        $('#giaCTVoucherSua').css('border-color', 'red');
        dieuKien4 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('giaCTVoucherSuaVL').innerHTML = "";
        $('#giaCTVoucherSua').css('border-color', '');
        dieuKien4 = true;
    }

    if (ghiChu == '') {
        document.getElementById('ghiChuCTVoucherSuaVL').innerHTML = "Ghi chú không được bỏ trống";
        $('#ghiChuCTVoucherSua').css('border-color', 'red');
        dieuKien5 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('ghiChuCTVoucherSuaVL').innerHTML = "";
        $('#ghiChuCTVoucherSua').css('border-color', '');
        dieuKien5 = true;
    }

    if (dieuKien1 && dieuKien2 && dieuKien3 && dieuKien4 && dieuKien5) {

        $.ajax({
            url: "/Voucher/SuaCTVoucher",
            type: "POST",
            data: JSON.stringify({
                MACHUONGTRINH: maCTVoucher,
                NGAYTAO: ngaybd,
                NGAYKETTHUC: ngaykt,
                GIAMGIA: giamGia,
                GHICHU: ghiChu,
            }),
            dataType: "Json",
            contentType: "application/json; charset = utf-8",
            success: function (res) {
                if (res.kq) {
                    $.notify(res.message, "success");
                    var timeout = window.setTimeout(function () {
                        window.location.reload();
                    }, 500);
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

function GetCTVoucher(e) {
    var maCT = $(e).attr('id');
    $.ajax({
        url: "/Voucher/GetCTVoucher",
        type: "GET",
        data: { maCT: maCT },
        dataType: "Json",
        contentType: "application/json; charset = utf-8",
        success: function (result) {
            if (result.kq) {
                $('#maCTVoucherSua').val(result.data[0].MACHUONGTRINH);
                $('#giaCTVoucherSua').val(result.data[0].GIAMGIA);
                $('#soLuongVoucherSua').val(result.data[0].SOLUONG);
                $('#ghiChuCTVoucherSua').val(result.data[0].GHICHU);

                var dateString1 = result.data[0].NGAYTAO.substring(6);
                var currentTime1 = new Date(parseInt(dateString1));
                var month1 = currentTime1.getMonth() + 1;
                var day1 = currentTime1.getDate();
                var year1 = currentTime1.getFullYear();
                if (month1.toString().length == 1)
                    month1 = "0" + month1.toString();
                if (day1.toString().length == 1) {
                    day1 = "0" + day1.toString();
                }
                var dateBD = year1 + "-" + month1 + "-" + day1;
                $('#ngayBDCTVoucherSua').val(dateBD);

                var dateString = result.data[0].NGAYKETTHUC.substring(6);
                var currentTime = new Date(parseInt(dateString));
                var month = currentTime.getMonth() + 1;
                var day = currentTime.getDate();
                var year = currentTime.getFullYear();
                if (month.toString().length == 1)
                    month = "0" + month.toString();
                if (day.toString().length == 1) {
                    day = "0" + day.toString();
                }
                var dateKT = year + "-" + month + "-" + day;
                $('#ngayKTCTVoucherSua').val(dateKT);
                $('#myModalEditCTVoucher').modal('show');
                console.log(result.data[0]);
            }
            else {
                $.notify(result.message, "error");
            }

        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    });
}

function GetIDVoucherDelete(e) {
    var maNV = $(e).attr('id');
    $('#idCTVoucherDelete').val(maNV);
}

$('#btnXoaCTVoucher').on('click', function (e) {
    var maCTVoucher = $('#idCTVoucherDelete').val();
    $.ajax({
        url: "/Voucher/XoaCTVoucher",
        type: "GET",
        data: { maCT: maCTVoucher },
        dataType: "Json",
        contentType: "application/json; charset = utf-8",
        success: function (result) {
            if (result.kq) {
                $.notify(result.message, "success");
                var timeout = window.setTimeout(function () {
                    window.location.reload();
                }, 500);
            }
            else {
                $.notify(result.message, "error");
            }
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    });
})

function KhoiPhucCTVoucher(e) {
    var maCT = $(e).attr('id');

    $.ajax({
        url: "/Voucher/KhoiPhucCTVoucher",
        type: "GET",
        data: { maCT: maCT },
        dataType: "Json",
        contentType: "application/json; charset = utf-8",
        success: function (result) {
            if (result.kq) {
                $.notify(result.message, "success");
                var timeout = window.setTimeout(function () {
                    window.location.reload();
                }, 500);
            }
            else {
                $.notify(result.message, "error");
            }
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    });
}

function GetIDVoucherSendMail(e) {
    var maVoucher = $(e).attr('id');
    $('#idCTVoucherSendMail').val(maVoucher);
}

$('#btnGuiMailVoucher').on('click', function (e) {
    var idKH = $('#idKHVoucher').val();
    var maCT = $('#maCTVoucher').val();
    var mavoucher = $('#idCTVoucherSendMail').val();

    var dieuKien1 = true;

    if (idKH == '') {
        document.getElementById('idKHVoucherVL').innerHTML = "Vui lòng chọn khách hàng nhận";
        $('#idKHVoucher').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('idKHVoucherVL').innerHTML = "";
        $('#idKHVoucher').css('border-color', '');
        dieuKien1 = true;
    }

    if (dieuKien1) {
        $.ajax({
            url: "/Voucher/SendMailVoucher",
            type: "GET",
            data: {
                maCT: maCT,
                maVoucher: mavoucher,
                idKH: idKH,
            },
            dataType: "Json",
            contentType: "application/json; charset = utf-8",
            success: function (result) {
                if (result.kq) {
                    $.notify(result.message, "success");
                    var timeout = window.setTimeout(function () {
                        window.location.reload();
                    }, 500);
                }
                else {
                    $.notify(result.message, "error");
                }
            },
            error: function (e) {
                $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
            }
        });
    }
    
})