package com.plyaka.eza.mychat;

import com.google.gson.Gson;

import static com.plyaka.eza.mychat.R.id.tvStatus;

public class Writer {

    MainActivity main;
    Request content;
    public String playerMove = "";
    public String playerName = "";
    public int roomNumber;
    final Gson gson = new Gson();

    public void goPlaying(String[] play) {
        content = new Request("HandShake", "Ok", play);
        main.SendPush(gson.toJson(content));
    }

    public void start(Request request) {
        String[] arg = new Gson().fromJson(request.Args.toString(), String[].class);

        double total = Double.parseDouble(arg[0]);
        int x = (int) total;
        roomNumber = x;
        String t = Integer.toString(x);
        content = new Request("Game", "Start", new Object[]{t, "XO"});

        main.SendPush(gson.toJson(content));
    }

    public void move(int number) {
        content = new Request("Game", "Move", new Object[]{roomNumber, playerMove, number, playerName});
        main.SendPush(gson.toJson(content));
    }


    public void statusPlay(String args) {
        if (args.equals(playerName)) {
            playerMove = "X";
            main.tvStatus.setText("play: " + playerMove + " go");
        } else {
            playerMove = "O";
            main.tvStatus.setText("play: " + playerMove + " wait");

        }

    }

}
