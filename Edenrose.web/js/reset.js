function DeclareFormWH(id, width, height) {
    $('#' + id).dialog({

        autoOpen: false,
        resizable: false,
        autoresize: true,
        width: width,
        height: height,
        modal: true,
        open: function (type, data) {
            $(this).parent().appendTo("form");
        }
    });
}

function DeclareForm(id, width) {
    $('#' + id).dialog({

        autoOpen: false,
        resizable: false,
        autoresize: true,
        width: width,
        modal: true,
        open: function (type, data) {
            $(this).parent().appendTo("form");
        }
    });
}

function DisableDelete() {
    $('#delete').css('display', 'none');
}

function openDialogResetT(id, title) {
    $('#' + id).dialog({
        title: title
    });

    $('#' + id + ' input:hidden').each(function () {
        $(this).attr("value", "0");

    });

    $('#' + id + ' input:text').each(function () {
        $(this).attr("value", "");
        if ($(this).attr("disabled"))
            $(this).removeAttr("disabled");
    });

    $('#' + id + ' input:password').each(function () {
        $(this).attr("value", "");
        if ($(this).attr("disabled"))
            $(this).removeAttr("disabled");
    });
    $('#' + id + ' textarea').each(function () {
        $(this).attr("value", "");
        if ($(this).attr("disabled"))
            $(this).removeAttr("disabled");
    });

    $('#' + id + ' select').each(function () {
        $(this).find('option:eq(0)').attr('selected', 'selected');
        if ($(this).attr("disabled"))
            $(this).removeAttr("disabled");

    });

    $('#' + id).dialog("open");

}
function ResetForm(id) {
    $('#' + id + ' input:hidden').each(function () {
        $(this).attr("value", "0");

    });

    $('#' + id + ' input:text').each(function () {
        $(this).attr("value", "");
        if ($(this).attr("disabled"))
            $(this).removeAttr("disabled");
    });

    $('#' + id + ' input:password').each(function () {
        $(this).attr("value", "");
        if ($(this).attr("disabled"))
            $(this).removeAttr("disabled");
    });
    $('#' + id + ' textarea').each(function () {
        $(this).attr("value", "");
        if ($(this).attr("disabled"))
            $(this).removeAttr("disabled");
    });

    $('#' + id + ' select').each(function () {
        $(this).find('option:eq(0)').attr('selected', 'selected');
        if ($(this).attr("disabled"))
            $(this).removeAttr("disabled");

    });
}
function openDialogT(id, title) {
    $('#' + id).dialog({
        title: title
    });

    $('#' + id).dialog("open");

}


function openDialog(id) {

    $('#' + id).dialog("open");
}

function closeDialog(id) {
    $('#' + id).dialog("close");
}
function searchKeyPress(e) {

    if (typeof e == 'undefined' && window.event) { e = window.event; }

}
function searchKeyPressTxt(e, idTxt) {

    if (typeof e == 'undefined' && window.event) { e = window.event; }
    if (e.keyCode == 13) {
        document.getElementById(idTxt).click();
    }
}
function doClick(buttonName, e) {
    var key;
    if (window.event)
        key = window.event.keyCode;     //IE
    else
        key = e.which;     //firefox
    if (key == 13) {
        var btn = document.getElementById(buttonName);
        if (btn != null) {
            btn.click();
            event.keyCode = 0
        }
    }
}
function checkOnly(stayChecked, classTable) {

    $(classTable).find("td input").each(function () {
        if ($(this).attr("checked") && $(this).attr("name") != stayChecked.name) $(this).removeAttr("checked");
    });
}
function SelectAllCheckboxes(chk, iClass) {
    $('.' + iClass).find("input:checkbox").each(function () {
        if (this != chk && this.disabled == false) {
            this.checked = chk.checked;
        }
    });
}
function parseDate(str) {
    var mdy = str.split('/');
    return new Date(mdy[2], mdy[1], mdy[0]);
}

