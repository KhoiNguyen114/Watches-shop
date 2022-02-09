function GetPhanQuyenThemNND(e) {
    var tenDN = $(e).attr('id');
    $('#tenDNNNDThem').val(tenDN);
    $('#myModalAddTKNND').modal('show');
}

function GetPhanQuyenSuaNND(e) {
    var tenDN = $(e).attr('id');
    $.ajax({
        url: "/PhanQuyen/GetPhanQuyenThemVaoNhom",
        type: "GET",
        data: { tenDN: tenDN },
        dataType: "Json",
        contentType: "application/json; charset = utf-8",
        success: function (result) {
            if (result.kq) {
                $('#tenDNNNDSua').val(result.data[0].TENDN);
                $('#tenNNDSua').val(result.data[0].IDGROUP);
                $('#myModalEditTKNND').modal('show');
            }
            else {
                $.notify(result.message, "error");
            }
            console.log(result.data);
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    });
}

$('#btnThemNND').on('click', function (e) {
    var tendn = $('#tenDNNNDThem').val();
    var tenNND = $('#tenNNDThem').val();

    var dieuKien1 = true;
   
    if (tendn == '') {
        document.getElementById('tenDNNNDThemVL').innerHTML = "Tên đăng nhập không được bỏ trống";
        $('#tenDNNNDThem').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tenDNNNDThemVL').innerHTML = "";
        $('#tenDNNNDThem').css('border-color', '');
        dieuKien1 = true;
    }    

    if (dieuKien1) {
        $.ajax({
            url: "/PhanQuyen/ThemGroupPermission",
            type: "POST",
            data: JSON.stringify({
                TENDN: tendn,
                IDGROUP: tenNND,
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

$('#btnSuaNND').on('click', function (e) {
    var tendn = $('#tenDNNNDSua').val();
    var tenNND = $('#tenNNDSua').val();

    var dieuKien1 = true;

    if (tendn == '') {
        document.getElementById('tenDNNNDSuaVL').innerHTML = "Tên đăng nhập không được bỏ trống";
        $('#tenDNNNDSua').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tenDNNNDSuaVL').innerHTML = "";
        $('#tenDNNNDSua').css('border-color', '');
        dieuKien1 = true;
    }

    if (dieuKien1) {
        $.ajax({
            url: "/PhanQuyen/CapNhatGroupPermission",
            type: "POST",
            data: JSON.stringify({
                TENDN: tendn,
                IDGROUP: tenNND,
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

$('#btnLuuPhanQuyen').on('click', function (e) {
    var result =
        $("tr.checkphanquyen > td > input:checkbox:checked").get();
    var a = $.map(result, function (element) {
        return $(element).attr("value");
    });
    var value = a.join("|");
    var b = $.map(result, function (element) {
        return $(element).attr("id");
    });
    var id = b.join("|");
    console.log(value);
    console.log(id);

    $.ajax({
        url: "/PhanQuyen/LuuPhanQuyen",
        type: "POST",
        data: JSON.stringify({
            tenNND: id,
            quyen: value,
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

});

$('#btnThemNNDMoi').on('click', function (e) {
    var tenNND = $('#tenNNDMoi').val();

    var dieuKien1 = true;
    if (tenNND == '') {
        document.getElementById('tenNNDMoiVL').innerHTML = "Tên nhóm người dùng không được bỏ trống";
        $('#tenNNDMoi').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tenNNDMoiVL').innerHTML = "";
        $('#tenNNDMoi').css('border-color', '');
        dieuKien1 = true;
    }

    if (dieuKien1) {
        $.ajax({
            url: "/PhanQuyen/ThemNhomNguoiDung",
            type: "POST",
            data: JSON.stringify({
                NAME: tenNND,
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