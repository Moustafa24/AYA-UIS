namespace AYA_UIS.Application.Contracts
{
    public interface IServiceManager
    {
        IAuthenticationService AuthenticationService { get; }
        IRoleService RoleService { get; }
    }
}
