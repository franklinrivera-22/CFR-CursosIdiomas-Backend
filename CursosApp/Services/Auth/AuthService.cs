using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using CursosApp.Entities;
using CursosApp.Dtos.Common;
using CursosApp.Dtos.Auth;
using CursosApp.Constants;
using CursosApp.Services.Auth;

namespace CursosApp.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(
            SignInManager<UserEntity> signInManager,
            UserManager<UserEntity> userManager,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto)
{
    var user = await _userManager.FindByEmailAsync(dto.Email);

    if (user == null)
    {
        return new ResponseDto<LoginResponseDto>
        {
            StatusCode = HttpStatusCode.UNAUTHORIZED,
            Status = false,
            Message = "Correo o contraseña incorrectos."
        };
    }

    var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);

    if (!isPasswordValid)
    {
        return new ResponseDto<LoginResponseDto>
        {
            StatusCode = HttpStatusCode.UNAUTHORIZED,
            Status = false,
            Message = "Correo o contraseña incorrectos."
        };
    }

    var roles = await _userManager.GetRolesAsync(user);

    var claims = BuildClaims(user, roles);
    var jwt = BuildToken(claims);

    return new ResponseDto<LoginResponseDto>
    {
        StatusCode = HttpStatusCode.OK,
        Status = true,
        Message = "Autenticación satisfactoria.",
        Data = new LoginResponseDto
        {
            Email = user.Email,
            FullName = $"{user.FirstName} {user.LastName}",
            Roles = roles.ToList(),
            Token = new JwtSecurityTokenHandler().WriteToken(jwt)
        }
    };
}

         public async Task<ResponseDto<LoginResponseDto>> RegisterAsync(RegisterDto dto)
        {
            var existing = await _userManager.FindByEmailAsync(dto.Email);
            if (existing is not null)
            {
                return new ResponseDto<LoginResponseDto>
                {
                    StatusCode = HttpStatusCode.CONFLICT,
                    Status = false,
                    Message = "Ya existe una cuenta con ese correo electrónico."
                };
            }

            var user = new UserEntity
            {
                UserName = dto.Email,
                Email = dto.Email,
                EmailConfirmed = true,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                return new ResponseDto<LoginResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = string.Join(" ", result.Errors.Select(e => e.Description))
                };
            }

            await _userManager.AddToRoleAsync(user, RolesConstant.NORMAL_USER);

            var roles = await _userManager.GetRolesAsync(user);
            var claims = BuildClaims(user, roles);
            var jwt = BuildToken(claims);

            return new ResponseDto<LoginResponseDto>
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Cuenta creada correctamente.",
                Data = new LoginResponseDto
                {
                    Email = user.Email,
                    FullName = $"{user.FirstName} {user.LastName}",
                    Roles = roles.ToList(),
                    Token = new JwtSecurityTokenHandler().WriteToken(jwt)
                }
            };
        }


        private static List<Claim> BuildClaims(UserEntity user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken BuildToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            return new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["JWT:Expires"])),
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
        }

    }
}