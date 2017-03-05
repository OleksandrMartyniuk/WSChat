//package com.plyaka.eza.mychat;
//
//import android.app.AlertDialog;
//
//import com.google.gson.Gson;
//import com.plyaka.eza.mychat.MainActivity;
//
//import java.util.ArrayList;
//
//
//public class Listener {
//
//    MainActivity main /*= new MainActivity()*/;
//
//    public void getMessage(String tmp) {
//        MainActivity.Request request = new Gson().fromJson(tmp, MainActivity.Request.class);
//        switch (request.Module){
//            case "Auth": Authorization(request); break;
//            case "Lobby": Lobby(request); break;
//            case "HandShake": HandShake(request); break;
//            case "Game": Game(request); break;
//        }
//    }
//
//    public void Authorization(MainActivity.Request response)
//    {
//        if (response.Cmd == "LogIn") {
//            if (response.Args != null) {
//                main.setTvName(response.Args.toString());
//
//            }
//        }
//    }
//    public void Lobby(MainActivity.Request response)
//    {
//        switch(response.Cmd)
//        {
//            case "refreshClients":
//                if (response.Args != null)
//                {
//                    /*ArrayList<Object> personlist = new ArrayList<Object>();
//                    personlist.add(response.Args);
//                    for (Object o: personlist) {
//                        main.setEtClients(o.toString());
//                    }*/
//
//
//                    Object[] personlist = new Gson().fromJson(response.Args.toString(), Object[].class);
//                    String[] newpersonlist = new String[personlist.length];
//                    for (int i = 0; i < newpersonlist.length; i++) {
//                        newpersonlist[i] = personlist[i].toString();
//                    }
//                    main.setEtClients(newpersonlist);
//                }
//                break;
//            case "Notification":
//                main.showAlert(response.Args.toString());
//                break;
//        }
//    }
//
//
//    public void HandShake(MainActivity.Request response) {
//        switch (response.Cmd) {
//            case "Invited":
//                Object[] arg = new Gson().fromJson(response.Args.toString(), Object[].class);
//                boolean r = main.showConfirm(arg[0].toString());
//                if (r == true)
//                    main.goPlaying(arg);
//                break;
//            case "Wait":
//                main.showAlert("Wait");
//                break;
//
//        }
//    }
//
//    public void Game(MainActivity.Request response)
//    {
//        Object[] arg = new Gson().fromJson(response.Args.toString(), Object[].class);
//
//        switch (response.Cmd) {
//            case "Start":
//
//                //main.start(arg);
//                main.ShowGame();
//                break;
//
//            case "Over":
//                main.ShowLobby();
//                break;
//
//            case "Move":
//                main.moveBtn(arg);
//                break;
//        }
//
//    }
//
//
//
//
//}
