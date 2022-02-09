$(document).ready(function () {
    $('#noiDungCheDoBaoHanhThem').summernote({
        callbacks: {
            onImageUpload: function (files) {
                for (let i = 0; i < files.length; i++) {
                    loadAnhSummernoteCheDoBH(files[i]);
                }
            },
            onMediaDelete: function (target) {
                var path = $(target[0]).attr('src').replace("..", "");
                XoaAnhSummernoteCheDoBH(path);
            }
        },
    });
    $('#noiDungCheDoBaoHanhSua').summernote({
        callbacks: {
            onImageUpload: function (files) {
                for (let i = 0; i < files.length; i++) {
                    loadAnhSummernoteCheDoBHCapNhat(files[i]);
                }
            },
            onMediaDelete: function (target) {
                var path = $(target[0]).attr('src').replace("..", "");
                XoaAnhSummernoteCheDoBH(path);
            }
        },
    });
});

function loadAnhSummernoteCheDoBH(image) {
    var formData = new FormData();
    formData.append("fupload", image);

    $.ajax({
        url: "/TinTuc/loadAnhSummernoteCheDoBaoHanh",
        type: "POST",
        data: formData,
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (res) {
            if (res.kq) {
                var imgNode = document.createElement('img');
                var duongDan = "../../../Content/Images/CheDoBaoHanh/" + res.data;
                imgNode.src = duongDan;
                $('#noiDungCheDoBaoHanhThem').summernote('insertNode', imgNode);
            }
        },
        error: function (e) {
            alert(e.responseText);
        }
    });
}

function XoaAnhSummernoteCheDoBH(image) {
    var bien = image;
    $.ajax({
        url: "/TinTuc/XoaAnhSummernoteCheDoBaoHanh",
        type: "GET",
        data: { filePath: bien },
        dataType: "json",
        contentType: "application/json; charset=uft-8",
        success: function (res) {
            if (res.kq) {

            }
        },
        error: function (e) {
            alert(e.responseText);
        }
    });
}

function loadAnhSummernoteCheDoBHCapNhat(image) {
    var formData = new FormData();
    formData.append("fupload", image);

    $.ajax({
        url: "/TinTuc/loadAnhSummernoteCheDoBaoHanh",
        type: "POST",
        data: formData,
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (res) {
            if (res.kq) {
                var imgNode = document.createElement('img');
                var duongDan = "../../../Content/Images/CheDoBaoHanh/" + res.data;
                imgNode.src = duongDan;
                $('#noiDungCheDoBaoHanhSua').summernote('insertNode', imgNode);
            }
        },
        error: function (e) {
            alert(e.responseText);
        }
    });
}

$('#btnThemCheDoBaoHanh').on('click', function (e) {
    var maNoiDung = $('#maCheDoBaoHanhThem').val();
    var noiDungBH = $('#noiDungCheDoBaoHanhThem').summernote('code');
    var noiDungKT = $('#noiDungCheDoBaoHanhThem').val();
    var valSoNamBH = $('#SoNamBaoHanhThem').val();
    var SoNamBH = parseInt(valSoNamBH) || 0;

    var dieuKien1 = true;
    var dieuKien2 = true;
    var dieuKien3 = true;

    if (maNoiDung == '') {
        document.getElementById('maCheDoBaoHanhThemVL').innerHTML = "Mã chế độ bảo hành không được bỏ trống";
        $('#maCheDoBaoHanhThem').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('maCheDoBaoHanhThemVL').innerHTML = "";
        $('#maCheDoBaoHanhThem').css('border-color', '');
        dieuKien1 = true;
        if (maNoiDung.length > 50) {
            document.getElementById('maCheDoBaoHanhThemVL').innerHTML = "Mã chế độ bảo hành không được vượt quá 50 kí tự";
            $('#maCheDoBaoHanhThem').css('border-color', 'red');
            dieuKien1 = false;
            e.preventDefault();
        }
        else {
            document.getElementById('maCheDoBaoHanhThemVL').innerHTML = "";
            $('#maCheDoBaoHanhThem').css('border-color', '');
            dieuKien1 = true;
        }
    }
    if (noiDungKT == '') {
        document.getElementById('noiDungCheDoBaoHanhThemVL').innerHTML = "Nội dung không được bỏ trống";
        $('#noiDungCheDoBaoHanhThem').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('noiDungCheDoBaoHanhThemVL').innerHTML = "";
        $('#noiDungCheDoBaoHanhThem').css('border-color', '');
        dieuKien2 = true;
    }

    if (SoNamBH == 0 || SoNamBH < 0) {
        document.getElementById('SoNamBaoHanhThemVL').innerHTML = "Số năm bảo hành phải là số nguyên lớn hơn 0";
        $('#SoNamBaoHanhThem').css('border-color', 'red');
        dieuKien3 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('SoNamBaoHanhThemVL').innerHTML = "";
        $('#SoNamBaoHanhThem').css('border-color', '');
        dieuKien3 = true;
    }

    if (dieuKien1 && dieuKien2 && dieuKien3) {
        $.ajax({
            url: "/BaoHanh/ThemCheDoBaoHanhMoi",
            type: "POST",
            data: JSON.stringify({
                MANOIDUNG: maNoiDung,
                NOIDUNG: noiDungBH,
                SONAMBH: SoNamBH,
            }),
            dataType: "json",
            contentType: "application/json; charset=uft-8",
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
                alert(e.responseText);
            }
        });
    }
})

