/*
Sender :"v1"
Text :"31231"
TimeStamp :"2017-02-28T14:37:06.6341744+02:00"

*/
function ChatMessage(message) {
    this.Sender = message? message.Sender: null;
    this.Text = message? message.Text : null;
    this.TimeStamp = message? new Date(message.TimeStamp): null;

    this.toString = function () {
        return '[' + this.TimeStamp.getHours() + ':' + this.TimeStamp.getMinutes() + ':' + this.TimeStamp.getSeconds() + ']' + this.Sender + ' : ' + this.Text;
    }
}