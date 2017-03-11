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

    wrapper = document.getElementById(wrapperId);
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
    refreshIcon.setAttribute('onclick', 'Messages.RequestHistory("' + roomName + '", "'+ (wrapperId == 'privateTab') + '")');
    
    var msgs = (document).createElement('ul');
    msgs.setAttribute('class', 'msgList');
    tabpage.appendChild(refreshIcon);
    tabpage.appendChild(msgs);

    tabPageContainer.appendChild(tabpage);
    Rooms.userEntered(roomName, '--Me--');
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
    Rooms.addRoomToMenu(new roomObj(roomName, []));

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

Rooms.addRoomToMenu = function(room) {
    var menu = (document).querySelector('#menu > div');

    var roomName = room.Name;

    var menuItem = (document).createElement('a');
    menuItem.setAttribute('href', '#');
    menuItem.setAttribute('class', "list-group-item");
    menuItem.setAttribute('data-toggle', "collapse");
    menuItem.setAttribute('data-target', "#"+roomName);
    menuItem.setAttribute('data-parent', "#menu");
    menuItem.setAttribute('ondblclick', 'UI.addRoomToTabList("' + roomName + '", "roomPanel")');
    menuItem.innerHTML = roomName;

    var sublinkCollapse = (document).createElement('div');
    sublinkCollapse.setAttribute('id', roomName);
    sublinkCollapse.setAttribute('class', "sublinks collapse");

    for (var i = 0; i < room.clients.length; i++) {

        var username = room.clients[i];
        var user = (document).createElement('a');
        user.setAttribute('class', 'list-group-item small');
        user.setAttribute('username', username);
        user.setAttribute('ondblclick', 'UI.addRoomToTabList("' + username + '", "privatePanel")');
        user.innerHTML = username;

        sublinkCollapse.appendChild(user);
    }

    menu.appendChild(menuItem);
    menu.appendChild(sublinkCollapse);
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
    user.setAttribute('ondblclick', 'UI.addRoomToTabList("' + username + '", "privatePanel")');
    user.innerHTML = username;

    sublinkCollapse.appendChild(user);
}

Rooms.userLeft = function(roomName, username) {
    var menu = (document).querySelector('#menu > div');
    var sublinkCollapse = $('#' + roomName, menu)[0];

    var user = $('a[username="' + username + '"]', sublinkCollapse);
    user.remove();
}

Messages.onRoomMessageReceived = function(roomName, message) {
   
    var list = Messages.getMessageList('roomPanel', roomName);
    Messages.incrementNotification(roomName);
    var item = (document).createElement('li');

    var msg = new ChatMessage(message);
    item.innerHTML = msg.toString();

    list.appendChild(item);
}

Messages.onPrivateMessageReceived = function(message) {
    
    var msg = new ChatMessage(message);

    UI.addRoomToTabList(msg.Sender, 'privatePanel');
    Messages.incrementNotification(msg.Sender, 'true');

    var list = Messages.getMessageList('privatePanel', msg.Sender);
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

    Messages.onPrivateMessageReceived(to, msg);
    RequestManager.SendPrivateMessage(to, message);
}

Messages.RequestHistory = function (roomName, isPrivate) {
    
    isPrivate = isPrivate != undefined ? isPrivate : false;
    var wrapperId = isPrivate ? 'roomPanel' : 'privatePanel';

    var lastMsg = Messages.getMessageList(wrapperId, roomName).children[0];
    datetime = lastMsg != undefined ? datetime : new Date();

    if (isPrivate == 'false') {
        RequestManager.RequestPrivateMessageList(roomName, datetime);
    }
    else {
        RequestManager.RequestMessageList(roomName, datetime);
    }
}

Messages.OnHistoryReceived = function (roomName, messageList, isPrivate) {
    var wrapperId = isPrivate == 'undefined' || !isPrivate ? 'roomPanel' : 'privatePanel';

    var list = Messages.getMessageList(wrapperId, roomName);

    for (var i = messageList.length - 1; i >= 0; i--) {
        var msg = document.createElement('li');
        msg.innerHTML = messageList[i].toString();
        $(list).prepend(msg);
    }
}
