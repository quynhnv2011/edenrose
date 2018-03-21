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
    $(".navbar-nav li a").click(function (event) {
        $(".navbar-collapse").collapse('hide');
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
        if ($('.navbar-toggle').hasClass('active')) {
            $('.navbar-collapse').collapse('toggle');
        }
       // $('.collapse').collapse('hide');
       
        //window.location.hash = link;
        return false;
    });

   // loadMaps();
    //$(window).resize(function () {
    //    loadMaps();
    //});

    $('map > area.fancybox').click(function (e) {
        e.preventDefault();
        var target = $(this).data("target");
        $("." + target, $(this).parent().next()).trigger('click');
    });

    $(document).click(function (e) {
        if (!$(e.target).is('a')) {           
            if ($('.navbar-toggle').hasClass('active')) {
                $('.navbar-collapse').collapse('toggle');
                $('.collapse').collapse('hide');
            }
        }
    });

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


