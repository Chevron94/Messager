using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Interpals
{
    public class Message : INotifyPropertyChanged
    {
        public string ID_Message { get; set; }
        public string From { get; set; }
        public string Time { get; set; }
        public string Text { get; set; }
        private bool readed;
        public bool Readed
        {
            get
            {
                return readed;
            }
            set
            {
                readed = value;
                OnPropertyChanged("Readed");
            } 
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }
        public Message(string id_message, bool readed, string from, string time, string text)
        {
            ID_Message = id_message;
            From = from;
            Time = time;
            Text = text;
            Readed = readed;
        }

        public override string ToString()
        {
            string res = "";
            if (Time!="")
                res+="["+Time+"] ";
            if (From != "")
                res += "[" + From + "]\n";
            res += Text;
            return res;
        }
        public override bool Equals(object obj)
        {
            return ID_Message == ((Message)obj).ID_Message;
        }
    }
}
