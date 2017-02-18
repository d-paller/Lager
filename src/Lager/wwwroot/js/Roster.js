$(document).ready(function () {

    $('.roster-select').click(function () {
        var sectionNumber = $(this).data("section");
        $('#Section').val(sectionNumber);

        $("#select-roster-form").submit();
    });
});