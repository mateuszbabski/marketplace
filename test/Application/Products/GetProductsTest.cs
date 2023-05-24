using Application.Common.Exceptions;
using Application.Features.Products;
using Application.Features.Products.GetAllProducts;
using Application.Features.Products.GetProductById;
using Domain.Shops.Entities.Products;
using Domain.Shops.Entities.Products.Repositories;
using FluentAssertions;
using Moq;

namespace UnitTest.Application.Products
{
    public class GetProductsTest
    {
        private readonly GetAllProductsQueryHandler _sut;
        private readonly Mock<IProductRepository> _productRepository = new();
        public GetProductsTest()
        {
            _sut = new GetAllProductsQueryHandler(_productRepository.Object);
        }

        [Fact]
        public async Task GetProducts_ReturnsIEnumerableProductDto_IfExists()
        {
            var productList = new ProductList();

            var command = new GetAllProductsQuery();

            _productRepository.Setup(p => p.GetAllProducts()).ReturnsAsync(productList.Products);

            var result = await _sut.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<ProductDto>>(result);
        }

        [Fact]
        public async Task GetProducts_ThrowsNotFoundException_IfDontExist()
        {
            var command = new GetAllProductsQuery();

            _productRepository.Setup(p => p.GetAllProducts()).Throws(new NotFoundException("Products not found"));

            var result = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));

            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Products not found", result.Message);
        }
    }
}
