
$(function () {
    // Reference the auto-generated proxy for the hub.
    var chat = $.connection.chat;

    // Create a function that the hub can call back to display messages.
    chat.client.addNewMessageToPage = function (name, message, connId) {
        $("#frndConnId").val(connId);
        $('.message-bar-head .usr-mg-info h3').html(name);

        let img_src = $(message).find('.mCS_img_loaded').attr('src');

        $('.message-bar-head .usr-ms-img > img').attr('src', img_src);

        // Add the message to the page.
        $('#mCSB_1_container').append(message);
    };

    // Set initial focus to message input box.
    
    $('#message-input').keypress(function (e) {
        if (e.which == 13) {//Enter key pressed
            $('#sendmessage').trigger('click');//Trigger search button click event
        }
    });


    // Start the connection.
    $.connection.hub.start().done(function () {
        chat.server.getAllActiveConnections().done(function (connections) {
            $.map(connections, function (item) {
                let $userDiv = $('.usr-list-item[data-userid="' + item.UserName + '"]');
                $userDiv.find('.connection-id').val(item.ConnectionId);
                $userDiv.find('.msg-status').removeClass('display-none');
            });

            $(".messages-list").find('ul li').click(function () {
                $('.message-bar-head .online-status').addClass('display-none')

                $('.message-bar-head .usr-mg-info h3').html($(this).find('.usr-mg-info h3').text());

                let img_src = $(this).find('.usr-ms-img img').attr('src');

                $('.message-bar-head .usr-ms-img > img').attr('src', img_src);
                
                $('.message-bar-head').show();

                if (!$(this).find('.msg-status').hasClass('display-none')) {
                    $('.message-bar-head .online-status').removeClass('display-none');
                    $("#frndConnId").val($(this).find('.connection-id').val());
                    $('#message-input').focus();
                }
            });
        });

       
        chat.server.connect(myName, myImage);

        $("#connId").val($.connection.hub.id);
        $('#sendmessage').click(function () {
            var frndConnId = $("#frndConnId").val();

            chat.server.send($('#displayname').val(), $('#message-input').val(), myImage, frndConnId, $("#connId").val());

            let msg = '<div class="main-message-box st3"><div class="message-dt st3"><div class="message-inner-dt"><p>'
                + $('#message-input').val() + '</p></div><span>5 minutes ago</span></div><div class="messg-usr-img"><img src="' +
                myImage
                + '" alt="" class="mCS_img_loaded"></div></div>';
            $('#mCSB_1_container').append(msg);
            $('#message-input').val('').focus();
        });
    });
});

function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}