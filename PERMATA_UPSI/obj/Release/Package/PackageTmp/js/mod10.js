

$(document).ready(function () {


    var imgStack = [];
    var answerStack = [];
    var answer = ['', '', '', '', '', '', '', '', '', '', '', '', '', '', '', ''];

    var blankImage;

    var userAnswer;
    var btnNext;
 
    var selectedBlok = null;
    var selectedPosition = null;

    mod10_init = function (next, blank, user_answer) {
        btnNext = next;
        userAnswer = user_answer;       
        blankImage = $(blank).attr('src');
        updateAnswer();
    }
    
    start = function () {
        btnNext.show();
    }

    setImage = function (e, position) {

        if (selectedBlok != null) {//replace

            if (selectedPosition != null) {//dr dlm

                if (selectedPosition == position) {//double klik to remove
                    $(e).attr('style', 'cursor: pointer');

                    $('img[data-choose="' + answer[position] + '"]').attr('src', $(e).attr('src'));


                    $(e).attr('src', blankImage);
                    answer[position] = '';
                    updateAnswer();
                    selectedBlok = null;
                    selectedPosition = null;
                }
                else {

                    if ($(e).attr('src') != blankImage) {

                        $('img[data-choose="' + answer[position] + '"]').attr('src', $(e).attr('src'));

                    }
                   
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

                if ($(e).attr('src') != blankImage) {

                    $('img[data-choose="' + answer[position] + '"]').attr('src', $(e).attr('src'));
                }
               
                $(selectedBlok).attr('style', 'cursor: pointer');
                $(e).attr('src', $(selectedBlok).attr('src'));
                answer[position] = $(selectedBlok).attr('data-choose');
                updateAnswer();
                $(selectedBlok).attr('src', blankImage);
                selectedPosition = null;
                selectedBlok = null;
                
            }
        }
        else {//select image

            if ($(e).attr('src') != blankImage) {
                selectedPosition = position;
                selectedBlok = e;
                $(e).attr('style', 'border-style:solid;border-color:blue;border-width:1px !important;');
                //$(e).css('width', '-=1px');
            }
        }


    }

    pushImage = function (e) {
        
        if ($(e).attr('src') != blankImage) {

            if (selectedBlok != e) {
                $(selectedBlok).attr('style', 'cursor: pointer');
                $(e).attr('style', 'cursor:pointer;border-style:solid;border-color:blue;border-width:1px !important;');
                //$(e).css('width', '-=1px');
                selectedBlok = e;
            }
            else {
                $(e).attr('style', 'cursor: pointer');
                selectedBlok = null;
            }

        }
        else
        {
            $(selectedBlok).attr('style', 'cursor: pointer');
            selectedBlok = null;
        }

        selectedPosition = null;
    }

    function updateAnswer() {
        $(userAnswer).val(answer.slice(0));
    }

    $('input[type=submit][value=Mula].btn.btn-default').css('visibility', 'visible');
    $('input[type=submit][value=Start].btn.btn-default').css('visibility', 'visible');
        
});