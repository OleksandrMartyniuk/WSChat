package com.plyaka.eza.mychat;

import android.accounts.AccountManager;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.content.pm.Signature;
import android.net.Uri;
import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Base64;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.facebook.Session;
import com.facebook.SessionState;
import com.facebook.UiLifecycleHelper;
import com.facebook.model.GraphUser;
import com.facebook.widget.LoginButton;
import com.google.android.gms.appindexing.Action;
import com.google.android.gms.appindexing.AppIndex;
import com.google.android.gms.appindexing.Thing;
import com.google.android.gms.auth.GoogleAuthException;
import com.google.android.gms.auth.GoogleAuthUtil;
import com.google.android.gms.auth.UserRecoverableAuthException;
import com.google.android.gms.common.AccountPicker;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.gson.Gson;

import org.java_websocket.client.WebSocketClient;
import org.java_websocket.handshake.ServerHandshake;

import java.io.IOException;
import java.net.URI;
import java.net.URISyntaxException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

public class MainActivity extends AppCompatActivity implements View.OnClickListener {

    Button btnLogin, btnReg, btnInvite, btnRefresh, btnForget, btnLogout;
    EditText etPassword, etLogin, etEmail;
    ListView etClients;
    TextView tvName, tvStatus;

    private WebSocketClient mWebSocketClient;
    final Gson gson = new Gson();

    Request content;
    Writer writer;
    Reader reader;

    String[] names;
    Button[] gamebuttons = new Button[9];
    private static final String TAG = "MyActivity";

    private UiLifecycleHelper uiHelper;
    private LoginButton enterByFB;

    private GoogleApiClient client;

    public void setTvName(String tvName) {
        this.tvName.setText(tvName);
    }



