using Cashbox.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cashbox.Views
{
    /// <summary>
    /// Логика взаимодействия для OrdersSection.xaml
    /// </summary>
    public partial class OrdersSection : Page
    {
        public ApplicationContext db { get; set; }
        public List<Product> ProductsList { get; set; }

        public List<Product> OrderList { get; set; }
        public OrdersSection()
        {
            InitializeComponent();
            OrderList = new List<Product>();
            ProductsList = new List<Product>();
            db = new ApplicationContext();
            db.Products.Load();
            foreach(Product product in db.Products.Local.ToList())
            {
                ProductsList.Add(product);
            }
            productsList.ItemsSource = ProductsList;
            orderList.ItemsSource = OrderList;
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

        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(productsList.ItemsSource).Refresh();
        }

        private void CancelOrder_Click(object sender, RoutedEventArgs e)
        {
            List<Product> pl = db.Products.Local.ToList();
            foreach (Product product in ProductsList)
            {
                foreach(Product product1 in OrderList)
                {
                    if (product1.Id == product.Id)
                    {
                        product.Quantity += product1.Quantity;
                    }
                }
            }
            OrderList.Clear();
            CollectionViewSource.GetDefaultView(productsList.ItemsSource).Refresh();
            CollectionViewSource.GetDefaultView(orderList.ItemsSource).Refresh();
            totalPrice.Text = "0";
        }
        private void FinishOrder_Click(object sender, RoutedEventArgs e)
        {
            if (new SaveCheck(OrderList).ShowDialog() == true)
            {
                db = new ApplicationContext();
                db.Products.Load();
                db.Consignments.Load();
                List<Product> productsList = db.Products.Local.ToList();
                BindingList<Consignment> consignmentsList = db.Consignments.Local.ToBindingList();
                for (int i = 0; i < OrderList.Count; i++)
                {
                    double quantity = (double)OrderList[i].Quantity;
                    Product product = db.Products.Find(OrderList[i].Id);
                    product.Quantity -= quantity;
                    int length = consignmentsList.Count;
                    int counter = 0;
                    for (int j = 0; j < length; j++)
                    {
                        if (consignmentsList[counter].ProductId == OrderList[i].Id)
                        {
                            
                            if (quantity >= consignmentsList[counter].Quantity)
                            {
                                quantity -= (double)consignmentsList[counter].Quantity;
                                consignmentsList.RemoveAt(counter);
                            }
                            else
                            {
                                consignmentsList[counter].Quantity -= quantity;
                                break;
                            }
                            if (quantity <= 0)
                            {
                                break;
                            }
                        }
                        counter++;
                    }

                }
                totalPrice.Text = "0";
                OrderList.Clear();
                CollectionViewSource.GetDefaultView(orderList.ItemsSource).Refresh();
                db.SaveChanges();
            }
            
        }

        private void ProductChoose_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CollectionViewSource.GetDefaultView(orderList.ItemsSource).Refresh();
            Product product = new Product((Product)productsList.SelectedItem);
            OrderProductQuantity orderProductQuantity = new OrderProductQuantity();
            if (orderProductQuantity.ShowDialog() == true)
            {
                if (product != null)
                {
                    if (((Product)(productsList.SelectedItem)).Quantity >= orderProductQuantity.ProductsQuantity)
                    {
                        if (orderProductQuantity.Sale <= 100 && 0 <= orderProductQuantity.Sale)
                        {
                            ProductsList[productsList.SelectedIndex].Quantity -= orderProductQuantity.ProductsQuantity;
                            product.Quantity = orderProductQuantity.ProductsQuantity;
                            product.Price = (double)((product.Price / 100) * (100 - orderProductQuantity.Sale) * product.Quantity);
                            OrderList.Add(product);
                            CollectionViewSource.GetDefaultView(orderList.ItemsSource).Refresh();
                            totalPrice.Text = (Int32.Parse(totalPrice.Text) + product.Price).ToString();
                        }
                        else
                        {

                        }
                        
                    }
                    else
                    {

                    }
                    
                }
            }
        }

        private void Del_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (productsList.SelectedItem == null) return;
                OrderList.RemoveAt(productsList.SelectedIndex);
                Product product = productsList.SelectedItem as Product;
                CollectionViewSource.GetDefaultView(orderList.ItemsSource).Refresh();
                totalPrice.Text = (Int32.Parse(totalPrice.Text) - product.Price).ToString();
            }

        }
    }
}
