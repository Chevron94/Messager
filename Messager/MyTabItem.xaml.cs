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
using Interpals;
using System.Threading;
using System.Collections.ObjectModel;

namespace Messager
{
    /// <summary>
    /// Interaction logic for TabItem.xaml
    /// </summary>
    public partial class MyTabItem : UserControl
    {
        public Friend friend;
        //ObservableCollection<Message> messages;
        string nickname;
        MediaPlayer mp = new MediaPlayer();
        bool stop = false;
        string last_message_id;
        bool last_message_status;
        Thread t;
        public MyTabItem(ref Friend f, string _nickname, bool program = false)
        {
            InitializeComponent();
            mp.Open(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/sounds/message.mp3"));
            mp.Volume = 100;
            nickname = _nickname;
            friend = f;
            FriendPhoto.Source = new BitmapImage(new Uri(friend.Photo));
            t = new Thread(new ThreadStart(Update));
            MessageHistory.ItemsSource = friend.MessageHistory;
            MessageHistory.ScrollIntoView(friend.MessageHistory.Last());
            last_message_id = friend.MessageHistory.Last().ID_Message;
            last_message_status = friend.MessageHistory.Last().Readed;
            if (program)
                if (friend.MessageHistory.Last().From != nickname)
                    mp.Play();
            t.Start();
            MessageField.Focus();
        }

        public void Focusing()
        {
            MessageField.Focus();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageField.Text.Trim() != "")
                if (friend.SendMessage(MessageField.Text))
                {
                    MessageField.Text = "";
                    
                }
                else MessageBox.Show("Sending message error!");
            MessageField.Focus();
        }

        private void Update()
        {
            while (!stop)
            {
                Thread.Sleep(50);
                List<Message> upd = friend.GetLastMessages();
                if (upd.Count > 0)
                {
                    MessageHistory.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                    {
                        foreach (Message m in upd)
                            if (friend.MessageHistory.Contains(m))
                            {
                                for (int i = friend.MessageHistory.Count - 1; i > -1; i--)
                                {
                                    if (friend.MessageHistory[i].ID_Message == m.ID_Message)
                                    {
                                        friend.MessageHistory[i].Readed = m.Readed;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                friend.MessageHistory.Add(m);
                                if (m.From != nickname)
                                {
                                    //mp.Open(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/sounds/message.mp3"));
                                    mp.Open(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/sounds/message.mp3"));
                                    mp.Play();
                                }
                                MessageHistory.ScrollIntoView(friend.MessageHistory.Last());
                            }
                       
                    }));
                }
                    
            }
        }

        public void Stop()
        {
            stop = true;
            if (t != null && t.IsAlive) t.Abort();
        }

        private void MessageField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && Keyboard.Modifiers == ModifierKeys.Control)
            {
                SendButton_Click(null, null);
                e.Handled = true;
            }
        }
    }
}