function isDate(txtDate) {
    var currVal = txtDate;
    if (currVal == '')
        return false;

    //Declare Regex  
    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
    var dtArray = currVal.match(rxDatePattern); // is format OK?

    if (dtArray == null)
        return false;
    var mdy = txtDate.split('/');
    //Checks for mm/dd/yyyy format.
    dtMonth = mdy[1];
    dtDay = mdy[0];
    dtYear = mdy[2];

    if (dtMonth < 1 || dtMonth > 12)
        return false;
    else if (dtDay < 1 || dtDay > 31)
        return false;
    else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
        return false;
    else if (dtMonth == 2) {
        var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
        if (dtDay > 29 || (dtDay == 29 && !isleap))
            return false;
    }
    return true;
}

function isUrl(s) {
    var regexp = /(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/;
    return regexp.test(s);
}
function isEmail(email) {
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    return reg.test(email);
}
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) return false;
    return true;
}
function SelectAllCheckboxes(tblId, chk) {
    var count = 0;
    $('#' + tblId).find("tr").each(function (idx) {
        $(this).find('td').find('input:checkbox').each(function (index) {
            if (index == 0) {
                if (this != chk && this.disabled == false) {
                    this.checked = chk.checked;
                    if (chk.checked) {
                        count = 1;
                    }
                }
                if (this.checked == false && idx == 0) {
                    $("#ltDeleteAll").removeClass("display-inline-block").addClass("display-none");
                }
            }
        });
    });
    if (count != 0) {
        $("#ltDeleteAll").removeClass("display-none").addClass("display-inline-block");
    } else {
        $("#ltDeleteAll").removeClass("display-inline-block").addClass("display-none");
    }
}
function EnableDeleteAll(tblId) {
    var isTrue = false;
    $('#' + tblId).find("tr").each(function (idx) {
        $(this).find('td').find('input:checkbox').each(function(index) {
            if (index == 0) {
                if (this.checked && idx != 0) {
                    isTrue = true;
                }
            } 
        });
    });
    if (isTrue) {
        $("#ltDeleteAll").removeClass("display-none").addClass("display-inline-block");
    } else {
        $("#ltDeleteAll").removeClass("display-inline-block").addClass("display-none");
    }
}
function RemoveChecked(tblId) {
    $('#' + tblId).find("input:checkbox").each(function () {
        $(this).removeAttr("checked");
    });
}
function ShowErrorSys(error) {
    $(".showMessage").html("");
    $(".showMessage").append(error);
    $("#showMessage").removeClass("display-none").addClass("display-block");
}

function CountWord(input) {
    
    $(input).parent().parent().find(".label .count").text("(" + $(input).val().length + ")");
}

function IsMobilePhone(txtPhone, isEmpty) {
    txtPhone = txtPhone.trim();
    var length = txtPhone.length;
    if (length == 0) {
        if (isEmpty == "1") {
            return true;
        }
        else
            return false;
    }
    if (txtPhone.indexOf('84') == 0) {
        txtPhone = txtPhone.substring(2, length - 2);
    } else {
        if (txtPhone.indexOf('84') == 0) {
            txtPhone = txtPhone.substring(3, length - 3);
        }
    }
    if ((txtPhone.indexOf('09') == 0 || txtPhone.indexOf('1') == 0) && txtPhone.length == 10) {
        return true;
    }
    if (txtPhone.indexOf('9') == 0 && txtPhone.length == 9) {
        return true;
    }
    if (txtPhone.indexOf('01') == 0 && txtPhone.length == 11) {
        return true;
    }
    return false;
}


function ShowLoading() {
    var element = $('body');
    if (element.length > 0) {
        element.append("<div class='loading-icon' style='position: fix'></div>");
        element.css("position", "relative");
    }
}

function HideLoading() {
    var element = $('body');
    if (element.length > 0) {
        element.find(".loading-icon").remove();
    }
}


