using System.Data.Entity;
using DAL.Context;

namespace DAL.Repository
{
    public class CoctailRepository:BaseRepository<Coctail>
    {
        public CoctailRepository(DbContext context) : base(context)
        {
        }
    }
}