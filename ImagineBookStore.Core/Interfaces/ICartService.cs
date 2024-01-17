using ImagineBookStore.Core.Models.Input;
using ImagineBookStore.Core.Models.Utilities;

namespace ImagineBookStore.Core.Interfaces
{
    public interface ICartService
    {
        Task<Result> AddToCart(CartModel model);

        Task<Result> RemoveFromCart(int cartId);

        Task<Result> ListCarts();
    }
}
