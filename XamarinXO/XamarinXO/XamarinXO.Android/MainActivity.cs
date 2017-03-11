using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net.WebSockets;
using WebSocket4Net;
using Newtonsoft.Json;



namespace XamarinXO.Droid
{
	[Activity (Label = "XamarinXO.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, View.IOnClickListener
	{
        Button btnLogin;
        Button btnReg;
        Button btnInvite;
        Button btnRefresh;
        Button btnForget;
        Button btnLogout;
        Button[] gamebuttons = new Button[9];

        EditText etPassword;
        EditText etLogin;
        EditText etEmail;

        ListView etClients;

        TextView tvName;
        TextView tvStatus;

        private WebSocket4Net.WebSocket _socket;
        private bool _connected;
        Writer writer = new Writer();

        string[] names;

        private string playerMove = "";
        private string playerName = "";
        private int roomNumber;

        public void setTvName(string tvName)
        {
            this.tvName.Text = tvName;
        }

        public void setEtClients(String[] names)
        {
            ArrayAdapter<string> adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItemSingleChoice, names);
            etClients.SetAdapter(adapter);

            this.names = names;
        }

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);			
			SetContentView (Resource.Layout.Main);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnReg = FindViewById<Button>(Resource.Id.btnReg);
            btnInvite = FindViewById<Button>(Resource.Id.btnInvite);
            btnRefresh = FindViewById<Button>(Resource.Id.btnRefresh);
            btnForget = FindViewById<Button>(Resource.Id.btnForget);
            btnLogout = FindViewById<Button>(Resource.Id.btnLogout);

            etPassword = FindViewById<EditText>(Resource.Id.etPassword);
            etLogin = FindViewById<EditText>(Resource.Id.etLogin);
            etEmail = FindViewById<EditText>(Resource.Id.etEmail);
            etClients = FindViewById<ListView>(Resource.Id.etClients);
            tvName = FindViewById<TextView>(Resource.Id.tvName);
            tvStatus = FindViewById<TextView>(Resource.Id.tvStatus);

            gamebuttons[0] = FindViewById<Button>(Resource.Id.bt1);
            gamebuttons[1] = FindViewById<Button>(Resource.Id.bt2);
            gamebuttons[2] = FindViewById<Button>(Resource.Id.bt3);
            gamebuttons[3] = FindViewById<Button>(Resource.Id.bt4);
            gamebuttons[4] = FindViewById<Button>(Resource.Id.bt5);
            gamebuttons[5] = FindViewById<Button>(Resource.Id.bt6);
            gamebuttons[6] = FindViewById<Button>(Resource.Id.bt7);
            gamebuttons[7] = FindViewById<Button>(Resource.Id.bt8);
            gamebuttons[8] = FindViewById<Button>(Resource.Id.bt9);

            foreach (Button item in gamebuttons)
            {
                item.SetOnClickListener(this);
            }


            btnLogin.SetOnClickListener(this);
            btnReg.SetOnClickListener(this);
            btnInvite.SetOnClickListener(this);
            btnRefresh.SetOnClickListener(this);
            btnForget.SetOnClickListener(this);
            btnLogout.SetOnClickListener(this);

            connection();


        }

        public void start(Request request)
        {
            string[] arg = JsonConvert.DeserializeObject<string[]>(request.Args.ToString());

            double total = double.Parse(arg[0]);
            int x = (int)total;
            roomNumber = x;
            string t = "" + x;

            _socket.Send(writer.game.SendStart(new object[] { t, "XO" }));
        }

        public bool inspection(string login, string password)
        {

            if (login.Contains(" ") || password.Contains(" "))
            {
                showAlert("take away spaces!");
                return false;
            }
            if (login == "" && password == "")
            {
                showAlert("Fill all fields!");
                return false;
            }
            return true;
        }
        public bool inspection(string login, string password, string email)
        {

            if (login.Contains(" ") || password.Contains(" ") || email.Contains(" "))
            {
                showAlert("take away spaces!");
                return false;
            }
            if (login == "" || password == "" || email == "")
            {
                showAlert("Fill all fields!");
                return false;
            }

            if (!email.Contains("@"))
            {
                showAlert("Email should be in format : example@examp.ex");
                return false;
            }

            return true;
        }

