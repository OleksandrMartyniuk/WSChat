function Lobby(response)
{
    switch(response.Cmd)
    {
        case "refreshClients":
            RefreshList(response);
            break;
        case "Notification":
            alert(response.Args);
            break;
    }
}
function RefreshList(response){
    if (response.Args.length > 0)
    {
        playersList.innerHTML = "";
        var personlist = new Array(response.Args);
        for (var i = 0; i < personlist[0].length; i++) {
            playersList.innerHTML += "<input type='radio' name='players' id='" + personlist[0][i] + "' />" + personlist[0][i] + "<br />";
        }
    }
}