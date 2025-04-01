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
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        public Profile()
        {

            InitializeComponent();
            WindowState = CurrentWindow.State;
            UserName.Text = CurrentUser.UserName.Trim();
            UserEmail.Text = CurrentUser.Email.Trim();
            UserPassword.Text = CurrentUser.Password.Trim();
            UserPhone.Text = CurrentUser.Phone.Trim();

            if (CurrentUser.Role == "Сотрудник")
            {
                btnTasks.Visibility = Visibility.Collapsed;
            }

            if (CurrentUser.Role != "Администратор")
            {
                btnUsers.Visibility = Visibility.Collapsed;
            }
            LoadNotifications();

        }

        private void LoadNotifications()
        {
            List<Notifications> notifications = DatabaseManager.GetMyNotifications();
            cbNotifications.Children.Clear();


            foreach (Notifications notification in notifications)
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




                Button btnNotification = new Button();
                btnNotification.Content = notification.Message;
                btnNotification.Style = buttonStyle;
                btnNotification.Click += (sender, e) => OpenTaskDetails(notification);
                cbNotifications.Children.Add(btnNotification);
            }
        }

        private void OpenTaskDetails(Notifications notification)
        {
            DatabaseManager.MarkNotificationAsRead(notification);
            Tasks task = DatabaseManager.GetTaskById(notification.TaskId);
            TaskDetailsWindow taskDetailsWindow = new TaskDetailsWindow(task);
            taskDetailsWindow.ShowDialog();
            LoadNotifications();

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


        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            profile.Show();
            this.Hide();
        }
       




        private void btnApplyUserDataChane_Click(object sender, RoutedEventArgs e)
        {
            // Получаем новые значения из текстовых полей
            string newName = UserName.Text.Trim();
            string newEmail = UserEmail.Text.Trim();
            string newPassword = UserPassword.Text.Trim();
            string newPhone = UserPhone.Text.Trim();

            // Проверяем корректность введенных данных
            if (!IsValidEmail(newEmail))
            {
                MessageBox.Show("Введите корректный адрес электронной почты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidPassword(newPassword))
            {
                MessageBox.Show("Пароль должен содержать не менее 8 символов и включать как минимум одну букву и одну цифру.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidPhoneNumber(newPhone))
            {
                MessageBox.Show("Введите корректный номер телефона.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Формируем сообщение для подтверждения изменений
            string confirmationMessage = "Вы уверены, что хотите внести изменения в свой профиль?";
            MessageBoxResult result = MessageBox.Show(confirmationMessage, "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Если пользователь подтвердил изменения
            if (result == MessageBoxResult.Yes)
            {
                CurrentUser.UserName = newName;
                CurrentUser.Email = newEmail;
                CurrentUser.Password = newPassword;
                CurrentUser.Phone = newPhone;

                // Обновляем данные в базе данных
                if (DatabaseManager.UpdateUserProfile())
                {
                    MessageBox.Show("Данные профиля успешно изменены.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка при обновлении данных профиля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }



        // Метод для проверки корректности адреса электронной почты
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Проверка с использованием регулярного выражения
            var emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return emailRegex.IsMatch(email);
        }


        // Метод для проверки корректности пароля
        private bool IsValidPassword(string password)
    {
        // Пароль должен содержать не менее 8 символов и включать как минимум одну букву и одну цифру
        return !string.IsNullOrEmpty(password) && password.Length >= 8 && Regex.IsMatch(password, @"\d") && Regex.IsMatch(password, "[a-zA-Z]");
    }


    // Метод для проверки корректности номера телефона
    private bool IsValidPhoneNumber(string phoneNumber)
    {
        // Регулярное выражение для проверки номера телефона (допустимы форматы: +1234567890, 123-456-7890, 123.456.7890, 1234567890)
        string phonePattern = @"^\+?\d{0,2}?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$";

        // Проверяем соответствие введенного номера телефона регулярному выражению
        return Regex.IsMatch(phoneNumber, phonePattern);
    }



    private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser.UserId = 0;
            string confirmationMessage = "Вы уверены, что хотите выйти из профиля?";

            MessageBoxResult result = MessageBox.Show(confirmationMessage, "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                LoginView loginView = new LoginView();
                loginView.Show();
                this.Hide();
            }
        }

        private void btnHome_Click_1(object sender, RoutedEventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void btnTasksManaement_Click(object sender, RoutedEventArgs e)
        {
            TaskManagement page = new TaskManagement();
            page.Show();
            this.Hide();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            SignUpView signUpVew = new SignUpView();
            signUpVew.Show();
            this.Hide();
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            UsersWindow page = new UsersWindow();
            page.Show();
            this.Hide();
        }
    }
}
