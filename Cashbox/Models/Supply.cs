using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cashbox.Models
{
    public class Supply : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string name;
        private string dateTime;
        private string description;
        private string info;
        public List<Product> SupplyProducts { get; set; }

        public Supply()
        {
            SupplyProducts = new List<Product>();
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Info
        {
            get { return info; }
            set
            {
                info = value;
                OnPropertyChanged("Info");
            }
        }
        public string DateTime
        {
            get { return dateTime; }
            set
            {
                dateTime = value;
                OnPropertyChanged("DateTime");
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
