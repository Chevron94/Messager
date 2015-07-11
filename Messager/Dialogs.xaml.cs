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
using System.Windows.Shapes;
using Interpals;
using System.Threading;
using System.Collections.ObjectModel;

namespace Messager
{
    /// <summary>
    /// Interaction logic for Dialogs.xaml
    /// </summary>
    public partial class Dialogs : Window
    {
        string nickname;
        public bool IsShow { get; set; }
        List<Friend> friends = new List<Friend>();
        public Dialogs(string _nickname)
        {
            InitializeComponent();
            nickname = _nickname;
            Dispatcher.Hooks.DispatcherInactive += MyIdle;
            
        }

        public void AddFriend(Friend f, bool program = false)
        {
            if (!friends.Contains(f, (new FriendsComparer())))
            {
                friends.Add(f);
                TabItem t = new TabItem();
                t.Header = f.NickName;
                MyTabItem m = new MyTabItem(ref f, nickname,program);
                t.Content = m;
                tabControl1.Items.Add(t);
                
                if (!program || tabControl1.Items.Count == 1)
                {
                    ((TabItem)tabControl1.Items[tabControl1.Items.Count - 1]).Focus();
                    tabControl1.SelectedIndex = tabControl1.Items.Count - 1;
                }
            }
            else
            {
                bool find = false;
                for (int i = 0; i < friends.Count && !find; i++)
                {
                    if (friends[i].Thread_Id == f.Thread_Id)
                    {
                        find = true;
                        ((TabItem)tabControl1.Items[i]).Focus();                     
                    }
                }
            }
            Show();
        }

        public void MyIdle(object sender, EventArgs e)
        {
            if (tabControl1.Items.Count == 0)
                Hide();
        }

        public int FriendsCount
        {
            get
            {
                return friends.Count;
            }
        }

        public void MyDoubleClick(object sender, MouseButtonEventArgs e)
        {
            bool found = false;
            TabItem x = null;
            MyTabItem y;
            for (int i = 0; i < tabControl1.Items.Count && !found; i++)
            {
                if ((((TabItem)tabControl1.Items[i]).Header) ==((Label)(e.Source)).Content)
                {
                    x = (TabItem)tabControl1.Items[i];
                    found = true;
                }
            }
            if (found)
            {
                y = (MyTabItem)x.Content;
                y.Stop();
                tabControl1.Items.Remove(x);
                friends.Remove(y.friend);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            foreach (var x in tabControl1.Items)
            {
                ((MyTabItem)((TabItem)x).Content).Stop();
            }
            tabControl1.Items.Clear();
            friends.Clear();
            Hide();
            e.Cancel = true;
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
        }

        private void Window_LostFocus(object sender, RoutedEventArgs e)
        {
           
        }

        
    }
}
