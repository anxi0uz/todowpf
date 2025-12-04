namespace todowpf.Services
{
    public interface ITokenStorage
    {
        string Token { get; set; }
        int UserId { get; set; }

        void Clear();
    }
}