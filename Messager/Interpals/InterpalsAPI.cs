using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using HtmlAgilityPack;
using System.Threading;

namespace Interpals
{
    public static class InterpalsAPI
    {
        public const string auth_url = "http://www.interpals.net/login.php";
        public const string dialogs_url = "http://www.interpals.net/pm.php";
        public const string dialogs_pages = "http://www.interpals.net/pm.php?view=paged";
        public const string dialogs_chat = "http://www.interpals.net/pm.php?view=chat";
        public const string albums_url = "http://www.interpals.net/albums.php?uid=";
        static CookieContainer cn = new CookieContainer();

        public static bool Auth(string login, string password)
        {
            var resp = Http.SteamWebRequest(auth_url, "login="+login+"&password="+password+"&auto_login=1&action=login", auth_url, cn);
            if (resp.Contains("password"))
                return false;
            return true;

        }

        public static List<String> GetPhotosLinks(string uid)
        {
            List<String> res = new List<string>();
            if (uid == "")
                return res;
            var resg = Http.SteamWebRequest(albums_url + uid, null, "http://www.interpals.net/", cn);
           
            if (resg == null)
            {
                Thread.Sleep(50);
                resg = Http.SteamWebRequest(albums_url + uid, null, "http://www.interpals.net/", cn);
            }
            
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(resg);
            List<String> albums = new List<string>();

            var albums_block = doc.GetElementbyId("album");
            var album_links = albums_block.ChildNodes.ToArray();

            for (int i = 2; i < album_links.Length-1; i ++)
            {
                string html_coding = album_links[i].ChildNodes[0].Attributes[0].Value;
                albums.Add(html_coding);
            }
            foreach(string album in albums)
            {
                resg = Http.SteamWebRequest("http://www.interpals.net/" + album, null, albums_url + uid, cn);
                if (resg == null)
                {
                    Thread.Sleep(50);
                    resg = Http.SteamWebRequest("http://www.interpals.net/" + album, null, albums_url + uid, cn);
                }
                
                string tmp = "";
                doc = new HtmlDocument();
                doc.LoadHtml(resg);
                var photos_block = doc.GetElementbyId("album");
                if (photos_block == null)
                    continue;
                var photos = photos_block.ChildNodes.ToArray();
                for (int i = 5; i < photos.Length-2; i++)
                {
                    tmp = photos[i].ChildNodes[1].ChildNodes[1].Attributes[1].Value;
                    tmp = tmp.Replace("/thumbs/180x180/", "/photos/");
                    if (tmp.Contains("http://th"))
                        tmp = tmp.Remove(7, 4);
                    string[] end_link = tmp.Split('/').Last().Split('_');
                    if (end_link.Length == 4)
                        tmp = tmp.Replace("_"+end_link.Last(), ".jpg");
                    res.Add(tmp);
                }
            }
            return res;
        }

        public static int GetCountMessages(string thread_id)
        {
            int page_num = Int32.MaxValue;
            
            var resg = Http.SteamWebRequest("http://www.interpals.net/pm.php", "thread_id=" + thread_id + "&page=" + page_num, dialogs_url, cn);
            if (resg == null)
            {
                Thread.Sleep(50);
                resg = Http.SteamWebRequest("http://www.interpals.net/pm.php", "thread_id=" + thread_id + "&page=" + page_num, dialogs_url, cn);
            }
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(resg);

            var conversation_block = doc.GetElementbyId("conversation");
            page_num = Int32.Parse(conversation_block.Attributes[1].Value);
            int result = (page_num-1)*25;
            var messages = conversation_block.ChildNodes.ToArray();
            int length = messages.Length;
            result += length / 2;
            return result;
        }

