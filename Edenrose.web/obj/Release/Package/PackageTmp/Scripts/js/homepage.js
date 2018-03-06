$(document).ready(function () {
    $(window).scroll(function (event) {
        var headerH = 40;
        if ($(document).scrollTop() >= headerH) {
            $(".page-header").addClass("fixed");
        }
        else {
            $(".page-header").removeClass("fixed");
        }

        if ($(window).scrollTop() + $(window).height() + 100 >= $(document).height()) {
            $(".banner-button").addClass("hide");
        }
        else {
            $(".banner-button").removeClass("hide");
        }
    });

    $("#section_banner .banner_cont .inner").height($(window).height());
    $(window).resize(function () {
        $("#section_banner .banner_cont .inner").height($(window).height());
    });

    $("a.fancybox").fancybox();

    $(".fancy-trigger").click(function (event) {
        $("a:first", $(this).next()).trigger('click');
    });

    $(".nav-menu li.nd-scroll a, a.btn-scroll-link, a.btn-register").click(function (event) {
        var id = $(this).prop("hash");
        if (id != '') {
            var top = $(id).offset().top - 60;
            if ($(window).width() <= 1190)
                top = top + 10;
            $("html, body").animate({ scrollTop: top }, 500);
        }
        $('.collapse').collapse('hide');
        //window.location.hash = link;
        return false;
    });
	/*$(".nav-menu a").click(function (event) {
        var id = $(this).data("link");
        if (id != '') {
            var top = $('#' + id).offset().top - 60;
            if ($(window).width() <= 1190)
                top = top + 10;
            $("html, body").animate({ scrollTop: top }, 500);
        }
        $('.collapse').collapse('hide');
    });*/
    //$("#tqForm").validate({
    //    submitHandler: function (form) {
    //        var fullname = $("#fullname").val();
    //        var mobile = $("#mobile").val();
    //        var email = $("#email").val();

    //        var defaultText = $(".btn-submit").val();
    //        $(".btn-submit").val("Loading...").prop('disabled', true);
    //        $.ajax({
    //            type: "POST",
    //            url: "SendMail.ashx",
    //            data: $(form).serialize(),
    //            success: function (msg) {
    //                if (msg == "true") {
    //                    // bootbox.alert("Đăng ký thành công");
    //                    window.location.href = '/cam-on.html';
    //                }
    //                else {
    //                    bootbox.alert("Đăng ký thất bại");
    //                }
    //                $(".btn-submit").val(defaultText).prop('disabled', false);
    //            },
    //            /*complete: function () {
    //              $.post( "https://docs.google.com/forms/d/e/1FAIpQLSefWsYCCwT5AzRPDa0Xj6b-sM8Uj-ifJss1WFdbWfBQ7JQnsA/formResponse", 
    //              { 
    //                      'entry.2144199976': fullname, 
    //                      'entry.93347924': mobile, 
    //                      'entry.456832431': email 
    //                  }).always(function( data ) {
    //                      $("#fullname").val("");
    //                  var mobile = $("#mobile").val("");
    //                  var email = $("#email").val("");
                      
    //                  alert("Đăng ký thành công. Cảm ơn Quý khách đã đăng ký.");
    //                  $(".btn-submit").val(defaultText).prop('disabled', false);
    //              });
    //            }*/
    //        });




    //    }
    //});

   // loadMaps();
    //$(window).resize(function () {
    //    loadMaps();
    //});

    $('map > area.fancybox').click(function (e) {
        e.preventDefault();
        var target = $(this).data("target");
        $("." + target, $(this).parent().next()).trigger('click');
    });

    //$(document).click(function (e) {
    //    if (!$(e.target).is('a')) {
    //        $('.collapse').collapse('hide');
    //    }
    //});

    $('.navbar-ex1-collapse').on('show.bs.collapse', function () {
        $(".page-header .navbar-toggle").addClass("active");
    });
    $('.navbar-ex1-collapse').on('hide.bs.collapse', function () {
        $(".page-header .navbar-toggle").removeClass("active");
    });


});

//function loadMaps() {
//    $('img[usemap]').mapster({
//        fillColor: '3dad65',
//        fillOpacity: 0.0,
//        stroke: false,
//        singleSelect: true,
//        clickNavigate: true
//    });
//}
$('#gotop').click(function () {
    $("body,html").animate({ scrollTop: 0 }, 1600);
    return false;
});

//$('.bxslider').bxSlider({
//    mode: 'horizontal', //horizontal', 'vertical', 'fade'
//    captions: false, //chu thich
//    controls: false,
//    auto: true,
//    infiniteLoop: true,
//    pause: 3000,
//    speed: 500
//});


