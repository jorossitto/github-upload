using System;
using Common;
using ACM.BL;


namespace Application.Data
{
    public class Product : EntityBase, ILoggable
    {
        public Product()
        {
            
        }

        public Product(int productId)
        {
            ProductId = productId;
        }

        public decimal? CurrentPrice { get; set; }
        public string ProductDescription { get; set; }
        public int ProductId { get; private set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbnailUrl { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }

        private string _name;
        public string Name
        {
            get
            {
                return _name;//.InsertSpaces();
            }
            set
            {
                _name = value;
            }
        }

        public float ListPrice { get; set; }

        public float GetPrice(ICustomer customer)
        {
            if (customer.IsGold)
                return ListPrice * 0.7f;

            return ListPrice;
        }

        public string Log() => $"Product: {Name} Detail: {ProductDescription} Status: {EntityState.ToString()}";
        public override string ToString() => Name;

        public override bool Validate()
        {
            var isValid = true;

            if (string.IsNullOrWhiteSpace(Name)) isValid = false;
            if (CurrentPrice == null) isValid = false;

            return isValid;
        }

        public static Product DefaultTestProduct()
        {
            return new Product(2)
            {
                Name = "Sunflowers",
                ProductDescription = "Assorted",
                CurrentPrice = 15.96M
            };
        }
    }
}
