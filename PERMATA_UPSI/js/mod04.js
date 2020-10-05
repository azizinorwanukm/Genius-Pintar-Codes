

$(document).ready(function () {

    var answerStack = ['','',''];
    var imgStack = [];
    var btnNext;
    var userAnswer;

    mod04_init = function (next,user_answer)
    {
        btnNext = next;
        userAnswer = user_answer;
    }

    setImage = function (e, arrayno) {

        if (answerStack[arrayno] === '')
        {
            answerStack[arrayno] = $(e).attr('data-choose');
            $(e).css({'border-color': 'Gray'});  
            imgStack.push($(e).attr('data-choose'));
        }

        if(imgStack.length>=3)
        {
            $(userAnswer).val(answerStack.slice(0));
            $(btnNext).click();
        }

    }

    $('input[type=submit][value=Mula].btn.btn-default').css('visibility', 'visible');
    $('input[type=submit][value=Start].btn.btn-default').css('visibility', 'visible');

});