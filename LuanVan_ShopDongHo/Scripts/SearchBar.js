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
                    the += '<a href="/SanPham/ChiTietSanPham/?MASP='+ sp.MASP +'">';
                    the += '<img src="../../../Content/Images/SanPham/' + sp.HINHANH + '" height="50" width="50" />';
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
            alert(error.responseText);
        }
    })
}

