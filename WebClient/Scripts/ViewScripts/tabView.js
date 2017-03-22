function Rooms(){

}

function Messages() {

}

function UI() {

}


UI.openRoom = function(evt, roomName, wrapperId) {
    var i, tabcontents, tablinkss;
    wrapper = document.getElementById(wrapperId);

    tabcontents = wrapper.getElementsByClassName("tabcontent");

    for (i = 0; i < tabcontents.length; i++) {
            tabcontents[i].style.display = "none";
    }
   
    var el = $("div[name='" + roomName + "']", wrapper)[0];
    el.style.display = "block";
    Messages.removeNotification(roomName, wrapperId == 'privatePanel');

    tablinkss = wrapper.getElementsByClassName("tablinks");
    for (i = 0; i < tablinkss.length; i++) {
        tablinkss[i].className = tablinkss[i].className.replace(" active", "");
    }

    evt.currentTarget.className += " active";
}

UI.addRoomToTabList = function(roomName, wrapperId) {

    var wrapper = document.getElementById(wrapperId);
    var isPrivate = wrapperId == 'privatePanel';
    var tabPageContainer = wrapper.getElementsByClassName("tabPage")[0];
    var tablinks = wrapper.getElementsByClassName("tab")[0];

    for (var i = 0; i < tablinks.children.length; i++) {
        if (tablinks.children[i].name == roomName) {
            return;
        }
    }

    var tablink = (document).createElement("a");
    
    tablink.setAttribute('href', "javascript:void(0)");
    tablink.setAttribute('class', "tablinks");
    tablink.setAttribute('name', roomName);
    tablink.setAttribute('onclick', "UI.openRoom(event, '" + roomName + "', '" + wrapperId +"')");
    tablink.innerHTML = roomName;

    var closeIcon = document.createElement('span');
    closeIcon.setAttribute('class', 'glyphicon glyphicon-remove');
    closeIcon.setAttribute('onclick', "UI.closeRoom('" + roomName + "', '" + wrapperId + "')");

    tablink.appendChild(closeIcon);
    tablinks.appendChild(tablink);

    tabpage = (document).createElement("div");
    tabpage.setAttribute('name', roomName);
    tabpage.setAttribute('class', 'tabcontent');


    var refreshIcon = document.createElement('span');
    refreshIcon.setAttribute('class', 'glyphicon glyphicon-refresh');
    refreshIcon.addEventListener('click', function () {
        var timestamp = $(refreshIcon).next().children().first().attr('timestamp');
        Messages.RequestHistory(roomName, isPrivate);
    });
    
    var msgs = (document).createElement('ul');
    msgs.setAttribute('class', 'msgList');
    tabpage.appendChild(refreshIcon);
    tabpage.appendChild(msgs);

    tabPageContainer.appendChild(tabpage);

    if (!isPrivate) {
        RequestManager.SetActiveRoom(roomName);
        Rooms.userEntered(roomName, '--Me--');
    }
}

UI.Ban = function () {
    $('#inputMessage').prop("disabled", true);
    $('#btn_sendMessage').prop("disabled", true);
    $('#creteRoomName').prop("disabled", true);
    $('#btnCreateRoom').prop("disabled", true);
}

UI.UnBan = function () {
    $('#inputMessage').prop("disabled", false);
    $('#btn_sendMessage').prop("disabled", false);
    $('#creteRoomName').prop("disabled", false);
    $('#btnCreateRoom').prop("disabled", false);
}

UI.SendMessage = function () {
    var msg = $('#inputMessage').val();
    if (msg.length == 0) {
        return;
    }
    Messages.sendRoomMessage(msg);

    $('#inputMessage').val('');
}

UI.SendPrivate = function () {
    var msg = $('#privateInput').val();
    if (msg.length == 0) {
        return;
    }
    Messages.sendPrivateMessage(msg);

    $('#privateInput').val('');
}

UI.CreateRoom = function () {
    var roomName = $('#creteRoomName').val();
    if (roomName.length < 4) {
        return;
    }
    RequestManager.CreateRoom(roomName);
    Rooms.addRoomToMenu(new roomObj(roomName, sessionStorage['username'], []));

    $('#creteRoomName').val('');
}

UI.closeRoom = function(roomName, wrapper) {
    var i, tabcontents, tablinkss;
    wrapper = document.getElementById(wrapper);

    var el = $("div[name='" + roomName + "']", wrapper)[0];
    el.remove();

    tabPanel = wrapper.getElementsByClassName("tab")[0];
    var links = tabPanel.getElementsByTagName('a');

    for (i = 0; i < links.length; i++) {
        if (links[i].name == roomName) {
            links[i].onclick = null;
            links[i].remove();
        }
    }
    Rooms.userLeft(roomName, '--Me--');
}

