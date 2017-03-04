function chatMessage() {
    this.Sender = null;
    this.Text = null;
    this.TimeStamp = null;

    this.toString = function () {
        this.TimeStamp = Date.parse(this.TimeStamp);
        return '[' + this.TimeStamp.getHours() + ':' + this.TimeStamp.getMinutes() + ':' + this.getSeconds() + ']' + this.Sender + ' : ' + this.Text();
    }
}