using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Store.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }

        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Discription { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }
    }
}