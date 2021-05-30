using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cashbox.Models
{
    public class Consignment
    {
        public int? ConsignmentId { get; set; }
        private int productId;
        private double? quantity;
        private string shelfLife;

        public Consignment() { }
        
        public Consignment(int id, double? quantity, string shelfLife)
        {
            ProductId = id;
            Quantity = quantity;
            ShelfLife = shelfLife;
        }

        public int ProductId
        {
            get { return productId; }
            set
            {
                productId = value;
                OnPropertyChanged("ProductId");
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
        public string ShelfLife
        {
            get { return shelfLife; }
            set
            {
                shelfLife = value;
                OnPropertyChanged("ShelfLife");
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
