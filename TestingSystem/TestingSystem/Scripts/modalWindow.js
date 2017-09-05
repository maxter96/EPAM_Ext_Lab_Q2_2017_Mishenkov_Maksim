$(function () {
    $.ajaxSetup({ cache: false });
    $("#createBtn, .infoBtn").click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#dialogContent').html(data);
            $('#modDialog').modal('show');
        });
    });
})