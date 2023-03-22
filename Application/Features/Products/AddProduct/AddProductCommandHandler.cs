using Domain.Shared.ValueObjects;
using Domain.Shop.Entities.Products.Factories;
using Domain.Shop.Entities.Products.Repositories;
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

        public AddProductCommandHandler(IProductRepository productRepository, IProductFactory productFactory)
        {
            _productRepository = productRepository;
            _productFactory = productFactory;
        }
        public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var shopId = "9EA4AF6D-895B-4B4C-A231-F758903EBE66";
            var validator = new AddProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                throw new Exception("Validation error");
            //throw new ValidationException();

            //TODO validation exception, and shopid mapping
            var price = MoneyValue.Of(request.Amount, request.Currency);

            var product = _productFactory.Create(Guid.NewGuid(),
                                                 request.ProductName,
                                                 request.ProductDescription,
                                                 price,
                                                 request.Unit,
                                                 //shopId
                                                 Guid.Parse(shopId));

            await _productRepository.Add(product);

            return product.Id;
        }
    }
}
