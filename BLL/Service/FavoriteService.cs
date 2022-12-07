using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Repository;

namespace BLL.Service
{
    public class FavoriteService
    {
        private FavoriteRepositiry favoriteRepository;

        public FavoriteService(FavoriteRepositiry favoriteRepository)
        {
            this.favoriteRepository = favoriteRepository;
        }
        public async Task<IEnumerable<FavoriteDTO>> GetAllAsync() => await Task.Run(() => GetAll());

        public IEnumerable<FavoriteDTO> GetAll()
        {
            return favoriteRepository
                .GetAll()
                .Select(coctail => new FavoriteDTO()
                {
                    FavoriteId = coctail.FavoriteId,
               
                });
        }
        public FavoriteDTO Get(int id)
        {
            var coctail = favoriteRepository.Get(id);
            return new FavoriteDTO()
            {
                FavoriteId = coctail.FavoriteId,
              
            };
        }

        public async Task<FavoriteDTO> GetAsync(int id) => await Task<FavoriteDTO>.Run(() => Get(id));

        public void Delete(FavoriteDTO coctailDto)
        {
            var coctailToDelete = favoriteRepository.Get(coctailDto.FavoriteId);
            favoriteRepository.Delete(coctailToDelete);
            favoriteRepository.Save();
        }

        public async Task DeleteAsync(FavoriteDTO coctailDto) => await Task.Run(() => Delete(coctailDto));
        public void Update(FavoriteDTO coctailDto)
        {
            var coctailToUpdate = favoriteRepository.Get(coctailDto.FavoriteId);
            coctailToUpdate.FavoriteId = coctailDto.FavoriteId;
            favoriteRepository.CreateOrUpdate(coctailToUpdate);
            favoriteRepository.Save();
        }

        public async Task UpdateAsync(FavoriteDTO favoriteDto) => await Task.Run(() => Update(favoriteDto));

        public FavoriteDTO Create(FavoriteDTO coctailDto)
        {
            var coctail = new DAL.Context.Favorite()
            {
                FavoriteId = coctailDto.FavoriteId,
            };
            favoriteRepository.CreateOrUpdate(coctail);
            favoriteRepository.Save();
            coctailDto.FavoriteId = coctail.FavoriteId;
            return coctailDto;
        }
        public async Task<FavoriteDTO> CreateAsync(FavoriteDTO coctailDto) => await Task<FavoriteDTO>.Run(() => Create(coctailDto));
    }
}