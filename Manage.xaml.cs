using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace AIC_shop
{
    /// <summary>
    /// Interaction logic for Manage.xaml
    /// </summary>
    public partial class Manage : Window
    {
        private DataContext context { get; set; }
        public Manage()
        {
            context = new DataContext();
            InitializeComponent();
            UpdateData();
        }

        public void UpdateData()
        {
            List<Product> DatabaseProducts = context.Products.Include(product => product.Category).ToList();
            ProductsItemList.ItemsSource = DatabaseProducts;

            List<Category> DatabaseCategories = context.Categories.ToList();
            CategoryItemList.ItemsSource = DatabaseCategories;

            CategoryComboBox.ItemsSource = DatabaseCategories;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            context.Categories.Add(new Category
            {
                Name = NameTextBox.Text,
                Description = DescriptonTextBox.Text,
            });
            context.SaveChanges();
            UpdateData();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Category selectedCategory = CategoryItemList.SelectedItem as Category;

            selectedCategory.Name = NameTextBox.Text;
            selectedCategory.Description = DescriptonTextBox.Text;
            context.SaveChanges();
            UpdateData();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (CategoryItemList.SelectedItem is Category selectedCategory)
            {
                context.Categories.Remove(selectedCategory);
                context.SaveChanges();
                UpdateData();
            }
            else
            {
                MessageBox.Show("Select some row", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var quantity = ProductQuantityTextBox.Text;
            var price = ProductPriceTextBox.Text;

            int.TryParse(quantity, out int intQuantity);
            int.TryParse(price, out int intPrice);

            context.Products.Add(new Product
            {
                Name = ProductNameTextBox.Text,
                Quantity = intQuantity,
                Price = intPrice,
                Category = CategoryComboBox.SelectedItem as Category,
                Description = ProductDescriptonTextBox.Text
            });
            context.SaveChanges();
            UpdateData();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (ProductsItemList.SelectedItem is Product selectedProduct)
            {
                var quantity = ProductQuantityTextBox.Text;
                var price = ProductPriceTextBox.Text;

                int.TryParse(quantity, out int intQuantity);
                int.TryParse(price, out int intPrice);

                selectedProduct.Name = ProductNameTextBox.Text;
                selectedProduct.Price = intPrice;
                selectedProduct.Quantity = intQuantity;
                selectedProduct.Category = CategoryComboBox.SelectedItem as Category;
                selectedProduct.Description = ProductDescriptonTextBox.Text;

                context.SaveChanges();
                UpdateData();
            }
            else
            {
                MessageBox.Show("Select some row", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (ProductsItemList.SelectedItem is Product selectedProduct)
            {
                context.Products.Remove(selectedProduct);

                context.SaveChanges();
                context.SaveChanges();
                UpdateData();
            }
            else
            {
                MessageBox.Show("Select some row", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
