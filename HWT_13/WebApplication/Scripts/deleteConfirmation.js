$(function() {
    $("#deleteBtn").mousedown(function () {
        var $items = $("[name = deleteOrder]:checked");
        var orders = [];

        $items.each(function ()
        {
            orders[orders.length] = (Number($(this).attr("orderID")));
        });

        if (orders.length === 0)
        {
            alert("Не выбрано ни одного заказа!");
            return;
        }

        if (confirm("Удалить заказы (выбрано: " + orders.length + ") ?"))
        {
            $.ajax({
                type: "POST",
                url: "/Home/Delete",
                data: JSON.stringify({ orders: orders }),
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                async: true,
                success: function () { location.pathname = "Home/Index";}
            });

            
        }
    })
});
