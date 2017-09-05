$(function () {
    $("#searchQuery").val("");

    $("#searchQuery").keyup(function()
    {
        var query = $(this).val();
        $(".item").each(function() {
            var name = $(this).find("#itemName").text().toLowerCase();
            if (name.indexOf(query.toLowerCase()) != -1)
            {
                $(this).show();
            }
            else
            {
                $(this).hide();
            }
        });
    });
})