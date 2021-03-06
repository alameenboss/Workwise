$(window).on("load", function() {
    "use strict";

    

    //  ============= POST PROJECT POPUP FUNCTION =========

    $(".post_project").on("click", function(){
        $(".post-popup.pst-pj").addClass("active");
        $(".wrapper").addClass("overlay");
        return false;
    });
    $(".post-project > a").on("click", function(){
        $(".post-popup.pst-pj").removeClass("active");
        $(".wrapper").removeClass("overlay");
        return false;
    });

    //  ============= POST JOB POPUP FUNCTION =========

    $(".post-jb").on("click", function(){
        $(".post-popup.job_post").addClass("active");
        $(".wrapper").addClass("overlay");
        return false;
    });
    $(".post-project > a").on("click", function(){
        $(".post-popup.job_post").removeClass("active");
        $(".wrapper").removeClass("overlay");
        return false;
    });

    //  ============= SIGNIN CONTROL FUNCTION =========

    //$('.sign-control li').on("click", function(){
    //    var tab_id = $(this).attr('data-tab');
    //    $('.sign-control li').removeClass('current');
    //    $('.sign_in_sec').removeClass('current');
    //    $(this).addClass('current animated fadeIn');
    //    $("#"+tab_id).addClass('current animated fadeIn');
    //    return false;
    //});

    //  ============= SIGNIN TAB FUNCTIONALITY =========

    $('.signup-tab ul li').on("click", function(){
        var tab_id = $(this).attr('data-tab');
        $('.signup-tab ul li').removeClass('current');
        $('.dff-tab').removeClass('current');
        $(this).addClass('current animated fadeIn');
        $("#"+tab_id).addClass('current animated fadeIn');
        return false;
    });

    //  ============= SIGNIN SWITCH TAB FUNCTIONALITY =========

    $('.tab-feed ul li').on("click", function(){
        var tab_id = $(this).attr('data-tab');
        $('.tab-feed ul li').removeClass('active');
        $('.product-feed-tab').removeClass('current');
        $(this).addClass('active animated fadeIn');
        $("#"+tab_id).addClass('current animated fadeIn');
        return false;
    });

    //  ============= COVER GAP FUNCTION =========

    var gap = $(".container").offset().left;
    $(".cover-sec > a, .chatbox-list").css({
        "right": gap
    });

    //  ============= OVERVIEW EDIT FUNCTION =========

    $(".overview-open").on("click", function(){
        $("#overview-box").addClass("open");
        $(".wrapper").addClass("overlay");
        return false;
    });
    $(".close-box").on("click", function(){
        $("#overview-box").removeClass("open");
        $(".wrapper").removeClass("overlay");
        return false;
    });


    $(".edit-basic-open").on("click", function () {
        $("#edit-basic-box").addClass("open");
        $(".wrapper").addClass("overlay");
        return false;
    });
    $(".close-box, #edit-basic-box .cancel").on("click", function () {
        $("#edit-basic-box").removeClass("open");
        $(".wrapper").removeClass("overlay");
        return false;
    });
    //  ============= EXPERIENCE EDIT FUNCTION =========

    $(".exp-bx-open").on("click", function(){
        $("#experience-box").addClass("open");
        $(".wrapper").addClass("overlay");
        return false;
    });
    $(".close-box").on("click", function(){
        $("#experience-box").removeClass("open");
        $(".wrapper").removeClass("overlay");
        return false;
    });

    //  ============= EDUCATION EDIT FUNCTION =========

    $(".ed-box-open").on("click", function(){
        $("#education-box").addClass("open");
        $(".wrapper").addClass("overlay");
        return false;
    });
    $(".close-box").on("click", function(){
        $("#education-box").removeClass("open");
        $(".wrapper").removeClass("overlay");
        return false;
    });

    //  ============= LOCATION EDIT FUNCTION =========

    $(".lct-box-open").on("click", function(){
        $("#location-box").addClass("open");
        $(".wrapper").addClass("overlay");
        return false;
    });
    $(".close-box").on("click", function(){
        $("#location-box").removeClass("open");
        $(".wrapper").removeClass("overlay");
        return false;
    });

    //  ============= SKILLS EDIT FUNCTION =========

    $(".skills-open").on("click", function(){
        $("#skills-box").addClass("open");
        $(".wrapper").addClass("overlay");
        return false;
    });
    $(".close-box").on("click", function(){
        $("#skills-box").removeClass("open");
        $(".wrapper").removeClass("overlay");
        return false;
    });

    //  ============= ESTABLISH EDIT FUNCTION =========

    $(".esp-bx-open").on("click", function(){
        $("#establish-box").addClass("open");
        $(".wrapper").addClass("overlay");
        return false;
    });
    $(".close-box").on("click", function(){
        $("#establish-box").removeClass("open");
        $(".wrapper").removeClass("overlay");
        return false;
    });

    //  ============= CREATE PORTFOLIO FUNCTION =========

    $(".portfolio-btn > a").on("click", function(){
        $("#create-portfolio").addClass("open");
        $(".wrapper").addClass("overlay");
        return false;
    });
    $(".close-box").on("click", function(){
        $("#create-portfolio").removeClass("open");
        $(".wrapper").removeClass("overlay");
        return false;
    });

    //  ============= EMPLOYEE EDIT FUNCTION =========

    $(".emp-open").on("click", function(){
        $("#total-employes").addClass("open");
        $(".wrapper").addClass("overlay");
        return false;
    });
    $(".close-box").on("click", function(){
        $("#total-employes").removeClass("open");
        $(".wrapper").removeClass("overlay");
        return false;
    });

    //  =============== Ask a Question Popup ============

    $(".ask-question").on("click", function(){
        $("#question-box").addClass("open");
        $(".wrapper").addClass("overlay");
        return false;
    });
    $(".close-box").on("click", function(){
        $("#question-box").removeClass("open");
        $(".wrapper").removeClass("overlay");
        return false;
    });


    //  ============== ChatBox ============== 


    $(".chat-mg").on("click", function(){
        $(this).next(".conversation-box").toggleClass("active");
        return false;
    });
    $(".close-chat").on("click", function(){
        $(".conversation-box").removeClass("active");
        return false;
    });

    //  ================== Edit Options Function =================


    $(".ed-opts-open").on("click", function(){
        $(this).next(".ed-options").toggleClass("active");
        return false;
    });


    // ============== Menu Script =============

    $(".menu-btn > a").on("click", function(){
        $("nav").toggleClass("active");
        return false;
    });


    //  ============ Notifications Open =============

    $(".not-box-open").on("click", function () {
        $("#message").hide();
        $(".user-account-settingss").hide();
        $(this).next("#notification").toggle();
    });


     //  ============ Messages Open =============

    $("#message .nt-title a").on("click", function (e) {
        e.preventDefault();
        $('#message .notfication-details').hide('slow', function () {
            $(this).remove();
        });
    });

    $(".not-box-openm").on("click", function () {
        $("#notification").hide();
        $(".user-account-settingss").hide();
        $(this).next("#message").toggle();
    });


    // ============= User Account Setting Open ===========
	/*
$(".user-info").on("click", function(){$("#users").hide();
        $(".user-account-settingss").hide();
        $(this).next("#notification").toggle();
    });
    
	*/
	$( ".user-info" ).click(function() {
  $( ".user-account-settingss" ).slideToggle( "fat");
	  $("#message").not($(this).next("#message")).slideUp();
	  $("#notification").not($(this).next("#notification")).slideUp();
    // Animation complete.
  });
 

    //  ============= FORUM LINKS MOBILE MENU FUNCTION =========

    $(".forum-links-btn > a").on("click", function(){
        $(".forum-links").toggleClass("active");
        return false;
    });
    $("html").on("click", function(){
        $(".forum-links").removeClass("active");
    });
    $(".forum-links-btn > a, .forum-links").on("click", function(){
        e.stopPropagation();
    });

    //  ============= PORTFOLIO SLIDER FUNCTION =========

    $('.profiles-slider').slick({
        slidesToShow: 3,
        slck:true,
        slidesToScroll: 1,
        prevArrow:'<span class="slick-previous"></span>',
        nextArrow:'<span class="slick-nexti"></span>',
        autoplay: true,
        dots: false,
        autoplaySpeed: 2000,
        responsive: [
        {
          breakpoint: 1200,
          settings: {
            slidesToShow: 2,
            slidesToScroll: 1,
            infinite: true,
            dots: false
          }
        },
        {
          breakpoint: 991,
          settings: {
            slidesToShow: 2,
            slidesToScroll: 2
          }
        },
        {
          breakpoint: 768,
          settings: {
            slidesToShow: 1,
            slidesToScroll: 1
          }
        }
        // You can unslick at a given breakpoint now by adding:
        // settings: "unslick"
        // instead of a settings object
      ]


    });

   

});

