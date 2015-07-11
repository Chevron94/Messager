using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Messager.Interpals;
using System.Threading;
using System.Collections.ObjectModel;
using System.Data.Objects;
namespace Interpals
{
    public class Friend
    {
        public string NickName { get; set; }
        public string User_Id { get; set; }
        public string Thread_Id { get; set; }
        public int Age { get; set; }
        public string Photo { get; set; }
        public string Country_Flag { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public ObservableCollection<Message> MessageHistory { get; set; }

        public Message LastMessage
        {
            get
            {
                return MessageHistory.Last();
            }
        }
 
        public bool Online { get; set; }

        public Friend(string _User_ID,string _NickName, string _Thread_Id, string _Photo_Url, int _Age, string _Country_Flag_Url, string _Country, string _City, Message _LastMessage, bool _Online)
        {
            User_Id = _User_ID;
            NickName = _NickName;
            Thread_Id = _Thread_Id;
            Age = _Age;
            Country = _Country;
            Country_Flag = _Country_Flag_Url;
            City = _City;
            Photo = _Photo_Url;
            MessageHistory = new ObservableCollection<Message>();
            MessageHistory.Add(_LastMessage);
            //LastMessage = _LastMessage;
            Online = _Online;
            ConvertPicturesLinks();
        }

        public bool SendMessage(String message)
        {
            return InterpalsAPI.SendMessage(message, Thread_Id);
        }

        public List<Message> GetLastMessages()
        {
            if (MessageHistory.Count == 0)
            {
                return InterpalsAPI.GetMessages(Thread_Id, 1);

            }
            else
            {
                try
                {
                    return InterpalsAPI.GetMessages(Thread_Id, MessageHistory.First(msg => msg.Readed == false).ID_Message);
                }
                catch
                {
                    return InterpalsAPI.GetMessages(Thread_Id, MessageHistory.Last().ID_Message);
                }
            }
        }

        public void GetAllMessages()
        {
            
            /*
            StreamWriter writer = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"history\" + NickName + ".txt",false);

            foreach (Message m in res)
            {
                writer.WriteLine(m.ToString());
                writer.WriteLine();
            }
            writer.Close();*/
            InterpalsEntities Context = new InterpalsEntities();
            string last;
            try
            {
                var id = Context.Messages.Where("it.ID_Thread = @ID_Thread", new ObjectParameter("ID_Thread", Thread_Id)).Max(p => p.ID);
                last = Context.Messages.Where("it.ID=@ID", new ObjectParameter("ID", id)).First().ID_Message;
            }
            catch
            {
                last = "";
            }
            List<Message> res = InterpalsAPI.GetMessages(Thread_Id, last);
            int init = last == "" ? 0 : 1;
            for (int i = init; i < res.Count; i++ )
            {
                Message m = res[i];
                Messages msg = new Messages() { From = m.From, ID_Message = m.ID_Message, ID_Thread = Thread_Id, Text = m.Text, Time = m.Time };
                Context.AddToMessages(msg);
            }
            Context.SaveChanges();
        }

        private void ConvertPicturesLinks()
        {
            string file_to_check = System.AppDomain.CurrentDomain.BaseDirectory + @"pictures\flags\" + Country + ".jpg";
            if (!File.Exists(file_to_check))
            {
                Downloader.Download(Country_Flag, file_to_check);
            }
            Country_Flag = file_to_check;

            if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + @"pictures\photos\"+NickName))
            {
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"pictures\photos\" + NickName);
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"pictures\photos\"+NickName+@"\icon");
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + @"pictures\photos\" + NickName + @"\full");

            }
            string[] tmp = Photo.Split('/').Last().Split('_');
            string picture_name = "";
            if (tmp.Length == 1)
                picture_name = NickName + "_" + tmp[0];
            else picture_name = NickName + "_" + tmp[1] +tmp[2];

            file_to_check = System.AppDomain.CurrentDomain.BaseDirectory + @"pictures\photos\";
            file_to_check += NickName + @"\icon\";
            file_to_check+= picture_name;

            if (!File.Exists(file_to_check))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + @"pictures\photos\" + NickName + @"\icon\");
                Downloader.Download(Photo, file_to_check);
            }
            Photo = file_to_check;
            
        }
        public void DownloadPhotos()
        {
            List<String> links = InterpalsAPI.GetPhotosLinks(User_Id);
            string picture_name = "";
            foreach (string link in links)
            {
                string[] tmp = link.Split('/').Last().Split('_');
                if (tmp.Length == 1)
                    picture_name = NickName + "_" + tmp[0];
                else picture_name = NickName + "_" + tmp[1] + tmp[2];
                string new_file_to_check = System.AppDomain.CurrentDomain.BaseDirectory + @"pictures\photos\";
                new_file_to_check += NickName + @"\full\";
                new_file_to_check += picture_name;

                if (!File.Exists(new_file_to_check))
                {
                    Downloader.Download(link, new_file_to_check);
                }
            }

        }

        public int GetMessagesCount()
        {
            return InterpalsAPI.GetCountMessages(Thread_Id);
        }

    }

  
    public class FriendsComparer : IEqualityComparer<Friend>
    {
        public bool Equals(Friend f1, Friend f2)
        {
            if (f1.Thread_Id == f2.Thread_Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public int GetHashCode(Friend f)
        {
            return f.GetHashCode();
        }
    }
}
