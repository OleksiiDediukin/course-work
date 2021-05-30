using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace Cashbox.Models
{
    public class Product : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string name;
        private string code;
        private string unit;
        private double price;
        private double? quantity;
        private string lastSupply;

        public Product() { }

        public Product(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Code = product.Code;
            Unit = product.Unit;
            Price = product.Price;
            Quantity = product.Quantity;
            LastSupply = product.LastSupply;
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
        public string Code
        {
            get { return code; }
            set
            {
                code = value;
                OnPropertyChanged("Code");
            }
        }
        public string Unit
        {
            get { return unit; }
            set
            {
                unit = value;
                OnPropertyChanged("Unit");
            }
        }
        public double Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }

        public string LastSupply
        {
            get { return lastSupply; }
            set
            {
                lastSupply = value;
                OnPropertyChanged("LastSupply");
            }
        }

        public double? Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged("Quantity");
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