$('#btnSearch').click(function () {
    searchFriends();
});
//$('.dropdown-menu').click(function () {
//    return false;
//});
$('.not-box-open').click(function () {
    $('#notification').find('.nott-list').load('/User/_UserNotifications', function () {

    });
})
$(document).find(".prettydate").prettydate({
    autoUpdate: true,
    dateFormat: "DD-MM-YYYY hh:mm:ss",
    duration: 30000
});
$(document).on("click", 'input[class$="btnCloseNotification"]', function () {
    $(this).parent(".divNotification").animate({ "opacity": "hide", top: "100" }, 500);
});
$(document).on("click", '.sendRequest', function (e) {
    e.preventDefault();
    var userID = $(this).attr('data-user-id');
    var loggedInUserID = $('#hdfLoggedInUserID').val();
    sendFriendRequest(userID, loggedInUserID);
    $('.sendRequest[data-user-id="' + userID + '"]').removeClass('sendRequest').addClass('disabled').html('Pending');
});
$(document).on("click", 'input[class$="request-response"]', function () {
    var userid = $(this).attr('data-user-id');
    var requestResponse = $(this).attr('data-value');
    sendResponseToRequest(userid, requestResponse, $('#hdfLoggedInUserID').val());
    $(this).val(requestResponse);
    $(this).addClass('disabled');
    $(this).siblings().addClass('disabled');
});
$(document).on('click', '.divNotificationPopup', function () {
    var status = $(this).attr('data-status');
    if (status != "Read") {
        $(this).attr('data-status', 'Read');
        var notificationID = $(this).attr('data-notificationID');
        changeUserNotificationStatus(notificationID);
    }
});
$(document).on('click', '.btn-send-chat-message', function () {
    sendChatMessage();
});
$(document).on('keypress', '.txt-chat-message', function (e) {
    if (e.which == 13) {
        sendChatMessage();
    }
    else {
        sendUserTypingStatus();
    }
    return;
});
$(document).on('click', '.usr-list-item', function () {
    var userID = $(this).attr('data-userid');
    $(this).siblings('li').removeClass('active');
    $(this).addClass('active');
    $('.hdf-current-chat-user-id').val(userID);
    $('.hdf-current-chat-user-profile-picture').val($(this).attr('data-userimg'));
    $('.hdf-current-chat-user-name').val($(this).attr('data-username'));
    $('.hdf-current-chat-user-id').val($(this).attr('data-userid'));
    $('.hdf-current-chat-user-id').val($(this).attr('data-userid'));
    initiateChat(userID);

});
function searchFriends() {
    var searchText = $('#txtSearch').val();
    if (searchText != null && searchText != '') {
        $("#divBody").html('');
        $("#divBody").load('/User/_UserSearchResult?name=' + searchText + '', function () {

        });
        $("#divBody").animate({ "opacity": "show", top: "100" }, 500);
    }
    else {
        $("#divBody").animate({ "opacity": "hide", top: "100" }, 500);
    }
}


