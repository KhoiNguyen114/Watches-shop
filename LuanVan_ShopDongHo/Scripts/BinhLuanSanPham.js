function ChinhSuaBL(e) {
    var idBL = $(e).attr('id');
    $.ajax({
        url: "/BinhLuan/GetBinhLuan",
        type: "GET",
        data: { IDBINHLUAN: idBL },
        dataType: "Json",
        contentType: "application/json; charset = utf-8",
        success: function (result) {
            $('#txtBLSP').val(result[0].NOIDUNG);
            var html = '';
            html += '<i class="fas fa-edit"></i> Cập nhật';
            $('#btnBLSP').html(html);
            $('#btnBLSP').attr('id', 'btn_CapNhatBinhLuan');
            $('#btnHuyBLSP').remove();
            var html_1 = '<button class="btn btn-secondary mt-2 ml-2" style="text-transform: uppercase; font-weight: bold;" id="btnHuyBLSP" onclick="HuyBinhLuan()">';
            html_1 += '<i class= "fas fa-times mr-2"></i>';
            html_1 += 'Hủy';
            html_1 += '</button>';
            html_1 += '<input type="text" id="inputBLSPHidden" hidden="hidden" value="' + idBL + '"/>';
            $('#divBinhLuan').append(html_1);
            $('#txtBLSP').focus();
            console.log(result);
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    });
}

function HuyBinhLuan() {
    $('#txtBLSP').val("");
    var a = $('#inputBLSPHidden').val();
    $('#inputBLSPHidden').remove();
    $('#btnHuyBLSP').remove();
    $('#btn_CapNhatBinhLuan').attr('id', 'btnBLSP');
    $('#btnBLSP').html('<i class="fas fa-edit mr-2"></i>Đăng');
    //var html = '';
    //html += '<button class="btn btn-primary mt-2" style="text-transform: uppercase; font-weight: bold;" id="btnBLSP">';
    //html += '<i class="fas fa-edit mr-2"></i>';
    //html += 'Đăng';
    //html += '</button>';
    //$('#divBinhLuan').append(html);
}

function XoaBinhLuan(e) {
    var maBL = $(e).attr('id');
    var ask = confirm("Bạn có chắc chắn muốn xóa không?");
    if (ask) {
        $.ajax({
            url: "/BinhLuan/XoaBinhLuan",
            type: "GET",
            data: { IDBINHLUAN: maBL },
            dataType: "Json",
            contentType: "application/json; charset = utf-8",
            success: function (result) {
                if (result.kq) {
                    window.location.reload();
                    alert(result.message);
                }
                else {
                    alert(result.message);
                }
            },
            error: function (e) {
                $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
            }
        });
    }
}

function GetIDButtonCapNhatBL() {
    var maBL = $('#inputBLSPHidden').val();
    return maBL;
}


$('#btnBLSP').on('click', function (e) {
    var noiDung = $('#txtBLSP').val();
    var maSP = $('#maSPBinhLuan').val();
    var idTinTuc = $('#idTinTucBinhLuan').val();
    var idBL = GetIDButtonCapNhatBL();

    var dieuKien1 = true;

    if (noiDung == '') {
        alert('Vui lòng nhập nội dung');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        dieuKien1 = true;        
    }    

    if (dieuKien1) {
        $.ajax({
            url: "/BinhLuan/DangBinhLuan",
            type: "POST",
            data: JSON.stringify({
                NOIDUNG: noiDung,
                MASP: maSP,
                IDBINHLUAN: idBL,
                IDTINTUC: idTinTuc,
            }),
            dataType: "Json",
            contentType: "application/json; charset = utf-8",
            success: function (res) {
                if (res.kq) {
                    alert(res.message);
                    window.location.reload();
                }
                else {
                    alert(res.message);
                }
            },
            error: function (e) {
                $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
            }
        });
    }
})

$('#btn_CapNhatBinhLuan').on('click', function (e) {
    var noiDung = $('#txtBLSP').val();
    var maSP = $('#maSPBinhLuan').val();
    var idTinTuc = $('#idTinTucBinhLuan').val();
    var idBL = GetIDButtonCapNhatBL();

    var dieuKien1 = true;

    if (noiDung == '') {
        alert('Vui lòng nhập nội dung');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        dieuKien1 = true;
    }

    if (dieuKien1) {
        $.ajax({
            url: "/BinhLuan/DangBinhLuan",
            type: "POST",
            data: JSON.stringify({
                NOIDUNG: noiDung,
                MASP: maSP,
                IDBINHLUAN: idBL,
                IDTINTUC: idTinTuc,
            }),
            dataType: "Json",
            contentType: "application/json; charset = utf-8",
            success: function (res) {
                if (res.kq) {
                    alert(res.message);
                    window.location.reload();
                }
                else {
                    alert(res.message);
                }
            },
            error: function (e) {
                $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
            }
        });
    }
})