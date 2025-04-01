using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using CURSACH.Classes;

namespace CURSACH.View
{
    /// <summary>
    /// Логика взаимодействия для TaskManagement.xaml
    /// </summary>
    public partial class TaskManagement : Window
    {
        public TaskManagement()
        {
            InitializeComponent();
            WindowState = CurrentWindow.State;

            LoadAllTasks();
            LoadWaitingForAssignTasks();
            LoadWaitingForApproveTasks();

            if (CurrentUser.Role == "Сотрудник")
            {
                btnTasks.Visibility = Visibility.Collapsed;
            }

            if (CurrentUser.Role != "Администратор")
            {
                btnUsers.Visibility = Visibility.Collapsed;
            }

            List<Users> users = DatabaseManager.GetUsers();

            foreach (Users user in users)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = user.UserName.Trim();
                item.Tag = user.Id;
                
                cbUser.Items.Add(item);
            }
            cbUser.SelectedIndex = 0;

            List<TaskType> types = DatabaseManager.GetTypes();

            foreach (TaskType type in types)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = type.Title.Trim();
                item.Tag = type.TypeId;
                cbType.Items.Add(item);
            }
            cbType.SelectedIndex = 0;

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





        private void LoadAllTasks()
        {
            List<Tasks> tasks = DatabaseManager.GetAllTasks();
            cbAllTasks.Children.Clear();

            foreach (Tasks task in tasks)
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




                Button btnTask = new Button();
                btnTask.Content = task.TaskDescription;
                btnTask.Style = buttonStyle;
                btnTask.Click += (sender, e) => OpenTaskDetails(task);
                cbAllTasks.Children.Add(btnTask);
            }
        }

        private void LoadWaitingForAssignTasks()
        {
            List<Tasks> tasks = DatabaseManager.GetWaitingForAssignTasks();
            cbWaitingForSAssign.Children.Clear();


            foreach (Tasks task in tasks)
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




                Button btnTask = new Button();
                btnTask.Content = task.TaskDescription;
                btnTask.Style = buttonStyle;
                btnTask.Click += (sender, e) => OpenTaskDetails(task);
                cbWaitingForSAssign.Children.Add(btnTask);
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            Home page = new Home();
            page.Show();
            this.Hide();
        }
        private void LoadWaitingForApproveTasks()
        {
            List<Tasks> tasks = DatabaseManager.GetWaitingForApproveTasks();
            cbWaitingForApprove.Children.Clear();


            foreach (Tasks task in tasks)
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




                Button btnTask = new Button();
                btnTask.Content = task.TaskDescription;
                btnTask.Style = buttonStyle;
                btnTask.Click += (sender, e) => OpenTaskDetails(task);
                cbWaitingForApprove.Children.Add(btnTask);
            }
        }


        // Открытие окна с подробной информацией о задаче

        private void OpenTaskDetails(Tasks task)
        {
            TaskDetailsWindow taskDetailsWindow = new TaskDetailsWindow(task);
            taskDetailsWindow.ShowDialog();
            LoadAllTasks();
            LoadWaitingForAssignTasks();
            LoadWaitingForApproveTasks();

        }



        private void btPlusTask_Click(object sender, RoutedEventArgs e)
        {
            TaskDetailsWindow taskDetailsWindow = new TaskDetailsWindow();
            taskDetailsWindow.ShowDialog(); // Отображаем окно как модальное
            LoadAllTasks();
            LoadWaitingForAssignTasks();
            LoadWaitingForApproveTasks();
        }

        private void btnTasksManaement_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            profile.Show();
            this.Hide();
        }

        
        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            UsersWindow page = new UsersWindow();
            page.Show();
            this.Hide();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string description = tbDescrioionToSearch.Text;
            List<Tasks> tasks = DatabaseManager.SearchTasksByDescription(description);
            cbAllTasks.Children.Clear();


            foreach (Tasks task in tasks)
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




                Button btnTask = new Button();
                btnTask.Content = task.TaskDescription;
                btnTask.Style = buttonStyle;
                btnTask.Click += (se, ee) => OpenTaskDetails(task);
                cbAllTasks.Children.Add(btnTask);
            }
        }

        private void FilterTasks(object sender, SelectionChangedEventArgs e)
        {
            if (cbType == null || cbUser == null || cbAllTasks == null ) return;
            int typeId = Convert.ToInt32(((ComboBoxItem)cbType.SelectedItem)?.Tag);
            int UserId = Convert.ToInt32(((ComboBoxItem)cbUser.SelectedItem)?.Tag);

            List<Tasks> tasks = DatabaseManager.SearchTasksByUserAndType(UserId, typeId);
            cbAllTasks.Children.Clear();


            foreach (Tasks task in tasks)
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

                Button btnTask = new Button();
                btnTask.Content = task.TaskDescription;
                btnTask.Style = buttonStyle;
                btnTask.Click += (se, ee) => OpenTaskDetails(task);
                cbAllTasks.Children.Add(btnTask);
            }
        }

       
    }
}
