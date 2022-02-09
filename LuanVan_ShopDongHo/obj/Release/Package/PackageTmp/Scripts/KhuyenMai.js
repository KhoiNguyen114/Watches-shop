$('#btnThemKM').on('click', function (e) {
    var maKM = $('#maKMThem').val();
    var tgbd = $('#tgbdKMThem').val();
    var tgkt = $('#tgktKMThem').val();
    var maSP = $('#maSPKMThem').val();
    var hinhAnh = $('#hinhAnhKMThem').get(0).files.length;
    var hinh = $('#hinhAnhKMThem').val();

    var valGia = $('#giaKMThem').val();
    var gia = parseInt(valGia) || 0;

    var dieuKien1 = true;
    var dieuKien2 = true;
    var dieuKien3 = true;
    var dieuKien4 = true;
    var dieuKien5 = true;

    if (maKM == '') {
        document.getElementById('maKMThemVL').innerHTML = "Mã khuyến mãi không được bỏ trống";
        $('#maKMThem').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('maKMThemVL').innerHTML = "";
        $('#maKMThem').css('border-color', '');
        dieuKien1 = true;
        if (maKM.length > 200) {
            document.getElementById('maKMThemVL').innerHTML = "Mã khuyến mãi không được vượt quá 200 kí tự";
            $('#maKMThem').css('border-color', 'red');
            dieuKien1 = false;
            e.preventDefault();
        }
        else {
            document.getElementById('maKMThemVL').innerHTML = "";
            $('#maKMThem').css('border-color', '');
            dieuKien1 = true;
        }
    }

    if (tgbd == '') {
        document.getElementById('tgbdKMThemVL').innerHTML = "Thời gian bắt đầu không được bỏ trống";
        $('#tgbdKMThem').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tgbdKMThemVL').innerHTML = "";
        $('#tgbdKMThem').css('border-color', '');
        dieuKien2 = true;
    }

    if (tgkt == '') {
        document.getElementById('tgktKMThemVL').innerHTML = "Thời gian kết thúc không được bỏ trống";
        $('#tgktKMThem').css('border-color', 'red');
        dieuKien3 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tgktKMThemVL').innerHTML = "";
        $('#tgktKMThem').css('border-color', '');
        dieuKien3 = true;
    }

    if (gia == 0 || gia < 0 || gia > 100) {
        document.getElementById('giaKMThemVL').innerHTML = "Giá khuyến mãi phải là số nguyên lớn hơn 0 và nhỏ hơn 100";
        $('#giaKMThem').css('border-color', 'red');
        dieuKien4 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('giaKMThemVL').innerHTML = "";
        $('#giaKMThem').css('border-color', '');
        dieuKien4 = true;
    }

    if (maSP == '') {
        document.getElementById('maSPKMThemVL').innerHTML = "Mã sản phẩm không được bỏ trống";
        $('#maSPKMThem').css('border-color', 'red');
        dieuKien5 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('maSPKMThemVL').innerHTML = "";
        $('#maSPKMThem').css('border-color', '');
        dieuKien5 = true;
    }

    if (dieuKien1 && dieuKien2 && dieuKien3 && dieuKien4 && dieuKien5) {

        $.ajax({
            url: "/KhuyenMai/ThemKhuyenMai",
            type: "POST",
            data: JSON.stringify({
                'km.maKM': maKM,
                'km.thoiGianBatDau': tgbd,
                'km.thoiGianKetThuc': tgkt,
                'ctkm.maSP': maSP,
                'km.km': gia,
                'km.hinhAnh': hinh,
            }),
            dataType: "Json",
            contentType: "application/json; charset = utf-8",
            success: function (res) {
                if (res.kq) {
                    loadAnh('hinhAnhKMThem');
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


function GetKhuyenMai(e) {
    var maKM = $(e).attr('id');
    $.ajax({
        url: "/KhuyenMai/GetKhuyenMai",
        type: "GET",
        data: { maKM: maKM },
        dataType: "Json",
        contentType: "application/json; charset = utf-8",
        success: function (result) {
            if (result.kq) {
                $('#maKMSua').val(result.data[0].MAKM);
                $('#giaKMSua').val(result.data[0].KHUYENMAI1);

                var dateString1 = result.data[0].THOIGIANBD.substring(6);
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
                $('#tgbdKMSua').val(dateBD);

                var dateString = result.data[0].THOIGIANKT.substring(6);
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
                $('#tgktKMSua').val(dateKT);

                console.log(dateBD);
                console.log(dateKT);

                var chuoi = result.sp.split(',');
                $('#maSPKMSua').val(chuoi).change();
                $('#Image_KMSua').attr('src', '../../Content/Images/KhuyenMai/' + result.data[0].HINHANH + '');

                $('#myModalEditKM').modal('show');
                console.log(chuoi);
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


$('#btnCapNhatKM').on('click', function (e) {
    var maKM = $('#maKMSua').val();
    var tgbd = $('#tgbdKMSua').val();
    var tgkt = $('#tgktKMSua').val();
    var maSP = $('#maSPKMSua').val();
    var hinhAnh = $('#hinhAnhKMSua').get(0).files.length;
    var hinh = $('#hinhAnhKMSua').val();

    var valGia = $('#giaKMSua').val();
    var gia = parseInt(valGia) || 0;

    var dieuKien1 = true;
    var dieuKien2 = true;
    var dieuKien3 = true;
    var dieuKien4 = true;
    var dieuKien5 = true;

    if (maKM == '') {
        document.getElementById('maKMSuaVL').innerHTML = "Mã khuyến mãi không được bỏ trống";
        $('#maKMSua').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('maKMSuaVL').innerHTML = "";
        $('#maKMSua').css('border-color', '');
        dieuKien1 = true;
        if (maKM.length > 200) {
            document.getElementById('maKMSuaVL').innerHTML = "Mã khuyến mãi không được vượt quá 200 kí tự";
            $('#maKMSua').css('border-color', 'red');
            dieuKien1 = false;
            e.preventDefault();
        }
        else {
            document.getElementById('maKMSuaVL').innerHTML = "";
            $('#maKMSua').css('border-color', '');
            dieuKien1 = true;
        }
    }

    if (tgbd == '') {
        document.getElementById('tgbdKMSuaVL').innerHTML = "Thời gian bắt đầu không được bỏ trống";
        $('#tgbdKMSua').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tgbdKMSuaVL').innerHTML = "";
        $('#tgbdKMSua').css('border-color', '');
        dieuKien2 = true;
    }

    if (tgkt == '') {
        document.getElementById('tgktKMSuaVL').innerHTML = "Thời gian kết thúc không được bỏ trống";
        $('#tgktKMSua').css('border-color', 'red');
        dieuKien3 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tgktKMSuaVL').innerHTML = "";
        $('#tgktKMSua').css('border-color', '');
        dieuKien3 = true;
    }

    if (gia == 0 || gia < 0 || gia > 100) {
        document.getElementById('giaKMSuaVL').innerHTML = "Giá khuyến mãi phải là số nguyên lớn hơn 0 và nhỏ hơn 100";
        $('#giaKMSua').css('border-color', 'red');
        dieuKien4 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('giaKMSuaVL').innerHTML = "";
        $('#giaKMSua').css('border-color', '');
        dieuKien4 = true;
    }

    if (maSP == '') {
        document.getElementById('maSPKMSuaVL').innerHTML = "Mã sản phẩm không được bỏ trống";
        $('#maSPKMSua').css('border-color', 'red');
        dieuKien5 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('maSPKMSuaVL').innerHTML = "";
        $('#maSPKMSua').css('border-color', '');
        dieuKien5 = true;
    }

    if (dieuKien1 && dieuKien2 && dieuKien3 && dieuKien4 && dieuKien5) {
        console.log(maSP);
        $.ajax({
            url: "/KhuyenMai/SuaKhuyenMai",
            type: "Post",
            data: JSON.stringify({
                'km.maKM': maKM,
                'km.thoiGianBatDau': tgbd,
                'km.thoiGianKetThuc': tgkt,
                'ctkm.maSP': maSP,
                'km.km': gia,
                'km.hinhAnh': hinh,
            }),
            dataType: "Json",
            contentType: "application/json; charset = utf-8",
            success: function (res) {
                if (res.kq) {
                    loadAnh('hinhAnhKMSua');
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

$(function () {
    $('#maSPKMSua').select2({
        multiple: true,
        allowClear: true
    });
    $('#maSPKMThem').select2({
        multiple: true,
        allowClear: true
    });
})

function GetIDKhuyenMaiDelete(e) {
    var maNV = $(e).attr('id');
    $('#idKhuyenMaiDelete').val(maNV);
}

$('#btnXoaKhuyenMai').on('click', function (e) {
    var maKM = $('#idKhuyenMaiDelete').val();
    $.ajax({
        url: "/KhuyenMai/XoaKhuyenMai",
        type: "GET",
        data: { maKM: maKM },
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

function KhoiPhucKhuyenMai(e) {
    var maKM = $(e).attr('id');

    $.ajax({
        url: "/KhuyenMai/KhoiPhucKhuyenMai",
        type: "GET",
        data: { maKM: maKM },
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

function LoadAnhFolderThemKM(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#Image_KMThem').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function LoadAnhFolderSuaKM(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#Image_KMSua').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function loadAnh(id) {
    var formData = new FormData();
    var file = document.getElementById(id).files[0];
    formData.append(id, file);

    $.ajax({
        url: "/KhuyenMai/UpLoadAnh",
        type: "POST",
        data: formData,
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (res) {
            if (res.kq) {

            } else {

            }
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    });
}