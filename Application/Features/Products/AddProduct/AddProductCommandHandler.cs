using Application.Common.Interfaces;
using Domain.Shared.ValueObjects;
using Domain.Shop;
using Domain.Shop.Entities.Products;
using Domain.Shop.Entities.Products.Factories;
using Domain.Shop.Entities.Products.Repositories;
using Domain.Shop.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.AddProduct
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductFactory _productFactory;
        private readonly ICurrentUserService _userService;

        public AddProductCommandHandler(IProductRepository productRepository,
                                        IProductFactory productFactory,
                                        ICurrentUserService userService)
        {
            _productRepository = productRepository;
            _productFactory = productFactory;
            _userService = userService;
        }


        public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var shopId = _userService.UserId;
            var validator = new AddProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new Exception("Validation error");
            //throw new ValidationException();

            //TODO validation exception
            var price = MoneyValue.Of(request.Amount, request.Currency);
            
            var product = _productFactory.Create(Guid.NewGuid(),
                                                 request.ProductName,
                                                 request.ProductDescription,
                                                 price,
                                                 request.Unit,
                                                 shopId);

            await _productRepository.Add(product);
            
            return product.Id;
        }
    }
}
