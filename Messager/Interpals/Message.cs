using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interpals
{
    public class Message
    {
        public string ID_Message { get; set; }
        public string From { get; set; }
        public string Time { get; set; }
        public string Text { get; set; }
        public bool Readed { get; set; }

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
    }
}