function GetCheDoBaoHanh(e) {
    var btnID = $(e).attr('id');
    $.ajax({
        url: "/BaoHanh/GetCheDoBaoHanh",
        type: "GET",
        data: { maCDBH: btnID },
        dataType: "Json",
        contentType: "application/json; charset = utf-8",
        success: function (result) {
            $('#maCheDoBaoHanhSua').val(result[0].MANOIDUNG);
            $('#SoNamBaoHanhSua').val(result[0].SONAMBH);
            $('#noiDungCheDoBaoHanhSua').summernote('code', result[0].NOIDUNG);
            console.log(result[0].NOIDUNG);
            $('#myModalEditCheDoBaoHanh').modal('show');
        },
        error: function (e) {
            alert(e.responseText);
        },
    });
}


$('#btnCapNhatCheDoBaoHanh').on('click', function (e) {
    var maNoiDung = $('#maCheDoBaoHanhSua').val();
    var noiDungBH = $('#noiDungCheDoBaoHanhSua').summernote('code');
    var noiDungKT = $('#noiDungCheDoBaoHanhSua').val();
    var valSoNamBH = $('#SoNamBaoHanhSua').val();
    var SoNamBH = parseInt(valSoNamBH) || 0;

    var dieuKien1 = true;
    var dieuKien2 = true;
    var dieuKien3 = true;

    if (maNoiDung == '') {
        document.getElementById('maCheDoBaoHanhSuaVL').innerHTML = "Mã chế độ bảo hành không được bỏ trống";
        $('#maCheDoBaoHanhSua').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('maCheDoBaoHanhSuaVL').innerHTML = "";
        $('#maCheDoBaoHanhSua').css('border-color', '');
        dieuKien1 = true;
        if (maNoiDung.length > 50) {
            document.getElementById('maCheDoBaoHanhSuaVL').innerHTML = "Mã chế độ bảo hành không được vượt quá 50 kí tự";
            $('#maCheDoBaoHanhSua').css('border-color', 'red');
            dieuKien1 = false;
            e.preventDefault();
        }
        else {
            document.getElementById('maCheDoBaoHanhSuaVL').innerHTML = "";
            $('#maCheDoBaoHanhSua').css('border-color', '');
            dieuKien1 = true;
        }
    }
    if (noiDungKT == '') {
        document.getElementById('noiDungCheDoBaoHanhSuaVL').innerHTML = "Nội dung không được bỏ trống";
        $('#noiDungCheDoBaoHanhSua').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('noiDungCheDoBaoHanhSuaVL').innerHTML = "";
        $('#noiDungCheDoBaoHanhSua').css('border-color', '');
        dieuKien2 = true;
    }

    if (SoNamBH == 0 || SoNamBH < 0) {
        document.getElementById('SoNamBaoHanhSuaVL').innerHTML = "Số năm bảo hành phải là số nguyên lớn hơn 0";
        $('#SoNamBaoHanhSua').css('border-color', 'red');
        dieuKien3 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('SoNamBaoHanhSuaVL').innerHTML = "";
        $('#SoNamBaoHanhSua').css('border-color', '');
        dieuKien3 = true;
    }

    if (dieuKien1 && dieuKien2 && dieuKien3) {
        $.ajax({
            url: "/BaoHanh/SuaCheDoBaoHanh",
            type: "POST",
            data: JSON.stringify({
                MANOIDUNG: maNoiDung,
                NOIDUNG: noiDungBH,
                SONAMBH: SoNamBH,
            }),
            dataType: "json",
            contentType: "application/json; charset=uft-8",
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
                alert(e.responseText);
            }
        });
    }
})

function GetIDCheDoBaoHanhDelete(e) {
    var maNV = $(e).attr('id');
    $('#idCheDoBaoHanhDelete').val(maNV);
}

$('#btnXoaCheDoBaoHanh').on('click', function (e) {
    var idCDBH = $('#idCheDoBaoHanhDelete').val();
    $.ajax({
        url: "/BaoHanh/XoaCheDoBaoHanh",
        type: "GET",
        data: { maCDBH: idCDBH },
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
            alert(e.responseText);
        }
    });
})

function KhoiPhucCheDoBaoHanh(e) {
    var idCDBH = $(e).attr('id');
    $.ajax({
        url: "/BaoHanh/KhoiPhucCheDoBaoHanh",
        type: "GET",
        data: { maCDBH: idCDBH },
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
            alert(e.responseText);
        }
    });
}