
$(document).ready(function () {

    var answerStack = [];    
    var btnNext;
    var userAnswer;

    mod08_init = function (next, user_answer) {
        btnNext = next;
        userAnswer = user_answer;
    }

    setImage = function (e) {

        if (answerStack.indexOf($(e).attr('data-choose')) < 0) {

            answerStack.push($(e).attr('data-choose'));

            $(e).css({ 'border-color': 'Gray' });            
        }

        $(userAnswer).val(answerStack.slice(0));

    }

    $('input[type=submit][value=Mula].btn.btn-default').css('visibility', 'visible');
    $('input[type=submit][value=Start].btn.btn-default').css('visibility', 'visible');

});