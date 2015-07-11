using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using Interpals;
using System.Collections.ObjectModel;
using System.IO;

namespace Messager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool stop = false;
        Dialogs dialogs;
        public MainWindow()
        {
            InitializeComponent();
            Login.Focus();
            if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory+@"history\"))
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory+@"history\");
        }

        List<Friend> Friends;
        List<Friend> StaticFriends;
        Thread t;
        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {

             if (InterpalsAPI.Auth(Login.Text, Password.Password))
            {
                Friends = InterpalsAPI.GetDialogs();
                dataGrid1.ItemsSource = Friends;
                StaticFriends = Friends;
                dialogs = new Dialogs(Login.Text);
                Thread tmp = new Thread(new ThreadStart(GetAllPhotos));
                Thread msgs = new Thread(new ThreadStart(GetAllMessages));
                tmp.Start();
                msgs.Start();
                foreach (var f in Friends)
                {
                    if (f.LastMessage.From != Login.Text && !f.LastMessage.Readed)
                    {
                        dialogs.AddFriend(f, true);
                    }
                }
                t = new Thread(new ThreadStart(Update));
                t.Start();
            }
            else
            {
                MessageBox.Show("Wrong Login/Password");
                Password.Password = "";
            }
        }


        public void GetAllPhotos()
        {
            foreach (var f in StaticFriends)
                f.DownloadPhotos();
        }

        public void GetAllMessages()
        {
            ThreadPool.SetMinThreads(2, 2);
            ThreadPool.SetMaxThreads(2, 2);
            foreach (var f in StaticFriends)
                f.GetAllMessages();
        }

        private void MessageHistory_Click(object sender, RoutedEventArgs e)
        {
            if (Friends != null)
            {
                dialogs.AddFriend((Friend)dataGrid1.CurrentItem);
            }
        }

        private void Update()
        {
            while (!stop)
            {
                Thread.Sleep(50);
                var tmpFriendList = InterpalsAPI.GetDialogs();
                dataGrid1.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                    {
                        bool changes = false;
                        if (tmpFriendList.Count != Friends.Count)
                        {
                            changes = true;
                        }
                        else
                        for (int i = 0; i < tmpFriendList.Count; i++)
                        {
                            bool found = false;
                            for (int j = 0; j < Friends.Count && !found; j++)
                            {
                                if (tmpFriendList[i].NickName == Friends[j].NickName)
                                {
                                    found = true;
                                    if (tmpFriendList[i].LastMessage.ID_Message != Friends[j].LastMessage.ID_Message)
                                    {
                                        if (tmpFriendList[i].LastMessage.From == tmpFriendList[i].NickName)
                                            dialogs.AddFriend(tmpFriendList[i], true);
                                        changes = true;
                                    }
                                    else
                                    {
                                        if (tmpFriendList[i].LastMessage.Readed != Friends[j].LastMessage.Readed ||
                                            tmpFriendList[i].Online != Friends[i].Online)
                                            changes = true;
                                    }
                                }
                            }
                        }
                        if (changes)
                        {
                            Friends = tmpFriendList;
                            dataGrid1.ItemsSource = tmpFriendList;
                        }
                    }));
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            stop = true;
            if (t!= null && t.IsAlive)
                t.Abort();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

    }
}
