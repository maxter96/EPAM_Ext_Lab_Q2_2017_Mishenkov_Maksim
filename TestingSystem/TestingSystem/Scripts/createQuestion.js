$(function () {
    $('#form0').submit(function(e) 
    {
        e.preventDefault();
        $that = $(this);

        var questionText = $that.find('#questionText').val();

        if (questionText.length < 1 || questionText.length > 200)
        {
            return;
        }

        var themeID = Number($that.find('#themeId').val());        
        var answers = [];

        $that.find('.answerItem').each(function(e) 
        {
            var answerText = $(this).find('#answerText').val();
            
            if (answerText.length < 1)
            {
                return;
            }

            var isCorrect = $(this).find('#isCorrect').prop('checked');

            var answer = {
                AnswerText : answerText,
                IsCorrect : isCorrect
            }

            answers[answers.length] = answer;
        })

        if (answers.length < 2)
        {
            return;
        }

        for (var i = 0; i < answers.length; i++)
        {
            if (answers[i].AnswerText.length < 1 || answers[i].AnswerText.length > 200)
            {
                return;
            }
        }

        var creatingQuestion = {
            QuestionText :questionText,
            ThemeID : themeID,
            Answers : answers
        };

        $.ajax({
            type: 'POST',
            url: $that.attr('action'),
            data: creatingQuestion,
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

    $('#form0').find('#questionText').keyup(function() 
    {
        var value = $(this).val().trim();
        if (value.length < 1)
        {
            $(this).css('background-color', '#FF9999');
            $(this).val(value);
        }
        else if (value.length > 200)
        {
            $(this).css('background-color', '#FF9999');
        }
        else
        {
            $(this).css('background-color', '#99FF99');
        }    
    })

    $('#form0').find('.answerItem').each(function() 
    {
        var $that = $(this);

        $that.find('#answerText').keyup(function() 
        {
            var value = $(this).val().trim();
            if (value.length < 1)
            {
                $(this).css('background-color', 'white');
                $(this).val(value);
                $that.find('#isCorrect').prop('disabled', 'disabled');
            }
            else if (value.length > 200)
            {
                $(this).css('background-color', '#FF9999');
                $that.find('#isCorrect').prop('disabled', 'disabled');
            }
            else
            {
                $(this).css('background-color', '#99FF99');
                $that.find('#isCorrect').prop('disabled', '');
            }
        })
    })
});