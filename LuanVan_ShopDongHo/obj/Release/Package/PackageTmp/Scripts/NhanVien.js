$('#btnThemNhanVien').on('click', function (e) {
    var maNV = $('#maNVThem').val();
    var tenNV = $('#tenNVThem').val();
    var ngaySinh = $('#ngaySinhNVThem').val();
    var diaChi = $('#diaChiNVThem').val();
    var sdt = $('#sdtNVThem').val();
    var soCMND = $('#soCMNDNVThem').val();
    var gioiTinh = '';
    var tenDN = $('#tenDNNVThem').val();

    if ($('#gioiTinhNVThemNam').prop("checked")) {
        gioiTinh = $('#gioiTinhNVThemNam').val();
    }
    else if ($('#gioiTinhNVThemNu').prop("checked")) {
        gioiTinh = $('#gioiTinhNVThemNu').val();
    }


    var dieuKien1 = true;
    var dieuKien2 = true;
    var dieuKien3 = true;
    var dieuKien4 = true;
    var dieuKien5 = true;
    var dieuKien6 = true;
    var dieuKien7 = true;

    if (maNV == '') {
        document.getElementById('maNVThemVL').innerHTML = "Mã nhân viên không được bỏ trống";
        $('#maNVThem').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('maNVThemVL').innerHTML = "";
        $('#maNVThem').css('border-color', '');
        dieuKien1 = true;
        if (maNV.length > 20) {
            document.getElementById('maNVThemVL').innerHTML = "Mã nhân viên không được vượt quá 20 kí tự";
            $('#maNVThem').css('border-color', 'red');
            dieuKien1 = false;
            e.preventDefault();
        }
        else {
            document.getElementById('maNVThemVL').innerHTML = "";
            $('#maNVThem').css('border-color', '');
            dieuKien1 = true;
        }
    }
    if (tenNV == '') {
        document.getElementById('tenNVThemVL').innerHTML = "Tên nhân viên không được bỏ trống";
        $('#tenNVThem').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tenNVThemVL').innerHTML = "";
        $('#tenNVThem').css('border-color', '');
        dieuKien2 = true;
    }
    if (ngaySinh == '') {
        document.getElementById('ngaySinhNVThemVL').innerHTML = "Ngày sinh nhân viên không được bỏ trống";
        $('#ngaySinhNVThem').css('border-color', 'red');
        dieuKien3 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('ngaySinhNVThemVL').innerHTML = "";
        $('#ngaySinhNVThem').css('border-color', '');
        dieuKien3 = true;
    }
    if (gioiTinh == '') {
        document.getElementById('gioiTinhNVThemVL').innerHTML = "Giới tính nhân viên không được bỏ trống";
        $('#gioiTinhNVThem').css('border-color', 'red');
        dieuKien4 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('gioiTinhNVThemVL').innerHTML = "";
        $('#gioiTinhNVThem').css('border-color', '');
        dieuKien4 = true;
    }
    if (diaChi == '') {
        document.getElementById('diaChiNVThemVL').innerHTML = "Địa chỉ nhân viên không được bỏ trống";
        $('#diaChiNVThem').css('border-color', 'red');
        dieuKien5 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('diaChiNVThemVL').innerHTML = "";
        $('#diaChiNVThem').css('border-color', '');
        dieuKien5 = true;
    }
    if (sdt == '') {
        document.getElementById('sdtNVThemVL').innerHTML = "Số điện thoại không được bỏ trống";
        $('#sdtNVThem').css('border-color', 'red');
        dieuKien6 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('sdtNVThemVL').innerHTML = "";
        $('#sdtNVThem').css('border-color', '');
        dieuKien6 = true;
    }

    if (soCMND == '') {
        document.getElementById('soCMNDNVThemVL').innerHTML = "Số CMND không được bỏ trống";
        $('#soCMNDNVThem').css('border-color', 'red');
        dieuKien7 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('soCMNDNVThemVL').innerHTML = "";
        $('#soCMNDNVThem').css('border-color', '');
        dieuKien7 = true;
    }

    if (dieuKien1 && dieuKien2 && dieuKien3 && dieuKien4 && dieuKien5 && dieuKien6 && dieuKien7) {
        $.ajax({
            url: "/NhanVien/ThemNhanVien",
            type: "POST",
            data: JSON.stringify({
                MANV: maNV,
                TENDN: tenDN,
                NGAYSINH: ngaySinh,
                GIOITINH: gioiTinh,
                DIENTHOAI: sdt,
                SOCMND: soCMND,
                DIACHI: diaChi,
                TENNV: tenNV,
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

function GetNhanVien(e) {
    var maNV = $(e).attr('id');
    $.ajax({
        url: "/NhanVien/GetNhanVien",
        type: "GET",
        data: { maNV: maNV },
        dataType: "Json",
        contentType: "application/json; charset = utf-8",
        success: function (result) {
            $('#maNVSua').val(result[0].MANV);
            $('#tenNVSua').val(result[0].TENNV);

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
            $('#ngaySinhNVSua').val(dateSinh);

            $('#diaChiNVSua').val(result[0].DIACHI);
            $('#sdtNVSua').val(result[0].DIENTHOAI);
            $('#soCMNDNVSua').val(result[0].SOCMND);

            if (result[0].GIOITINH == "Nữ") {
                $('#gioiTinhNVSuaNu').prop('checked', true);
            }
            else if (result[0].GIOITINH == "Nam") {
                $('#gioiTinhNVSuaNam').prop('checked', true);
            }
            $('#tenDNNVSua').val(result[0].TENDN);
            $('#myModalEditNhanVien').modal('show');
            console.log(result);
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    });
}

$('#btnCapNhatNhanVien').on('click', function (e) {
    var maNV = $('#maNVSua').val();
    var tenNV = $('#tenNVSua').val();
    var ngaySinh = $('#ngaySinhNVSua').val();
    var diaChi = $('#diaChiNVSua').val();
    var sdt = $('#sdtNVSua').val();
    var gioiTinh = '';
    var tenDN = $('#tenDNNVSua').val();
    var soCMND = $('#soCMNDNVSua').val();

    if ($('#gioiTinhNVSuaNam').prop("checked")) {
        gioiTinh = $('#gioiTinhNVSuaNam').val();
    }
    else if ($('#gioiTinhNVSuaNu').prop("checked")) {
        gioiTinh = $('#gioiTinhNVSuaNu').val();
    }

    var dieuKien1 = true;
    var dieuKien2 = true;
    var dieuKien3 = true;
    var dieuKien4 = true;
    var dieuKien5 = true;
    var dieuKien6 = true;
    var dieuKien7 = true;

    if (maNV == '') {
        document.getElementById('maNVSuaVL').innerHTML = "Mã nhân viên không được bỏ trống";
        $('#maNVSua').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('maNVSuaVL').innerHTML = "";
        $('#maNVSua').css('border-color', '');
        dieuKien1 = true;
        if (maNV.length > 20) {
            document.getElementById('maNVSuaVL').innerHTML = "Mã nhân viên không được vượt quá 20 kí tự";
            $('#maNVSua').css('border-color', 'red');
            dieuKien1 = false;
            e.preventDefault();
        }
        else {
            document.getElementById('maNVSuaVL').innerHTML = "";
            $('#maNVSua').css('border-color', '');
            dieuKien1 = true;
        }
    }
    if (tenNV == '') {
        document.getElementById('tenNVSuaVL').innerHTML = "Tên nhân viên không được bỏ trống";
        $('#tenNVSua').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tenNVSuaVL').innerHTML = "";
        $('#tenNVSua').css('border-color', '');
        dieuKien2 = true;
    }
    if (ngaySinh == '') {
        document.getElementById('ngaySinhNVSuaVL').innerHTML = "Ngày sinh nhân viên không được bỏ trống";
        $('#ngaySinhNVSua').css('border-color', 'red');
        dieuKien3 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('ngaySinhNVSuaVL').innerHTML = "";
        $('#ngaySinhNVSua').css('border-color', '');
        dieuKien3 = true;
    }
    if (gioiTinh == '') {
        document.getElementById('gioiTinhNVSuaVL').innerHTML = "Giới tính nhân viên không được bỏ trống";
        $('#gioiTinhNVSua').css('border-color', 'red');
        dieuKien4 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('gioiTinhNVSuaVL').innerHTML = "";
        $('#gioiTinhNVSua').css('border-color', '');
        dieuKien4 = true;
    }
    if (diaChi == '') {
        document.getElementById('diaChiNVSuaVL').innerHTML = "Địa chỉ nhân viên không được bỏ trống";
        $('#diaChiNVSua').css('border-color', 'red');
        dieuKien5 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('diaChiNVSuaVL').innerHTML = "";
        $('#diaChiNVSua').css('border-color', '');
        dieuKien5 = true;
    }
    if (sdt == '') {
        document.getElementById('sdtNVSuaVL').innerHTML = "Số điện thoại không được bỏ trống";
        $('#sdtNVSua').css('border-color', 'red');
        dieuKien6 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('sdtNVSuaVL').innerHTML = "";
        $('#sdtNVSua').css('border-color', '');
        dieuKien6 = true;
    }

    if (soCMND == '') {
        document.getElementById('soCMNDKHSuaVL').innerHTML = "Số CMND không được bỏ trống";
        $('#soCMNDKHSua').css('border-color', 'red');
        dieuKien7 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('soCMNDKHSuaVL').innerHTML = "";
        $('#soCMNDKHSua').css('border-color', '');
        dieuKien7 = true;
    }

    if (dieuKien1 && dieuKien2 && dieuKien3 && dieuKien4 && dieuKien5 && dieuKien6 && dieuKien7) {
        $.ajax({
            url: "/NhanVien/SuaNhanVien",
            type: "POST",
            data: JSON.stringify({
                MANV: maNV,
                TENDN: tenDN,
                NGAYSINH: ngaySinh,
                GIOITINH: gioiTinh,
                DIENTHOAI: sdt,
                SOCMND: soCMND,
                DIACHI: diaChi,
                TENNV: tenNV,
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

function GetIDNhanVienDelete(e) {
    var maNV = $(e).attr('id');
    $('#idNhanVienDelete').val(maNV);
}

$('#btnXoaNhanVien').on('click', function (e) {
    var maNV = $('#idNhanVienDelete').val();
    $.ajax({
        url: "/NhanVien/XoaNhanVien",
        type: "GET",
        data: { maNV: maNV },
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

function KhoiPhucNhanVien(e) {
    var maNV = $(e).attr('id');
    $.ajax({
        url: "/NhanVien/KhoiPhucNhanVien",
        type: "GET",
        data: { maNV: maNV },
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
