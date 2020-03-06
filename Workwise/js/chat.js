let sentSvg = '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 15" width="16" height="15"><path fill="#a6a0a0" d="M15.01 3.316l-.478-.372a.365.365 0 0 0-.51.063L8.666 9.879a.32.32 0 0 1-.484.033l-.358-.325a.319.319 0 0 0-.484.032l-.378.483a.418.418 0 0 0 .036.541l1.32 1.266c.143.14.361.125.484-.033l6.272-8.048a.366.366 0 0 0-.064-.512zm-4.1 0l-.478-.372a.365.365 0 0 0-.51.063L4.566 9.879a.32.32 0 0 1-.484.033L1.891 7.769a.366.366 0 0 0-.515.006l-.423.433a.364.364 0 0 0 .006.514l3.258 3.185c.143.14.361.125.484-.033l6.272-8.048a.365.365 0 0 0-.063-.51z"></path></svg>';
let viewedSvg = '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 18 18" width="18" height="18"><path fill="#4FC3F7" d="M17.394 5.035l-.57-.444a.434.434 0 0 0-.609.076l-6.39 8.198a.38.38 0 0 1-.577.039l-.427-.388a.381.381 0 0 0-.578.038l-.451.576a.497.497 0 0 0 .043.645l1.575 1.51a.38.38 0 0 0 .577-.039l7.483-9.602a.436.436 0 0 0-.076-.609zm-4.892 0l-.57-.444a.434.434 0 0 0-.609.076l-6.39 8.198a.38.38 0 0 1-.577.039l-2.614-2.556a.435.435 0 0 0-.614.007l-.505.516a.435.435 0 0 0 .007.614l3.887 3.8a.38.38 0 0 0 .577-.039l7.483-9.602a.435.435 0 0 0-.075-.609z"></path></svg>';

var chat = $.connection.chatHub;

