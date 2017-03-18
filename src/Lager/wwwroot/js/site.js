$(".edit-user-btn").click(function () {
    $(".floating-form").fadeIn(500).slideDown(1000);
});


$('.close-float').click(function () {
    this.slideUp(1000).fadeOut(500);
});

$("btn btn-info").click(function(){
    var x =document.forms
})

$(".edit-Btn").click(function () {
    var id = $(this).data("part-id")
    $.post("/Admin/Edit", {id: id})
});


$(".btn-danger").click(function () {
    $.ajax({
        url: "/Admin/RemoveItem",
        type: "POST",
        data: { id: $(this).data("part-id") },
        success: function () {
            alert("success");
        },
        error: function () {
            alert("error" + window.u);
        }
    })
})
