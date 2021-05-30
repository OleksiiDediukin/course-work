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

namespace Cashbox.Views
{
    /// <summary>
    /// Логика взаимодействия для OrderProductQuantity.xaml
    /// </summary>
    public partial class OrderProductQuantity : Window
    {
        public double ProductsQuantity { get; set; }
        public int Sale { get; set; }
        public OrderProductQuantity()
        {
            InitializeComponent();
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 0x00000002;
            this.Left = (screenWidth - this.Width) / 0x00000002;
        }

        private void AddOrderProduct_Click(object sender, RoutedEventArgs e)
        {
            Double.TryParse(quantity.Text, out double productQuantity);
            ProductsQuantity = productQuantity;
            Int32.TryParse(sale.Text, out int productSale);
            Sale = productSale;
            DialogResult = true;
        }
    }
}
