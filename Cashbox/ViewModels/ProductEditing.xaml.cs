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
using Cashbox.Models;

namespace Cashbox.Views
{
    /// <summary>
    /// Логика взаимодействия для ItemAdding.xaml
    /// </summary>
    public partial class ProductEditing : Window
    {
        public Product Product { get; private set; }
        public ProductEditing(Product product)
        {
            InitializeComponent();
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 0x00000002;
            this.Left = (screenWidth - this.Width) / 0x00000002;
            Product = product;
            this.DataContext = Product;
        }

        private void SelectionText(object sender, MouseButtonEventArgs e)
        {
            ((TextBox)sender).SelectedText = ((TextBox)sender).Text;
        }

        private void SaveItem_Click(object sender, RoutedEventArgs e)
        {
            string Name = productEditingName.Text;
            string Code = productEditingCode.Text;
            string Price = productEditingPrice.Text;
            string Unit = productEditingUnit.Text;
            if (Name!="" && Code!="" && Price!="" && Unit != "")
            {
                DialogResult = true;
            }
            else
            {

            }
            
        }
    }
}
