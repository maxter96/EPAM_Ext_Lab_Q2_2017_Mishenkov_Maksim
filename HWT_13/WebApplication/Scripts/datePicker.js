$(document).ready(function () {
    $('#orderDate').datepicker({
        firstDay: 1,
        dateFormat: 'dd.mm.yy',
        minDate: new Date(1990, 1, 1),
        maxDate: new Date(9999, 1, 1)
    });
    $('#shippedDate').datepicker({
        firstDay: 1,
        dateFormat: 'dd.mm.yy',
        minDate: new Date(1990, 1, 1),
        maxDate: new Date(9999, 1, 1)
    });
    $('#reqDate').datepicker({
        firstDay: 1,
        dateFormat: 'dd.mm.yy',
        minDate: new Date(1990, 1, 1),
        maxDate: new Date(9999, 1, 1)
    });

    $.validator.addMethod('date',
        function (value, element) {
            var ok = true;
            try {
                $.datepicker.parseDate('dd.mm.yy', value);
            }
            catch (err) {
                ok = false;
            }
            return ok;
        });
});