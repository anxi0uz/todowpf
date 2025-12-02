using todowpf.Models;

namespace todowpf.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> Login(AuthRequest request);
    }
}