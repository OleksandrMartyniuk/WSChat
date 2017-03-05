var roomNumber;
var userName;
var gamestorage;

function Game(response)
{
    switch (response.Cmd) {
        case "Start":
            roomNumber = response.Args;
            start(roomNumber);
            ShowGame();
            break;
        case "Over": alert(response.Args); ShowLobby();  break;
        case "Move": moveBtn(response.Args); break;
        case "Role": statusPlay(response.Args); break; 
    }
}

function statusPlay(args) {
    if (args === sessionStorage["username"]) {
        gamestorage = "X";
        course.innerHTML = "play: "+gamestorage+" go";
    }
    else {
        gamestorage = "O";
        course.innerHTML = "play: " + gamestorage+" wait";
    }
}

function AddToplayersList(names) {
    playersList.innerHTML = "";

    for (var i = 0; i < names.length; i++) {
        if (names[i] === userName.value) {
            continue;
        }
        playersList.innerHTML += "<input type='radio' name='players' id='" + names[i] + "' />" + names[i] + "<br />";
    }
}

function GetSelectedPlayer() {
    
    for (var i = 0; i < playersList.childNodes.length; i++) {
        if (playersList.childNodes[i].checked) {
            return playersList.childNodes[i + 1].nodeValue;
        }
    }
}

function OnButtonClicked(coord) {
    move(new Array(roomNumber[0],gamestorage, coord, sessionStorage['username']));
}

function moveBtn(args) {
    if (gamestorage === args[0]) {
        course.innerHTML = "play: " + gamestorage + " wait";
    }
    else {
        course.innerHTML = "play: " + gamestorage + " go";
    }
    
    if (args[1] == 0) {
        b0.value = args[0];
    }
    else if (args[1] == 1) {
        b1.value = args[0];
    }
    else if (args[1] == 2) {
        b2.value = args[0];
    }
    else if (args[1] == 3) {
        b3.value = args[0];
    }
    else if (args[1] == 4) {
        b4.value = args[0];
    }
    else if (args[1] == 5) {
        b5.value = args[0];
    }
    else if (args[1] == 6) {
        b6.value = args[0];
    }
    else if (args[1] == 7) {
        b7.value = args[0];
    }
    else if (args[1] == 8) {
        b8.value = args[0];
    }
}