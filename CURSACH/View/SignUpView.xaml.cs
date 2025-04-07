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
using System.Text.RegularExpressions;
using CURSACH.Classes;


namespace CURSACH.View
{
    /// <summary>
    /// Логика взаимодействия для SignUpView.xaml
    /// </summary>
    public partial class SignUpView : Window
    {
        public SignUpView()
        {
            InitializeComponent();
            WindowState = CurrentWindow.State;

        }

        // управление окном
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

        // Регистрация
        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            // Получаем данные из текстовых полей
            string name = tbName.Text;
            string email = tbEnmail.Text;
            string phone = tbPhone.Text;
            string password = txtPass.Password;

            // Проверяем корректность данных
            if (name == null || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (phone.Length < 11)
            {
                MessageBox.Show("Телефон должен содержать 11 цифр.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!(IsPasswordValid(password)))
            {
                MessageBox.Show("Пароль должен содержать минимум 8 символов и включать в себя буквы и цифры.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            // Регистрация пользователя
            bool success = DatabaseManager.AddUser(name, email, phone, password);

            if (success)
            {
                MessageBox.Show("Пользователь успешно зарегистрирован.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Home home = new Home();
                home.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Ошибка при регистрации пользователя. Попробуйте снова.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // проверка пользовательского ввода
        public bool IsPasswordValid(string password)
            {
                // Проверяем, что пароль содержит минимум 8 символов, буквы и цифры
                string pattern = @"^(?=.*[A-Za-z])(?=.*\d).{8,}$";
                return Regex.IsMatch(password, pattern);
            }

        private void btnBacToLogin_Click(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }
    }
}
