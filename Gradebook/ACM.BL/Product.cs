using System;
using Common;

namespace ACM.BL
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

        private string _productName;
        public string ProductName
        {
            get
            {
                return _productName.InsertSpaces();
            }
            set
            {
                _productName = value;
            }
        }

        public float ListPrice { get; set; }

        public float GetPrice(ICustomer customer)
        {
            if (customer.IsGold)
                return ListPrice * 0.7f;

            return ListPrice;
        }

        public string Log() => $"Product: {ProductName} Detail: {ProductDescription} Status: {EntityState.ToString()}";
        public override string ToString() => ProductName;

        public override bool Validate()
        {
            var isValid = true;

            if (string.IsNullOrWhiteSpace(ProductName)) isValid = false;
            if (CurrentPrice == null) isValid = false;

            return isValid;
        }

        public static Product DefaultTestProduct()
        {
            return new Product(2)
            {
                ProductName = "Sunflowers",
                ProductDescription = "Assorted",
                CurrentPrice = 15.96M
            };
        }
    }
}
