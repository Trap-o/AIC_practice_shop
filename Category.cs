using System.ComponentModel.DataAnnotations;

namespace AIC_shop
{
    internal class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; }
    }
}
