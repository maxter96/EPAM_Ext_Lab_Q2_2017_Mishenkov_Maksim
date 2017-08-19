$(function () {
    $("#ProductID").val($("#productSelector option", 0).attr("id"));

    $("#productSelector").change(function () {
        var id = $("#productSelector option:selected").attr("id");
        $("#ProductID").val(id);
    });
})