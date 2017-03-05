function ShowAuth() {
    document.getElementById("auth").style.display = 'flex';
    document.getElementById("lobby").style.display = 'none';
    document.getElementById("game").style.display = 'none';
    document.getElementById("playersList").style.display = 'none';
}
function ShowLobby() {
    document.getElementById("auth").style.display = 'none';
    document.getElementById("lobby").style.display = 'flex';
    document.getElementById("game").style.display = 'none';
    document.getElementById("playersList").style.display = 'flex';
    
    document.getElementById("label").innerHTML ="Your name: "+ sessionStorage['username'];
    $(".gameField").val(function (index, x) { return " "; });
}
function ShowGame() {
    document.getElementById("auth").style.display = 'none';
    document.getElementById("lobby").style.display = 'none';
    document.getElementById("game").style.display = 'flex';
    document.getElementById("playersList").style.display = 'none';
    document.getElementById("namelab").innerHTML = "Your name: " + sessionStorage['username'];
}

