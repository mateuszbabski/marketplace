using MediatR;

namespace Application.Features.ShoppingCarts.ChangeShoppingCartCurrency
{
    public class ChangeShoppingCartCurrencyCommand : IRequest
    {
        public string Currency { get; set; }
    }
}
