
jQuery(document).ready(function() {

    /*
        Background slideshow
    */
    $.backstretch([
      "assets/img/backgrounds/1.jpg"
    , "assets/img/backgrounds/2.jpg"
    , "assets/img/backgrounds/3.jpg"
    ], {duration: 3000, fade: 750});

    /*
        Tooltips
    */
    $('.links a.home').tooltip();
    $('.links a.blog').tooltip();
    $('.links a.registerme').tooltip();
    $('.links a.login').tooltip();
    $('.links a.profile').tooltip();
    $('.links a.carian').tooltip();
    $('.links a.contactus').tooltip();
    $('.links a.logout').tooltip();

});


