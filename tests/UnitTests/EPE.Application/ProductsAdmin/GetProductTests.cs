using EPE.Application.ProductsAdmin;
using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using Moq;
using System;
using Xunit;
using static EPE.Application.ProductsAdmin.GetProduct;

namespace UnitTests.EPE.Application.ProductsAdmin
{
    public class GetProductTests
    {
        private readonly Mock<IProductManager> _productManagerMock = new();
        private readonly GetProduct _getProduct;

        public GetProductTests()
        {
            _getProduct = new(_productManagerMock.Object);
        }

        [Fact]
        public void ValidId_ShouldSucced()
        {
            // Arrange
            var num = 1;
            var name = "asd";
            var vm = new ProductViewModel
            {
                Id = num,
                Name = name,
                Description = "aa",
                Value = 1,
            };

            _productManagerMock
                .Setup(x => x.GetProductById(
                    num,
                    It.IsAny<Func<Product, ProductViewModel>>()))
                .Returns(vm);

            // Act
            var product = _getProduct.Do(num); 

            // Assert
            Assert.True(product != null);
            Assert.Equal(num, product.Id);
            Assert.Equal(name, product.Name);
        }

        [Fact]
        public void InvalidId_ShouldFail()
        {
            // Arrange
            _productManagerMock
                .Setup(x => x.GetProductById(
                    It.IsAny<int>(),
                    It.IsAny<Func<Product, ProductViewModel>>()))
                .Returns(() => null);

            // Act
            var product = _getProduct.Do(1);

            // Assert
            Assert.True(product == null);
        }
    }
}
