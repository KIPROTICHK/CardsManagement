namespace CardsManagement.Application.Service.Interface
{
    public interface ISeedDataService
    {
        Task SeedRoles();
        Task SeedAdmin();
        Task SeedUser();
    }

}
