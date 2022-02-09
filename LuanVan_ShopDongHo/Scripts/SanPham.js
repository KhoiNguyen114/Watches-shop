$(document).ready(function () {
    $('#txtMoTaSPThem').summernote({
        callbacks: {
            onImageUpload: function (files) {
                for (let i = 0; i < files.length; i++) {
                    loadAnhSummernoteSP(files[i]);
                }
            },
            onMediaDelete: function (target) {
                var path = $(target[0]).attr('src').replace("..", "");
                XoaAnhSummernoteSP(path);
            }
        },
    });
    $('#txtMoTaSPSua').summernote({
        callbacks: {
            onImageUpload: function (files) {
                for (let i = 0; i < files.length; i++) {
                    loadAnhSummernoteSPCapNhat(files[i]);
                }
            },
            onMediaDelete: function (target) {
                var path = $(target[0]).attr('src').replace("..", "");
                XoaAnhSummernoteSP(path);
            }
        },
    });
})

function LoadAnhFolderThemSP(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#Image_SPThem').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function LoadAnhFolderSuaSP(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#Image_SPSua').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function loadAnh(id) {
    var formData = new FormData();
    var file = document.getElementById(id).files[0];
    formData.append(id, file);

    $.ajax({
        url: "/QuanLy/UpLoadAnh",
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

function GetSanPham(e) {
    var btnID = $(e).attr('id');
    $.ajax({
        url: "/QuanLy/GetSanPham",
        type: "GET",
        data: { maSP: btnID },
        dataType: "Json",
        contentType: "application/json; charset = utf-8",
        success: function (result) {
            $('#maSPSua').val(result[0].MASP);
            $('#tenSPSua').val(result[0].TENSP);
            $('#chiTietSPSua').val(result[0].CHITIETSP);
            $('#donGiaSPSua').val(result[0].DONGIA);
            $('#Image_SPSua').attr('src', '../Content/Images/SanPham/' + result[0].HINHANH + '');
            $('#soLuongSPSua').val(result[0].SOLUONG);
            $('#maHangSPSua').val(result[0].MAHANG);
            $('#maNoiDungSPSua').val(result[0].MABAOHANH);
            $('#ThoiGianBHSPSua').val(result[0].ThoiHanBH);
            $('#maDanhMucSPSua').val(result[0].MADM);
            $('#phienBanSPSua').val(result[0].DONGSP);

            var chuoi = result[0].KICHTHUOC.toString().replace("mm", "");
            var kt = parseFloat(chuoi);
            $('#kichThuocSPSua').val(kt);

            $('#nangLuongSPSua').val(result[0].NANGLUONG);
            $('#loaiDaySPSua').val(result[0].LOAIDAY);
            $('#xuatSuSPSua').val(result[0].XUATSU);
            $('#txtMoTaSPSua').summernote('code', result[0].MOTASP);
            console.log(result[0].MOTASP);
            $('#myModalEdit').modal('show');
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        },
    });
}

function GetIDSanPhamQLSPDelete(e) {
    var maSP = $(e).attr('id');
    console.log(maSP);
    $('#idSanPhamQLSPDelete').val(maSP);
}

$('#btnXoaSanPhamQLSP').on('click', function (e) {
    var maSP = $('#idSanPhamQLSPDelete').val();
    $.ajax({
        url: "/QuanLy/XoaSanPham",
        type: "GET",
        data: { MASP: maSP },
        contentType: "application/json; charset = utf-8",
        dataType: "json",
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
    })
})


$('#btnCapNhat').on('click', function (e) {
    var masp = $('#maSPSua').val();
    var tensp = $('#tenSPSua').val();
    var chiTietSP = $('#chiTietSPSua').val();

    var valDG = $('#donGiaSPSua').val();
    var donGia = parseInt(valDG) || 0;

    var hinhAnh = $('#hinhAnhSPSua').get(0).files.length;
    var hinh = $('#hinhAnhSPSua').val();

    var valSL = $('#soLuongSPSua').val();
    var soLuong = parseInt(valSL) || 0;

    var maHang = $('#maHangSPSua').val();
    var maNDBH = $('#maNoiDungSPSua').val();
    var maDMSP = $('#maDanhMucSPSua').val();

    var thoiHanBH = $('#ThoiGianBHSPSua').val();

    var phienBan = $('#phienBanSPSua').val();

    var valKT = $('#kichThuocSPSua').val();
    var parsevalKT = parseFloat(valKT) || 0;
    var kichThuoc = parsevalKT.toFixed(1);

    var nangLuong = $('#nangLuongSPSua').val();
    var loaiDay = $('#loaiDaySPSua').val();
    var xuatSu = $('#xuatSuSPSua').val();

    var moTaSP = $('#txtMoTaSPSua').summernote('code');
    var moTaSPKT = $('#txtMoTaSPSua').val();

    var dieuKien1 = true;
    var dieuKien2 = true;
    var dieuKien3 = true;
    var dieuKien4 = true;
    var dieuKien5 = true;
    var dieuKien6 = true;
    var dieuKien7 = true;
    var dieuKien8 = true;
    var dieuKien9 = true;
    var dieuKien10 = true;

    if (masp == '') {
        document.getElementById('maSPSuaVL').innerHTML = "Mã sản phẩm không được bỏ trống";
        $('#maSPSua').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('maSPSuaVL').innerHTML = "";
        $('#maSPSua').css('border-color', '');
        dieuKien2 = true;
    }

    if (tensp == '') {
        document.getElementById('tenSPSuaVL').innerHTML = "Tên sản phẩm không được bỏ trống";
        $('#tenSPSua').css('border-color', 'red');
        dieuKien2 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('tenSPSuaVL').innerHTML = "";
        $('#tenSPSua').css('border-color', '');
        dieuKien2 = true;
    }
    if (chiTietSP == '') {
        document.getElementById('chiTietSPSuaVL').innerHTML = "Chi tiết sản phẩm không được bỏ trống";
        $('#chiTietSPSua').css('border-color', 'red');
        dieuKien3 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('chiTietSPSuaVL').innerHTML = "";
        $('#chiTietSPSua').css('border-color', '');
        dieuKien3 = true;
    }
    if (donGia == 0 || donGia < 0) {
        document.getElementById('donGiaSPSuaVL').innerHTML = "Đơn giá phải là số nguyên lớn hơn 0";
        $('#donGiaSPSua').css('border-color', 'red');
        dieuKien4 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('donGiaSPSuaVL').innerHTML = "";
        $('#donGiaSPSua').css('border-color', '');
        dieuKien4 = true;
    }
    if (chiTietSP == '') {
        document.getElementById('chiTietSPSuaVL').innerHTML = "Chi tiết sản phẩm không được bỏ trống";
        $('#chiTietSPSua').css('border-color', 'red');
        dieuKien5 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('chiTietSPSuaVL').innerHTML = "";
        $('#chiTietSPSua').css('border-color', '');
        dieuKien5 = true;
    }

    if (soLuong == 0 || soLuong < 0) {
        document.getElementById('soLuongSPSuaVL').innerHTML = "Số lượng phải là số nguyên lớn hơn 0";
        $('#soLuongSPSua').css('border-color', 'red');
        dieuKien7 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('soLuongSPSuaVL').innerHTML = "";
        $('#soLuongSPSua').css('border-color', '');
        dieuKien7 = true;
    }

    if (thoiHanBH == 0 || thoiHanBH < 0) {
        document.getElementById('ThoiGianBHSPSuaVL').innerHTML = "Số lượng phải là số nguyên lớn hơn 0";
        $('#ThoiGianBHSPSua').css('border-color', 'red');
        dieuKien8 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('ThoiGianBHSPSuaVL').innerHTML = "";
        $('#ThoiGianBHSPSua').css('border-color', '');
        dieuKien8 = true;
    }

    if (kichThuoc == 0 || kichThuoc < 0) {
        document.getElementById('kichThuocSPSuaVL').innerHTML = "Kích thước phải là số lớn hơn 0";
        $('#kichThuocSPSua').css('border-color', 'red');
        dieuKien9 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('kichThuocSPSuaVL').innerHTML = "";
        $('#kichThuocSPSua').css('border-color', '');
        dieuKien9 = true;
    }

    if (moTaSPKT == '') {
        document.getElementById('txtMoTaSPSuaVL').innerHTML = "Mô tả sản phẩm không được bỏ trống";
        $('#txtMoTaSPSua').css('border-color', 'red');
        dieuKien10 = false;
        e.preventDefault();
    }
    else {
        document.getElementById('txtMoTaSPSuaVL').innerHTML = "";
        $('#txtMoTaSPSua').css('border-color', '');
        dieuKien10 = true;
    }

    if (dieuKien1 && dieuKien2 && dieuKien3 && dieuKien4 && dieuKien5 && dieuKien6 && dieuKien7 && dieuKien8 && dieuKien9 && dieuKien10) {
        $.ajax({
            url: "/QuanLy/SuaSanPham",
            type: "POST",
            data: JSON.stringify({
                MASP: masp,
                TENSP: tensp,
                CHITIETSP: chiTietSP,
                DONGIA: donGia,
                HINHANH: hinh,
                SOLUONG: soLuong,
                MADM: maDMSP,
                MABAOHANH: maNDBH,
                ThoiHanBH: thoiHanBH,
                DONGSP: phienBan,
                MAHANG: maHang,
                NANGLUONG: nangLuong,
                KICHTHUOC: kichThuoc,
                LOAIDAY: loaiDay,
                XUATSU: xuatSu,
                MOTASP: moTaSP,
            }),
            dataType: "Json",
            contentType: "application/json; charset = utf-8",
            success: function (res) {
                if (res.kq) {
                    loadAnh('hinhAnhSPSua');
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
});

function loadSP() {
    var a = $('#inputSearch').val();
    $.ajax({
        url: "/TimKiem/LoadGoiY",
        type: "GET",
        data:
        {
            tuKhoa: a,
        },
        contentType: "application/json; charset = utf-8",
        dataType: "json",
        cache: false,
        success: function (result) {
            if (result.success == true) {
                var the = '';
                $.each(result.data, function (key, sp) {
                    the += '<li>';
                    the += '<a href="/SanPham/ChiTietSanPham/?MASP=' + sp.MASP + '" style="text-decoration: none; color: black">';
                    the += '<img src="../Content/Images/SanPham/' + sp.HINHANH + '" height="50" width="50" />';
                    the += sp.TENSP;
                    the += '</a>';
                    the += '</li>';
                })
                $('#autocom-box').html(the);
            }
            else {
                $('#autocom-box li').remove();
            }
        },
        error: function (error) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    })
}

function KhoiPhucSanPham(e) {
    var btnID = $(e).attr('id');
    $.ajax({
        url: "/QuanLy/KhoiPhucSanPham/?MASP=" + btnID,
        type: "GET",
        contentType: "application/json; charset = utf-8",
        dataType: "json",
        success: function (result) {
            if (result) {
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
    })

}

function loadAnhSummernoteSP(image) {
    var formData = new FormData();
    formData.append("fupload", image);

    $.ajax({
        url: "/QuanLy/loadAnhSummernoteSanPham",
        type: "POST",
        data: formData,
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (res) {
            if (res.kq) {
                var imgNode = document.createElement('img');
                var duongDan = "../../../Content/Images/SanPham/MoTaSanPham/" + res.data;
                imgNode.src = duongDan;
                $('#txtMoTaSPThem').summernote('insertNode', imgNode);
            }
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    });
}

function XoaAnhSummernoteSP(image) {
    var bien = image;
    $.ajax({
        url: "/QuanLy/XoaAnhSummernoteSanPham",
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

function loadAnhSummernoteSPCapNhat(image) {
    var formData = new FormData();
    formData.append("fupload", image);

    $.ajax({
        url: "/QuanLy/loadAnhSummernoteSanPham",
        type: "POST",
        data: formData,
        dataType: "json",
        contentType: false,
        processData: false,
        success: function (res) {
            if (res.kq) {
                var imgNode = document.createElement('img');
                var duongDan = "../../../Content/Images/SanPham/MoTaSanPham/" + res.data;
                imgNode.src = duongDan;
                $('#txtMoTaSPSua').summernote('insertNode', imgNode);
            }
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    });
}



