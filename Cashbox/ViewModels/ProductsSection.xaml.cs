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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cashbox.Models;
using Cashbox.Views;

namespace Cashbox.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductsSection.xaml
    /// </summary>
    public partial class ProductsSection : Page
    {
        public ApplicationContext db { get; set; }
        public List<Product> ProductsList {get; set;}

        public ProductsSection()
        {
            InitializeComponent();
            db = new ApplicationContext();
            db.Products.Load();
            db.Inventories.Load();
            ProductsList = db.Products.Local.ToList();
            productsList.ItemsSource = ProductsList;
            inventoryHistory.ItemsSource = db.Inventories.Local.ToBindingList();
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

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductEditing productEditing = new ProductEditing(new Product());
            if (productEditing.ShowDialog() == true)
            {
                Product product = productEditing.Product;
                product.Quantity = 0;
                ProductsList.Add(product);
                CollectionViewSource.GetDefaultView(productsList.ItemsSource).Refresh();
                db.Products.Add(product);
                db.SaveChanges();
            }
            else
            {
                
            }
        }

        private void EditingProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductEditing productEditing = new ProductEditing(new Product((Product)productsList.SelectedItem));
            if (productEditing.ShowDialog() == true)
            {
                ProductsList.RemoveAt(productsList.SelectedIndex);
                Product product = db.Products.Find(productEditing.Product.Id);
                ProductsList.Insert(productsList.SelectedIndex, product);
                CollectionViewSource.GetDefaultView(productsList.ItemsSource).Refresh();
                if (product != null)
                {
                    product.Name = productEditing.Product.Name;
                    product.Code = productEditing.Product.Code;
                    product.Price = productEditing.Product.Price;
                    product.Unit = productEditing.Product.Unit;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        private void Del_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (productsList.SelectedItem == null) return;
                Product product = productsList.SelectedItem as Product;
                db.Products.Load();
                db.Consignments.Load();
                db.Products.Remove(db.Products.Find(product.Id));
                int counter = 0;
                while (db.Consignments.ToList().Count > counter)
                {
                    db.Consignments.Load();
                    if (db.Consignments.ToList().ElementAt(counter).ProductId == product.Id)
                    {
                        db.Consignments.Remove(db.Consignments.ToList().ElementAt(counter));
                        db.SaveChanges();
                        continue;
                    }
                    counter++;
                }
                
                db.SaveChanges();
                ProductsList.RemoveAt(productsList.SelectedIndex);
                CollectionViewSource.GetDefaultView(productsList.ItemsSource).Refresh();
                
            }
            
        }

        private void Inventory_Click(object sender, RoutedEventArgs e)
        {
            
            InventoryName inventoryName = new InventoryName();
            if (inventoryName.ShowDialog() == true)
            {
                //db = new ApplicationContext();
                db.Inventories.Load();
                db.Consignments.Load();
                db.Products.Load();
                string inventoryInfo = "";
                List<Consignment> consignmentsList = db.Consignments.Local.ToList();
                List<Product> products = db.Products.Local.ToList();
                foreach (Consignment consignment in consignmentsList)
                {
                    
                    Product product = db.Products.Find(consignment.ProductId);
                    inventoryInfo += $"Product name: {product.Name} | ";
                    inventoryInfo += $"Product price: {product.Price} | ";
                    inventoryInfo += $"Quantity: {consignment.Quantity} | ";
                    inventoryInfo += $"ShelfLife: {consignment.ShelfLife}";
                    if (consignment != consignmentsList[consignmentsList.Count - 1])
                    {
                        inventoryInfo += " ~ ";
                    }
                }
                db.Inventories.Add(new Inventory(inventoryName.Name != "" ? inventoryName.Name : "Inventory name", inventoryInfo, DateTime.Now.ToString()));
                db.SaveChanges();
                CollectionViewSource.GetDefaultView(inventoryHistory.ItemsSource).Refresh();
            }
            
        }

        private void inventoryHistory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string info = ((Inventory)(inventoryHistory.SelectedItem)).Info;
            string[] consignmentsInfo = info.Split('~');
            List<string> vs = new List<string>();
            for (int i = 0; i < consignmentsInfo.Length; i++)
            {
                string tempStr = "";
                foreach(string str in consignmentsInfo[i].Split('|'))
                {
                    tempStr += str + "\n";
                }
                vs.Add(tempStr);
            }
            InventoryInfo inventoryInfo = new InventoryInfo(vs);
            if (inventoryInfo.ShowDialog() == true)
            {
               
            }
        }
    }
}
