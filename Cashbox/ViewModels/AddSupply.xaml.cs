using Cashbox.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для AddSupply.xaml
    /// </summary>
    public partial class AddSupply : Window
    {
        public Supply Supply { get; private set; }
        public ApplicationContext db { get; set; }
        public List<Product> ProductsList { get; set; }
        public string SupplyName { get; set; }
        public string SupplyDescription { get; set; }

        public List<string> ShelfLifes { get; set; }
        public AddSupply(Supply supply)
        {
            InitializeComponent();
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 0x00000002;
            this.Left = (screenWidth - this.Width) / 0x00000002;
            Supply = supply;
            ShelfLifes = new List<string>();
            db = new ApplicationContext();
            db.Products.Load();
            ProductsList = db.Products.Local.ToList();
            productsList.ItemsSource = ProductsList;
            supplyProductsList.ItemsSource = this.Supply.SupplyProducts;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(productsList.ItemsSource);
            view.Filter = UserFilter;
            
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as Product).Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(productsList.ItemsSource).Refresh();
        }

        private void Del_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (supplyProductsList.SelectedItem == null) return;
                this.Supply.SupplyProducts.RemoveAt(supplyProductsList.SelectedIndex);
                ShelfLifes.RemoveAt(supplyProductsList.SelectedIndex);
                CollectionViewSource.GetDefaultView(supplyProductsList.ItemsSource).Refresh();
            }

        }
        private void ChooseProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductsQuantitySelecting productsQuantitySelecting = new ProductsQuantitySelecting();
            if (productsQuantitySelecting.ShowDialog() == true)
            {
                Product selectedProduct = (Product)productsList.SelectedItem;
                Product product = new Product();
                product.Id = selectedProduct.Id;
                product.Name = selectedProduct.Name;
                product.Code = selectedProduct.Code;
                product.Price = selectedProduct.Price;
                product.Unit = selectedProduct.Unit;
                product.LastSupply = selectedProduct.LastSupply;
                double quantity = 0;
                Double.TryParse(productsQuantitySelecting.quantity.Text, out quantity);
                product.Quantity = quantity;
                this.Supply.SupplyProducts.Add(product);
                ShelfLifes.Add(productsQuantitySelecting.ShelfLife);
                CollectionViewSource.GetDefaultView(supplyProductsList.ItemsSource).Refresh();
            }
            
        }
        private void SaveSupply_Click(object sender, RoutedEventArgs e)
        {
            SupplyDescription supplyDescription = new SupplyDescription();
            if (supplyDescription.ShowDialog() == true)
            {
                SupplyName = supplyDescription.supplyName.Text;
                this.SupplyDescription = supplyDescription.supplyDescription.Text;
                DialogResult = true;
            }
        }

        

    }
}