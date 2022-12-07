using System.Data.Entity;
using BLL.DTO;
using BLL.Service;
using DAL.Context;
using DAL.Repository;
using Ninject.Modules;

namespace BLL.Module
{
    public class CoctailsModule:NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<CoctailsContext>();
            Bind<CoctailRepository>().To<CoctailRepository>();
            Bind<FavoriteRepositiry>().To<FavoriteRepositiry>();
            Bind<CoctailService>().To<CoctailService>();
            Bind<FavoriteService>().To<FavoriteService>();
           
          
        }
    }
    }
