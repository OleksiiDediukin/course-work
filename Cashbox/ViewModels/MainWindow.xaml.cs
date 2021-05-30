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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using Cashbox.Models;

using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Configuration;
using Path = System.IO.Path;

namespace Cashbox.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OrdersSection OrdersSection { get; set; }
        private ProductsSection ProductsSection { get; set; }
        private SuppliesSection SuppliesSection { get; set; }

        private ApplicationContext db { get; set; }

        public MainWindow()
        {
            /*var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            string path = Path.GetFullPath("Cashbox//cashbox.db");
            connectionStringsSection.ConnectionStrings["DefaultConnection"].ConnectionString = "";
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");*/

            InitializeComponent();
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (screenHeight - this.Height) / 0x00000002;
            this.Left = (screenWidth - this.Width) / 0x00000002;
            db = new ApplicationContext();
            OrdersSection = new OrdersSection();
            ProductsSection = new ProductsSection();
            SuppliesSection = new SuppliesSection();
            MainFrame.Content = OrdersSection;
            Login login = new Login();
            if (login.ShowDialog() == true)
            {

            }
        }

        private void GoToOrdersSection(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = OrdersSection;
            SuppliesSectionButton.Background = new SolidColorBrush(Color.FromArgb(255, 36, 40, 46));
            OrdersSectionButton.Background = new SolidColorBrush(Color.FromArgb(255, 0, 120, 215));
            ProductsSectionButton.Background = new SolidColorBrush(Color.FromArgb(255, 36, 40, 46));
            db = new ApplicationContext();
            db.Products.Load();
            List<Product> l = db.Products.Local.ToList();
            ProductsSection.ProductsList = l;
            OrdersSection.ProductsList.Clear();
            for (int i = 0; i < ProductsSection.ProductsList.Count; i++)
            {
                OrdersSection.ProductsList.Add(ProductsSection.ProductsList[i]);
            }
            CollectionViewSource.GetDefaultView(OrdersSection.productsList.ItemsSource).Refresh();
        }
        private void GoToProductsSection(object sender, RoutedEventArgs e)
        {
            SuppliesSectionButton.Background = new SolidColorBrush(Color.FromArgb(255, 36, 40, 46));
            OrdersSectionButton.Background = new SolidColorBrush(Color.FromArgb(255, 36, 40, 46));
            ProductsSectionButton.Background = new SolidColorBrush(Color.FromArgb(255, 0, 120, 215));
            MainFrame.Content = ProductsSection;
            db = new ApplicationContext();
            db.Products.Load();
            List<Product> l = db.Products.Local.ToList();
            ProductsSection.ProductsList.Clear();
            foreach(Product product in l)
            {
                ProductsSection.ProductsList.Add(product);
            }
            ProductsSection.productsList.ItemsSource = ProductsSection.ProductsList;
            CollectionViewSource.GetDefaultView(ProductsSection.productsList.ItemsSource).Refresh();
        }
        private void GoToSuppliesSection(object sender, RoutedEventArgs e)
        {
            SuppliesSectionButton.Background = new SolidColorBrush(Color.FromArgb(255, 0, 120, 215));
            OrdersSectionButton.Background = new SolidColorBrush(Color.FromArgb(255, 36, 40, 46));
            ProductsSectionButton.Background = new SolidColorBrush(Color.FromArgb(255, 36, 40, 46));
            MainFrame.Content = SuppliesSection;
        }
        
    }
}
