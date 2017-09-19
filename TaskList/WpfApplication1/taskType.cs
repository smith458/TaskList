using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TaskList
{
    public class TaskType : Object, INotifyPropertyChanged
    {
        private string name;
        private string project;
        private DateTime dueDate;

        public int EffortHours { get; set; }

        public string Name
        {
            get { return name != default(string) ? name : "NewTask"; }
            set { name = value; }
        }

        public string Project
        {
            get { return project != default(string) ? project : "NewProj"; }
            set { project = value; }
        }

        public DateTime DueDate
        {
            get { return dueDate != default(DateTime) ? dueDate : DateTime.Today; }
            set { dueDate = value; }
        }

        public TaskType ShallowCopy()
        {
            return (TaskType) this.MemberwiseClone();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
