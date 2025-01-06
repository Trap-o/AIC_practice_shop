using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Windows;
using System.Windows.Controls;


namespace AIC_shop
{
    /// <summary>
    /// Interaction logic for Purchase.xaml
    /// </summary>
    public partial class Purchase : Window
    {
        private IEnumerable productsItemList;
        private Receipt receipt {  get; set; }
        private DataContext context { get; set; }

        public Purchase()
        {
            InitializeComponent();
            context = new DataContext();
            receipt = new Receipt();
            context.Receipts.Add(receipt);
            receipt.ReceiptProduct = new List<ReceiptProduct>();
            receipt.Amount = 0;
            receipt.Products = new List<Product>();
            receipt.DateTime = DateTime.Now;
            UpdateData();
        }

        public void UpdateData()
        {
            context.SaveChanges();
            List<Category> DatabaseCategories = context.Categories.Include(category => category.Products).ToList();
            CateforyItemList.ItemsSource = DatabaseCategories;
            productsItemList = ProductsItemList.ItemsSource;
            ProductsItemList.ItemsSource = null;
            ProductsItemList.ItemsSource = productsItemList;
            ReceiptProductsItemList.ItemsSource = null;
            ReceiptProductsItemList.ItemsSource = receipt.ReceiptProduct;
            Amount.Content = receipt.Amount;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Product product = (Product)ProductsItemList.SelectedItem;
            if (product != null )
            {
                int quantity = Convert.ToInt32(QuantitySlider.Value);
                int amountForProduct = quantity * product.Price;

                product.Quantity -= quantity;
                receipt.ReceiptProduct.Add(new ReceiptProduct
                {
                    Product = product,
                    Quantity = quantity,
                    Amount = amountForProduct
                });
                receipt.Amount += amountForProduct;
            }
            UpdateData();
        }

        private void CategoryItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Category category = (Category)e.AddedItems[0];
            ProductsItemList.ItemsSource = category.Products;
        }

        private void ProductsItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
            {
                Product product = (Product)e.AddedItems[0];
                QuantitySlider.Maximum = product.Quantity;
            }
        }

        private void QuantitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Product product = (Product)ProductsItemList.SelectedItem;
            if(product != null)
            {
                AmountForProduct.Content = e.NewValue * product.Price;
            }
        }

        private void Button_click_1(object sender, RoutedEventArgs e)
        {
            ReceiptProduct receiptProduct = (ReceiptProduct)ReceiptProductsItemList.SelectedItem;
            receipt.Amount -= receiptProduct.Amount;
            receiptProduct.Product.Quantity += receiptProduct.Quantity;
            receipt.ReceiptProduct.Remove(receiptProduct);
            UpdateData();
        }

        private void Button_click_3(object sender, RoutedEventArgs e)
        {
            foreach(ReceiptProduct receiptProduct in receipt.ReceiptProduct)
            {
                receiptProduct.Product.Quantity += receiptProduct.Quantity;
            }
            context.Receipts.Remove(receipt);
            UpdateData();
            this.Close();
        }

        private void Button_click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
