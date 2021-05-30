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
    /// Логика взаимодействия для SuppliesSection.xaml
    /// </summary>
    public partial class SuppliesSection : Page
    {
        public ApplicationContext db { get; set; }

        public SuppliesSection()
        {
            InitializeComponent();
            db = new ApplicationContext();
            db.Supplies.Load();
            suppliesList.ItemsSource = db.Supplies.Local.ToBindingList();
        }

        private void NewSupply_Click(object sender, RoutedEventArgs e)
        {
            AddSupply addSupply = new AddSupply(new Supply());
            if (addSupply.ShowDialog() == true)
            {
                Supply supply = addSupply.Supply;
                supply.Name = addSupply.SupplyName;
                supply.Description = addSupply.SupplyDescription;
                supply.DateTime = (DateTime.Now).ToString();
                supply.Info = ShapingSupplyInfo(supply.SupplyProducts);
                UpdateConsignments(supply.SupplyProducts, addSupply.ShelfLifes);
                UpdateProductsQuantity(supply.SupplyProducts);
                supply.SupplyProducts.Clear();
                db.Supplies.Add(supply);
                db.SaveChanges();
            }
        }

        private void ChooseSupply_Click(object sender, MouseButtonEventArgs e)
        {
            Supply supply = (Supply)(suppliesList.SelectedItem);
            if (supply != null)
            {
                SupplyInfo supplyInfo = new SupplyInfo(supply.Info);
                if (supplyInfo.ShowDialog() == true)
                {

                }
            }
            
        }

        private string ShapingSupplyInfo(List<Product> supplyProducts)
        {
            string supplyProductInfo = "";
            foreach(Product product in supplyProducts)
            {
                supplyProductInfo += $"Product name: {product.Name} ";
                supplyProductInfo += $"Product price: {product.Price} ";
                supplyProductInfo += $"Quantity: {product.Quantity} ,";
            }
            return supplyProductInfo;
        }

        private void UpdateConsignments(List<Product> supplyList, List<string> shelfLifes)
        {
            db.Consignments.Load();
            for(int i = 0; i < supplyList.Count; i++) 
            {
                Consignment consignment = new Consignment(supplyList[i].Id, supplyList[i].Quantity, shelfLifes[i]);
                db.Consignments.Add(consignment);
            }
            db.SaveChanges();
        }

        private void UpdateProductsQuantity(List<Product> productsList)
        {
            db.Products.Load();
            for (int i = 0; i < productsList.Count; i++)
            {
                Product product = db.Products.Find(productsList[i].Id);
                if (product.Quantity == null)
                {
                    product.Quantity = productsList[i].Quantity;
                }
                else
                {
                    product.Quantity += productsList[i].Quantity;
                }
                
                product.LastSupply = DateTime.Now.ToString();
            }
            db.SaveChanges();
            
        }
    }
}
