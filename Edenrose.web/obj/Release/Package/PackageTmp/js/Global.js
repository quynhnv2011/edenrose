var _global = {
    setCookie: function (cname, cvalue, exdays) {
        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
    },
    getCookie: function (cname) {
        var name = cname + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    },
    hideBodyScroll: function () {
        $("htm,body").css('height', '100%');
        $("html,body").css('overflow', 'hidden');
    },
    showBodyScroll: function () {
        $("html,body").css('height', '');
        $("html,body").css('overflow', '');
    },
    blockUI: function (control) {
        $(control).block({
            message: '<i class="icon-lock"></i> Đang tải...',
            css: {
                border: "none",
                padding: "15px",
                backgroundColor: "#000",
                "-webkit-border-radius": "10px",
                "-moz-border-radius": "10px",
                color: "#fff"
            }
        });
    },
    unBlockUI: function (control) {
        $(control).unblock();
    },
    successMsg: function (msg) {
        $.smallBox({
            title: "Thành công",
            content: msg,
            color: "#3c763d",
            timeout: 3000
        });
    },
    errMsg: function (msg) {
        $.smallBox({
            title: "Lưu ý",
            content: msg,
            color: "#a94442",
            timeout: 3000
        });
    }
}

$(document).on('click', 'a.action', function () {
    var href = this.href;
    var object = this;

    var isConfirm = $(object).hasClass("confirm");
    if (isConfirm) {
        if (!confirm("Anh/chị có chắc chăn xóa dữ liệu này !")) {
            return false;
        }
    }

    $.ajax({
        url: href,
        success: function (result) {
            if (result.Json.IsSuccess) {
                //thao tác thành công => reload
                location.reload();
            } else {
                _global.errMsg(result.Json.Message);
            }
        }
    });
    return false;
});
$(document).on('click', 'a.dialog-rewrite', function () {
    var href = this.href;
    var title = this.title;
    var object = this;

    $.ajax({
        url: href,
        beforeSend: function () {
            _global.blockUI('#content');
        },
        success: function (result) {
            OpenDialogReWrite(result, title, 0, 0);
            _global.unBlockUI('#content');
        },
        error: function (xhr, status, text) {
            _global.unBlockUI('#content');
            _global.showMsgErr(_global.getMsg("Global_ActionFailed"));
        }
    });
    return false;
});
function OpenDialogReWrite(data, title) {
    var div = document.createElement('div');
    $("body").prepend(div);

    $(div).html(data).dialog({
        title: title,
        resizable: true,
        width: "auto",
        height: "auto",
        modal: true,
        show: {
            effect: "fade",
            duration: 400
        },
        closeOnEscape: true,
        close: function (event, ui) {
            this.remove();
        }
    }).dialog("open");
    $.validator.unobtrusive.parse("#addForm1");
    jQuery('.datetime').datetimepicker({
        format: 'd/m/Y',
        timepicker: false
    });
    bindForm($(div).attr("id"));
}

$(document).on('click', 'a.dialog,a.dialog-fullscreen', function () {
    var href = this.href;
    var title = this.title;
    var object = this;

    $.ajax({
        url: href,
        beforeSend: function () {
            _global.blockUI('#content');
        },
        success: function (result) {
            if ($(object).hasClass("dialog-fullscreen")) {
                OpenDialogFullScreen(result, title);
            } else {
                OpenDialog(result, title, 0, 0);
            }
            _global.unBlockUI('#content');

        },
        error: function (xhr, status, text) {
            _global.unBlockUI('#content');
            _global.errMsg('Tải trang không thành công, mã lỗi: ' + xhr.status);
        }
    });
    return false;
});

function OpenDialog(data, title) {
    var div = document.createElement('div');
    $("body").prepend(div);

    $(div).html(data).dialog({
        title: title,
        resizable: true,
        width: "auto",
        height: "auto",
        modal: true,
        show: {
            effect: "fade",
            duration: 400
        },
        closeOnEscape: true,
        close: function (event, ui) {
            this.remove();
        }
    }).dialog("open");
    $.validator.unobtrusive.parse("#addForm");
    jQuery('.datetime').datetimepicker({
        format: 'd/m/Y',
        timepicker: false
    });
    bindForm($(div).attr("id"));
}

