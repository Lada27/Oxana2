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
using CURSACH.Classes;
using CURSACH.View;



namespace CURSACH.View
{
    /// <summary>
    /// Логика взаимодействия для TaskDetailsWindow.xaml
    /// </summary>
    public partial class TaskDetailsWindow : Window
    {
        public Tasks Task { get; set; }


        string action = "изменить";
        public TaskDetailsWindow()
        {
            InitializeComponent();
            action = "добавить";
            btnDeleteTask.Visibility = Visibility.Collapsed;
            cbStatus.IsEditable = false;
            dpDeadline.SelectedDate = DateTime.Now;

            cbAssigned.Visibility = Visibility.Collapsed;
            brAssigned.Visibility = Visibility.Collapsed;   
            Assigned.Visibility = Visibility.Collapsed;

            TimeSpent.Visibility = Visibility.Collapsed;
            brTimeSpent.Visibility = Visibility.Collapsed;
            tbTimeSpent.Visibility = Visibility.Collapsed;

            if (CurrentUser.Role == "Сотрудник")
            {

                dpDeadline.Visibility = Visibility.Collapsed;
                tbSelectedDate.IsEnabled = false; // Запрещаем редактирование TextBlock

            }


            List<Users> users = DatabaseManager.GetUsers();

            foreach (Users user in users)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = user.UserName.Trim();
                item.Tag = user.Id; 
                if (user.Id == CurrentUser.UserId)
                {
                    item.IsSelected = true;
                }
                cbAuthor.Items.Add(item);

                
            }

            cbAuthor.IsEditable = false;

            List<TaskStatus> statuses = DatabaseManager.GetStatuses();

            foreach (TaskStatus status in statuses)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = status.Title.Trim();
                item.Tag = status.StatusId;
                cbStatus.Items.Add(item);
            }
            cbStatus.SelectedIndex = 0;

            cbStatus.IsEnabled = false;

            List<TaskPriority> priorities = DatabaseManager.GetPriorities();

