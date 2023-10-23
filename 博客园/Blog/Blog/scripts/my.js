$(document).ready(function() {
    //登录与未登录 显示状态
    if ($(".m").val() == null) {
        $(".m").hide();
        $(".t").hide();
    }
    if ($(".m").text() != "") {
        $(".dy").hide();
    }


    // 设置头像

    $('.abc').hide();
    var a = $('.ab').attr("src");
    if (a != null) {
        $('.abc').show();
        $('.hea').hide();
    }


    //显示昵称 或 用户名
    var nc = $.trim($(".tw .m").text()).length;
    if (nc != 0) {
        $(".on").hide();

    }

    //头像上传  显示
    var b = $('.headi').attr("src");
    if (b == null) {
        $('.headd').hide();
    }
    if (b != null) {
        $('#view').hide();
    }



});