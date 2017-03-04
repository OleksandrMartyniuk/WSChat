function openRoom(evt, roomName, wrapperId) {
    var i, tabcontents, tablinkss;
    wrapper = document.getElementById(wrapperId);

    
    tabcontents = wrapper.getElementsByClassName("tabcontent");

    for (i = 0; i < tabcontents.length; i++) {
            tabcontents[i].style.display = "none";
    }

    var el = $("div[name='" + roomName + "']", wrapper)[0];
    el.style.display = "block";

    tablinkss = wrapper.getElementsByClassName("tablinks");
    for (i = 0; i < tablinkss.length; i++) {
        tablinkss[i].className = tablinkss[i].className.replace(" active", "");
    }

    evt.currentTarget.className += " active";
}

function addRoomToTabList(roomName, wrapperId) {

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
    tablink.setAttribute('onclick', "openRoom(event, '" + roomName + "', '" + wrapperId +"')");
    tablink.innerHTML = roomName;

    var closeIcon = document.createElement('span');
    closeIcon.setAttribute('class', 'glyphicon glyphicon-remove');
    closeIcon.setAttribute('onclick', "closeRoom('" + roomName + "', '" + wrapperId + "')");

    tablink.appendChild(closeIcon);
    tablinks.appendChild(tablink);

    tabpage = (document).createElement("div");
    tabpage.setAttribute('name', roomName);
    tabpage.setAttribute('class', 'tabcontent');


    var refreshIcon = document.createElement('span');
    refreshIcon.setAttribute('class', 'glyphicon glyphicon-refresh');
    //refreshIcon.addEventListener('click', function () { closeRoom(roomName, wrapperId) });
    
    var msgs = (document).createElement('ul');
    tabpage.appendChild(refreshIcon);
    tabpage.appendChild(msgs);

    tabPageContainer.appendChild(tabpage);
}

function closeRoom(roomName, wrapper) {
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
}

function addRoomToMenu(room) {
    var menu = (document).querySelector('#menu > div');

    var roomName = room.Name;

    var menuItem = (document).createElement('a');
    menuItem.setAttribute('href', '#');
    menuItem.setAttribute('class', "list-group-item");
    menuItem.setAttribute('data-toggle', "collapse");
    menuItem.setAttribute('data-target', "#"+roomName);
    menuItem.setAttribute('data-parent', "#menu");
    menuItem.setAttribute('ondblclick', 'addRoomToTabList("' + roomName + '", "roomPanel")');
    menuItem.innerHTML = roomName;

    var sublinkCollapse = (document).createElement('div');
    sublinkCollapse.setAttribute('id', roomName);
    sublinkCollapse.setAttribute('class', "sublinks collapse");

    for (var i = 0; i < room.clients.length; i++) {

        var username = room.clients[i];
        var user = (document).createElement('a');
        user.setAttribute('class', 'list-group-item small');
        user.setAttribute('username', username);
        user.setAttribute('ondblclick', 'addRoomToTabList("' + username + '", "privatePanel")');
        user.innerHTML = username;

        sublinkCollapse.appendChild(user);
    }

    menu.appendChild(menuItem);
    menu.appendChild(sublinkCollapse);
}

function removeRoom(roomName) {
    var menu = (document).querySelector('#menu > div');
    var link = $('a[data-target = "#' + roomName + '"]', menu);
    link.remove();
    var body = $('#' + roomName, menu)[0];
    body.remove();
}

function userEntered(roomName, username) {
    var menu = (document).querySelector('#menu > div');
    var sublinkCollapse = $('#' + roomName, menu)[0];

    var user = (document).createElement('a');
    user.setAttribute('class', 'list-group-item small');
    user.setAttribute('username', username);
    user.setAttribute('ondblclick', 'addRoomToTabList("' + username + '", "privatePanel")');
    user.innerHTML = username;

    sublinkCollapse.appendChild(user);
}

function userLeft(roomName, username) {
    var menu = (document).querySelector('#menu > div');
    var sublinkCollapse = $('#' + roomName, menu)[0];

    var user = $('a[username="' + username + '"]', sublinkCollapse);
    user.remove();
}

function onRoomMessageReceived(roomName, message) {
    wrapper = document.getElementById('roomPanel');
    var el = $("div[name='" + roomName + "']", wrapper)[0];
}

function onPrivateMessageReceived(From, message) {
    wrapper = document.getElementById('privatePanel');
    var el = $("div[name='" + roomName + "']", wrapper)[0];
}

function addRoomMessage(room, message) {

}

function addPrivateMessage(room, message) {

}


//<span class="label label-info">5</span>