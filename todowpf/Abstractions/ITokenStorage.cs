namespace todowpf.Services
{
    public interface ITokenStorage
    {
        string Token { get; set; }

        void Clear();
    }
}