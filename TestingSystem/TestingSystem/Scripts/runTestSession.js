$(function () {
    var sessionId = $('#testSessionId').val();
    var userId = $('#userId').val();
    console.log(sessionId);
    
    var $items = $('.answerItem');
    $items.change(function() 
    {
        var answerId = $(this).prop('id');
        var model = 
        {
            TestSessionID : parseInt(sessionId),
            AnswerID : parseInt(answerId)
        }

        if ($(this).prop('checked'))
        {
            $.ajax({
                type: 'POST',
                url: "CreateUserAnswer",
                data: model,
            });
        }
        else
        {
            $.ajax({
                type: 'POST',
                url: "DeleteUserAnswer",
                data: model,
            });
        }
    });

    var $remainingTime = $('#remainingTime');

    function ParseTime(minutes)
    {
        var hours = Math.floor(minutes / 60);
        var mins = minutes - hours * 60;
        var result = '';

        if (hours < 10)
        {
            result += '0';
        }

        result += hours.toString() + ':';

        if (mins < 10)
        {
            result += '0';
        }

        result += mins.toString();
        return result;
    }

    var postData = 
    {
        UserID : parseInt(userId),
        TestSessionID : parseInt(sessionId)
    };

    function UpdateTime()
    { 
         $.ajax({
            type: 'POST',
            url: "GetRemainingTime",
            data: postData,
            success: function(data) {
                if (data <= 0)
                {
                    $.ajax({
                        type: 'POST',
                        url: "EndTestSession",
                        data: postData,
                        success: function(newData) {
                            window.location.href = newData;
                        }
                    })
                }
                $remainingTime.text(ParseTime(data));
            }
         });
    }

    $('#endTestSession').click(function()
    {
         $.ajax({
            type: 'POST',
            url: "EndTestSession",
            data: postData,
            success: function(newData) {
                window.location.href = newData;
            }
         })
    });
    
    UpdateTime();
    var interval = setInterval(UpdateTime, 10000);
})