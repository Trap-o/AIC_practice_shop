using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace AIC_shop.screens
{
    /// <summary>
    /// Interaction logic for Sell.xaml
    /// </summary>
    public partial class Sell : Window
    {
        private DataContext context { get; set; }

        public Sell()
        {
            context = new DataContext();
            InitializeComponent();
            UpdateData();
        }

        public void Window_Closed(object sender, EventArgs e)
        {
            UpdateData();
        }

        public void UpdateData()
        {
            List<Receipt> DatabaseReceipts = context.Receipts.Include(receipt => receipt.Products).Include(receipt => receipt.ReceiptProduct).ToList();
            ReceiptsItemList.ItemsSource = DatabaseReceipts;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ReceiptsItemList.SelectedItem is Receipt selectedReceipt)
            {
                context.Receipts.Remove(selectedReceipt);
                context.SaveChanges();
                UpdateData();
            }
            else
            {
                MessageBox.Show("Select some row", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ReceiptsItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Receipt receipt = (Receipt)e.AddedItems[0];
                ReceiptProductsItemList.ItemsSource = receipt.ReceiptProduct;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Purchase purchase = new Purchase();
            purchase.Closed += Window_Closed;
            purchase.ShowDialog();
        }
    }
}
