$(function () {
    $('#form0').submit(function (e) 
    {
        $that = $(this);
        e.preventDefault();

        if (!$that.valid())
        {
            return;
        }

        var msg = $that.serialize();
        $.ajax({
            type: 'POST',
            url: $that.attr('action'),
            data: msg,
            success: function(data) {
                if (data == '')
                {
                   location.reload();
                }
                else
                {
                    alert(data);
                }
            },
            error:  function(xhr, str) {
	            alert('Error: ' + xhr.responseCode);
            }
        });
    })
})