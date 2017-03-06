package com.plyaka.eza.mychat;

import android.util.Log;

import com.google.gson.Gson;


public class Reader {

    Request request;
    Writer writer;
    MainActivity main;

    public void getMessage(String tmp) {
        request = new Gson().fromJson(tmp, Request.class);
        switch (request.Module) {
            case "Auth":        Authorization(request); break;
            case "Lobby":       Lobby(request);         break;
            case "HandShake":   HandShake(request);     break;
            case "Game":        Game(request);          break;
            default:    break;
        }
    }

    public void Game(Request response) {
        String[] arg;
        switch (response.Cmd) {
            case "Role":
                String str = response.Args.toString();
                writer.statusPlay(str);
                break;
            case "Over":
//                arg = new Gson().fromJson(response.Args.toString(), String[].class);
                main.ShowLobby();
                //               showAlert(arg[0].toString());
                break;
            case "Start":

                writer.start(response);
                main.ShowGame();
                break;
            case "Move":
                arg = new Gson().fromJson(response.Args.toString(), String[].class);
                main.moveBtn(arg);
                break;
            default:
                break;
        }

    }

    public void HandShake(Request response) {
        switch (response.Cmd) {
            case "Wait":
                break;
            case "Invited":
                String[] arg = new Gson().fromJson(response.Args.toString(), String[].class);
                try {
                    main.showConfirm(arg);
                } catch (Exception e) {
                    Log.i("ShowConfirm go game", e.toString());
                }
                break;
            default:
                break;


        }
    }

    public void Lobby(Request response) {
        switch (response.Cmd) {
            case "refreshClients":
                if (response.Args != null) {
                    String[] personlist = new Gson().fromJson(response.Args.toString(), String[].class);
                    String[] newpersonlist = new String[personlist.length];
                    for (int i = 0; i < newpersonlist.length; i++) {
                        newpersonlist[i] = personlist[i];
                    }
                    main.setEtClients(newpersonlist);
                }
                break;
            case "Notification":
                main.showAlert(response.Args.toString());
                break;
        }
    }

    public void Authorization(Request response) {
        switch (response.Cmd) {
            case "LogIn": {
                if (response.Args != null) {
                    writer.playerName = response.Args.toString();
                    main.setTvName("Your name is: " + writer.playerName);
                    //   btnLogout.setVisibility(View.VISIBLE);
                    //    btnInvite.setEnabled(true);
                    //    btnRefresh.setEnabled(true);


                }
            }
        }

    }
}
