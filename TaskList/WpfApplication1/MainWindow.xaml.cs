using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace TaskList
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            filePath = "tasks.xml";


            if (File.Exists(filePath))
            {
                XmlSerializer reader = new XmlSerializer(typeof(ObservableCollection<TaskType>));
                using (StreamReader file = new StreamReader(filePath))
                {
                    taskList = (ObservableCollection<TaskType>) reader.Deserialize(file);
                }
            }
            else
            {
                taskList = new ObservableCollection<TaskType>()
                {
                    new TaskType() { Name = "Cow", Project = "ThisOne", DueDate=DateTime.Today, EffortHours=4 },
                    new TaskType() { Name = "Chicken", Project = "AnotherOne", DueDate=DateTime.Today, EffortHours=8 },
                    new TaskType() { Name = "Pig", Project = "LastOne", DueDate=DateTime.Today, EffortHours=15 }
                };
            }
        }

        private void NewTask_Click(object sender, RoutedEventArgs e)
        {
            EditDialog newDialog = new EditDialog(new TaskType());
            newDialog.Owner = this; //For centering on parent
            if (newDialog.ShowDialog() == true)
            {
                taskList.Add(newDialog.currentTask);
            }

        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            if (taskListBox.SelectedItem != null)
            {
                int currentIndex = taskListBox.SelectedIndex;
                EditDialog newDialog = new EditDialog(taskList[currentIndex].ShallowCopy());
                newDialog.Owner = this; //For centering on parent
                if (newDialog.ShowDialog() == true)
                {
                    taskList[currentIndex] = newDialog.currentTask;
                }
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (taskListBox.SelectedItem != null)
            {
                int currentIndex = taskListBox.SelectedIndex;
                int nextSelection = (currentIndex != (taskList.Count - 1)) ? currentIndex: currentIndex - 1;

                taskList.RemoveAt(currentIndex);
                taskListBox.SelectedIndex = nextSelection;
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            XmlSerializer writer = new XmlSerializer(typeof(ObservableCollection<TaskType>));
            using (FileStream file = File.Create(filePath))
            {
                writer.Serialize(file, taskList);
            }
        }

        public ObservableCollection<TaskType> taskList { get; set; }
        private string filePath;
    }
}
