using ImagineBookStore.Core.Models.App;
using ImagineBookStore.Core.Models.Utilities;

namespace ImagineBookStore.Core.Interfaces;

public interface ITokenGenerator
{
    Task<Result> GenerateJwtToken(User user);

    Task InvalidateToken(string userReference);
}