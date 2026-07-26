using CursosApp.Dtos.Auth;
using CursosApp.Dtos.Common;

namespace CursosApp.Services.Auth
{
    public interface IAuthService
    {
        Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto);
        Task<ResponseDto<LoginResponseDto>> RegisterAsync(RegisterDto dto);
    }
}