        public static List<Message> GetMessages(string thread_id, int count_pages)
        {
            Stack<Message> res = new Stack<Message>();
            int page_num = 1;
            int current_page = 1;
            int max_page = count_pages;
            for (page_num = 1; page_num <= max_page; page_num++)
            {
                var resg = Http.SteamWebRequest("http://www.interpals.net/pm.php", "thread_id=" + thread_id + "&page=" + page_num, dialogs_url, cn);
                if (resg == null)
                {
                    Thread.Sleep(50);
                    resg = Http.SteamWebRequest("http://www.interpals.net/pm.php", "thread_id=" + thread_id + "&page=" + page_num, dialogs_url, cn);
                }
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(resg);

                var conversation_block = doc.GetElementbyId("conversation");
                current_page = Int32.Parse(conversation_block.Attributes[1].Value);
                if (current_page != page_num || conversation_block.ChildNodes[1].Attributes[0].Value == "no_messages")
                    return res.ToList<Message>();
                var messages = conversation_block.ChildNodes.ToArray();
                int length = messages.Length;

                for (int i = 1; i < length; i += 2)
                {
                    bool msg_readed;
                    if (messages[i].Attributes[0].Value == "pm_msg pm_unread")
                        msg_readed = false;
                    else msg_readed = true;
                    string msg_id = messages[i].Attributes[1].Value.Split('_')[1];

                    string msg_from = messages[i].ChildNodes[3].ChildNodes[1].InnerText;
                    string msg_time = messages[i].ChildNodes[5].ChildNodes[1].InnerText;
                    string msg_text = System.Web.HttpUtility.HtmlDecode(messages[i].ChildNodes[7].InnerText).Trim();
                    Message msg = new Message(msg_id, msg_readed, msg_from, msg_time, msg_text);
                    res.Push(msg);
                }
            }
            return res.ToList<Message>();
        }

        public static List<Message> GetMessages(string thread_id, string last_message_id="-1")
        {
            Stack<Message> res = new Stack<Message>();
            int page_num = 1;
            int current_page = 1;
            int max_page = Int32.MaxValue;
            for (page_num = 1; page_num<=max_page ; page_num++)
            {
                var resg = Http.SteamWebRequest("http://www.interpals.net/pm.php", "thread_id=" + thread_id + "&page="+page_num, dialogs_url, cn);
                if (resg == null)
                {
                    Thread.Sleep(50);
                    resg = Http.SteamWebRequest("http://www.interpals.net/pm.php", "thread_id=" + thread_id + "&page=" + page_num, dialogs_url, cn);
                }
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(resg);

                var conversation_block = doc.GetElementbyId("conversation");
                current_page = Int32.Parse(conversation_block.Attributes[1].Value);
                if (current_page != page_num || conversation_block.ChildNodes[1].Attributes[0].Value=="no_messages")
                    return res.ToList<Message>();
                var messages = conversation_block.ChildNodes.ToArray();
                int length = messages.Length;

                for (int i = 1; i < length; i += 2)
                {
                    bool msg_readed;
                    if (messages[i].Attributes[0].Value == "pm_msg pm_unread")
                        msg_readed = false;
                    else msg_readed = true;
                    string msg_id = messages[i].Attributes[1].Value.Split('_')[1];
                    string msg_from = messages[i].ChildNodes[3].ChildNodes[1].InnerText;
                    string msg_time = messages[i].ChildNodes[5].ChildNodes[1].InnerText;
                    string msg_text = System.Web.HttpUtility.HtmlDecode(messages[i].ChildNodes[7].InnerText).Trim();
                    Message msg = new Message(msg_id, msg_readed, msg_from, msg_time, msg_text);
                    res.Push(msg);
                    if (msg_id == last_message_id)
                        return res.ToList<Message>();
                }
            }
            return res.ToList<Message>();
        }

        public static bool SendMessage(string Message, string thread_id)
        {
            bool result = true;
            var resg = Http.SteamWebRequest("http://www.interpals.net/pm.php", "thread_id="+thread_id, dialogs_url, cn);
            resg = Http.SteamWebRequest(dialogs_url, "action=send_message&thread="+thread_id+"&message="+Message, dialogs_url + "&" + "thread_id="+thread_id, cn);
            return result;
        }