        public void statusPlay(string args)
        {
            if (args.Equals(playerName))
            {
                playerMove = "X";
                tvStatus.Text = "play: " + playerMove + " go";
            }
            else
            {
                playerMove = "O";
                tvStatus.Text = "play: " + playerMove + " wait";

            }

        }

        public void OnClick(View v)
        {
            string login = etLogin.Text;
            string password = etPassword.Text;
            string email = etEmail.Text;

            switch (v.Id)
            {
                case Resource.Id.btnLogin:
                    if (inspection(login, password) == true)
                    {
                        _socket.Send(writer.auth.SendLogIn(login, password));
                    }
                    break;

                case Resource.Id.btnLogout:
                    _socket.Send(writer.auth.SendLogOut(login));
                    btnLogout.Visibility = ViewStates.Gone;
                    btnLogin.Visibility = ViewStates.Visible;
                    break;

                case Resource.Id.btnReg:

                    if (inspection(login, password, email) == true)
                    {
                        _socket.Send(writer.auth.SendRegistaration(login, password, email));
                    }
                    break;

                case Resource.Id.btnForget:
                    _socket.Send(writer.auth.SendForget(login));
                    break;
                case Resource.Id.btnInvite:
                    object player = GetSelectedPlayer();
                    _socket.Send(writer.handShake.SendInvite(player));
                    break;
                case Resource.Id.btnRefresh:
                    _socket.Send(writer.lobby.SendRefresh());
                    break;

                case Resource.Id.bt1:
                    _socket.Send(writer.game.SendMove(roomNumber, playerName, playerMove, 0));
                    break;
                case Resource.Id.bt2:
                    _socket.Send(writer.game.SendMove(roomNumber, playerName, playerMove, 1));
                    break;
                case Resource.Id.bt3:
                    _socket.Send(writer.game.SendMove(roomNumber, playerName, playerMove, 2));
                    break;
                case Resource.Id.bt4:
                    _socket.Send(writer.game.SendMove(roomNumber, playerName, playerMove, 3));
                    break;
                case Resource.Id.bt5:
                    _socket.Send(writer.game.SendMove(roomNumber, playerName, playerMove, 4));
                    break;
                case Resource.Id.bt6:
                    _socket.Send(writer.game.SendMove(roomNumber, playerName, playerMove, 5));
                    break;
                case Resource.Id.bt7:
                    _socket.Send(writer.game.SendMove(roomNumber, playerName, playerMove, 6));
                    break;
                case Resource.Id.bt8:
                    _socket.Send(writer.game.SendMove(roomNumber, playerName, playerMove, 7));
                    break;
                case Resource.Id.bt9:
                    _socket.Send(writer.game.SendMove(roomNumber, playerName, playerMove, 8));
                    break;


            }
        }

        private object GetSelectedPlayer()
        {
            return names[etClients.SelectedItemPosition];
        }

