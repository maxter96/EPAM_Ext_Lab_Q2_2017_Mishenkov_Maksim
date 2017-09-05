$(function () {
    
    var $form0 = $('#form0');
    var $testName = $form0.find('#testName');
    var $timeLimit = $form0.find('#timeLimit');
    var $attemptsCount = $form0.find('#attemptsCount');

    $form0.submit(function(e) 
    {
        e.preventDefault();

        var name = $testName.val().trim();
        if (name.length < 1 || name.length > 50)
        {
            return;
        }

        var re = /^\d+$/;
        var limit = $timeLimit.val().trim();
        var limitValue = 10;
        var attempts = $attemptsCount.val().trim();
        var attemptsValue = 1;

        if (!re.test(limit))
        {
            return;
        }
        else
        {
            limitValue = parseInt(limit);
            if (limitValue < 10 || limitValue > 240)
            {
                return;
            }
        }

        if (!re.test(attempts))
        {
            return;
        }
        else
        {
            attemptsValue = parseInt(attempts);
            if (attemptsValue < 1 || attemptsValue > 5)
            {
                return;
            }
        }   

        var $questionItems = $form0.find('.questionItem');
        var questions = [];

        $questionItems.each(function(e)
        {
            if ($(this).prop('checked'))
            {
                questions[questions.length] = parseInt($(this).prop('id'));
            }
        })

        var creatingTest = {
            TestName : name,
            TimeLimit : limitValue,
            AttemptsCount: attemptsValue,
            Questions : questions
        };

        $.ajax({
            type: 'POST',
            url: $form0.attr('action'),
            data: creatingTest,
            success: function(data) {
                if (data == '')
                {
                    location.pathname = "Management/Tests";
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


    $testName.keyup(function() 
    {
        var value = $(this).val().trim();
        if (value.length < 1)
        {
            $(this).css('background-color', '#FF9999');
            $(this).val(value);
        }
        else if (value.length > 50)
        {
            $(this).css('background-color', '#FF9999');
        }
        else
        {
            $(this).css('background-color', '#99FF99');
        }    
    })

    function timeLimitHandler()  
    {
        var re = /^\d+$/;
        var value = $(this).val().trim();
        $(this).val(value);

        if (!re.test(value))
        {
            $(this).css('background-color', '#FF9999');
        }
        else
        {
            var number = parseInt(value);
            if (number >= 10 && number <= 240)
            {
                $(this).css('background-color', '#99FF99');
            }
            else
            {
                $(this).css('background-color', '#FF9999');
            }
        }  
    }

    function attemptsCountHandler()  
    {
        var re = /^\d+$/;
        var value = $(this).val().trim();
        $(this).val(value);

        if (!re.test(value))
        {
            $(this).css('background-color', '#FF9999');
        }
        else
        {
            var number = parseInt(value);
            if (number >= 1 && number <= 5)
            {
                $(this).css('background-color', '#99FF99');
            }
            else
            {
                $(this).css('background-color', '#FF9999');
            }
        }  
    }

    $timeLimit.mouseup(timeLimitHandler)
    $timeLimit.keyup(timeLimitHandler)
    $timeLimit.val(10)
    $timeLimit.css('background-color', '#99FF99')

    $attemptsCount.mouseup(attemptsCountHandler)
    $attemptsCount.keyup(attemptsCountHandler)
    $attemptsCount.val(1)
    $attemptsCount.css('background-color', '#99FF99')

    $testName.val("TestName")
    $testName.css('background-color', '#99FF99')
});