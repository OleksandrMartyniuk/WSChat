package com.plyaka.eza.mychat;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

public class RoomActivity extends AppCompatActivity implements View.OnClickListener {

    Button btnSend;
    EditText etMessage, etChat;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_room);

        btnSend = (Button) findViewById(R.id.btnSend);
        etMessage = (EditText) findViewById(R.id.etMessage);
        etChat = (EditText) findViewById(R.id.etChat);

        btnSend.setOnClickListener(this);

    }

    @Override
    public void onClick(View v) {
        switch (v.getId())
        {
            case R.id.btnSend:
                etChat.setText(etMessage.getText());

                break;

        }
    }
}
