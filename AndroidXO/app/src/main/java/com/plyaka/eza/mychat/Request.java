package com.plyaka.eza.mychat;


public class Request {

    String Module;
    String Cmd;
    Object Args;

    public Request(String Module, String Cmd, Object Args) {
        this.Module = Module;
        this.Cmd = Cmd;
        this.Args = Args;
    }
}
