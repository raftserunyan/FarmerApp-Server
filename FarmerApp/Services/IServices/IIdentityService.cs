using FarmerApp.Models;
using FarmerApp.Models.ViewModels.RequestModels;

namespace FarmerApp.Services.IServices
{
    public interface IIdentityService
    {
        AuthenticationResponse Login(LoginRequest loginRequest);
        AuthenticationResponse Refresh(RefreshTokenRequest refreshTokenRequest);
        void Logout(int userId);
        void ChangePassword(int userId, ChangePasswordRequest changePasswordRequest);
    }
}
