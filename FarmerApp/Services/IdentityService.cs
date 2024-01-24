using FarmerApp.Models;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Repository.IRepository;
using FarmerApp.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FarmerApp.Services
{
    public class IdentityService : IIdentityService
    {
        const string IdClaimName = "NameIdentifier";

        int accessExpiryMinutes;
        int refreshExpiryMinutes;
        string clientSecretKey;
        string clientSecretKeyForRefresh;

        private readonly IUserRepository _userRepository;
        private readonly ApplicationSettings _settings;

        public IdentityService(IUserRepository userRepository, ApplicationSettings settings)
        {
            _userRepository = userRepository;
            _settings = settings;

            clientSecretKey = _settings.JwtSecretKey;
            clientSecretKeyForRefresh = _settings.JwtRefreshSecretKey;
            accessExpiryMinutes = _settings.AccessTokenExpiryMinutes;
            refreshExpiryMinutes = _settings.RefreshTokenExpiryMinutes;
        }

        public AuthenticationResponse Login(LoginRequest loginRequest)
        {
            var user = _userRepository.GetByEmail(loginRequest.UserName);

            if (user == null)
                throw new BadHttpRequestException("Invalid credentials");

            var verificationResult = new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, loginRequest.Password);
            if (verificationResult != PasswordVerificationResult.Success)
                throw new BadHttpRequestException("Invalid credentials");

            var accessToken = GenerateJwtToken(user, clientSecretKey, accessExpiryMinutes);
            var refreshToken = GenerateJwtToken(user, clientSecretKeyForRefresh, refreshExpiryMinutes);

            user.RefreshToken = refreshToken;
            _userRepository.Update(user);

            return new AuthenticationResponse
            {
                Name = user.Name,
                Username = user.Email,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public void Logout(int userId)
        {
            var user = _userRepository.GetById(userId);
            if (user != null)
                throw new BadHttpRequestException("No user was found");

            if (user.RefreshToken == default)
                throw new BadHttpRequestException("User is not logged in");

            user.RefreshToken = default;
            _userRepository.Update(user);
        }

        public void ChangePassword(int userId, ChangePasswordRequest changePasswordRequest)
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
                throw new BadHttpRequestException("No user was found");

            var passwordHasher = new PasswordHasher<User>();
            var oldPasswordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, changePasswordRequest.OldPassword);
            if (oldPasswordVerificationResult != PasswordVerificationResult.Success)
                throw new BadHttpRequestException("Invalid old password");

            user.Password = passwordHasher.HashPassword(user, changePasswordRequest.NewPassword);
            _userRepository.Update(user);
        }

        public AuthenticationResponse Refresh(RefreshTokenRequest refreshTokenRequest)
        {
            var claimsPrincipal = GetClaimsPrincipalFromJwtToken(refreshTokenRequest.RefreshToken, clientSecretKeyForRefresh);

            var user = _userRepository.GetById(int.Parse(claimsPrincipal.Claims.FirstOrDefault(x => x.Type == IdClaimName).Value));

            if (user == null)
                throw new BadHttpRequestException("Could not get user from access token");

            if (refreshTokenRequest.RefreshToken != user.RefreshToken)
                throw new BadHttpRequestException("Invalid refresh token");

            if (IsTokenExpired(refreshTokenRequest.RefreshToken))
            {
                user.RefreshToken = null;
                _userRepository.Update(user);

                throw new BadHttpRequestException("Invalid refresh token");
            }

            var refreshToken = GenerateJwtToken(user, clientSecretKeyForRefresh, refreshExpiryMinutes);
            user.RefreshToken = refreshToken;
            _userRepository.Update(user);

            return new AuthenticationResponse
            {
                Name = user.Name,
                Username = user.Email,
                AccessToken = GenerateJwtToken(user, clientSecretKey, accessExpiryMinutes),
                RefreshToken = refreshToken
            };
        }

        private string GenerateJwtToken(User user, string secretKey, int expiryMinutes)
        {
            // Create claims for the token
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Email),
                new Claim(IdClaimName, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, "example_user"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            // Create the signing credentials using the secret key
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // Create the token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
                SigningCredentials = signingCredentials
            };

            // Create the token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Generate the token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Serialize the token to a string
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        private ClaimsPrincipal GetClaimsPrincipalFromJwtToken(string jwtToken, string secretKey)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false, // Set to true if you want to validate the issuer
                ValidateAudience = false, // Set to true if you want to validate the audience
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            // Read the token and parse its claims
            ClaimsPrincipal claimsPrincipal = null;

            try
            {
                claimsPrincipal = tokenHandler.ValidateToken(jwtToken, validationParameters, out var validatedToken);
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException("Invalid token");
            }

            return claimsPrincipal;
        }

        private bool IsTokenExpired(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;

            if (jwtSecurityToken == null || jwtSecurityToken.ValidTo == default)
            {
                // Invalid token or missing expiration claim
                return true;
            }

            var expirationDateTime = jwtSecurityToken.ValidTo;
            var currentDateTime = DateTime.UtcNow;

            return currentDateTime > expirationDateTime;
        }
    }
}