    public void setEtClients(String[] names) {
        ArrayAdapter<String> adapter = new ArrayAdapter<String>(this,
                android.R.layout.simple_list_item_single_choice, names);
        etClients.setAdapter(adapter);

        this.names = names;
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        uiHelper = new UiLifecycleHelper(this, statusCallback);
        uiHelper.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        connectWebSocket();
        btnLogin = (Button) findViewById(R.id.btnLogin);
        btnLogout = (Button) findViewById(R.id.btnLogout);
        btnReg = (Button) findViewById(R.id.btnReg);
        btnInvite = (Button) findViewById(R.id.btnInvite);
        btnRefresh = (Button) findViewById(R.id.btnRefresh);
        btnForget = (Button) findViewById(R.id.btnForget);


        gamebuttons[0] = (Button) findViewById(R.id.bt1);
        gamebuttons[1] = (Button) findViewById(R.id.bt2);
        gamebuttons[2] = (Button) findViewById(R.id.bt3);
        gamebuttons[3] = (Button) findViewById(R.id.bt4);
        gamebuttons[4] = (Button) findViewById(R.id.bt5);
        gamebuttons[5] = (Button) findViewById(R.id.bt6);
        gamebuttons[6] = (Button) findViewById(R.id.bt7);
        gamebuttons[7] = (Button) findViewById(R.id.bt8);
        gamebuttons[8] = (Button) findViewById(R.id.bt9);

        for (Button b : gamebuttons) {
            b.setOnClickListener(this);
        }

        etPassword = (EditText) findViewById(R.id.etPassword);
        etLogin = (EditText) findViewById(R.id.etLogin);
        etEmail = (EditText) findViewById(R.id.etEmail);
        etClients = (ListView) findViewById(R.id.etClients);
        etClients.setChoiceMode(ListView.CHOICE_MODE_SINGLE);

        tvName = (TextView) findViewById(R.id.tvName);
        tvStatus = (TextView) findViewById(R.id.tvStatus);

        enterByFB = (LoginButton) findViewById(R.id.fb_login_button);

        enterByFB.setUserInfoChangedCallback(new LoginButton.UserInfoChangedCallback() {
            @Override
            public void onUserInfoFetched(GraphUser user) {
                if (user != null) {
                    tvName.setText(user.getName());
                }
            }
        });

        View btn = (View) findViewById(R.id.sign_in_button);
        btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                Intent intent = AccountPicker.newChooseAccountIntent(null, null, new String[]{"com.google"},
                        false, null, null, null, null);
                startActivityForResult(intent, 123);

            }
        });


        btnLogin.setOnClickListener(this);
        btnReg.setOnClickListener(this);
        btnInvite.setOnClickListener(this);
        btnRefresh.setOnClickListener(this);

        btnInvite.setEnabled(true);
        btnRefresh.setEnabled(true);

        client = new GoogleApiClient.Builder(this).addApi(AppIndex.API).build();
    }

    public boolean inspection(String login, String password) {

        if (login.contains(" ") || password.contains(" ")) {
            showAlert("take away spaces!");
            return false;
        }
        if (login == "" && password == "") {
            showAlert("Fill all fields!");
            return false;
        }
        return true;
    }

    public boolean inspection(String login, String password, String email) {

        if (login.contains(" ") || password.contains(" ") || email.contains(" ")) {
            showAlert("take away spaces!");
            return false;
        }
        if (login == "" || password == "" || email == "") {
            showAlert("Fill all fields!");
            return false;
        }

        if (!email.contains("@")) {
            showAlert("Email should be in format : example@examp.ex");
            return false;
        }

        return true;
    }
    @Override
    public void onClick(View v) {

        String login = etLogin.getText().toString();
        String password = etPassword.getText().toString();
        String email = etEmail.getText().toString();
        switch (v.getId()) {
            case R.id.btnLogin:
                if (inspection(login, password) == true) {
                    content = new Request("Auth", "LogIn", new Object[]{login, password});
                    SendPush(gson.toJson(content));
                }
                break;
            case R.id.btnLogout:
                content = new Request("Auth", "LogIn", login);
                SendPush(gson.toJson(content));
                btnLogout.setVisibility(View.GONE);
                btnLogin.setVisibility(View.VISIBLE);

                break;
            case R.id.btnReg:
                if (inspection(login, password, email) == true) {
                    content = new Request("Auth", "Registration", new Object[]{login, password, email});
                    SendPush(gson.toJson(content));
                }
                break;
            case R.id.btnForget:
                content = new Request("Auth", "Forget", login);
                SendPush(gson.toJson(content));
                break;
            case R.id.btnInvite:
                Object player = GetSelectedPlayer();
                content = new Request("HandShake", "Invite", new Object[]{player, "XO"});
                SendPush(gson.toJson(content));
                break;
            case R.id.btnRefresh:
                content = new Request("Lobby", "refreshClients", "null");
                SendPush(gson.toJson(content));
                break;


            case R.id.bt1: writer.move(0); break;
            case R.id.bt2: writer.move(1); break;
            case R.id.bt3: writer.move(2); break;
            case R.id.bt4: writer.move(3); break;
            case R.id.bt5: writer.move(4); break;
            case R.id.bt6: writer.move(5); break;
            case R.id.bt7: writer.move(6); break;
            case R.id.bt8: writer.move(7); break;
            case R.id.bt9: writer.move(8); break;
        }
    }


    private Object GetSelectedPlayer() {
        return names[etClients.getCheckedItemPosition()];
    }

    public void SendPush(String info) {
        try {

            Log.i("SendPush", info);
            mWebSocketClient.send(info);
        } catch (Exception e) {
            Log.i("SendPush", e.toString());
        }
    }

    private void connectWebSocket() {
        URI uri;

        try {
            uri = new URI("ws://192.168.0.101:9898");
        } catch (URISyntaxException e) {
            e.printStackTrace();
            return;
        }

        mWebSocketClient = new WebSocketClient(uri) {
            @Override
            public void onOpen(ServerHandshake serverHandshake) {
                Log.i("Websocket", "Opened");
            }

            @Override
            public void onMessage(String s) {
                final String message = s;
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        reader.getMessage(message);
                        Log.i("Websocket", message);
                    }
                });
            }

            @Override
            public void onClose(int i, String s, boolean b) {
                Log.v(TAG, "Closed " + s);
                Log.i("Websocket", "Closed " + s);
            }

            @Override
            public void onError(Exception e) {
                Log.v(TAG, "Error " + e.getMessage());
                Log.i("Websocket", "Error " + e.getMessage());
            }
        };
        mWebSocketClient.connect();
    }

    public void showAlert(String message) {
        AlertDialog.Builder builder = new AlertDialog.Builder(MainActivity.this);
        builder.setTitle("Важное сообщение!")
                .setMessage(message)
                .setCancelable(false)
                .setNegativeButton("ОК",
                        new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int id) {
                                dialog.cancel();
                            }
                        });
        AlertDialog alert = builder.create();
        alert.show();
    }


    public void showConfirm(String[] arg) {
        final String[] info = arg;
        AlertDialog.Builder alertDialog = new AlertDialog.Builder(MainActivity.this);
        alertDialog.setTitle("GO Games");

        alertDialog.setMessage("Player " + arg[0] + "vs " + arg[1] + " wants to play with you!");
        alertDialog.setPositiveButton("OK", new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int whichButton) {
                writer.goPlaying(info);
                dialog.cancel();
            }
        });
        alertDialog.setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                dialog.cancel();
            }
        });
        alertDialog.show();
        // goPlaying(arg);
    }

    public void ShowGame() {
        for (Button b : gamebuttons) {
            b.setVisibility(View.VISIBLE);
        }

        Button[] lobbybuttons = new Button[]{btnInvite, btnLogin, btnRefresh, btnReg, btnForget, btnLogout};

        for (Button b : lobbybuttons) {
            b.setVisibility(View.GONE);
        }

        etClients.setVisibility(View.GONE);
        etEmail.setVisibility(View.GONE);
        etPassword.setVisibility(View.GONE);
        etLogin.setVisibility(View.GONE);
        tvStatus.setVisibility(View.VISIBLE);

    }

    public void ShowLobby() {
        for (Button b : gamebuttons) {
            b.setVisibility(View.GONE);
        }

        Button[] lobbybuttons = new Button[]{btnInvite, btnLogin, btnRefresh, btnReg, btnForget, btnLogout};

        for (Button b : lobbybuttons) {
            b.setVisibility(View.VISIBLE);
        }

        etClients.setVisibility(View.VISIBLE);
        etEmail.setVisibility(View.VISIBLE);
        etPassword.setVisibility(View.VISIBLE);
        etLogin.setVisibility(View.VISIBLE);
        tvStatus.setVisibility(View.GONE);
    }


    public void moveBtn(Object[] args) {

        int i = 0;
        for (Button b : gamebuttons) {
            if (Integer.parseInt(args[1].toString()) == i++) {
                b.setText(args[0].toString());
            }
        }
    }

    public Action getIndexApiAction() {
        Thing object = new Thing.Builder()
                .setName("Main Page") // TODO: Define a title for the content shown.
                // TODO: Make sure this auto-generated URL is correct.
                .setUrl(Uri.parse("http://[ENTER-YOUR-URL-HERE]"))
                .build();
        return new Action.Builder(Action.TYPE_VIEW)
                .setObject(object)
                .setActionStatus(Action.STATUS_TYPE_COMPLETED)
                .build();
    }

    @Override
    public void onStart() {
        super.onStart();
        client.connect();
        AppIndex.AppIndexApi.start(client, getIndexApiAction());
    }

    @Override
    public void onStop() {
        super.onStop();
        AppIndex.AppIndexApi.end(client, getIndexApiAction());
        client.disconnect();
    }



    private Session.StatusCallback statusCallback = new Session.StatusCallback() {
        @Override
        public void call(Session session, SessionState state, Exception exception) {
            if (state.isOpened()) {
                Log.d("FacebookSampleActivity", "Facebook session opened");
            } else if (state.isClosed()) {
                Log.d("FacebookSampleActivity", "Facebook session closed");
            }
        }
    };

    @Override
    public void onResume() {
        super.onResume();
        uiHelper.onResume();
    }

    @Override
    public void onPause() {
        super.onPause();
        uiHelper.onPause();
    }
    @Override
    public void onDestroy() {
        super.onDestroy();
        uiHelper.onDestroy();
    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        uiHelper.onActivityResult(requestCode, resultCode, data);
    }

    @Override
    public void onSaveInstanceState(Bundle savedState) {
        super.onSaveInstanceState(savedState);
        uiHelper.onSaveInstanceState(savedState);
    }

}
