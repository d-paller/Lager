$(".edit-user-btn").click(function () {
    $(".floating-form").fadeIn(500).slideDown(1000);
});


$('.close-float').click(function () {
    this.slideUp(1000).fadeOut(500);
});

$("btn btn-info").click(function(){
    var x =document.forms
})

$(".btn-xs btn-success").click(function () {
    $.ajax({
        type:"POST",
        url: "/Admin/findItem",
        data: { id: $(this).val },
        success: function (result) {
            alert(result.name);
        },
        error: function () {
            alert("error");
        }
    })
})