        public static List<Friend> GetDialogs()
        {
            
            List<Friend> res = new List<Friend>();
            HtmlDocument doc = new HtmlDocument();
            string html = "";
            string previus_page_first_thread_id = "";

            html = Http.SteamWebRequest("http://www.interpals.net/pm.php?view=paged", null, dialogs_url, cn);
            for (int page_number = 1; ; page_number++)
            {
                html = Http.SteamWebRequest("http://www.interpals.net/pm.php", "page="+page_number, dialogs_url, cn);
                if (html == null)
                {
                    Thread.Sleep(50);
                    html = Http.SteamWebRequest("http://www.interpals.net/pm.php", "page=" + page_number, dialogs_url, cn);
                }
                doc.LoadHtml(html);

                var rs = doc.GetElementbyId("threads_left").ChildNodes.ToArray();
                string thread_id = rs[1].Attributes[1].Value.Split('_')[1];

                if (thread_id == previus_page_first_thread_id)
                    return res;
                previus_page_first_thread_id = thread_id;

                var tmp = rs[1].ChildNodes[1].ChildNodes.ToArray();
                string[] log_age = tmp[3].ChildNodes[1].ChildNodes[3].InnerText.Trim().Split(',');
                string login = log_age[0];
                if (login == "Inactive User")
                    continue;
                string[] tmpcnt = tmp[1].ChildNodes[1].Attributes[1].Value.Split(' ');
                string country = tmpcnt[3];
                for (int j = 4; j < tmpcnt.Length; j++)
                    country += " " + tmpcnt[j];
                string photo_url = tmp[1].ChildNodes[1].ChildNodes[1].Attributes[1].Value;
                string uid = "";
                if (photo_url != "http://eu.ipstatic.net/images/blanks/empty-male-50x50.png" &&
                    photo_url != "http://eu.ipstatic.net/images/blanks/empty-female-50x50.png")
                    uid = photo_url.Split('_')[1];
                bool online = tmp[3].ChildNodes[1].ChildNodes[1].Attributes[0].Value.Split(' ')[2] == "online-now";
                
                int age = Int32.Parse(log_age[1]);
                string country_flag_url = tmp[3].ChildNodes[3].ChildNodes[0].Attributes[1].Value;
                string city = tmp[3].ChildNodes[5].InnerText;

                bool Readed = tmp[5].Attributes[1].Value.Split(' ').Length == 1 && tmp[1].ChildNodes.Count != 5;
                Message message = GetMessages(thread_id, 1).Last();
                message.Readed = Readed;
                //string message = System.Web.HttpUtility.HtmlDecode(tmp[5].InnerText.Trim());

                //bool Readed = tmp[5].Attributes[1].Value.Split(' ').Length == 1 && tmp[1].ChildNodes.Count != 5;
                string LastMessageFrom = tmp[5].ChildNodes.Count > 1 ? "" : login;
                Friend first = new Friend(uid,login, thread_id, photo_url, age, country_flag_url, country, city,message,online);
                if (res.Contains(first,(new FriendsComparer())))
                {
                    return res;
                }
                res.Add(first);
                for (int i = 3; i < rs.Length; i += 2)
                {
                    thread_id = rs[i].Attributes[1].Value.Split('_')[1];
                    tmp = rs[i].ChildNodes[1].ChildNodes.ToArray();
                    tmpcnt = tmp[1].ChildNodes[1].Attributes[1].Value.Split(' ');
                    log_age = tmp[3].ChildNodes[1].ChildNodes[3].InnerText.Trim().Split(',');
                    login = log_age[0];
                    if (login == "Inactive User")
                        continue;
                    country = tmpcnt[3];
                    for (int j = 4; j < tmpcnt.Length; j++)
                        country += " " + tmpcnt[j];
                    photo_url = tmp[1].ChildNodes[1].ChildNodes[1].Attributes[1].Value;
                    if (photo_url != "http://eu.ipstatic.net/images/blanks/empty-male-50x50.png" &&
                        photo_url != "http://eu.ipstatic.net/images/blanks/empty-female-50x50.png")
                    uid = photo_url.Split('_')[1];
                    online = tmp[3].ChildNodes[1].ChildNodes[1].Attributes[0].Value.Split(' ')[2] == "online-now";
                    
                    age = Int32.Parse(log_age[1]);
                    country_flag_url = tmp[3].ChildNodes[3].ChildNodes[0].Attributes[1].Value;
                    city = tmp[3].ChildNodes[5].InnerText;
                    Readed = tmp[5].Attributes[1].Value.Split(' ').Length == 1 && tmp[1].ChildNodes.Count != 5;
                    message = GetMessages(thread_id, 1).Last();
                    message.Readed = Readed;
                    Readed = tmp[5].Attributes[1].Value.Split(' ').Length == 1;
                    LastMessageFrom = tmp[5].ChildNodes.Count > 1 ? "" : login;
                    res.Add(new Friend(uid,login, thread_id, photo_url, age, country_flag_url, country, city,message, online));
                }
            }

        }
    }
}