function ChangeToSlug(idTitle, idSlug) {
    var title, slug;
    //Lấy text từ thẻ input title 
    title = $('#' + idTitle).val().replace('"', '');

    //Đổi chữ hoa thành chữ thường
    slug = title.toLowerCase();

    //Đổi ký tự có dấu thành không dấu
    slug = slug.replace(/á|à|ả|ạ|ã|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ/gi, 'a');
    slug = slug.replace(/é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ/gi, 'e');
    slug = slug.replace(/i|í|ì|ỉ|ĩ|ị/gi, 'i');
    slug = slug.replace(/ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ/gi, 'o');
    slug = slug.replace(/ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự/gi, 'u');
    slug = slug.replace(/ý|ỳ|ỷ|ỹ|ỵ/gi, 'y');
    slug = slug.replace(/đ/gi, 'd');
    //Xóa các ký tự đặt biệt
    slug = slug.replace(/\`|\~|\!|\@|\#|\||\$|\%|\^|\&|\*|\(|\)|\+|\=|\,|\.|\/|\?|\>|\<|\'|\"|\:|\;|_/gi, '');
    //Đổi khoảng trắng thành ký tự gạch ngang
    slug = slug.replace(/ /gi, "-");
    //Đổi nhiều ký tự gạch ngang liên tiếp thành 1 ký tự gạch ngang
    //Phòng trường hợp người nhập vào quá nhiều ký tự trắng
    slug = slug.replace(/\-\-\-\-\-/gi, '-');
    slug = slug.replace(/\-\-\-\-/gi, '-');
    slug = slug.replace(/\-\-\-/gi, '-');
    slug = slug.replace(/\-\-/gi, '-');
    //Xóa các ký tự gạch ngang ở đầu và cuối
    slug = '@' + slug + '@';
    slug = slug.replace(/\@\-|\-\@|\@/gi, '');
    //In slug ra textbox có id “slug”
    $('#' + idSlug).val(slug);
}


///Start confirm form
function ShowDeleteAll() {
    $("#mess_delete").text("Anh/chị có chắc chắn muốn xóa những mục đã chọn!");
    var id = "";
    $('#dt_basic').find("tr").each(function (idx) {
        $(this).find('td').find('input:checkbox').each(function (index) {
            if (index == 0) {
                if (this.checked && idx != 0) {
                    id += $(this).parent().parent().find('.item-id').val() + "|";
                }
            }
        });
    });
    $("#hfDeleteId").val(id);
    $('#dialog_delete').dialog("open");
    return false;
}

function ShowDeleteAllBieu(bieu) {
    $("#mess_delete_bieu").text("Anh/chị có chắc chắn muốn xóa những mục đã chọn!");
    var id = "";
    $('#dt_basic').find("tr").each(function (idx) {
        $(this).find('td').find('input:checkbox').each(function (index) {
            if (index == 0) {
                if (this.checked && idx != 0) {
                    id += $(this).parent().parent().find('.item-id').val() + "|";
                }
            }
        });
    });
    $("#hfDeleteId").val(id);
    $("#hfDeleteMaBieu").val(bieu);
    $('#dialog_delete_bieu').dialog("open");
    return false;
}
function ShowMessager(id) {
    $("#mess_delete").text("Anh/chị có chắc chắn muốn xóa mục này!");
    $("#hfDeleteId").val(id);
    $('#dialog_delete').dialog("open");
    return false;
}
function ShowMessagerBieu(id,bieu) {
    $("#mess_delete_bieu").text("Anh/chị có chắc chắn muốn xóa mục này!");
    $("#hfDeleteId").val(id);
    $("#hfDeleteMaBieu").val(bieu);
    $('#dialog_delete_bieu').dialog("open");
    return false;
}
function DeleteConfig() {
    $.ajax({
        url: $("#hfDeleteUrl").val(),
        type: 'POST',
        data: { "id": $("#hfDeleteId").val() },
        success: function (data) {
            if (data.IsSuccess === true) {
                location.reload();
            } else {
                toastr["error"](data.Message, "Thông báo");
            }
        },
        error: function (result, text, error) {
            toastr["error"]("Xảy ra lỗi khi xóa. Anh/chị vui lòng thử lại sau!", "Thông báo");
        }
    });
}
function DeleteConfigBieu() {
    $.ajax({
        url: $("#hfDeleteUrl").val(),
        type: 'POST',
        data: { "id": $("#hfDeleteId").val(), 'BieuMau': $("#hfDeleteMaBieu").val() },
        success: function (data) {
            if (data.IsSuccess === true) {
                location.reload();
            } else {
                toastr["error"](data.Message, "Thông báo");
            }
        },
        error: function (result, text, error) {
            toastr["error"]("Xảy ra lỗi khi xóa. Anh/chị vui lòng thử lại sau!", "Thông báo");
        }
    });
}
$('#dialog_delete').dialog({
        autoOpen: false,
        width: 600,
        resizable: false,
        modal: true,
        title: 'Thông báo',
        buttons: [{
            html: "<i class='fa fa-trash-o'></i>&nbsp; Đồng ý",
            "class": "btn btn-danger",
            click: function () {
                DeleteConfig();
                $(this).dialog("close");
            }
        }, {
            html: "<i class='fa fa-times'></i>&nbsp; Hủy",
            "class": "btn btn-default",
            click: function () {
                $(this).dialog("close");
            }
        }]
});

$('#dialog_delete_bieu').dialog({
    autoOpen: false,
    width: 600,
    resizable: false,
    modal: true,
    title: 'Thông báo',
    buttons: [{
        html: "<i class='fa fa-trash-o'></i>&nbsp; Đồng ý",
        "class": "btn btn-danger",
        click: function () {
            DeleteConfigBieu();
            $(this).dialog("close");
        }
    }, {
        html: "<i class='fa fa-times'></i>&nbsp; Hủy",
        "class": "btn btn-default",
        click: function () {
            $(this).dialog("close");
        }
    }]
});

    $('#dialog_success').dialog({
        autoOpen: false,
        width: 600,
        resizable: false,
        modal: true,
        title: 'Thông báo',
        buttons: [{
            html: "<i class='fa fa-check'></i>&nbsp; Đồng ý",
            "class": "btn btn-success",
            click: function () {
                location.reload();
                $(this).dialog("close");
            }
        }]
    });
///End confirm form

    $(document).ready(function () {
        if ($('#hfTmpErrorMsg').val().length > 0) {
            toastr["error"]($('#hfTmpErrorMsg').val(), "Thông báo");
        }
        if ($('#hfTmpSuccessMsg').val().length > 0) {
            toastr["success"]($('#hfTmpSuccessMsg').val(), "Thông báo");
        }
        $(".datepicker").datepicker({
            yearRange: "2010:2030",
            changeMonth: true,
            changeYear: true,
            dateFormat: "dd/mm/yy"
        });
       
        $(window).scroll(function () {
            if ($(window).scrollTop() > 50) {
                $('.btn-command').css({ "position": "fixed", "top": "0px", "z-index": "99999", "right": "13px", "background": "#fafafa", "padding": "0px 5px 5px 5px" });
            } else {
                $('.btn-command').removeAttr("style");
            }

        });
        $('#dialog_delete').dialog({
            autoOpen: false,
            width: 600,
            resizable: false,
            modal: true,
            title: 'Thông báo',
            buttons: [{
                html: "<i class='fa fa-trash-o'></i>&nbsp; Đồng ý",
                "class": "btn btn-danger",
                click: function () {
                    DeleteConfig();
                    $(this).dialog("close");
                }
            }, {
                html: "<i class='fa fa-times'></i>&nbsp; Hủy",
                "class": "btn btn-default",
                click: function () {
                    $(this).dialog("close");
                }
            }]
        });
        $('#dialog_success').dialog({
            autoOpen: false,
            width: 600,
            resizable: false,
            modal: true,
            title: 'Thông báo',
            buttons: [{
                html: "<i class='fa fa-check'></i>&nbsp; Đồng ý",
                "class": "btn btn-success",
                click: function () {
                    location.reload();
                    $(this).dialog("close");
                }
            }]
        });

        //input money
        $('input[type="numeric"]').keydown(function (e) {
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 188]) !== -1 || (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) || (e.keyCode >= 35 && e.keyCode <= 40)) {
                return;
            }
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        $('input[type="numeric"]').on("keyup", function () {
            $(this).data("value", $(this).val().replace(/[a-zA-Z-.]+/g, ''));
            $(this).data("value", $(this).val().replace(/[^0-9\,]/g, ''));
            $(this).val(addCommas($(this).data("value")));
        });
        function addCommas(nStr, leng) {
            nStr += '';
            x = nStr.split(',');
            x1 = x[0];
            x2 = x.length > 1 ? ',' + x[1].substring(0, 2) : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1)) {
                x1 = x1.replace(rgx, '$1' + '.' + '$2');
            }
            if (leng > 0)
                return x1 + x2;
            else return x1;
        }
    });