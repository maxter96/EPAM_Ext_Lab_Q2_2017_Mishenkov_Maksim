$(function () {
    $('.deleteBtn').click(function (e) 
    {
        e.preventDefault();

        if (confirm('Вы точно хотите удалить элемент?'))
        {
            $that = $(this);
            console.log($that.attr('href'));
            $.ajax({
                type: 'GET',
                url: $that.attr('href'),
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
        }
    })
})