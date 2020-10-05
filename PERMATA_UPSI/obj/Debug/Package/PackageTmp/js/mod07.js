
$(document).ready(function () {

    var answerStack = ['', '', ''];
    var imgStack = [];
    var btnNext;
    var userAnswer;
    var answerNo;

    mod07_init = function (next, user_answer, answer_no) {
        btnNext = next;
        userAnswer = user_answer;
        answerNo = answer_no;
    }

    setImage = function (e, arrayno) {

        if (answerStack[arrayno] === '') {
            answerStack[arrayno] = $(e).attr('data-choose');
            $(e).css({ 'border-color': 'Gray' });
            imgStack.push($(e).attr('data-choose'));
        }

        if (imgStack.length >= answerNo) {
            $(userAnswer).val(answerStack.slice(0));
            $(btnNext).click();
        }

    }

});