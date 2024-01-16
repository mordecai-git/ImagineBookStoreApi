using ImagineBookStore.Core.Models.Input;
using ImagineBookStore.Core.Models.Utilities;

namespace ImagineBookStore.Core.Interfaces;

public interface IAuthService
{
    Task<Result> CreateUser(RegisterModel model);

    Task<Result> AuthenticateUser(LoginModel model);

    Task<Result> Logout(string userReference);
}