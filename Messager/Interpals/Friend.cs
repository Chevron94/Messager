using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Messager.Interpals;
using System.Threading;
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
        public Message LastMessage { get; set; }
 
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
            LastMessage = _LastMessage;
            Online = _Online;
            ConvertPicturesLinks();
        }

        public bool SendMessage(String message)
        {
            return InterpalsAPI.SendMessage(message, Thread_Id);
        }

        public List<Message> GetLastMessages()
        {
            return InterpalsAPI.GetMessages(Thread_Id);
        }

        public void GetAllMessages()
        {
            /*if (!File.Exists(System.AppDomain.CurrentDomain.BaseDirectory+@"history\"+NickName+".txt"))
                File.Create(System.AppDomain.CurrentDomain.BaseDirectory+@"history\"+NickName+".txt");*/
            List<Message> res = InterpalsAPI.GetMessages(Thread_Id, true);

            StreamWriter writer = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"history\" + NickName + ".txt",false);

            foreach (Message m in res)
            {
                writer.WriteLine(m.ToString());
                writer.WriteLine();
            }
            writer.Close();
            //return res;
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
