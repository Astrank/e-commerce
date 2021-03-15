using EPE.Application.ProductsAdmin;
using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using Moq;
using System;
using Xunit;

namespace UnitTests.EPE.Application.ProductsAdmin
{
    public class CreateProductTests
    {
        private readonly Mock<IProductManager> _productManager = new Mock<IProductManager>();
        private readonly CreateProduct _createProduct;

        public CreateProductTests()
        {
            _createProduct = new CreateProduct(_productManager.Object);
        }

        [Fact]
        public void InvalidRequestShouldFail()
        {
            var createProductResult = _createProduct.Do(null);

            Assert.False(createProductResult.IsCompletedSuccessfully);
        }
    }
}