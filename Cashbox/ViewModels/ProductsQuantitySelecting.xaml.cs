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
    /// Логика взаимодействия для ProductsQuantitySelecting.xaml
    /// </summary>
    public partial class ProductsQuantitySelecting : Window
    {
        public string ShelfLife { get; set; }
        public ProductsQuantitySelecting()
        {
            
            InitializeComponent();
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 0x00000002;
            this.Left = (screenWidth - this.Width) / 0x00000002;
        }

        private void AddSupplyProduct_Click(object sender, RoutedEventArgs e)
        {
            ShelfLife = ShelfLifePicker.ToString();
            DialogResult = true;
        }
    }
}
