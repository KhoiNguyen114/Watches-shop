$(document).ready(function () {
    $('#txtThongTinTinTuc').summernote({
        callbacks: {
            onImageUpload: function (files) {
                for (let i = 0; i < files.length; i++) {
                    loadAnhSummernote(files[i]);
                }
            },
            onMediaDelete: function (target) {
                var path = $(target[0]).attr('src').replace("..", "");
                XoaAnhSummernote(path);
            }
        },
    });
    $('#txtThongTinTinTucSua').summernote({
        callbacks: {
            onImageUpload: function (files) {
                for (let i = 0; i < files.length; i++) {
                    loadAnhSummernoteCapNhat(files[i]);
                }
            },
            onMediaDelete: function (target) {
                var path = $(target[0]).attr('src').replace("..", "");
                XoaAnhSummernote(path);
            }
        },
    });
});


$('#btnThemTinTuc').on('click', function (e) {
    var noiDung = $('#txtThongTinTinTuc').summernote('code');
    var noiDungKT = $('#txtThongTinTinTuc').val();
    var tieuDe = $('#tieuDeTinTuc').val();
    var hinhAnh = $('#hinhDaiDienTinTuc').get(0).files.length;
    var hinh = $('#hinhDaiDienTinTuc').val();    
    var tomTat = $('#tomTatTinTuc').val();

    var dieuKien1 = true;
    var dieuKien2 = true;
    var dieuKien3 = true;
    var dieuKien4 = true;

    if (tieuDe == '') {
        document.getElementById('tieuDeTinTucVL').innerHTML = "Tiêu đề không được bỏ trống";
        $('#tieuDeTinTuc').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tieuDeTinTucVL').innerHTML = "";
        $('#tieuDeTinTuc').css('border-color', '');
        dieuKien1 = true;
    }
    if (noiDungKT == '') {
        document.getElementById('txtThongTinTinTucVL').innerHTML = "Nội dung không được bỏ trống";
        $('#txtThongTinTinTuc').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('txtThongTinTinTucVL').innerHTML = "";
        $('#txtThongTinTinTuc').css('border-color', '');
        dieuKien2 = true;
    }

    if (hinhAnh == '') {
        document.getElementById('hinhDaiDienTinTucVL').innerHTML = "Hình đại diện tin tức không được bỏ trống";
        $('#hinhDaiDienTinTuc').css('border-color', 'red');
        dieuKien3 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('hinhDaiDienTinTucVL').innerHTML = "";
        $('#hinhDaiDienTinTuc').css('border-color', '');
        dieuKien3 = true;
    }

    if (tomTat == '') {
        document.getElementById('tomTatTinTucVL').innerHTML = "Tóm tắt tin tức không được bỏ trống";
        $('#tomTatTinTuc').css('border-color', 'red');
        dieuKien4 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tomTatTinTucVL').innerHTML = "";
        $('#tomTatTinTuc').css('border-color', '');
        dieuKien4 = true;
        if (tomTat.length > 500) {
            document.getElementById('tomTatTinTucVL').innerHTML = "Tóm tắt tin tức không được vượt quá 500 kí tự";
            $('#tomTatTinTuc').css('border-color', 'red');
            dieuKien4 = false;
            e.preventDefault();
        }
        else {
            document.getElementById('tomTatTinTucVL').innerHTML = "";
            $('#tomTatTinTuc').css('border-color', '');
            dieuKien4 = true;
        }
    }

    console.log(tomTat.length);
    console.log(noiDung);

    if (dieuKien1 && dieuKien2 && dieuKien3 && dieuKien4) {
        $.ajax({
            url: "/TinTuc/ThemTinTucMoi",
            type: "POST",
            data: JSON.stringify({
                TIEUDE: tieuDe,
                TOMTAT: tomTat,
                NOIDUNG: noiDung,
                HINHDAIDIEN: hinh,
            }),
            dataType: "json",
            contentType: "application/json; charset=uft-8",
            success: function (res) {
                if (res.kq) {
                    upLoadHinhDaiDienTinTuc('hinhDaiDienTinTuc');
                    $.notify(res.message, "success");
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

$('#btnCapNhatTinTuc').on('click', function (e) {
    var noiDung = $('#txtThongTinTinTucSua').summernote('code');
    var noiDungKT = $('#txtThongTinTinTucSua').val();
    var tieuDe = $('#tieuDeTinTucSua').val();
    var idTinTuc = $('#idTinTucSua').val();
    var hinhAnh = $('#hinhDaiDienTinTucSua').get(0).files.length;
    var hinh = $('#hinhDaiDienTinTucSua').val();
    var tomTat = $('#tomTatTinTucSua').val();


    var dieuKien1 = true;
    var dieuKien2 = true;
    var dieuKien3 = true;
    var dieuKien4 = true;

    if (tieuDe == '') {
        document.getElementById('tieuDeTinTucSuaVL').innerHTML = "Tiêu đề không được bỏ trống";
        $('#tieuDeTinTucSua').css('border-color', 'red');
        dieuKien1 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tieuDeTinTucSuaVL').innerHTML = "";
        $('#tieuDeTinTucSua').css('border-color', '');
        dieuKien1 = true;
    }
    if (noiDungKT == '') {
        document.getElementById('txtThongTinTinTucSuaVL').innerHTML = "Nội dung không được bỏ trống";
        $('#txtThongTinTinTucSua').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('txtThongTinTinTucSuaVL').innerHTML = "";
        $('#txtThongTinTinTucSua').css('border-color', '');
        dieuKien2 = true;
    }

    if (idTinTuc == '') {
        document.getElementById('idTinTucSuaVL').innerHTML = "Tiêu đề không được bỏ trống";
        $('#idTinTucSua').css('border-color', 'red');
        dieuKien3 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('idTinTucSuaVL').innerHTML = "";
        $('#idTinTucSua').css('border-color', '');
        dieuKien3 = true;
    }

    if (tomTat == '') {
        document.getElementById('tomTatTinTucSuaVL').innerHTML = "Tóm tắt tin tức không được bỏ trống";
        $('#tomTatTinTucSua').css('border-color', 'red');
        dieuKien4 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tomTatTinTucSuaVL').innerHTML = "";
        $('#tomTatTinTucSua').css('border-color', '');
        dieuKien4 = true;
        if (tomTat.length > 500) {
            document.getElementById('tomTatTinTucSuaVL').innerHTML = "Tóm tắt tin tức không được vượt quá 500 kí tự";
            $('#tomTatTinTucSua').css('border-color', 'red');
            dieuKien4 = false;
            e.preventDefault();
        }
        else {
            document.getElementById('tomTatTinTucSuaVL').innerHTML = "";
            $('#tomTatTinTucSua').css('border-color', '');
            dieuKien4 = true;
        }
    }

    console.log(noiDung);
    if (dieuKien1 && dieuKien2 && dieuKien3 && dieuKien4) {
        $.ajax({
            url: "/TinTuc/SuaTinTuc",
            type: "POST",
            data: JSON.stringify({
                IDTINTUC: idTinTuc,
                TIEUDE: tieuDe,
                TOMTAT: tomTat,
                NOIDUNG: noiDung,
                HINHDAIDIEN: hinh,
            }),
            dataType: "json",
            contentType: "application/json; charset=uft-8",
            success: function (res) {
                if (res.kq) {
                    upLoadHinhDaiDienTinTuc('hinhDaiDienTinTucSua');
                    $.notify(res.message, "success");
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

function GetIDTinTucDelete(e) {
    var maTT = $(e).attr('id');
    $('#idTinTucDelete').val(maTT);
}

$('#btnXoaTinTuc').on('click', function (e) {
    var idTT = $('#idTinTucDelete').val();
    $.ajax({
        url: "/TinTuc/XoaTinTuc",
        type: "GET",
        data: { idTinTuc: idTT },
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


function KhoiPhucTinTuc(e) {
    var idTT = $(e).attr('id');
    $.ajax({
        url: "/TinTuc/KhoiPhucTinTuc",
        type: "GET",
        data: { idTinTuc: idTT },
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

function loadAnhSummernote(image) {
    var formData = new FormData();
    formData.append("fupload", image);

    $.ajax({
        url: "/TinTuc/loadAnhSummernote",
        type: "POST",
        data: formData,
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (res) {
            if (res.kq) {
                var imgNode = document.createElement('img');
                var duongDan = "../../../../Content/Images/TinTuc/" + res.data;
                imgNode.src = duongDan;
                $('#txtThongTinTinTuc').summernote('insertNode', imgNode);
            }
        },
        error: function (e) {
             $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    });
}

function XoaAnhSummernote(image) {
    var bien = image;
    $.ajax({
        url: "/TinTuc/XoaAnhSummernote",
        type: "GET",
        data: { filePath: bien },
        dataType: "json",
        contentType: "application/json; charset=uft-8",
        success: function (res) {
            if (res.kq) {

            }
        },
        error: function (e) {
             $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    });
}

function loadAnhSummernoteCapNhat(image) {
    var formData = new FormData();
    formData.append("fupload", image);

    $.ajax({
        url: "/TinTuc/loadAnhSummernote",
        type: "POST",
        data: formData,
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (res) {
            if (res.kq) {
                var imgNode = document.createElement('img');
                var duongDan = "../../../../Content/Images/TinTuc/" + res.data;
                imgNode.src = duongDan;
                $('#txtThongTinTinTucSua').summernote('insertNode', imgNode);
            }
        },
        error: function (e) {
             $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    });
}

function LoadAnhFolderHinhDaiDienTinTucThem(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#Image_DaiDienTinTuc').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function LoadAnhFolderHinhDaiDienTinTucSua(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#Image_DaiDienTinTucSua').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function upLoadHinhDaiDienTinTuc(id) {
    var formData = new FormData();
    var file = document.getElementById(id).files[0];
    formData.append(id, file);

    $.ajax({
        url: "/TinTuc/UpLoadHinhDaiDienTinTuc",
        type: "POST",
        data: formData,
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (res) {
            if (res.kq) {

            }
            else {

            }
        },
        error: function (e) {
             $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    });
}