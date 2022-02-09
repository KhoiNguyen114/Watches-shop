$('#btnThemTaiKhoan').on('click', function (e) {
    var tendn = $('#tenDNTKThem').val();
    var matKhau = $('#matKhauTKThem').val();
    var email = $('#emailTKThem').val();
    var reg = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    var dieuKien1 = true;
    var dieuKien2 = true;
    var dieuKien3 = true;

    if (tendn == '') {
        document.getElementById('tenDNTKThemVL').innerHTML = "Tên đăng nhập không được bỏ trống";
        $('#tenDNTKThem').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tenDNTKThemVL').innerHTML = "";
        $('#tenDNTKThem').css('border-color', '');
        dieuKien1 = true;
        if (tendn.length > 50) {
            document.getElementById('tenDNTKThemVL').innerHTML = "Tên đăng nhập không được vượt quá 50 kí tự";
            $('#tenDNTKThem').css('border-color', 'red');
            dieuKien1 = false;
            e.preventDefault();
        }
        else {
            document.getElementById('tenDNTKThemVL').innerHTML = "";
            $('#tenDNTKThem').css('border-color', '');
            dieuKien1 = true;
        }
    }

    if (matKhau == '') {
        document.getElementById('matKhauTKThemVL').innerHTML = "Mật khẩu không được bỏ trống";
        $('#matKhauTKThem').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('matKhauTKThemVL').innerHTML = "";
        $('#matKhauTKThem').css('border-color', '');
        dieuKien2 = true;
    }

    if (email == '') {
        document.getElementById('emailTKThemVL').innerHTML = "Email không được bỏ trống";
        $('#emailTKThem').css('border-color', 'red');
        dieuKien3 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('emailTKThemVL').innerHTML = "";
        $('#emailTKThem').css('border-color', '');
        dieuKien3 = true;
        if (!reg.test(email)) {
            document.getElementById('emailTKThemVL').innerHTML = "Email không đúng định dạng";
            $('#emailTKThem').css('border-color', 'red');
            dieuKien3 = false;
            e.preventDefault();
        }
        else {
            document.getElementById('emailTKThemVL').innerHTML = "";
            $('#emailTKThem').css('border-color', '');
            dieuKien3 = true;
        }
    }

    if (dieuKien1 && dieuKien2 && dieuKien3) {
        $.ajax({
            url: "/TaiKhoan/TaoTaiKhoan",
            type: "POST",
            data: JSON.stringify({
                TENDN: tendn,
                MATKHAU: matKhau,
                Email: email,
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

function GetTaiKhoan(e) {
    var tenDN = $(e).attr('id');
    $.ajax({
        url: "/TaiKhoan/GetTaiKhoan",
        type: "GET",
        data: { tenDN: tenDN },
        dataType: "Json",
        contentType: "application/json; charset = utf-8",
        success: function (result) {
            $('#tenDNTKSua').val(result[0].TENDN);
            $('#matKhauTKSua').val(result[0].MATKHAU);
            $('#emailTKSua').val(result[0].Email);
            if (result[0].TRANGTHAI == false) {
                $('#trangThaiTKSuaF').prop('checked', true);
            }
            else {
                $('#trangThaiTKSuaT').prop('checked', true);
            }
            $('#myModalEditTaiKhoan').modal('show');
            console.log(result);
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    });
}

$('#btnCapNhatTaiKhoan').on('click', function (e) {
    var tendn = $('#tenDNTKSua').val();
    var matKhau = $('#matKhauTKSua').val();
    var email = $('#emailTKSua').val();

    var reg = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    var dieuKien1 = true;
    var dieuKien2 = true;
    var dieuKien3 = true;

    if (tendn == '') {
        document.getElementById('tenDNTKSuaVL').innerHTML = "Tên đăng nhập không được bỏ trống";
        $('#tenDNTKSua').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tenDNTKSuaVL').innerHTML = "";
        $('#tenDNTKSua').css('border-color', '');
        dieuKien1 = true;
        if (tendn.length > 50) {
            document.getElementById('tenDNTKSuaVL').innerHTML = "Tên đăng nhập không được vượt quá 50 kí tự";
            $('#tenDNTKSua').css('border-color', 'red');
            dieuKien1 = false;
            e.preventDefault();
        }
        else {
            document.getElementById('tenDNTKSuaVL').innerHTML = "";
            $('#tenDNTKSua').css('border-color', '');
            dieuKien1 = true;
        }
    }

    if (matKhau == '') {
        document.getElementById('matKhauTKSuaVL').innerHTML = "Mật khẩu không được bỏ trống";
        $('#matKhauTKSua').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('matKhauTKSuaVL').innerHTML = "";
        $('#matKhauTKSua').css('border-color', '');
        dieuKien2 = true;
    }

    if (email == '') {
        document.getElementById('emailTKSuaVL').innerHTML = "Email không được bỏ trống";
        $('#emailTKSua').css('border-color', 'red');
        dieuKien3 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('emailTKSuaVL').innerHTML = "";
        $('#emailTKSua').css('border-color', '');
        dieuKien3 = true;
        if (!reg.test(email)) {
            document.getElementById('emailTKSuaVL').innerHTML = "Email không đúng định dạng";
            $('#emailTKSua').css('border-color', 'red');
            dieuKien3 = false;
            e.preventDefault();
        }
        else {
            document.getElementById('emailTKSuaVL').innerHTML = "";
            $('#emailTKSua').css('border-color', '');
            dieuKien3 = true;
        }
    }


    if (dieuKien1 && dieuKien2 && dieuKien3) {
        $.ajax({
            url: "/TaiKhoan/CapNhatTaiKhoan",
            type: "POST",
            data: JSON.stringify({
                TENDN: tendn,
                MATKHAU: matKhau,
                Email: email,
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

function GetIDTaiKhoanDelete(e) {
    var tenDN = $(e).attr('id');
    $('#idTaiKhoanDelete').val(tenDN);
}

$('#btnXoaTaiKhoan').on('click', function (e) {
    var tenDN = $('#idTaiKhoanDelete').val();
    $.ajax({
        url: "/TaiKhoan/BanTaiKhoan",
        type: "GET",
        data: { tenDN: tenDN },
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
})

function KhoiPhucTaiKhoan(e) {
    var tenDN = $(e).attr('id');
    $.ajax({
        url: "/TaiKhoan/KhoiPhucTaiKhoan",
        type: "GET",
        data: { tenDN: tenDN },
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


$('#btnDoiMatKhauTaiKhoan').on('click', function (e) {
    var matKhauCu = $('#matKhauCu').val();
    var matKhauMoi = $('#matKhauMoi').val();
    var xacNhanMatKhau = $('#xacNhanMatKhau').val();

    var dieuKien1 = true;
    var dieuKien2 = true;
    var dieuKien3 = true;

    if (matKhauCu == '') {
        document.getElementById('matKhauCuVL').innerHTML = "Mật khẩu cũ không được bỏ trống";
        $('#matKhauCu').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('matKhauCuVL').innerHTML = "";
        $('#matKhauCu').css('border-color', '');
    }

    if (matKhauMoi == '') {
        document.getElementById('matKhauMoiVL').innerHTML = "Mật khẩu mới không được bỏ trống";
        $('#matKhauMoi').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('matKhauMoiVL').innerHTML = "";
        $('#matKhauMoi').css('border-color', '');
        dieuKien2 = true;
    }

    if (xacNhanMatKhau == '') {
        document.getElementById('xacNhanMatKhauVL').innerHTML = "Mật khẩu xác nhận không được bỏ trống";
        $('#xacNhanMatKhau').css('border-color', 'red');
        dieuKien3 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('xacNhanMatKhauVL').innerHTML = "";
        $('#xacNhanMatKhau').css('border-color', '');
        dieuKien3 = true;
        if (xacNhanMatKhau != matKhauMoi) {
            document.getElementById('xacNhanMatKhauVL').innerHTML = "Mật khẩu xác nhận không trùng khớp";
            $('#xacNhanMatKhau').css('border-color', 'red');
            dieuKien3 = false;
            e.preventDefault();
        }
        else {
            document.getElementById('xacNhanMatKhauVL').innerHTML = "";
            $('#xacNhanMatKhau').css('border-color', '');
            dieuKien3 = true;
        }
    }


    if (dieuKien1 && dieuKien2 && dieuKien3) {
        $.ajax({
            url: "/TaiKhoan/DoiMatKhau",
            type: "POST",
            data: JSON.stringify({
                matKhauMoi: matKhauMoi,
                matKhauCu: matKhauCu,
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