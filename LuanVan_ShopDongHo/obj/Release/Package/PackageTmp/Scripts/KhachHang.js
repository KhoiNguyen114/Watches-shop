//$('#btnThemKhachHang').on('click', function (e) {
//    var maKH = $('#maKHThem').val();
//    var tenKH = $('#tenKHThem').val();
//    var ngaySinh = $('#ngaySinhKHThem').val();
//    var diaChi = $('#diaChiKHThem').val();
//    var sdt = $('#sdtKHThem').val();
//    var soCMND = $('#sdtKHThem').val();
//    var gioiTinh = '';
//    var tenDN = $('#tenDNKHThem').val();

//    if ($('#gioiTinhKHThemNam').prop("checked")) {
//        gioiTinh = $('#gioiTinhKHThemNam').val();
//    }
//    else if ($('#gioiTinhKHThemNu').prop("checked")) {
//        gioiTinh = $('#gioiTinhKHThemNu').val();
//    }


//    var dieuKien1 = true;
//    var dieuKien2 = true;
//    var dieuKien3 = true;
//    var dieuKien4 = true;
//    var dieuKien5 = true;
//    var dieuKien6 = true;
//    var dieuKien7 = true;

//    if (maKH == '') {
//        document.getElementById('maKHThemVL').innerHTML = "Mã khách hàng không được bỏ trống";
//        $('#maKHThem').css('border-color', 'red');
//        dieuKien1 = false;
//        e.preventDefault();
//    }
//    else {
//        document.getElementById('maKHThemVL').innerHTML = "";
//        $('#maKHThem').css('border-color', '');
//        dieuKien1 = true;
//        if (maKH.length > 50) {
//            document.getElementById('maKHThemVL').innerHTML = "Mã khách hàng không được vượt quá 50 kí tự";
//            $('#maKHThem').css('border-color', 'red');
//            dieuKien1 = false;
//            e.preventDefault();
//        }
//        else {
//            document.getElementById('maKHThemVL').innerHTML = "";
//            $('#maKHThem').css('border-color', '');
//            dieuKien1 = true;
//        }
//    }
//    if (tenKH == '') {
//        document.getElementById('tenKHThemVL').innerHTML = "Tên khách hàng không được bỏ trống";
//        $('#tenKHThem').css('border-color', 'red');
//        dieuKien2 = false;
//        e.preventDefault();
//    }
//    else {
//        document.getElementById('tenKHThemVL').innerHTML = "";
//        $('#tenKHThem').css('border-color', '');
//        dieuKien2 = true;
//    }
//    if (ngaySinh == '') {
//        document.getElementById('ngaySinhKHThemVL').innerHTML = "Ngày sinh khách hàng không được bỏ trống";
//        $('#ngaySinhKHThem').css('border-color', 'red');
//        dieuKien3 = false;
//        e.preventDefault();
//    }
//    else {
//        document.getElementById('ngaySinhKHThemVL').innerHTML = "";
//        $('#ngaySinhKHThem').css('border-color', '');
//        dieuKien3 = true;
//    }
//    if (gioiTinh == '') {
//        document.getElementById('gioiTinhKHThemVL').innerHTML = "Giới tính khách hàng không được bỏ trống";
//        $('#gioiTinhKHThem').css('border-color', 'red');
//        dieuKien4 = false;
//        e.preventDefault();
//    }
//    else {
//        document.getElementById('gioiTinhKHThemVL').innerHTML = "";
//        $('#gioiTinhKHThem').css('border-color', '');
//        dieuKien4 = true;
//    }
//    if (diaChi == '') {
//        document.getElementById('diaChiKHThemVL').innerHTML = "Địa chỉ khách hàng không được bỏ trống";
//        $('#diaChiKHThem').css('border-color', 'red');
//        dieuKien5 = false;
//        e.preventDefault();
//    }
//    else {
//        document.getElementById('diaChiKHThemVL').innerHTML = "";
//        $('#diaChiKHThem').css('border-color', '');
//        dieuKien5 = true;
//    }
//    if (sdt == '') {
//        document.getElementById('sdtKHThemVL').innerHTML = "Số điện thoại không được bỏ trống";
//        $('#sdtKHThem').css('border-color', 'red');
//        dieuKien6 = false;
//        e.preventDefault();
//    }
//    else {
//        document.getElementById('sdtKHThemVL').innerHTML = "";
//        $('#sdtKHThem').css('border-color', '');
//        dieuKien6 = true;
//    }

