

$(document).ready(function () {

    var answerStack = [];

    var userAnswer;
    var btnNext;
    var lblInstruction;
    var totalAnswer;
    var instruction1;
    var instruction2;

    mod05_init = function (next, user_answer, instruction, instruction_1, instruction_2, total_answer)
    {
        btnNext = next;
        userAnswer = user_answer;
        lblInstruction = instruction;
        totalAnswer = total_answer;
        instruction1 = instruction_1;
        instruction2 = instruction_2;
    }

    start = function()
    {
        $('.question').css('display', 'block');
        $(lblInstruction).text(instruction1);
    }
       

    setImage = function (e) {                
        
        if (answerStack.indexOf($(e).attr('data-choose'))<0) {

            answerStack.push($(e).attr('data-choose'));

            $(e).css({ 'border-color': 'Gray' });

            if (answerStack.length >= totalAnswer) {
                $(userAnswer).val(answerStack.slice(0));
                $(btnNext).click();
            }
        }

    }

    showAnswer = function () {
        $('#time-left').parent().css('visibility', 'hidden');
        $('.question').hide();
        $('.waiting').show();
        $(lblInstruction).html('&nbsp;');

        setInterval(function () {
            $('.answer').show();
            $(lblInstruction).text(instruction2);
            $('.waiting').hide();
        }, 1000);
    };
        
    $('input[type=submit][value=Mula].btn.btn-default').css('visibility', 'visible');
    $('input[type=submit][value=Start].btn.btn-default').css('visibility', 'visible');

});