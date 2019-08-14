using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Moq;
using ACM.BL;
using TestNinja.Fundamentals;

namespace ACM.Tests
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        [Test]
        public void RetrieveTest()
        {
            var productRepository = new ProductRepository();
            Product expected = Product.DefaultTestProduct();

            var actual = productRepository.Retrieve(2);

            Assert.AreEqual(expected.CurrentPrice, actual.CurrentPrice);
            Assert.AreEqual(expected.ProductDescription, actual.ProductDescription);
            Assert.AreEqual(expected.ProductName, actual.ProductName);
        }

        [Test]
        public void SaveTestValid()
        {
            var productRepository = new ProductRepository();
            var updatedProduct = new Product(2)
            {
                CurrentPrice = 18M,
                ProductDescription = "Assorted",
                ProductName = "Sunflowers",
                HasChanges = true
            };

            var actual = productRepository.Save(updatedProduct);

            Assert.AreEqual(true, actual);
        }
        [Test]
        public void SaveTestMissingPrice()
        {
            var productRepository = new ProductRepository();
            var updatedProduct = new Product(2)
            {
                CurrentPrice = null,
                ProductDescription = "Assorted",
                ProductName = "Sunflowers",
                HasChanges = true
            };

            var actual = productRepository.Save(updatedProduct);

            Assert.AreEqual(false, actual);
        }
    }

}
