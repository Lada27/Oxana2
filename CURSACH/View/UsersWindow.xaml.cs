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
    /// Логика взаимодействия для UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        public UsersWindow()
        {
            InitializeComponent();
            WindowState = CurrentWindow.State;

            LoadAllUsers();

            if (CurrentUser.Role == "Сотрудник")
            {
                btnTasks.Visibility = Visibility.Collapsed;
            }

            if (CurrentUser.Role != "Администратор")
            {
                btnAddUser.Visibility = Visibility.Collapsed;
            }
        }

        // Rнопки упраавления всего окна
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




        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            profile.Show();
            this.Hide();
        }
        private void btnTasks_Click(object sender, RoutedEventArgs e)
        {
            TaskManagement page = new TaskManagement();
            page.Show();
            this.Hide();
        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Home page = new Home();
            page.Show();
            this.Hide();
        }



        // Открытие окна с подробной информацией о задаче
        private void OpenUserDetails(Users user)
        {
            UserDetails page = new UserDetails(user);
            page.ShowDialog();
            LoadAllUsers();

        }



        

        private void btnTasksManaement_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            profile.Show();
            this.Hide();
        }

        

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            SignUpView page = new SignUpView();
            page.ShowDialog(); // Отображаем окно как модальное
            LoadAllUsers();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string name = tbNameToSearch.Text;
            List<Users> users = DatabaseManager.SearchUsersByName(name);
            cbAllUsers.Children.Clear();


            foreach (Users user in users)
            {
                Style buttonStyle = new Style(typeof(Button));
                buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5DEB3"))));

                buttonStyle.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.Black));
                buttonStyle.Setters.Add(new Setter(Button.FontSizeProperty, 14.0));
                buttonStyle.Setters.Add(new Setter(Button.MarginProperty, new Thickness(5, 10, 5, 0)));
                buttonStyle.Setters.Add(new Setter(Border.BorderThicknessProperty, new Thickness(0)));
                buttonStyle.Setters.Add(new Setter(FrameworkElement.MinWidthProperty, 90.0)); // Устанавливаем минимальную ширину в 100
                buttonStyle.Setters.Add(new Setter(FrameworkElement.MinHeightProperty, 40.0)); // Устанавливаем минимальную высоту в 50


                // Добавление Setter в стиль кнопок
                buttonStyle.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));


                // Создание объекта Border для задания радиуса скругления углов
                CornerRadius cornerRadius = new CornerRadius(30); // Устанавливаем радиус скругления углов

                // Создание объекта Setter для установки свойства CornerRadius
                Setter cornerRadiusSetter = new Setter(Border.CornerRadiusProperty, cornerRadius);

                // Добавление Setter в стиль кнопок
                buttonStyle.Setters.Add(cornerRadiusSetter);




                Button btnUser = new Button();
                btnUser.Content = user.UserName;
                btnUser.Style = buttonStyle;
                btnUser.Click += (se, ee) => OpenUserDetails(user);
                cbAllUsers.Children.Add(btnUser);
            }
        }

        private void LoadAllUsers()
        {
            List<Users> users = DatabaseManager.GetAllUsers();
            cbAllUsers.Children.Clear();


            foreach (Users user in users)
            {
                Style buttonStyle = new Style(typeof(Button));
                buttonStyle.Setters.Add(new Setter(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F5DEB3"))));

                buttonStyle.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.Black));
                buttonStyle.Setters.Add(new Setter(Button.FontSizeProperty, 14.0));
                buttonStyle.Setters.Add(new Setter(Button.MarginProperty, new Thickness(5, 10, 5, 0)));
                buttonStyle.Setters.Add(new Setter(Border.BorderThicknessProperty, new Thickness(0)));
                buttonStyle.Setters.Add(new Setter(FrameworkElement.MinWidthProperty, 90.0)); // Устанавливаем минимальную ширину в 100
                buttonStyle.Setters.Add(new Setter(FrameworkElement.MinHeightProperty, 40.0)); // Устанавливаем минимальную высоту в 50


                // Добавление Setter в стиль кнопок
                buttonStyle.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));


                // Создание объекта Border для задания радиуса скругления углов
                CornerRadius cornerRadius = new CornerRadius(30); // Устанавливаем радиус скругления углов

                // Создание объекта Setter для установки свойства CornerRadius
                Setter cornerRadiusSetter = new Setter(Border.CornerRadiusProperty, cornerRadius);

                // Добавление Setter в стиль кнопок
                buttonStyle.Setters.Add(cornerRadiusSetter);




                Button btnUser = new Button();
                btnUser.Content = user.UserName;
                btnUser.Style = buttonStyle;
                btnUser.Click += (sender, e) => OpenUserDetails(user);
                cbAllUsers.Children.Add(btnUser);
            }
        }

   
    }
}