        public void showAlert(string message)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            Dialog dialog = builder.Create();
            builder.SetTitle("Важное сообщение!")
                    .SetMessage(message)
                    .SetCancelable(false)
                    .SetNegativeButton("ОК", delegate
                     {
                         dialog.Dismiss();
                     });
            dialog.Show();
        }

        public void showConfirm(String[] arg)
        {
            string[] info = arg;
            AlertDialog.Builder alertDialog = new AlertDialog.Builder(this);
            Dialog dialog = alertDialog.Create();
            alertDialog.SetTitle("GO Games");

            alertDialog.SetMessage("Player " + arg[0] + "vs " + arg[1] + " wants to play with you!");
            alertDialog.SetPositiveButton("OK",  delegate
                {
                    writer.handShake.SendOk(info);
                    dialog.Dismiss();
                });

            alertDialog.SetNegativeButton("Cancel", delegate
            {
                dialog.Dismiss();
            });
            dialog.Show();
            // goPlaying(arg);
        }

        public void ShowGame()
        {
            foreach (Button b in gamebuttons)
            {
                b.Visibility = ViewStates.Visible;
            }
            Button[] lobbybuttons = new Button[] { btnInvite, btnLogin, btnRefresh, btnReg, btnForget, btnLogout };

            foreach (Button b in lobbybuttons)
            {
                b.Visibility = ViewStates.Gone;
            }

            etClients.Visibility = ViewStates.Gone;
            etEmail.Visibility = ViewStates.Gone;
            etPassword.Visibility = ViewStates.Gone;
            etLogin.Visibility = ViewStates.Gone;
            tvStatus.Visibility = ViewStates.Visible;
        }

        public void ShowLobby()
        {
            foreach (Button b in gamebuttons)
            {
                b.Visibility = ViewStates.Gone;
            }
            Button[] lobbybuttons = new Button[] { btnInvite, btnLogin, btnRefresh, btnReg, btnForget, btnLogout };

            foreach (Button b in lobbybuttons)
            {
                b.Visibility = ViewStates.Visible;
            }

            etClients.Visibility = ViewStates.Visible;
            etEmail.Visibility = ViewStates.Visible;
            etPassword.Visibility = ViewStates.Visible;
            etLogin.Visibility = ViewStates.Visible;
            tvStatus.Visibility = ViewStates.Gone;
        }

        public void moveBtn(Object[] args)
        {

            int i = 0;
            foreach (Button b in gamebuttons)
            {
                if (Int32.Parse(args[1].ToString()) == i++)
                {
                    b.Text = args[0].ToString();
                }
            }
        }

        public void getMessage(String tmp)
        {
            Request request = JsonConvert.DeserializeObject<Request>(tmp);
            switch (request.Module)
            {
                case "Auth":
                    Authorization(request);
                    break;
                case "Lobby":
                    Lobby(request);
                    break;
                case "HandShake":
                    HandShake(request);
                    break;
                case "Game":
                    Game(request);
                    break;
                default:
                    break;
            }
        }


        private void Authorization(Request response)
        {
            switch (response.Cmd)
            {
                case "LogIn":
                    {
                        if (response.Args != null)
                        {
                            playerName = response.Args.ToString();
                            setTvName("Your name is: " + playerName);
                            btnLogout.Visibility = ViewStates.Invisible;
                            btnInvite.Enabled = true;
                            btnRefresh.Enabled = true;
                        }
                    }
                    break;
            }
        }

        public void Lobby(Request response)
        {
            switch (response.Cmd)
            {
                case "refreshClients":
                    if (response.Args != null)
                    {
                        string[] personlist = JsonConvert.DeserializeObject<string[]>(response.Args.ToString());
                        string[] newpersonlist = new String[personlist.Length];
                        for (int i = 0; i < newpersonlist.Length; i++)
                        {
                            newpersonlist[i] = personlist[i];
                        }
                        setEtClients(newpersonlist);
                    }
                    break;
                case "Notification":
                    showAlert(response.Args.ToString());
                    break;
            }
        }

        public void HandShake(Request response)
        {
            switch (response.Cmd)
            {
                case "Wait":
                    // showAlert("Wait"); ///////////////////////////////////////////////  Wait

                    break;
                case "Invited":
                    string[] arg = JsonConvert.DeserializeObject<string[]>(response.Args.ToString());
                    try
                    {
                        showConfirm(arg);
                    }
                    catch (Exception)
                    {
                    }
                    break;
                default:
                    break;
            }
        }

        public void Game(Request response)
        {
            string[] arg;
            switch (response.Cmd)
            {
                case "Role":
                    string str = response.Args.ToString();
                    statusPlay(str);
                    break;
                case "Over":
                    ShowLobby();
                    break;
                case "Start":

                    start(response);
                    ShowGame();
                    break;
                case "Move":
                    arg = JsonConvert.DeserializeObject<string[]>(response.Args.ToString());
                    moveBtn(arg);
                    break;
                default:
                    break;
            }
        }
        public void connection()
        {
            _socket = new WebSocket4Net.WebSocket("ws://192.168.0.113:9898");
            _socket.Opened += OnConnectionOpen;
            _socket.MessageReceived += OnMessage;
            _socket.Open();
        }

        private void OnMessage(object sender, MessageReceivedEventArgs e)
        {
            getMessage(e.Message);
        }

        private void OnConnectionOpen(object sender, EventArgs e)
        {
            _connected = true;
        }


    }
}