            foreach (TaskPriority priority in priorities)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = priority.Title.Trim();
                item.Tag = priority.PriorityId;
                cbPriority.Items.Add(item);
            }
            cbPriority.SelectedIndex = 1;

            List<TaskType> types = DatabaseManager.GetTypes();

            foreach (TaskType type in types)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = type.Title.Trim();
                item.Tag = type.TypeId;
                cbType.Items.Add(item);
            }

            cbType.SelectedIndex = 1;

            foreach (Users user in users)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = user.UserName.Trim();
                item.Tag = user.Id; // Используем Tag для хранения Id проекта
                cbAssigned.Items.Add(item);
            }

            if (CurrentUser.Role == "Сотрудник")
            {
                cbAssigned.IsEditable = false; 
            }
        }

        public TaskDetailsWindow(Tasks task)
        {
            InitializeComponent();
            if (CurrentUser.Role == "Сотрудник")
            {
                cbAssigned.IsEditable = false; 
            }

            Task = task;
            TaskDescription.Text = task.TaskDescription;
            tbTimeAllocated.Text = task.TimeAllocated.ToString();
            tbTimeSpent.Text = task.TimeSpent.ToString();
            tbComments.Text = task.Comments;
            List<Users> users = DatabaseManager.GetUsers();

            foreach (Users user in users)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = user.UserName.Trim();
                item.Tag = user.Id;
                if (user.Id == task.ReporterId)
                {
                    item.IsSelected = true;
                }
                cbAuthor.Items.Add(item);


            }

            List<TaskStatus> statuses = DatabaseManager.GetStatuses();

            foreach (TaskStatus status in statuses)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = status.Title.Trim();
                item.Tag = status.StatusId;
                if (status.StatusId == task.TaskStatusId)
                {
                    item.IsSelected = true;
                }
                cbStatus.Items.Add(item);
            }

            List<TaskPriority> priorities = DatabaseManager.GetPriorities();

            foreach (TaskPriority priority in priorities)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = priority.Title.Trim();
                item.Tag = priority.PriorityId;
                if (priority.PriorityId == task.TaskPriorityId)
                {
                    item.IsSelected = true;
                }
                cbPriority.Items.Add(item);
            }

            List<TaskType> types = DatabaseManager.GetTypes();

            foreach (TaskType type in types)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = type.Title.Trim();
                item.Tag = type.TypeId;
                if (type.TypeId == task.TypeId)
                {
                    item.IsSelected = true;
                }
                cbType.Items.Add(item);
            }

            ComboBoxItem man = new ComboBoxItem();
            man.Content = "Не назначен";
            man.IsSelected = true;
            cbAssigned.Items.Add(man);

            foreach (Users user in users)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = user.UserName.Trim();
                item.Tag = user.Id;
                if (user.Id == task.AssignedUserId)
                {
                    item.IsSelected = true;
                }
                cbAssigned.Items.Add(item);
            }

            tbSelectedDate.Text = task.Deadline.ToString("dd.MM.yyyy");

            dpDeadline.SelectedDate = task.Deadline;
            dpDeadline.Text = task.Deadline.ToString("dd.MM.yyyy");

           
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

        

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            int taskId = Task.Id;
            // Выводим сообщение пользователю с запросом подтверждения удаления задачи
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить эту задачу?", "Удаление задачи", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Если пользователь подтвердил удаление, удаляем задачу из базы данных
            if (result == MessageBoxResult.Yes)
            {
                try
                {                   
                    DatabaseManager.DeleteTask(taskId);
                    MessageBox.Show("Задача удалена");
                    Close();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении задачи: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnCloseDetails_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Проверка обязательных полей
            if (string.IsNullOrWhiteSpace(TaskDescription.Text))
            {
                MessageBox.Show("Введите описание задачи!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (cbStatus.SelectedItem == null || cbPriority.SelectedItem == null || cbType.SelectedItem == null)
            {
                MessageBox.Show("Выберите статус, приоритет и тип задачи!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            

            if (!decimal.TryParse(tbTimeAllocated.Text, out decimal timeAllocated) || timeAllocated < 0)
            {
                MessageBox.Show("Введите корректное время на выполнение задачи!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            decimal timeSpent = 0;
            if (!string.IsNullOrWhiteSpace(tbTimeSpent.Text) && (!decimal.TryParse(tbTimeSpent.Text, out timeSpent) || timeSpent < 0))
            {
                MessageBox.Show("Введите корректное затраченное время!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!dpDeadline.SelectedDate.HasValue)
            {
                MessageBox.Show("Выберите дату дедлайна!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DateTime deadline = dpDeadline.SelectedDate.Value;

            // Получаем данные из UI
            string description = TaskDescription.Text;
            string comments = tbComments.Text;

            int statusId = Convert.ToInt32(((ComboBoxItem)cbStatus.SelectedItem)?.Tag);
            int priorityId = Convert.ToInt32(((ComboBoxItem)cbPriority.SelectedItem)?.Tag);
            int typeId = Convert.ToInt32(((ComboBoxItem)cbType.SelectedItem)?.Tag);
            int? userId = ((ComboBoxItem)cbAssigned.SelectedItem)?.Tag is int tagValue ? tagValue : (int?)null;

            if (action == "изменить")
            {
                Task.TaskDescription = description;
                Task.Comments = comments;
                Task.Deadline = deadline;
                Task.TaskStatusId = statusId;
                Task.TypeId = typeId;
                Task.ReporterId = CurrentUser.UserId;
                Task.AssignedUserId = userId;
                Task.TaskPriorityId = priorityId;
                Task.TimeAllocated = timeAllocated;
                Task.TimeSpent = timeSpent;

                // Подтверждение изменения
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите изменить задачу?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    DatabaseManager.UpdateTask(Task);
                    MessageBox.Show("Задача успешно изменена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            if (action == "добавить")
            {
                Tasks taskToSave = new Tasks
                {
                    TaskDescription = description,
                    Comments = comments,
                    Deadline = deadline,
                    ReporterId = CurrentUser.UserId,
                    AssignedUserId = userId,
                    TaskPriorityId = priorityId,
                    TypeId = typeId,
                    TimeAllocated = timeAllocated,
                    TimeSpent = timeSpent
                };

                DatabaseManager.AddTask(taskToSave);
                MessageBox.Show("Задача успешно добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DatePicker datePicker && datePicker.SelectedDate.HasValue)
            {
                DateTime selectedDate = datePicker.SelectedDate.Value;
                tbSelectedDate.Text = selectedDate.ToString("dd.MM.yyyy");
            }
        }


       
    }
}
