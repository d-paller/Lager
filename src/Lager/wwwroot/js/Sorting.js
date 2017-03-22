$(document).ready(function () {

    // Header
    $(".table-header").click(function (evt) {
        var sortfield = $(this).data("sortby");
        if ($("#PagingInfo_SortField").val() === sortfield) {
            if ($("#PagingInfo_SortDesc").val() === "true") {
                $("#PagingInfo_SortDesc").val(false);
            }
            else {
                $("#PagingInfo_SortDesc").val(true);
            }
        }
        else {
            $("#PagingInfo_SortField").val(sortfield);
            $("#PagingInfo_SortDesc").val(false);
        }
        evt.preventDefault();
        $("#inventory-table").submit();
    });

    // Paging
    $(".page-nav-btn").click(function () {
        var pageindex = $(this).data("page-nav");
        var currentPage = $("#PagingInfo_CurrentPageIndex").val();
        var lastpage = $("#PagingInfo_PageCount").val();

        if (pageindex === "first") {
            $("#PagingInfo_CurrentPageIndex").val(0);
        }
        else if (pageindex === "previous") {
            $("#PagingInfo_CurrentPageIndex").val(currentPage - 1);
        }
        else if (pageindex === "next") {
            $("#PagingInfo_CurrentPageIndex").val(currentPage + 1);
        }
        else if (pageindex === "last") {
            $("#PagingInfo_CurrentPageIndex").val(lastpage - 1);
        }
        else {
            $("#PagingInfo_CurrentPageIndex").val(pageindex);
        }
        $("#inventory-table").submit();
    });

    var currentPageIndex = $("#PagingInfo_CurrentPageIndex").val();
    var lastpage = $("#PagingInfo_PageCount").val();

    if (currentPageIndex === 0) {
        $("#pging-first").removeClass("page-nav-btn").addClass("disabled").unbind("click");
        $("#pging-previous").removeClass("page-nav-btn").addClass("disabled").unbind("click");
    }

    if (currentPageIndex === lastpage-1) {
        $("#pging-last").removeClass("page-nav-btn").addClass("disabled").unbind("click");
        $("#pging-next").removeClass("page-nav-btn").addClass("disabled").unbind("click");
    }
});