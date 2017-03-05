function HandShake(response) {
    switch (response.Cmd) {
        case "Invited":
            var r = confirm("User " + response.Args[0] + " wants to play with you");
            if (r == true)  goPlaying(response.Args);
            break;
        case "Wait": alert("Wait"); break;
    }
}
