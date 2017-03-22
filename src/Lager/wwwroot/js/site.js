$('.close-float').click(function () {
    this.slideUp(1000).fadeOut(500);
});

$("btn btn-info").click(function(){
    var x =document.forms
})


$(".expand-btn").click(function () {
    var id = $(this).data("item-id");
    $("#row-for-" + id).slideToggle();
    if ($(this).find("span").hasClass("glyphicon glyphicon-menu-down"))
    {
        $(this).find("span").removeClass("glyphicon glyphicon-menu-down").addClass("glyphicon glyphicon-menu-up");
    }
    else
    {
        $(this).find("span").removeClass("glyphicon glyphicon-menu-up").addClass("glyphicon glyphicon-menu-down");
    }
    

});

// Manage parts students checked out
$(".student-in-list").click(function () {

    $(this).parent().find('a').each(function () {
        $(this).removeClass("active");
    });

    $(this).addClass("active");

});

$("#check-in").click(function () {
    var data = JSON.stringify({
        'name': $(this).data("part-name"),
        'id': $(this).data("part-id")
    });
    $.ajax({
        type: "POST",
        data: data,
        url: "/Admin/CheckIn",
        success: function () {
            alert("Success");
            $(".close").click();
        },
        error: function () {
            alert("error");
        }
    })
});