UI.scrollDown = function (element) {
    element.scrollTop = element.scrolHeight;
}

UI.signalPrivate = function () {
    if ($('#collapse1').hasClass('in')) {
        return;
    }

    $("#private_panel_header").addClass('signal');
}

Rooms.addRoomToMenu = function (room) {
    var menu = (document).querySelector('#menu > div');

    var roomName = room.Name;

    var menuItem = (document).createElement('a');
    menuItem.setAttribute('href', '#');
    menuItem.setAttribute('class', "list-group-item");
    menuItem.setAttribute('creator', room.creator);
    menuItem.addEventListener('dblclick', function () { UI.addRoomToTabList(roomName, 'roomPanel') });
    menuItem.innerHTML = roomName;

    var controlWrapper = (document).createElement('span');
    controlWrapper.setAttribute('class', 'menu_controls');

    var userDropDown = (document).createElement('span');
    userDropDown.setAttribute('class', "caret menuGliphs");
    userDropDown.setAttribute('data-toggle', "collapse");
    userDropDown.setAttribute('data-target', "#"+roomName);
    userDropDown.setAttribute('data-parent', "#menu");

    var actionsDropDown = (document).createElement('span');
    actionsDropDown.setAttribute('class', "glyphicon glyphicon-option-vertical menuGliphs");
    actionsDropDown.setAttribute('data-toggle', "collapse");
    actionsDropDown.setAttribute('data-target', "#"+roomName + '_action');
    actionsDropDown.setAttribute('data-parent', "#menu");

    controlWrapper.appendChild(userDropDown);
    controlWrapper.appendChild(actionsDropDown);

    menuItem.appendChild(controlWrapper);

    menu.appendChild(menuItem);
    //
    //Creating users menu
    //

    var userlist = (document).createElement('div');
    userlist.setAttribute('id', roomName);
    userlist.setAttribute('class', "sublinks collapse");

    for (var i = 0; i < room.clients.length; i++) {
        var username = room.clients[i];
        var user = (document).createElement('a');
        user.setAttribute('class', 'list-group-item small');
        user.setAttribute('username', username);
        user.addEventListener('dblclick', function () { UI.addRoomToTabList(username, 'privatePanel') });
        user.innerHTML = username;

        userlist.appendChild(user);
    }

    //
    //Creating action menu
    //

    var actionList = (document).createElement('div');
    actionList.setAttribute('id', roomName + '_action');
    actionList.setAttribute('class', "sublinks collapse");
    actionList.addEventListener('click', function () { $(actionsDropDown).click() });
    
    var enterRoomAction = (document).createElement('a');
    enterRoomAction.setAttribute('class', 'list-group-item small');
    enterRoomAction.innerHTML = "Enter room";
    enterRoomAction.addEventListener('click', function () { UI.addRoomToTabList(roomName, 'roomPanel') });

    actionList.appendChild(enterRoomAction);

    var isAdmin = sessionStorage['role'] == 'admin';
    if ( isAdmin || room.creator == sessionStorage['username']) {
        var removeRoom = (document).createElement('a');
        removeRoom.setAttribute('class', 'list-group-item small');
        removeRoom.innerHTML = "Remove room";
        if (isAdmin) {
            removeRoom.addEventListener('click', function () { RequestManager.AdminCloseRoom(roomName) });
        }
        else {
            removeRoom.addEventListener('click', function () { RequestManager.CloseRoom(roomName) });
        }
        
        actionList.appendChild(removeRoom);
    }

    menu.appendChild(userlist);
    menu.appendChild(actionList);
}

Rooms.removeRoom = function(roomName) {
    var menu = (document).querySelector('#menu > div');
    var link = $('a[data-target = "#' + roomName + '"]', menu);
    link.remove();
    var body = $('#' + roomName, menu)[0];
    body.remove();
}

Rooms.userEntered = function(roomName, username) {
    var menu = (document).querySelector('#menu > div');
    var sublinkCollapse = $('#' + roomName, menu)[0];

    var user = (document).createElement('a');
    user.setAttribute('class', 'list-group-item small');
    user.setAttribute('username', username);
    user.setAttribute('dblclick', function () { UI.addRoomToTabList(username, 'privatePanel') });
    user.innerHTML = username;

    sublinkCollapse.appendChild(user);
}

Rooms.userLeft = function(roomName, username) {
    var menu = (document).querySelector('#menu > div');
    var sublinkCollapse = $('#' + roomName, menu)[0];

    var user = $('a[username="' + username + '"]', sublinkCollapse);
    user.remove();
}

Rooms.OnDataReceived = function(roomList){
    for(var i=0; i<roomList.length; i++){
        Rooms.addRoomToMenu(roomList[i]);
    }
}