chat.client.refreshOnlineUsers = function () {
    refreshOnlineUsers();
};
chat.client.receiveNotification = function (notificationType, userInfo, notificationID, notificationCounts) {
    changeUserNotificationCounts(notificationCounts);
    var notificationMessage = '';
    if (notificationType == "FriendRequest") {
        notificationMessage = '<span><a href="/User/Profile/' + userInfo.UserID + '"><img src="' + userInfo.ProfilePicture + '" class="profilePictureCircle" />&nbsp;&nbsp;&nbsp;' + userInfo.Name + '</a><br /><span class="pull-right"><span style="font-size:11px;">Friend Request</span> &nbsp;&nbsp;<input type="button" class="btn btn-success btn-xs request-response" data-user-id="' + userInfo.UserID + '" data-value="Accepted" value="Accept" />&nbsp;&nbsp;<input type="button" class="btn btn-danger btn-xs request-response" data-user-id="' + userInfo.UserID + '" data-value="Rejected" value="Reject" /></span><br /></span>';
    }
    else if (notificationType == "FriendRequestAccepted") {
        notificationMessage = '<span><a href="/User/Profile/' + userInfo.UserID + '"><img src="' + userInfo.ProfilePicture + '" class="profilePictureCircle" />&nbsp;&nbsp;&nbsp;' + userInfo.Name + '</a><br /><span class="pull-right"><span style="font-size:11px;">Accepted your request</span></span><br /></span>';
    }
    var notificationHtml = '<div data-notificationID="' + notificationID + '" style="display:none" id="divNotificationPopUp-' + notificationID + '" class="alert alert-dismissible alert-info divNotificationPopup"><button type="button" class="close btnCloseNotification" data-notificationID="' + notificationID + '">&times;</button>' + notificationMessage + '</div>';
    $('.new-notificaion-window').append(notificationHtml);
    $(document).find('#divNotificationPopUp-' + notificationID + '').animate({ "opacity": "show", top: "100" }, 500);
    setTimeout(function () {
        removeNotificationPop(notificationID);
    }, 60000)
}
chat.client.refreshNotificationCounts = function (notificationCounts) {
    changeUserNotificationCounts(notificationCounts)
}
chat.client.removeNotification = function (notificationID) {
    removeNotificationPop(notificationID);
    var notificationRow = $(document).find('div[class$="divNotification"][data-notificationid="' + notificationID + '"]');
    removeHtmlElement(notificationRow);
}
chat.client.addNewChatMessage = function (messageRow, fromUserId, toUserId, fromUserName, fromUserProfilePic, toUserName, toUserProfilePic) {
    debugger;
    var currentUserId = $('#hdfLoggedInUserID').val();
    var currentChatUserID = $(document).find('.hdf-current-chat-user-id').val();
    if (currentChatUserID == fromUserId || currentChatUserID == toUserId) {
        createNewMessageBlock(fromUserName, fromUserProfilePic, messageRow.CreatedOn, messageRow.Message, (currentUserId == fromUserId ? 'right' : 'left'), messageRow.ChatMessageID, messageRow.Status);
        if (currentUserId != fromUserId) {
            var currentChatUserID = $(document).find('.hdf-current-chat-user-id').val();
            setTimeout(function () {
                UpdateChatMessageStatus(messageRow.ChatMessageID, currentChatUserID);
            }, 100);
        }
    }
    if (currentUserId != fromUserId) {
        var windowActive = $('#hdfWindowIsActiveOrNot').val();
        if (windowActive == 'False') {
            document.title = "Message received from " + fromUserName;
        }
    }
    addChatMessageCount(currentUserId, fromUserId, fromUserName, fromUserProfilePic, toUserId, toUserName, toUserProfilePic, messageRow.Message, messageRow.CreatedOn)
}
chat.client.userIsTyping = function (fromUserId) {
    var currentChatUserID = $(document).find('.hdf-current-chat-user-id').val();
    if (currentChatUserID == fromUserId) {
        //$(document).find('#user-is-typing').show();
        //setTimeout(function () {
        //    $(document).find('#user-is-typing').hide();
        //}, 1000);
        $(document).find('.online-status').html("Typing.....")
        setTimeout(function () {
            $(document).find('.online-status').html("Online")
        }, 1000);
    }
    //$(document).find(".messages-line").mCustomScrollbar("scrollTo", "bottom", { scrollInertia: 1 });
}
chat.client.updateMessageStatusInChatWindow = function (messageID, currentUserID, fromUserID) {
    if (messageID > 0) {
        var message = $(document).find('span[class="chat-message-status"][data-chat-message-id="' + messageID + '"]');
        if (message.length > 0) {
            $(message).html(viewedSvg);
        }
    }
    else {
        var currentChatUserID = $(document).find('.hdf-current-chat-user-id').val();
        if (currentChatUserID == currentUserID) {
            var messages = $(document).find('span[class="chat-message-status"]');
            $(messages).each(function (index, item) {
                $(item).html(viewedSvg);
            });
        }
    }
}
chat.client.refreshOnlineUserByUserID = function (userID, isOnline, lastSeen) {
    if (isOnline == true) {
        $(document).find('.usr-list-item[data-userid="' + userID + '"]').find('.msg-status').removeClass('display-none');
    } else {
        $(document).find('.usr-list-item[data-userid="' + userID + '"]').find('.msg-status').addClass('display-none');

    }
    var currentChatUserID = $(document).find('.hdf-current-chat-user-id').val();
    if (currentChatUserID == userID) {
        if (isOnline == true) {
            $(document).find('.message-bar-head .online-status').html('Online');
        }
        else {
            $(document).find('.message-bar-head .online-status').html('Last seen : <span class="prettydate">' + lastSeen +'</span>');
        }
    }
}
$.connection.transports.longPolling.supportsKeepAlive = function () {
    return false;
}
$.connection.hub.qs = "UserID=" + $('#hdfLoggedInUserID').val();
//$.connection.hub.start({ transport: ['longPolling', 'webSockets'], waitForPageLoad: false }).done(function () {
$.connection.hub.start().done(function () {
    refreshUserNotificationCounts($('#hdfLoggedInUserID').val());
    refreshOnlineUsers();
    refreshRecentChats();
});
$.connection.hub.disconnected(function () {
    setTimeout(function () {
        $.connection.hub.start();
    }, 5000); // Restart connection after 5 seconds.
});
$(document).ready(function () {
    $('#hdfWindowIsActiveOrNot').val('True');
    $(window).blur(function () {
        $('#hdfWindowIsActiveOrNot').val('False');
    });
    $(window).focus(function () {
        $('#hdfWindowIsActiveOrNot').val('True');
        document.title = "Chat";
    });

});
function sendResponseToRequest(userid, requestResponse, loggedInUserID) {
    chat.server.sendResponseToRequest(userid, requestResponse, loggedInUserID);
}
function sendFriendRequest(userID, loggedInUserID) {
    chat.server.sendRequest(userID, loggedInUserID);
}
function refreshUserNotificationCounts(loggedInUserID) {
    chat.server.refreshNotificationCounts(loggedInUserID);
}
function changeUserNotificationStatus(notificationID) {
    chat.server.changeNotitficationStatus(notificationID, $('#hdfLoggedInUserID').val());
}
function refreshOnlineUsers() {

    $.ajax({
        url: '/User/_OnlineFriends',
        method: 'GET',
        success: function (response) {
            response.forEach(function (v) {
                $(document).find('.usr-list-item[data-userid="' + v.UserId + '"]').find('.msg-status').removeClass('display-none');
            })
            
        }
    });
}
function changeUserOnlineStatus(cItem) {
    $(cItem).find('img').removeClass('online-user-profile-pic');
    var userID = $(cItem).attr('data-user-id');
    var onlineItem = $(document).find('.online-friends').find('a[data-user-id="' + userID + '"]');
    if (onlineItem.length > 0) {
        $(cItem).find('img').addClass('online-user-profile-pic');
    }
}
function changeUserNotificationCounts(notificationCounts) {
    if (notificationCounts != null && notificationCounts != '' && notificationCounts != 0 && notificationCounts != '0') {
        $('.user-notification-count').text(notificationCounts).show();
    }
    else {
        $('.user-notification-count').text('').hide();
    }
}
function removeHtmlElement(ele) {
    if (ele.length > 0) {
        ele.animate({ "opacity": "hide", top: "100" }, 500);
        setTimeout(function () {
            ele.remove();
        }, 500);
    }
}
function removeNotificationPop(notificationID) {
    var notificationPopup = $(document).find('#divNotificationPopUp-' + notificationID + '');
    removeHtmlElement(notificationPopup);
}
function unfriendUser(friendMappingID) {
    chat.server.unfriendUser(friendMappingID);
}
function initiateChat(toUserID) {
    $(".main-conversation-place-holder").load('/Messages/_Messages/' + toUserID, function () {
        $(".main-conversation-place-holder").animate({ "opacity": "show", top: "100" }, 500);
        var currentChatUserID = $(document).find('.hdf-current-chat-user-id').val();
        UpdateChatMessageStatus(0, currentChatUserID);
        var chatUser = $(document).find('li[data-userid="' + toUserID + '"]');
        
        if (chatUser.length > 0) {
            var badge = $(chatUser).find('.msg-notifc');
            $(badge).text('');
            $(badge).addClass('display-none');
            $(chatUser).find('.usr-mg-info > p').html('');
            $(chatUser).find('.posted_time').text('');
        }
        $(".chat-hist, .messages-line").mCustomScrollbar();
        $(document).find(".messages-line").mCustomScrollbar("scrollTo", "bottom", { scrollInertia: 1 });
        $(document).find(".prettydate").prettydate({
            autoUpdate: true,
            dateFormat: "DD-MM-YYYY hh:mm:ss",
            duration: 30000
        });
    });
}
function createNewMessageBlockHtml(name, profilePicture, createOn, message, align, chatMessageID, status) {
    let className = "ta-right";
    let status1 = "";
    if (align == "left") {
        className = "st3"
       
    }
    if (align == "right") {
        status1 = '<span data-chat-message-id="' + chatMessageID + '" class="chat-message-status">' + sentSvg + '</span>';
        if (status == "Viewed") {
            status1 = '<span data-chat-message-id="' + chatMessageID + '" class="chat-message-status">' + viewedSvg + '</span>';
        }
    }
    var html = '<div class="main-message-box ' + className + '" data-chat-message-id="' + chatMessageID + '">' +
        '<div class="message-dt ' + className + '">' +
        '<div class="message-inner-dt" data-chat-message-id="' + chatMessageID + '">' +
        '<p>' + message + '</p></div><span><span class="prettydate">' + createOn + '</span>' +  status1 +
        '</span></div><div class="messg-usr-img"><img src="' + profilePicture + '" alt="' + name + '" class="mCS_img_loaded"></div></div>';
    return html;
}
function sendChatMessage() {
    var fromUserID = $('#hdfLoggedInUserID').val();
    var fromUserName = $('#hdfLoggedInUserName').val();
    var fromUserPrifilePic = $('#hdfLoggedInUserProfilePicture').val();
    var chatMessage = $(document).find('.txt-chat-message').val();
    var toUserID = $(document).find('.hdf-current-chat-user-id').val();
    var toUserName = $(document).find('.hdf-current-chat-user-name').val();
    var toUserProfilePic = $(document).find('.hdf-current-chat-user-profile-picture').val();
    if (chatMessage != null && chatMessage != '') {
        chat.server.sendMessage(fromUserID, toUserID, chatMessage, fromUserName, fromUserPrifilePic, toUserName, toUserProfilePic);
        $(document).find('.txt-chat-message').val('');
    }
}
function createNewMessageBlock(name, profilePicture, createOn, message, align, chatMessageID, status) {
    $(document).find('.chat').append(createNewMessageBlockHtml(name, profilePicture, createOn, message, align, chatMessageID, status));
    $(document).find(".messages-line").mCustomScrollbar("scrollTo", "bottom", { scrollInertia: 1 });
}
function sendUserTypingStatus() {
    var toUserID = $(document).find('.hdf-current-chat-user-id').val();
    var fromUserID = $('#hdfLoggedInUserID').val();
    chat.server.sendUserTypingStatus(toUserID, fromUserID);
    
}
function refreshRecentChats() {
    $('.recent-chats').load('/User/_RecentChats', function () {

    });
}
function addChatMessageCount(currentUserId, fromUserId, fromUserName, fromUserProfilePic, toUserId, toUserName, toUserProfilePic,lastMessage,createdOn) {
    debugger;
    //var recentChatWindow = $(document).find('.recent-chats');
    var chatUser = $(document).find('li[data-userid="' + ((currentUserId != fromUserId) ? fromUserId : toUserId) + '"]');
    if (chatUser.length > 0) {
        //$(chatUser).parent().prepend(chatUser);

        var currentChatUserID = $(document).find('.hdf-current-chat-user-id').val();
        if (currentUserId != fromUserId && (currentChatUserID != fromUserId)) {
            var messageCountItem = $(chatUser).find('.msg-notifc');
            var count = messageCountItem.text();
            if (count.match(/^\d+$/)) {
                $(messageCountItem).text(parseInt(count) + 1);
            }
            else {
                $(messageCountItem).removeClass('display-none');
                $(messageCountItem).text(1);
            }
            $(chatUser).find('.usr-mg-info p').text(lastMessage);
            $(chatUser).find('.posted_time ').text(createdOn);
        }
    }
    //else {
    //    var html = '';
    //    if (currentUserId != fromUserId) {
    //        var uName = fromUserName.split(' ');
    //        html = '<a href="javascript:;" data-user-id="' + fromUserId + '" class="list-group-item chat-user"><img src="' + fromUserProfilePic + '" class="profilePictureCircle online-user-profile-pic" />&nbsp;&nbsp;&nbsp;' + uName[0] + '<span class="custom-badge chat-message-count" data-user-id="' + fromUserId + '">1</span></a>';
    //    }
    //    else {
    //        var uName = toUserName.split(' ');
    //        html = '<a href="javascript:;" data-user-id="' + toUserId + '" class="list-group-item chat-user"><img src="' + toUserProfilePic + '" class="profilePictureCircle online-user-profile-pic" />&nbsp;&nbsp;&nbsp;' + uName[0] + '<span class="custom-badge chat-message-count hide" data-user-id="' + toUserId + '"></span></a>';
    //    }
    //    if ($('.no-recent-chats').length > 0) {
    //        $('.no-recent-chats').remove();
    //    }
    //    $(recentChatWindow).prepend(html);
    //}
}
function UpdateChatMessageStatus(messageID, fromUserID) {
    var currentUserID = $('#hdfLoggedInUserID').val();
    chat.server.updateMessageStatus(messageID, currentUserID, fromUserID);
}
function GetOldMessages() {
    var isOldMessageExsit = $(document).find('.hdf-old-messages-exist');
    if ($(isOldMessageExsit).val() == "True") {
        var currentChatUserID = $(document).find('.hdf-current-chat-user-id').val();
        var lastMessageID = $(document).find('.hdf-last-chat-message-id').val();
        console.log($(document).find("div.right-chat-panel").scrollTop())
        $.get('/Chat/GetRecentMessages?Id=' + currentChatUserID + '&lastChatMessageId=' + lastMessageID, function (messages) {
            if (messages.ChatMessages.length > 0) {
                $(isOldMessageExsit).val((messages.ChatMessages.length < 20 ? "False" : "True"));
                $(document).find('.hdf-last-chat-message-id').val(messages.LastChatMessageId);
                var html = '';
                var currentUserId = $('#hdfLoggedInUserID').val();
                var fromUserName = $('#hdfLoggedInUserName').val();
                var fromUserPrifilePic = $('#hdfLoggedInUserProfilePicture').val();
                var chatMessage = $(document).find('.txt-chat-message').val();
                var toUserID = $(document).find('.hdf-current-chat-user-id').val();
                var toUserName = $(document).find('.hdf-current-chat-user-name').val();
                var toUserProfilePic = $(document).find('.hdf-current-chat-user-profile-picture').val();
                $(messages.ChatMessages).each(function (index, item) {
                    if (item.FromUserID == currentUserId) {
                        html += createNewMessageBlockHtml(fromUserName, fromUserPrifilePic, item.CreatedOn, item.Message, "right", item.ChatMessageID, item.Status);
                    }
                    else {
                        html += createNewMessageBlockHtml(toUserName, toUserProfilePic, item.CreatedOn, item.Message, "left", item.ChatMessageID, item.Status);
                    }
                });
                var firstMsg = $('.chat li:first');
                $(document).find('.chat').prepend(html);
                $(document).find(".messages-line").mCustomScrollbar("scrollTo", "bottom", { scrollInertia: 1 });
            }
            else {
                $(isOldMessageExsit).val("False");
            }
        });
    }
}
