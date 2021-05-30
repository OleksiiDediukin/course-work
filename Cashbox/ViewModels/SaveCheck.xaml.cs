using Cashbox.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для SaveCheck.xaml
    /// </summary>
    public partial class SaveCheck : Window
    {
        private List<Product> ProductsList;
        public SaveCheck(List<Product> productsList)
        {
            InitializeComponent();
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 0x00000002;
            this.Left = (screenWidth - this.Width) / 0x00000002;
            ProductsList = productsList;
        }

        private void SaveCheck_Click(object sender, RoutedEventArgs e)
        {
            string checkInfo = "";
            foreach(Product product in ProductsList)
            {
                checkInfo += $"Product name: {product.Name} \n ";
                checkInfo += $"Price: {product.Price} \n ";
                checkInfo += $"Quantity: {product.Quantity} x {product.Unit}\n ";
                checkInfo += "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~";
            }

            
            File.WriteAllText($"C:\\Users\\lolik\\source\\repos\\Cashbox\\Cashbox\\Checks\\test{Directory.GetFiles($"C:\\Users\\lolik\\source\\repos\\Cashbox\\Cashbox\\Checks\\").Length}.txt", checkInfo);
            DialogResult = true;
        }
    }
}
