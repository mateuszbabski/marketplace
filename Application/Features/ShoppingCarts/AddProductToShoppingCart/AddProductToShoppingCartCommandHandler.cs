using Application.Common.Interfaces;
using Domain.Customers.Entities.ShoppingCarts;
using Domain.Customers.Entities.ShoppingCarts.Repositories;
using Domain.Shops.Entities.Products.Repositories;
using MediatR;

namespace Application.Features.ShoppingCarts.AddProductToShoppingCart
{
    public class AddProductToShoppingCartCommandHandler : IRequestHandler<AddProductToShoppingCartCommand, Guid>
    {
        private readonly ICurrentUserService _userService;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrencyConverter _currencyConverter;

        public AddProductToShoppingCartCommandHandler(ICurrentUserService userService,
                                                      IShoppingCartRepository shoppingCartRepository,
                                                      IProductRepository productRepository,
                                                      IUnitOfWork unitOfWork,
                                                      ICurrencyConverter currencyConverter)
        {
            _userService = userService;
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _currencyConverter = currencyConverter;
        }

        public async Task<Guid> Handle(AddProductToShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var customerId = _userService.UserId;
            var product = await _productRepository.GetById(request.ProductId);
            var shoppingCart = await ReturnOrCreateNewShoppingCart(customerId, product.Price.Currency);

            var productConvertedPrice = await _currencyConverter.GetConvertedPrice(product.Price.Amount,
                                                                                   product.Price.Currency,
                                                                                   shoppingCart.TotalPrice.Currency);

            shoppingCart.AddProductToShoppingCart(product, request.Quantity, productConvertedPrice);

            await _unitOfWork.CommitAsync();

            return shoppingCart.Id;
        }

        private async Task<ShoppingCart> ReturnOrCreateNewShoppingCart(Guid customerId, string currency)
        {
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByCustomerId(customerId);

            if (shoppingCart == null)
            {
                var newShoppingCart = ShoppingCart.CreateShoppingCart(customerId, currency);
                await _shoppingCartRepository.Create(newShoppingCart);
                return newShoppingCart;
            }

            return shoppingCart;
        }
    }
}