function OpenDialogFullScreen(data, title) {
    var div = document.createElement('div');
    $("body").prepend(div);

    $(div).html(data).dialog({
        title: title,
        resizable: false,
        draggable: false,
        height: $(window).height(),
        width: '100%',
        closeOnEscape: true,
        open: function (event, ui) {
            _global.hideBodyScroll();
        },
        close: function (event, ui) {
            _global.showBodyScroll();
            this.remove();
        }
    }).dialog("open");
    $.validator.unobtrusive.parse("#addForm");
    bindForm($(div).attr("id"));
}

function bindForm(dialogId) {
    //đăng ký event submit form dialog    
    var dialogId2 = "#" + dialogId;
    $("form", dialogId2).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.Json != undefined && result.Json.IsSuccess) {
                    _global.unBlockUI(dialogId2);
                    location.reload();
                    return true;

                    ////cập nhật thành công => redirect/reload
                    //if (result.url !== undefined && result.url !== null && result.url !== '') {
                    //    window.href = result.url;
                    //} else {
                    //    // location.reload();
                    //    window.href = window.location.href;
                    //}
                } else {
                    $(dialogId2).html(result);
                    bindForm(dialogId);
                    //$.smallBox({
                    //    title: "Có lỗi xảy ra",
                    //    content: result.Json.Message,
                    //    color: "#a94442",
                    //    timeout: 3000
                    //});
                    //_global.unBlockUI(dialogId);
                }
            },
            beforeSend: function () {
                _global.blockUI(dialogId);
            },
            error: function (xhr, status, text) {
                _global.unBlockUI(dialogId);
                _global.errMsg('Thao tác không thành công, mã lỗi: ' + xhr.status);
            }
        });
        return false;
    });
}
function validateNumber(e) {
    var charX = e.key;
    var evt = (e) ? e : window.event;
    var charCode = (evt.keyCode) ? evt.keyCode : evt.which;
    var flagComma = false;
    var flagAll = false;
    if (charX === "," || charX === "-") {
        flagComma = true;
    }
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        flagAll = true;
    }
    if (!flagComma) {
        if (flagAll) {
            return false;
        }
    }
    return true;
};

function GetWindowWidth() {
    var myWidth = 0, myHeight = 0;
    if (typeof (window.innerWidth) == 'number') {
        //Non-IE
        myWidth = window.innerWidth;
        myHeight = window.innerHeight;
    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        //IE 6+ in 'standards compliant mode'
        myWidth = document.documentElement.clientWidth;
        myHeight = document.documentElement.clientHeight;
    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        //IE 4 compatible
        myWidth = document.body.clientWidth;
        myHeight = document.body.clientHeight;
    }
    return myWidth;
}

function GetWindowHeight() {
    var myWidth = 0, myHeight = 0;
    if (typeof (window.innerWidth) == 'number') {
        //Non-IE
        myWidth = window.innerWidth;
        myHeight = window.innerHeight;
    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        //IE 6+ in 'standards compliant mode'
        myWidth = document.documentElement.clientWidth;
        myHeight = document.documentElement.clientHeight;
    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        //IE 4 compatible
        myWidth = document.body.clientWidth;
        myHeight = document.body.clientHeight;
    }
    return myHeight;
}

function loading(loading) {
    if (!document.getElementById("AjaxLoading")) {
        var left = (GetWindowWidth() - 36) / 2;
        var ajaxLoading = '<div id="AjaxLoading" onclick="loading(false)" style="background:#fff url(/./img/AjaxLoading.gif) no-repeat center;display:none;width:36px;height:36px;line-height:36px;position:fixed;_position:absolute;top:40%;left:' + left + 'px;z-index:9999;border:solid 1px #fff;border-radius:36px"><!----></div>';
        jQuery("body").append(ajaxLoading);
    }

    if (typeof loading == 'undefined' || loading) {
        jQuery("#AjaxLoading").show();
    } else {
        jQuery("#AjaxLoading").fadeOut();
    }
}

$(function () {
    $(document).on('click', '.goback', function (e) {
        window.location.href = "/";
    });
    $(document).on('click', 'a .disabled', function (e) {
        e.preventDefault();
    });
});

