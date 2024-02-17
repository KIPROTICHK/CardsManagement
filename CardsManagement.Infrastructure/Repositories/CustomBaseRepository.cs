

namespace CardsManagement.Infrastructure.Repositories
{
    public class CustomBaseRepository(CoreContext context) : BaseRepository(context), ICustomBaseRepository
    {

    }
}
