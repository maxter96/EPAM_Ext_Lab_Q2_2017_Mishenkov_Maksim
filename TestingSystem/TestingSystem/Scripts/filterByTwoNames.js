$(function () {
    $("#searchQuery").val("");

    $("#searchQuery").keyup(function()
    {
        var query = $(this).val();
        $(".item").each(function() {
            var firstName = $(this).find("#firstItemName").text().toLowerCase();
            var secondName = $(this).find("#secondItemName").text().toLowerCase();
            if (firstName.indexOf(query.toLowerCase()) != -1 || secondName.indexOf(query.toLowerCase()) != -1)
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