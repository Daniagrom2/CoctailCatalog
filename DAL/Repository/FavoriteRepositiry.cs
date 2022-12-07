using System.Data.Entity;
using DAL.Context;

namespace DAL.Repository
{
    public class FavoriteRepositiry : BaseRepository<Favorite>
    {
        public FavoriteRepositiry(DbContext context) : base(context)
        {
        }
    }
}
