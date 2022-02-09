$('#btnThemDM').on('click', function (e) {
    var maDM = $('#maDMThem').val();
    var tenDM = $('#tenDMThem').val();

    var dieuKien1 = true;
    var dieuKien2 = true;

    if (maDM == '') {
        document.getElementById('maDMThemVL').innerHTML = "Mã danh mục sản phẩm không được bỏ trống";
        $('#maDMThem').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('maDMThemVL').innerHTML = "";
        $('#maDMThem').css('border-color', '');
        dieuKien1 = true;
        if (maDM.length > 10) {
            document.getElementById('maDMThemVL').innerHTML = "Mã danh mục sản phẩm không được vượt quá 10 kí tự";
            $('#maDMThem').css('border-color', 'red');
            dieuKien1 = false;
            e.preventDefault();
        }
        else {
            document.getElementById('maDMThemVL').innerHTML = "";
            $('#maDMThem').css('border-color', '');
            dieuKien1 = true;
        }
    }



    if (tenDM == '') {
        document.getElementById('tenDMThemVL').innerHTML = "Tên danh mục sản phẩm không được bỏ trống";
        $('#tenDMThem').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tenDMThemVL').innerHTML = "";
        $('#tenDMThem').css('border-color', '');
        dieuKien2 = true;
    }

    if (dieuKien1 && dieuKien2) {
        $.ajax({
            url: "/QuanLy/ThemDanhMucMoi",
            type: "POST",
            data: JSON.stringify({
                MADM: maDM,
                TENDM: tenDM,
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


function GetDanhMuc(e) {
    var maDM = $(e).attr('id');
    $.ajax({
        url: "/QuanLy/GetDanhMuc",
        type: "GET",
        data: { maDM: maDM },
        dataType: "Json",
        contentType: "application/json; charset = utf-8",
        success: function (result) {
            $('#maDMSua').val(result[0].MADM);
            $('#tenDMSua').val(result[0].TENDM);
            $('#myModalEditDM').modal('show');
            console.log(result);
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    });
}


$('#btnCapNhatDM').on('click', function (e) {
    var maDM = $('#maDMSua').val();
    var tenDM = $('#tenDMSua').val();

    var dieuKien1 = true;
    var dieuKien2 = true;

    if (maDM == '') {
        document.getElementById('maDMSuaVL').innerHTML = "Mã danh mục sản phẩm không được bỏ trống";
        $('#maDMSua').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('maDMSuaVL').innerHTML = "";
        $('#maDMSua').css('border-color', '');
        dieuKien1 = true;
        if (maDM.length > 10) {
            document.getElementById('maDMSuaVL').innerHTML = "Mã danh mục sản phẩm không được vượt quá 10 kí tự";
            $('#maDMSua').css('border-color', 'red');
            dieuKien1 = false;
            e.preventDefault();
        }
        else {
            document.getElementById('maDMSuaVL').innerHTML = "";
            $('#maDMSua').css('border-color', '');
            dieuKien1 = true;
        }
    }

    if (tenDM == '') {
        document.getElementById('tenDMSuaVL').innerHTML = "Tên danh mục sản phẩm không được bỏ trống";
        $('#tenDMSua').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tenDMSuaVL').innerHTML = "";
        $('#tenDMSua').css('border-color', '');
        dieuKien2 = true;
    }

    if (dieuKien1 && dieuKien2) {
        $.ajax({
            url: "/QuanLy/SuaDanhMuc",
            type: "POST",
            data: JSON.stringify({
                MADM: maDM,
                TENDM: tenDM,
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

function GetIDDanhMucDelete(e) {
    var maDM = $(e).attr('id');
    $('#idDanhMucDelete').val(maDM);
}


$('#btnXoaDanhMuc').on('click', function (e) {
    var maDM = $('#idDanhMucDelete').val();
    $.ajax({
        url: "/QuanLy/XoaDanhMuc",
        type: "GET",
        data: { maDM: maDM },
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


function KhoiPhucDanhMuc(e) {
    var maDM = $(e).attr('id');
    $.ajax({
        url: "/QuanLy/KhoiPhuDanhMuc",
        type: "GET",
        data: { maDM: maDM },
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