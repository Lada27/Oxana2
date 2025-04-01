using CURSACH.Classes;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CURSACH.View
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            WindowState = CurrentWindow.State;

        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMaximaze_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            { 
                WindowState = WindowState.Normal;
                CurrentWindow.State = WindowState.Normal;

            }
            else
            {
                WindowState = WindowState.Maximized;
                CurrentWindow.State = WindowState.Maximized;

            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            String email = txtUser.Text;
            String password = txtPass.Password;
            Users authdUser = DatabaseManager.AuthenticateUser(email, password);

            if (authdUser != null)
            {
                CurrentUser.UserId = authdUser.Id;
                CurrentUser.UserName = authdUser.UserName;
                CurrentUser.Email = authdUser.Email;
                CurrentUser.Password = authdUser.Password;
                CurrentUser.Role =DatabaseManager.GetUserRole(authdUser.Id);
                CurrentUser.Phone = authdUser.Phone;


                Home home = new Home();
                home.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Пользователь не найден");
            }
        }


        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            SignUpView signUpVew = new SignUpView();
            signUpVew.Show();
            this.Hide();
        }
    }
}
