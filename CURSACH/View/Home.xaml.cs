using CURSACH.Classes;
using LiveCharts.Wpf;
using LiveCharts;
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
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : Window
    {

        
        public Home()
        {
            InitializeComponent();
            WindowState = CurrentWindow.State;

            LoadMyTasks();
            LoadTasksToTake();
            LoadTasksInWork();

            if (CurrentUser.Role == "Сотрудник")
            {
                btnTasks.Visibility = Visibility.Collapsed;
            }

            if (CurrentUser.Role != "Администратор")
            {
                 btnUsers.Visibility = Visibility.Collapsed;
            }

            fillDiagrams();
        }

        private void fillDiagrams()
        {
            //Получаем данные о пользователе с наибольшим количеством задач
            var userData = DatabaseManager.GetUserWithMostTasks();

            // Привязываем данные для диаграммы, проверяем, что данные валидны
            if (userData.completedTasks >= 0 && userData.remainingTasks >= 0)
            {
                // Привязываем данные для диаграммы
                chartUserWithMostTasks.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "Выполненные задачи",
                        Values = new ChartValues<int> { userData.completedTasks },
                        DataLabels = true
                    },
                    new PieSeries
                    {
                        Title = "Не завершенные задачи",
                        Values = new ChartValues<int> { userData.remainingTasks },
                        DataLabels = true
                    }
                };

                // Отображаем имя пользователя
                UserNameText.Text = $"Лучший работник: {userData.userName}";
            }
            else
            {
                // Если данных нет, показываем ошибку или заглушку
                UserNameText.Text = "Нет данных о пользователе.";
                // Здесь можно также настроить отображение пустой диаграммы или другое поведение
            }


            // Получаем данные о пользователе с наибольшим количеством задач
            var mydata = DatabaseManager.GetMyStatistic();

            // Привязываем данные для диаграммы, проверяем, что данные валидны
            if (mydata.completedTasks >= 0 && mydata.remainingTasks >= 0)
            {
                // Привязываем данные для диаграммы
                chartMyTasks.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "Выполненные задачи",
                        Values = new ChartValues<int> { mydata.completedTasks },
                        DataLabels = true
                    },
                    new PieSeries
                    {
                        Title = "Не завершенные задачи",
                        Values = new ChartValues<int> { mydata.remainingTasks },
                        DataLabels = true
                    }
                };

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




        private void LoadMyTasks()
        {
            List<Tasks> tasks = DatabaseManager.GetMyTasks();
            cbMyTasks.Children.Clear();

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
                cbMyTasks.Children.Add(btnTask);
            }
        }

        private void LoadTasksInWork()
        {
            List<Tasks> tasks = DatabaseManager.GetTasksInWork();
            cbInWork.Children.Clear();


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
                cbInWork.Children.Add(btnTask);
            }
        }

        
        private void LoadTasksToTake()
        {
            List<Tasks> tasks = DatabaseManager.GetTasksToTake();
            cbTakeTask.Children.Clear();


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
                cbTakeTask.Children.Add(btnTask);
            }
        }


        // Открытие окна с подробной информацией о задаче

        private void OpenTaskDetails(Tasks task)
        {
            TaskDetailsWindow taskDetailsWindow = new TaskDetailsWindow(task);
            taskDetailsWindow.ShowDialog(); 
            int Id = CurrentUser.UserId;
            LoadMyTasks();
            LoadTasksToTake();
            LoadTasksInWork();
            fillDiagrams();


        }



        private void btPlusTask_Click(object sender, RoutedEventArgs e)
        {
            TaskDetailsWindow taskDetailsWindow = new TaskDetailsWindow();
            taskDetailsWindow.ShowDialog(); // Отображаем окно как модальное
            LoadMyTasks();
            LoadTasksToTake();
            LoadTasksInWork();
            fillDiagrams();

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
    }
}
