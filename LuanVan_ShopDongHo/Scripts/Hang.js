function LoadAnhFolderThemLogoHang(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#Image_LogoHang').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function LoadAnhFolderThemBannerHang(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#Image_bannerHang').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function LoadAnhFolderThemLogoHangSua(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#Image_LogoHangSua').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function LoadAnhFolderThemBannerHangSua(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#Image_bannerHangSua').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

$('#btnThemHang').on('click', function (e) {
    var maHang = $('#maHangThem').val();
    var tenHang = $('#tenHangThem').val();
    var thongTin = $('#thongTinHangThem').val();

    var hinhAnh = $('#LogoHangThem').get(0).files.length;
    var hinh = $('#LogoHangThem').val();

    var url = $('#urlHangThem').val();

    var banner = $('#BannerHangThem').val();

    var quocGia = $('#quocGiaHangThem').val();


    var dieuKien1 = true;
    var dieuKien2 = true;
    var dieuKien3 = true;
    var dieuKien4 = true;
    var dieuKien5 = true;

    if (maHang == '') {
        document.getElementById('maHangThemVL').innerHTML = "Mã hãng không được bỏ trống";
        $('#maHangThem').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('maHangThemVL').innerHTML = "";
        $('#maHangThem').css('border-color', '');
        dieuKien1 = true;
        if (maHang.length > 10) {
            document.getElementById('maHangThemVL').innerHTML = "Mã hãng không được vượt quá 10 kí tự";
            $('#maHangThem').css('border-color', 'red');
            dieuKien1 = false;
            e.preventDefault();
        }
        else {
            document.getElementById('maHangThemVL').innerHTML = "";
            $('#maHangThem').css('border-color', '');
            dieuKien1 = true;
        }
    }

    if (tenHang == '') {
        document.getElementById('tenHangThemVL').innerHTML = "Tên hãng không được bỏ trống";
        $('#tenHangThem').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tenHangThemVL').innerHTML = "";
        $('#tenHangThem').css('border-color', '');
        dieuKien2 = true;
    }
    if (thongTin == '') {
        document.getElementById('thongTinHangThemVL').innerHTML = "Thông tin về hãng không được bỏ trống";
        $('#thongTinHangThem').css('border-color', 'red');
        dieuKien3 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('thongTinHangThemVL').innerHTML = "";
        $('#thongTinHangThem').css('border-color', '');
        dieuKien3 = true;
    }

    if (hinhAnh == 0) {
        document.getElementById('LogoHangThemVL').innerHTML = "Vui lòng chọn hình ảnh logo cho hãng";
        dieuKien4 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('LogoHangThemVL').innerHTML = "";
        dieuKien4 = true;
    }

    if (url == '') {
        document.getElementById('urlHangThemVL').innerHTML = "Link chính thức của hãng không được bỏ trống";
        $('#urlHangThem').css('border-color', 'red');
        dieuKien5 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('urlHangThemVL').innerHTML = "";
        $('#urlHangThem').css('border-color', '');
        dieuKien5 = true;
    }

    if (dieuKien1 && dieuKien2 && dieuKien3 && dieuKien4 && dieuKien5) {
        $.ajax({
            url: "/QuanLy/ThemHangMoi",
            type: "POST",
            data: JSON.stringify({
                MAHANG: maHang,
                TENHANG: tenHang,
                THONGTIN: thongTin,
                LOGO: hinh,
                URL: url,
                BANNER: banner,
                QUOCGIA: quocGia,
            }),
            dataType: "Json",
            contentType: "application/json; charset = utf-8",
            success: function (res) {
                if (res.kq) {
                    loadAnhLogoHang('LogoHangThem');
                    loadAnhBannerHang('BannerHangThem');
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

function loadAnhLogoHang(id) {
    var formData = new FormData();
    var file = document.getElementById(id).files[0];
    formData.append(id, file);

    $.ajax({
        url: "/QuanLy/UpLoadAnhLogoHang",
        type: "POST",
        data: formData,
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (res) {

        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    });
}

function loadAnhBannerHang(id) {
    var formData = new FormData();
    var file = document.getElementById(id).files[0];
    formData.append(id, file);

    $.ajax({
        url: "/QuanLy/UpLoadAnhBannerHang",
        type: "POST",
        data: formData,
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (res) {

        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    });
}

function GetHang(e) {
    var maHang = $(e).attr('id');
    $.ajax({
        url: "/QuanLy/GetHang",
        type: "GET",
        data: { maHang: maHang },
        dataType: "Json",
        contentType: "application/json; charset = utf-8",
        success: function (result) {
            $('#maHangSua').val(result[0].MAHANG);
            $('#tenHangSua').val(result[0].TENHANG);
            $('#thongTinHangSua').val(result[0].THONGTIN);
            $('#Image_LogoHangSua').attr('src', '../Content/Images/Brand_Logo/' + result[0].LOGO + '');
            $('#Image_bannerHangSua').attr('src', '../Content/Images/BannerHang/' + result[0].BANNER + '');
            $('#urlHangSua').val(result[0].URL);
            $('#quocGiaHangSua').val(result[0].QUOCGIA);
            $('#trangThaiHangSua').val(result[0].TRANGTHAI);
            $('#myModalEditHang').modal('show');
            console.log(result);
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    });
}

$('#btnCapNhatHang').on('click', function (e) {
    var maHang = $('#maHangSua').val();
    var tenHang = $('#tenHangSua').val();
    var thongTin = $('#thongTinHangSua').val();
    var hinh = $('#LogoHangSua').val();
    var url = $('#urlHangSua').val();
    var banner = $('#BannerHangSua').val();
    var quocGia = $('#quocGiaHangSua').val();

    var dieuKien1 = true;
    var dieuKien2 = true;
    var dieuKien3 = true;
    var dieuKien4 = true;

    if (maHang == '') {
        document.getElementById('maHangSuaVL').innerHTML = "Mã hãng không được bỏ trống";
        $('#maHangSua').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('maHangSuaVL').innerHTML = "";
        $('#maHangSua').css('border-color', '');
        dieuKien1 = true;
    }
    if (maHang.length > 10) {
        document.getElementById('maHangSuaVL').innerHTML = "Mã hãng không được vượt quá 10 kí tự";
        $('#maHangSua').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('maHangSuaVL').innerHTML = "";
        $('#maHangSua').css('border-color', '');
        dieuKien1 = true;
    }
    if (tenHang == '') {
        document.getElementById('tenHangSuaVL').innerHTML = "Tên hãng không được bỏ trống";
        $('#tenHangSua').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tenHangSuaVL').innerHTML = "";
        $('#tenHangSua').css('border-color', '');
        dieuKien2 = true;
    }
    if (thongTin == '') {
        document.getElementById('thongTinHangSuaVL').innerHTML = "Thông tin về hãng không được bỏ trống";
        $('#thongTinHangSua').css('border-color', 'red');
        dieuKien3 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('thongTinHangSuaVL').innerHTML = "";
        $('#thongTinHangSua').css('border-color', '');
        dieuKien3 = true;
    }

    if (url == '') {
        document.getElementById('urlHangSuaVL').innerHTML = "Link chính thức của hãng không được bỏ trống";
        $('#urlHangSua').css('border-color', 'red');
        dieuKien4 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('urlHangSuaVL').innerHTML = "";
        $('#urlHangSua').css('border-color', '');
        dieuKien4 = true;
    }

    if (dieuKien1 && dieuKien2 && dieuKien3 && dieuKien4) {
        $.ajax({
            url: "/QuanLy/SuaHang",
            type: "POST",
            data: JSON.stringify({
                MAHANG: maHang,
                TENHANG: tenHang,
                THONGTIN: thongTin,
                LOGO: hinh,
                URL: url,
                BANNER: banner,
                QUOCGIA: quocGia,
            }),
            dataType: "Json",
            contentType: "application/json; charset = utf-8",
            success: function (res) {
                if (res.kq) {
                    loadAnhLogoHang('LogoHangSua');
                    loadAnhBannerHang('BannerHangSua');
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

function GetIDHangDelete(e) {
    var maHang = $(e).attr('id');
    $('#idHangDelete').val(maHang);
}

$('#btnXoaHang').on('click', function (e) {
    var maHang = $('#idHangDelete').val();
    $.ajax({
        url: "/QuanLy/XoaHang",
        type: "GET",
        data: { maHang: maHang },
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

function KhoiPhucHang(e) {
    var maHang = $(e).attr('id');
    $.ajax({
        url: "/QuanLy/KhoiPhucHang",
        type: "GET",
        data: { maHang: maHang },
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