Messages.onRoomMessageReceived = function(roomName, message) {
   
    var list = Messages.getMessageList('roomPanel', roomName);
    Messages.incrementNotification(roomName);
    var item = (document).createElement('li');

    var msg = new ChatMessage(message);
    item.innerHTML = msg.toString();
    item.setAttribute('timestamp', msg.TimeStamp);

    list.appendChild(item);
    list.parentElement.scrollTop = list.parentElement.scrollHeight;
}

Messages.onPrivateMessageReceived = function(message) {
    
    var msg = new ChatMessage(message);

    UI.addRoomToTabList(msg.Sender, 'privatePanel');
    Messages.incrementNotification(msg.Sender, 'true');

    var list = Messages.getMessageList('privatePanel', msg.Sender);
    var item = (document).createElement('li');
    
    item.innerHTML = msg.toString();
    item.setAttribute('timestamp', msg.TimeStamp);

    list.appendChild(item);

    UI.signalPrivate();
}

Messages.appendPrivateMessage = function (to, message) {

    var msg = new ChatMessage(message);

    UI.addRoomToTabList(to, 'privatePanel');
    Messages.incrementNotification(msg.Sender, 'true');

    var list = Messages.getMessageList('privatePanel', to);
    var item = (document).createElement('li');

    item.innerHTML = msg.toString();

    list.appendChild(item);
}

Messages.incrementNotification = function(roomName, isPrivate){
    var wrapperId = 'roomPanel';
    if (isPrivate) {
        wrapperId = 'privatePanel';
    }
    var link = $('#' + wrapperId + ' > div.tab > a[name="' + roomName + '"]')[0];
    if (!$(link).hasClass('active')) {
        var notificator = $('.label-info', link)[0];
        if (notificator != undefined) {
            notificator.innerHTML = parseInt(notificator.innerHTML) + 1;
        }
        else {
            notificator = (document).createElement('span');
            notificator.setAttribute('class', 'label label-info');
            notificator.innerHTML = '1';
            $(notificator).insertBefore($('span', link)[0]);
        }
    }
}

Messages.removeNotification = function(roomName, isPrivate){
    var wrapperId = 'roomPanel';
    if (isPrivate) {
        wrapperId = 'privatePanel';
    }
    var link = $('#' + wrapperId + ' > div.tab > a[name="' + roomName + '"]')[0];
    var notificator = $('.label-info', link)[0];
    if (notificator != undefined) {
        notificator.remove();
    }
}

Messages.getMessageList = function(wrapperId, roomName) {
    wrapper = document.getElementById(wrapperId);
    var el = $("div[name='" + roomName + "']", wrapper)[0];
    return $("ul", el)[0];
}

Messages.sendRoomMessage = function(message) {
    var panel = Messages.getActivePanel('roomPanel');
    var room = panel.getAttribute('name');

    var msg = new ChatMessage();
    msg.Sender = 'Me';
    msg.Text = message;
    msg.TimeStamp = new Date();

    Messages.onRoomMessageReceived(room, msg);
    RequestManager.SendMessage(message, room);
}

Messages.getActivePanel = function(wrapper){
    wrapper = document.getElementById(wrapper);
    var panel = $("div.tabcontent").filter(function(){
        var $this = $(this);
        return $this.css("display") == "block";
    })[0];

    return panel;
}

Messages.sendPrivateMessage = function(message) {
    var panel = Messages.getActivePanel('privatePanel');
    var to = panel.getAttribute('name');

    var msg = new ChatMessage();
    msg.Sender = 'Me';
    msg.Text = message;
    msg.TimeStamp = new Date();

    Messages.appendPrivateMessage(to, msg);
    RequestManager.SendPrivateMessage(to, message);
}

Messages.RequestHistory = function (roomName, isPrivate) {
    
    isPrivate = isPrivate != undefined ? isPrivate : false;
    var wrapperId = isPrivate ? 'privatePanel' : 'roomPanel';

    var lastMsg = Messages.getMessageList(wrapperId, roomName).children[0];
    var datetime = lastMsg != undefined ? lastMsg.getAttribute('timestamp') : new Date();

    if (isPrivate) {
        RequestManager.RequestPrivateMessageList(roomName, datetime);
    }
    else {
        RequestManager.RequestMessageList(roomName, datetime);
    }
}

Messages.OnHistoryReceived = function (roomName, messageList, isPrivate) {
    var wrapperId = isPrivate == 'undefined' || !isPrivate ? 'roomPanel' : 'privatePanel';

    var list = Messages.getMessageList(wrapperId, roomName);

    if (messageList.length < 10) {
        $(list).prev().hide();
    }

    for (var i = messageList.length - 1; i >= 0; i--) {
        var msg = document.createElement('li');
        msg.innerHTML = messageList[i].toString();
        msg.setAttribute('timestamp', (messageList[i]).TimeStamp);
        $(list).prepend(msg);
    }
}
