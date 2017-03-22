$(document).ready(function () {

    $('.parts-select').click(function () {
        var sectionNumber = $(this).data("category");
        $('#Category').val(category);

        $("#select-parts-form").submit();
    });
});