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
        ObservableCollection<Message> messages;
        string nickname;
        MediaPlayer mp = new MediaPlayer();
        bool stop = false;
        string last_message_id;
        bool last_message_status;
        Thread t;
        public MyTabItem(Friend f, string _nickname, bool program = false)
        {
            InitializeComponent();
            mp.Open(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "/sounds/message.mp3"));
            mp.Volume = 100;
            nickname = _nickname;
            friend = f;
            FriendPhoto.Source = new BitmapImage(new Uri(friend.Photo));
            t = new Thread(new ThreadStart(Update));
            var tmp = friend.GetLastMessages();
            messages = new ObservableCollection<Message>(friend.GetLastMessages());
            MessageHistory.ItemsSource = messages;
            MessageHistory.ScrollIntoView(messages.Last());
            last_message_id = messages.Last().ID_Message;
            last_message_status = messages.Last().Readed;
            if (program)
                if (messages.Last().From != nickname)
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
                messages = new ObservableCollection<Message>(friend.GetLastMessages());
                MessageHistory.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                {
                    if (last_message_id != messages.Last().ID_Message || messages.Last().Readed != last_message_status)
                    {
                        MessageHistory.ItemsSource = messages;
                        last_message_id = messages.Last().ID_Message;
                        last_message_status = messages.Last().Readed;
                        MessageHistory.ScrollIntoView(messages.Last());
                        if (messages.Last().From != nickname)
                        {
                            mp.Play();
                        }
                    }

                }));
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
