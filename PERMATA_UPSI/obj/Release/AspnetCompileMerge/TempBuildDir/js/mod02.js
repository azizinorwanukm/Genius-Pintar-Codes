

$(document).ready(function () {

    var imgStack = [];

    var userAnswer;
    var btnNext;

    mod02_init = function (next,user_answer)
    {
        btnNext = next;
        userAnswer = user_answer;
    }
   

    setImage = function (e) {
        $(userAnswer).val($(e).attr('data-choose'));
        $(btnNext).click();
    }


});