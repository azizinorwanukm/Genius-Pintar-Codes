

$(document).ready(function () {

    var imgStack = [];
    var answerStack = [];
    var answer = ['', '', '', '', '', '', '', '', '', '', '', '', '', '', ''];

    var blankImage;
    var btnNext;
    var userAnswer;
    var timerLeft;

    var selectedBlok = null;
    var selectedPosition = null;

    mod01_init = function (next, blank, user_answer, time_left) {
        btnNext = next;
        blankImage = $(blank).attr('src');
        userAnswer = user_answer;
        timerLeft = time_left;
        updateAnswer();
    }

    start = function (e, interval) {
        $(e).hide();
        $(specialRow).css('visibility', 'visible');
        startCountdown(interval);
    }

    setImage1 = function (e, position) {
        if ($(e).attr('src') === blankImage) {
            if (imgStack.length > 0) {
                $(e).attr('src', imgStack.shift());
                answer[position] = answerStack.shift();
                updateAnswer();
            }
        }
        else {
            imgStack.push($(e).attr('src'));
            answerStack.push(answer[position]);
            $(e).attr('src', blankImage);
            answer[position] = '';
            updateAnswer();

        }
    }

    pushImage1 = function (e) {
        $(e).remove();
        imgStack.push($(e).attr('src'));
        answerStack.push($(e).attr('data-choose'));
    }

    setImage2 = function (e, position) {

        if (selectedBlok != null) {

            if (selectedPosition != null) {//dr dlm

                if (selectedPosition == position) {
                    $(e).attr('style', 'cursor: pointer');
                    $(e).attr('src', blankImage);
                    answer[position] = '';
                    updateAnswer();
                    selectedBlok = null;
                    selectedPosition = null;
                }
                else {
                    
                    $(e).attr('src', $(selectedBlok).attr('src'));
                    answer[position] = answer[selectedPosition];
                    $(selectedBlok).attr('style', 'cursor: pointer');
                    $(selectedBlok).attr('src', blankImage);
                    answer[selectedPosition] = '';
                    updateAnswer();
                    selectedPosition = null;
                    selectedBlok = null;
                }
                
            }
            else {//dr luar

                $(selectedBlok).attr('style', 'cursor: pointer');
                $(e).attr('src', $(selectedBlok).attr('src'));
                answer[position] = $(selectedBlok).attr('data-choose');
                updateAnswer();
                selectedPosition = null;
                selectedBlok = null;
            }
        }
        else {

            if ($(e).attr('src') != blankImage) {
                selectedPosition = position;
                selectedBlok = e;
                $(e).attr('style', 'width:98px;border-style:solid;border-color:blue;border-width:2px !important;');
            }
        }


    }

    pushImage2 = function (e) {
        
        selectedPosition = null;

        if (selectedBlok != e) {
            $(selectedBlok).attr('style', 'cursor: pointer');
            $(e).attr('style', 'cursor:pointer;width:98px;border-style:solid;border-color:blue;border-width:2px !important;');
            selectedBlok = e;
        }
        else {
            $(e).attr('style', 'cursor: pointer');
            selectedBlok = null;
        }
    }

    function updateAnswer() {
        $(userAnswer).val(answer.slice(0));
    }
    
    $('input[type=submit][value=Mula].btn.btn-default').css('visibility', 'visible');
    $('input[type=submit][value=Start].btn.btn-default').css('visibility', 'visible');

});