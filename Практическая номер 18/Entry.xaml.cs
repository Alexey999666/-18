using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Практическая_номер_18.ModelsDB;

namespace Практическая_номер_18
{
    /// <summary>
    /// Логика взаимодействия для Entry.xaml
    /// </summary>
    public partial class Entry : Window
    {
        public Entry()
        {
            InitializeComponent();
        }
        DispatcherTimer _timer;
        int _countLogin = 1;
        void GetCaptcha()
        {

        }
        private void Window_Activated(object sender, EventArgs e)
        {
            tbLogin.Focus();
            DataBaseSession.Login = false;
                _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 10);
            _timer.Tick += new EventHandler(timer_Tick);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            SPenel.IsEnabled = true;
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            using (SessionContext _db = new SessionContext())
            {
                var user = _db.Users.Where(user => user.UserLogin == tbLogin.Text && user.UserPassword == tbPas.Password);
                if (user.Count() == 1 && txtCaprcha.Text == tbCaptcha.Text) 
                {
                    DataBaseSession.Login = true;
                    DataBaseSession.UserSurname = user.First().UserSurname;
                    DataBaseSession.UserName = user.First().UserName;
                    DataBaseSession.UserPatronymic = user.First().UserPatronymic;
                    _db.Roles.Load();
                    DataBaseSession.Right = user.First().UserRoleNavigation.RoleName;
                    Close();
                }
            }
        }

        private void btnEsc_Click(object sender, RoutedEventArgs e)
        {
              Close();
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            DataBaseSession.Login = true;
            DataBaseSession.UserSurname = "Гость";
            DataBaseSession.UserName = "";
            DataBaseSession.UserPatronymic = "";
            DataBaseSession.Right = "Гость";
            Close();

        }
    }
}
