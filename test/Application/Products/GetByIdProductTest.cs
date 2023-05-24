using Application.Common.Exceptions;
using Application.Features.Products;
using Application.Features.Products.GetProductById;
using Domain.Shops.Entities.Products.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Application.Products
{
    public class GetByIdProductTest
    {
        private readonly GetProductByIdQueryHandler _sut;
        private readonly Mock<IProductRepository> _productRepository = new();
        public GetByIdProductTest()
        {
            _sut = new GetProductByIdQueryHandler(_productRepository.Object);
        }

        [Fact]
        public async Task GetProductById_ReturnsProductDetailsDto_IfExists()
        {
            var productList = new ProductList();

            var command = new GetProductByIdQuery()
            {
                Id  = productList.Products.First().Id
            };

            _productRepository.Setup(p => p.GetById(command.Id)).ReturnsAsync(productList.Products.First());

            var result = await _sut.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<ProductDetailsDto>(result);
        }

        [Fact]
        public async Task GetProductById_ThrowsNotFoundException_IfDoesntExist()
        {
            var productList = new ProductList();

            var command = new GetProductByIdQuery()
            {
                Id = Guid.NewGuid()
            };

            _productRepository.Setup(p => p.GetById(command.Id)).Throws(new NotFoundException("Product not found"));

            var result = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));

            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Product not found", result.Message);
        }
    }
}
