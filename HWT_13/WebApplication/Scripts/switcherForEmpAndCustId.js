$(function () {
    $("#CustomerID").val($("#customerSelector option", 0).attr("id"));
    $("#EmployeeID").val($("#employeeSelector option", 0).attr("id"));

    $("#customerSelector").change(function () {
        var id = $("#customerSelector option:selected").attr("id");
        $("#CustomerID").val(id);
    });

    $("#employeeSelector").change(function () {
        var id = $("#employeeSelector option:selected").attr("id");
        $("#EmployeeID").val(id);
    });
})