//    if (soCMND == '') {
//        document.getElementById('soCMNDKHThemVL').innerHTML = "Số CMND không được bỏ trống";
//        $('#soCMNDKHThem').css('border-color', 'red');
//        dieuKien6 = false;
//        e.preventDefault();
//    }
//    else {
//        document.getElementById('soCMNDKHThemVL').innerHTML = "";
//        $('#soCMNDKHThem').css('border-color', '');
//        dieuKien6 = true;
//    }

//    if (dieuKien1 && dieuKien2 && dieuKien3 && dieuKien4 && dieuKien5 && dieuKien6 && dieuKien7) {
//        $.ajax({
//            url: "/KhachHang/ThemKhachHang",
//            type: "POST",
//            data: JSON.stringify({
//                MAKH: maKH,
//                TENDN: tenDN,
//                NGAYSINH: ngaySinh,
//                GIOITINH: gioiTinh,
//                SDT: sdt,
//                SOCMND: soCMND,
//                DIACHI: diaChi,
//                TENKH: tenKH,
//            }),
//            dataType: "Json",
//            contentType: "application/json; charset = utf-8",
//            success: function (res) {
//                if (res.kq) {
//                    $.notify(res.message, "success");
//                  var timeout = window.setTimeout(function () {
//                          window.location.reload();
//                      }, 500);
//                }
//                else {
//                    $.notify(res.message, "error");
//                }
//            },
//            error: function (e) {
//                alert(e.responseText);
//            }
//        });
//    }
//})

