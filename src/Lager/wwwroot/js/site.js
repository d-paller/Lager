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
    $.post("/Admin/lame", {
        a: "hello", function(data) {
            alert(data);
        }
    })
});

$(".btn-danger").click(function () {
    $.ajax({
        url: '/',
        type: "POST",
        data: { id: $(this).data("partid") },
        success: function (result) {
            alert(result.name);
        },
        error: function () {
            alert("error" + window.u);
        }
    })
})
