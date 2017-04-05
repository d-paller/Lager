$('.close-float').click(function () {
    this.slideUp(1000).fadeOut(500);
});

$(".edit-Btn").click(function () {
    $.ajax({
        url: "/Admin/Edit",
        type: "GET",
        data: { id: $(this).data("part-id") },
        success: function () {
            alert("success");
            $(location).attr('href', "/Admin/Edit", $(this).data("part-id"))
        },
        error: function () {
            alert("error" + window.u);
        }
    })
});


$(".btn-danger").click(function () {
    $.ajax({
        url: "/Admin/RemoveItem",
        type: "POST",
        data: { id: $(this).data("part-id") },
        success: function () {
            alert("success");
        },
    })
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