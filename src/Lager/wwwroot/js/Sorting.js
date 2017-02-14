$('.table-header').click(function () {
    var column = $(this).data("sortby");

    $.post("Admin/Inventory", column, null, null);
});