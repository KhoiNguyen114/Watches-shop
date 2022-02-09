$(document).ready(function () {
    $('#maHDThongTinBHThem').select2({
        dropdownParent: $('#myModalAddThongTinBaoHanh .modal-content')
    });
    
})


$('#btnThemThongTinBaoHanh').on('click', function (e) {
    var maHD = $('#maHDThongTinBHThem').val();
    var maSP = $('#maSPThongTinBHThem').val();
    var ngayKT = $('#ngayKTThongTinBHThem').val();
    var ghiChu = $('#ghiChuThongTinBHThem').val();

    var dieuKien1 = true;
    var dieuKien2 = true;
    var dieuKien3 = true;
    var dieuKien4 = true;

    if (maHD == '') {
        document.getElementById('maHDThongTinBHThemVL').innerHTML = "Mã hóa đơn không được bỏ trống";
        $('#maHDThongTinBHThem').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('maHDThongTinBHThemVL').innerHTML = "";
        $('#maHDThongTinBHThem').css('border-color', '');
        dieuKien1 = true;
    }

    if (maSP == null) {
        document.getElementById('maSPThongTinBHThemVL').innerHTML = "Sản phẩm không được bỏ trống";
        $('#maSPThongTinBHThem').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('maSPThongTinBHThemVL').innerHTML = "";
        $('#maSPThongTinBHThem').css('border-color', '');
        dieuKien2 = true;
    }
    if (ngayKT == '') {
        document.getElementById('ngayKTThongTinBHThemVL').innerHTML = "Ngày kết thúc không được bỏ trống";
        $('#ngayKTThongTinBHThem').css('border-color', 'red');
        dieuKien3 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('ngayKTThongTinBHThemVL').innerHTML = "";
        $('#ngayKTThongTinBHThem').css('border-color', '');
        dieuKien3 = true;
    }
    if (ghiChu == '') {
        document.getElementById('ghiChuThongTinBHThemVL').innerHTML = "Ghi chú không được bỏ trống";
        $('#ghiChuThongTinBHThem').css('border-color', 'red');
        dieuKien4 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('ghiChuThongTinBHThemVL').innerHTML = "";
        $('#ghiChuThongTinBHThem').css('border-color', '');
        dieuKien4 = true;
    }

    if (dieuKien1 && dieuKien2 && dieuKien3 && dieuKien4) {
        $.ajax({
            url: "/BaoHanh/ThemThongTinBaoHanhMoi",
            type: "POST",
            data: JSON.stringify({
                IDCHITIET: maHD,
                MASP: maSP,
                THOIGIANKT: ngayKT,
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
                    console.log(success);
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

function loadSPTheoHD() {
    var idHD = $('#maHDThongTinBHThem').val();
    if (idHD != '') {
        $.ajax({
            url: "/BaoHanh/GetSanPhamTheoHD",
            type: "GET",
            data: { idHoaDon: idHD },
            dataType: "Json",
            contentType: "application/json; charset = utf-8",
            success: function (result) {
                if (result.kq) {
                    $('#maSPThongTinBHThem').html("");
                    for (var i = 0; i < result.data.length; i++) {
                        var sp = result.data[i];
                        var item = document.createElement("option");
                        item.text = sp.Text;
                        item.value = sp.Value;
                        $('#maSPThongTinBHThem').append(item);
                    }
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
}

$('#maHDThongTinBHThem').on('change', function (e) {
    var idHD = $('#maHDThongTinBHThem').val();
    $.ajax({
        url: "/BaoHanh/GetSanPhamTheoHD",
        type: "GET",
        data: { idHoaDon: idHD },
        dataType: "Json",
        contentType: "application/json; charset = utf-8",
        success: function (result) {
            if (result.kq) {
                $('#maSPThongTinBHThem').html("");
                for (var i = 0; i < result.data.length; i++) {
                    var sp = result.data[i];
                    var item = document.createElement("option");
                    item.text = sp.Text;
                    item.value = sp.Value;
                    $('#maSPThongTinBHThem').append(item);
                }
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

function GetThongTinBaoHanh(e) {
    var maBH = $(e).attr('id');
    $.ajax({
        url: "/BaoHanh/GetThongTinBaoHanh",
        type: "GET",
        data: { idTTBH: maBH },
        dataType: "Json",
        contentType: "application/json; charset = utf-8",
        success: function (result) {
            $('#maHDThongTinBHSua').val(result.maHoaDon);
            $('#maSPThongTinBHSua').val(result.tenSP);
            $('#ghiChuThongTinBHSua').val(result.ghiChu);

            //Bind ngày bắt đầu
            var dateString = result.ngayBD.substring(6);
            var currentTime = new Date(parseInt(dateString));
            var month = currentTime.getMonth() + 1;
            var day = currentTime.getDate();
            var year = currentTime.getFullYear();
            if (month.toString().length == 1)
                month = "0" + month.toString();
            if (day.toString().length == 1) {
                day = "0" + day.toString();
            }
            var ngayBD = year + "-" + month + "-" + day;
            $('#ngayBDThongTinBHSua').val(ngayBD);

            //Bind ngày kết thúc
            var dateString = result.ngayKT.substring(6);
            var currentTime = new Date(parseInt(dateString));
            var month = currentTime.getMonth() + 1;
            var day = currentTime.getDate();
            var year = currentTime.getFullYear();
            if (month.toString().length == 1)
                month = "0" + month.toString();
            if (day.toString().length == 1) {
                day = "0" + day.toString();
            }
            var ngayKT = year + "-" + month + "-" + day;
            $('#ngayKTThongTinBHSua').val(ngayKT);
            $('#maBHThongTinBHSua').val(result.idBaoHanh);
            $('#myModalEditThongTinBaoHanh').modal('show');
            console.log(result);
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    });
}

$('#btnCapNhatThongTinBaoHanh').on('click', function (e) {
    var maHD = $('#maHDThongTinBHSua').val();
    var maSP = $('#maSPThongTinBHSua').val();
    var ngayKT = $('#ngayKTThongTinBHSua').val();
    var ghiChu = $('#ghiChuThongTinBHSua').val();
    var idBH = $('#maBHThongTinBHSua').val();

    var dieuKien1 = true;
    var dieuKien2 = true;

    if (ngayKT == '') {
        document.getElementById('ngayKTThongTinBHSuaVL').innerHTML = "Ngày kết thúc không được bỏ trống";
        $('#ngayKTThongTinBHSua').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('ngayKTThongTinBHSuaVL').innerHTML = "";
        $('#ngayKTThongTinBHSua').css('border-color', '');
        dieuKien1 = true;
    }
    if (ghiChu == '') {
        document.getElementById('ghiChuThongTinBHSuaVL').innerHTML = "Ghi chú không được bỏ trống";
        $('#ghiChuThongTinBHSua').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('ghiChuThongTinBHSuaVL').innerHTML = "";
        $('#ghiChuThongTinBHSua').css('border-color', '');
        dieuKien2 = true;
    }

    if (dieuKien1 && dieuKien2) {
        $.ajax({
            url: "/BaoHanh/SuaThongTinBaoHanh",
            type: "POST",
            data: JSON.stringify({
                IDCHITIET: maHD,
                MASP: maSP,
                THOIGIANKT: ngayKT,
                GHICHU: ghiChu,
                IDBAOHANH: idBH,
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