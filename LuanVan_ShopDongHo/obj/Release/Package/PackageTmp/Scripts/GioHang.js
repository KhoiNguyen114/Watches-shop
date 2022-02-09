$(document).ready(function () {
    loadGioHang();
})

function ReloadTongTien(masp) {
    var a = parseInt($('#' + masp).val());
    var b = parseInt($('#' + masp).attr('max'));
    var c = parseInt($('#' + masp).attr('min'));

    if (a < c) {
        a = c;
        $('#' + masp).val(a);
    }

    if (a > b) {
        a = b;
        $('#' + masp).val(a);
    }
    var para = {
        maSP: masp,
        soLuong: a,
    };
    $.ajax({
        url: "/DatHang/reloadTongTien",
        type: "POST",
        data: JSON.stringify(para),
        contentType: "application/json; charset = utf-8",
        dataType: "json",
        cache: false,
        success: function (result) {
            if (result.kq) {
                loadGioHang();
            }
            else {
                $('#' + masp).val(1);
            }
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    })
}

function loadGioHang() {
    $.ajax({
        url: "/DatHang/LoadGioHang",
        type: "GET",
        contentType: "application/json; charset = utf-8",
        dataType: "json",
        success: function (result) {
            var the = '';
            the += '<table class="table table-bordered">';
            the += '<tr style = "text-align: center; color: white; background: #04a4b2; text-transform: uppercase; font-size: 18px" >';
            the += '<th>STT</th>';
            the += '<th>Mã sản phẩm</th>';
            the += '<th>Tên sản phẩm</th>';
            the += '<th>Hình</th>'
            the += '<th>Đơn giá (VNĐ)</th>';
            the += '<th>Số lượng</th>';
            the += '<th>Khuyến mãi ( giá khuyến mãi sản phẩm)</th>';
            the += '<th>Thành tiền (VNĐ)</th>';
            the += '<th>Tổng tiền (VNĐ)</th>';
            the += '<th>Thao tác</th>';
            the += '</tr>';
            var a = Intl.NumberFormat('en-US');
            var dem = 1;
            $.each(result.data, function (key, sp) {
                the += '<tr style="font-size: 18px">';
                the += '<td style="text-align: right">' + dem + '</td>';
                the += '<td>' + sp.maSP + '</td>';
                the += '<td>Đồng hồ ' + sp.tenSP + ' - ' + sp.kichThuoc + ' - ' + sp.loaiDay + '</td>';
                the += '<td>';
                the += '<img src="../Content/Images/SanPham/' + sp.hinh + '" width = "50" height = "50" />';
                the += '</td>';
                the += '<td style="text-align: right">' + a.format(sp.donGia) + '</td >';
                the += '<td style="text-align: right">';
                the += '<input type = "number" value = "' + sp.soLuong + '" id = "' + sp.maSP + '" min = "1" max = "' + sp.soLuongHienCo + '" onchange = "ReloadTongTien(\'' + sp.maSP + '\')" style = "transition: 0.4s ease; border: none; text-align: right; font-size: 18px" class="form-control-sm" />';
                the += '</td>';
                the += '<td style="text-align: right" id="km_' + sp.maSP + '" >' + a.format(sp.khuyenMai) + '</td >';
                the += '<td style = "text-align: right" id = "tt_' + sp.maSP + '" >' + a.format(sp.thanhTien) + '</td >';
                the += '<td style = "text-align: right" id = "total_' + sp.maSP + '" >' + a.format(sp.tongTien) + '</td >';
                the += '<td style = "text-align: center">';
                the += '<a class="btn btn-danger" class="xoaGioHang"  onclick="XoaGioHang(\'' + sp.maSP + '\')" > <i class="fas fa-trash mr-2"></i>Xóa</a>';
                the += '</td>';
                the += '</tr>';
                dem++;
            });
            the += '<tr style="font-size: 18px">';
            the += '<td colspan = "6" style = "text-align: right; font-weight: bold; text-transform: uppercase">Tổng cộng</td>';
            the += '<td style="text-align: right; font-weight: 800" id="TongTien">' + a.format(result.tongTien) + '</td>';
            the += '<td></td>';
            the += '</tr>';
            the += '</table>';
            $('#tblGioHang').html(the);
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        }
    });
}

function XoaGioHang(maSP) {
    $.ajax({
        url: "/DatHang/XoaMatHang?MASP=" + maSP,
        type: "GET",
        contentType: "application/json; charset = utf-8",
        dataType: "json",
        success: function (result) {
            if (result.status) {
                loadGioHang();
            }
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        },
    });
}

function ThemVaoGio(maSP) {
    $.ajax({
        url: "/DatHang/ThemVaoGio",
        type: "GET",
        data: { maSP: maSP },
        contentType: "application/json; charset = utf-8",
        dataType: "json",
        success: function (result) {
            if (result.kq) {
                window.location.reload();
            }
            else {
                alert(result.message);
            }
        },
        error: function (e) {
            $.notify("Có lỗi xảy ra trong quá trình xử lý", "error");
        },
    })
}
