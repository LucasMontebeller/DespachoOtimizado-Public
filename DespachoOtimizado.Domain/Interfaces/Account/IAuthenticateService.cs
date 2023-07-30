namespace DespachoOtimizado.Domain.Interfaces.Account
{
    public interface IAuthenticateService
    {
        Task<bool> Authenticate(string email, string password);
        Task<Tuple<bool, IEnumerable<string>>> RegisterUser(string name, string email, string phone, string password);
        Task Logout();
        Task<bool> ExistsUser(string email);
    }
}