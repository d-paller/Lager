$(".edit-user-btn").click(function () {
    $(".floating-form").fadeIn(500).slideDown(1000);
    $('#name-of-user-to-edit').val() = $(".edit-user-btn").data();
});


$('.close-float').click(function () {
    this.slideUp(1000).fadeOut(500);
});

$("btn btn-info").click(function(){
    var x =document.forms
})


$(".expand-btn").click(function () {
    var id = $(this).data("item-id");
    $("#row-for-" + id).slideToggle();
});