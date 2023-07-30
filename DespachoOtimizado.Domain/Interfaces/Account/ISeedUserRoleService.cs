namespace DespachoOtimizado.Domain.Interfaces.Account
{
    public interface ISeedUserRoleService
    {
        Task SeedUsers();
        Task SeedRoles();
    }
}