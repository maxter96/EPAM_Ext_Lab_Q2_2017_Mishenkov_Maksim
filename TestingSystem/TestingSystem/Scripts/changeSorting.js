$(function () {
    $('#sortBtn').click(function()
    {
        var id = $('#sorting option:selected').prop('id');
        if (id >= 0)
        {
            window.location.href = "Rating?themeId=" + id;
        }
    });
});