function GetKhachHang(e) {
    var maKH = $(e).attr('id');
    $.ajax({
        url: "/KhachHang/GetKhachHang",
        type: "GET",
        data: { maKH: maKH },
        dataType: "Json",
        contentType: "application/json; charset = utf-8",
        success: function (result) {
            $('#maKHSua').val(result[0].MAKH);
            $('#tenKHSua').val(result[0].TENKH);

            var dateString = result[0].NGAYSINH.substring(6);
            var currentTime = new Date(parseInt(dateString));
            var month = currentTime.getMonth() + 1;
            var day = currentTime.getDate();
            var year = currentTime.getFullYear();
            if (month.toString().length == 1)
                month = "0" + month.toString();
            if (day.toString().length == 1) {
                day = "0" + day.toString();
            }
            var dateSinh = year + "-" + month + "-" + day;
            $('#ngaySinhKHSua').val(dateSinh);

            $('#diaChiKHSua').val(result[0].DIACHI);
            $('#sdtKHSua').val(result[0].SDT);
            $('#soCMNDKHSua').val(result[0].SOCMND);

            if (result[0].GIOITINH == "Nữ") {
                $('#gioiTinhKHSuaNu').prop('checked', true);
            }
            else if (result[0].GIOITINH == "Nam") {
                $('#gioiTinhKHSuaNam').prop('checked', true);
            }
            //$('#tenDNKHSua').val(result[0].TENDN);
            $('#myModalEditKhachHang').modal('show');
            console.log(result);
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    });
}

$('#btnCapNhatKhachHang').on('click', function (e) {
    var maKH = $('#maKHSua').val();
    var tenKH = $('#tenKHSua').val();
    var ngaySinh = $('#ngaySinhKHSua').val();
    var diaChi = $('#diaChiKHSua').val();
    var sdt = $('#sdtKHSua').val();
    var soCMND = $('#soCMNDKHSua').val();
    var gioiTinh = '';
    //var tenDN = $('#tenDNKHSua').val();

    if ($('#gioiTinhKHSuaNam').prop("checked")) {
        gioiTinh = $('#gioiTinhKHSuaNam').val();
    }
    else if ($('#gioiTinhKHSuaNu').prop("checked")) {
        gioiTinh = $('#gioiTinhKHSuaNu').val();
    }

    var dieuKien1 = true;
    var dieuKien2 = true;
    var dieuKien3 = true;
    var dieuKien4 = true;
    var dieuKien5 = true;
    var dieuKien6 = true;
    var dieuKien7 = true;

    if (maKH == '') {
        document.getElementById('maKHSuaVL').innerHTML = "Mã khách hàng không được bỏ trống";
        $('#maKHSua').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('maKHSuaVL').innerHTML = "";
        $('#maKHSua').css('border-color', '');
        dieuKien1 = true;
        if (maKH.length > 50) {
            document.getElementById('maKHSuaVL').innerHTML = "Mã khách hàng không được vượt quá 50 kí tự";
            $('#maKHSua').css('border-color', 'red');
            dieuKien1 = false;
            e.preventDefault();
        }
        else {
            document.getElementById('maKHSuaVL').innerHTML = "";
            $('#maKHSua').css('border-color', '');
            dieuKien1 = true;
        }
    }
    if (tenKH == '') {
        document.getElementById('tenKHSuaVL').innerHTML = "Tên khách hàng không được bỏ trống";
        $('#tenKHSua').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tenKHSuaVL').innerHTML = "";
        $('#tenKHSua').css('border-color', '');
        dieuKien2 = true;
    }
    if (ngaySinh == '') {
        document.getElementById('ngaySinhKHSuaVL').innerHTML = "Ngày sinh khách hàng không được bỏ trống";
        $('#ngaySinhKHSua').css('border-color', 'red');
        dieuKien3 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('ngaySinhKHSuaVL').innerHTML = "";
        $('#ngaySinhKHSua').css('border-color', '');
        dieuKien3 = true;
    }
    if (gioiTinh == '') {
        document.getElementById('gioiTinhKHSuaVL').innerHTML = "Giới tính khách hàng không được bỏ trống";
        $('#gioiTinhKHSua').css('border-color', 'red');
        dieuKien4 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('gioiTinhKHSuaVL').innerHTML = "";
        $('#gioiTinhKHSua').css('border-color', '');
        dieuKien4 = true;
    }
    if (diaChi == '') {
        document.getElementById('diaChiKHSuaVL').innerHTML = "Địa chỉ khách hàng không được bỏ trống";
        $('#diaChiKHSua').css('border-color', 'red');
        dieuKien5 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('diaChiKHSuaVL').innerHTML = "";
        $('#diaChiKHSua').css('border-color', '');
        dieuKien5 = true;
    }
    if (sdt == '') {
        document.getElementById('sdtKHSuaVL').innerHTML = "Số điện thoại không được bỏ trống";
        $('#sdtKHSua').css('border-color', 'red');
        dieuKien6 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('sdtKHSuaVL').innerHTML = "";
        $('#sdtKHSua').css('border-color', '');
        dieuKien6 = true;
    }

    if (soCMND == '') {
        document.getElementById('soCMNDKHSuaVL').innerHTML = "Số CMND không được bỏ trống";
        $('#soCMNDKHSua').css('border-color', 'red');
        dieuKien6 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('soCMNDKHSuaVL').innerHTML = "";
        $('#soCMNDKHSua').css('border-color', '');
        dieuKien6 = true;
    }

    if (dieuKien1 && dieuKien2 && dieuKien3 && dieuKien4 && dieuKien5 && dieuKien6 && dieuKien7) {
        $.ajax({
            url: "/KhachHang/SuaKhachHang",
            type: "POST",
            data: JSON.stringify({
                MAKH: maKH,
                //TENDN: tenDN,
                NGAYSINH: ngaySinh,
                GIOITINH: gioiTinh,
                SDT: sdt,
                SOCMND: soCMND,
                DIACHI: diaChi,
                TENKH: tenKH,
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
        });
    }
})

function GetIDKhachHangDelete(e) {
    var maKH = $(e).attr('id');
    $('#idKhachHangDelete').val(maKH);
}

$('#btnXoaKhachHang').on('click', function (e) {
    var maKH = $('#idKhachHangDelete').val();
    $.ajax({
        url: "/KhachHang/XoaKhachHang",
        type: "GET",
        data: { maKH: maKH },
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

function KhoiPhucKhachHang(e) {
    var maKH = $(e).attr('id');
    $.ajax({
        url: "/KhachHang/KhoiPhucKhachHang",
        type: "GET",
        data: { maKH: maKH },
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

