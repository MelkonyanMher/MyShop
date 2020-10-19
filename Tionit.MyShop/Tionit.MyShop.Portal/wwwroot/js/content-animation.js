window.animateContent = function(){
    var animation = "animate-fade-in-up";
    $("#kt_content")
        .one("webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend",
            e => $("#kt_content").removeClass(animation))
        .removeClass(animation).addClass(animation);
}