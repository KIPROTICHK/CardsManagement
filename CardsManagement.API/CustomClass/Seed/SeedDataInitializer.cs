
using CardsManagement.Application.Service.Interface;

namespace CardsManagement.API.CustomClass.Seed
{
    public static class SeedDataInitializer
    {
        public static async Task SeedAccount(this IApplicationBuilder app)
        {
            var provider = app.ApplicationServices;
            var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();

            using var scope = scopeFactory.CreateScope();
            try
            {
                var seedDataService = scope.ServiceProvider.GetRequiredService<ISeedDataService>();

                await seedDataService.SeedRoles();
                await seedDataService.SeedAdmin();
                await seedDataService.SeedUser();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

    }
}
