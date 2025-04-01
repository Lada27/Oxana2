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

namespace CURSACH.View
{
    /// <summary>
    /// Логика взаимодействия для UserDetails.xaml
    /// </summary>
    public partial class UserDetails : Window
    {
        

        public Users User { get; set; }


        string action = "изменить";

        public UserDetails(Users user)
        {
            InitializeComponent();
            

            User = user;
            UserName.Text = user.UserName;
            tbPhone.Text = user.Phone;
            tbEmail.Text = user.Email;
            List<Role> roles = DatabaseManager.GetAllRoles();

            foreach (Role role in roles)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = role.Title.Trim();
                item.Tag = role.RoleId;
                if (user.RoleId == role.RoleId)
                {
                    item.IsSelected = true;
                }
                cbRole.Items.Add(item);

            }

            action = "изменить";
            DataContext = this;
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




        private void btnCloseDetails_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Проверка обязательных полей
            if (string.IsNullOrWhiteSpace(UserName.Text) || string.IsNullOrWhiteSpace(tbEmail.Text) || string.IsNullOrWhiteSpace(tbPhone.Text) )
            {
                MessageBox.Show("Введите имя, почту и телефон!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (cbRole.SelectedItem == null )
            {
                MessageBox.Show("Выберите роль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            // Получаем данные из UI
            string name = UserName.Text;
            string email = tbEmail.Text;
            string phone = tbPhone.Text;
            int roleId = Convert.ToInt32(((ComboBoxItem)cbRole.SelectedItem)?.Tag);



            if (action == "изменить")
            {
                User.Email = email;
                User.Phone = phone;
                User.UserName = name;
                User.RoleId = roleId;
                // Подтверждение изменения
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите изменить пользователя?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    DatabaseManager.UpdateUser(User);
                    MessageBox.Show("Пользователь изменен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }

        
    }
}
