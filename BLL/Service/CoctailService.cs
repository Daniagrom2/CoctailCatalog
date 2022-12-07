using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Context;
using DAL.Repository;

namespace BLL.Service
{
    public class CoctailService
    {
        private CoctailRepository coctailRepository;

        public CoctailService(CoctailRepository coctailRepository)
        {
            this.coctailRepository = coctailRepository;
        }
        public async Task<IEnumerable<CoctailDTO>> GetAllAsync() => await Task.Run(() => GetAll());

        public IEnumerable<CoctailDTO> GetAll()
        {
            return coctailRepository
                .GetAll()
                .Select(coctail => new CoctailDTO()
                {
                    Id = coctail.CoctailId,
                    Name = coctail.CoctailName,
                    GlassName = coctail.GlassName,
                    Image = coctail.CoctailImage,
                    Favorite = Convert.ToBoolean(coctail.Favorite),
                    Type = coctail.CoctailType,
                    Ingridients = coctail.Ingridients.Split(new []{','},StringSplitOptions.RemoveEmptyEntries).ToList(),
                    Instructions = coctail.Instructions

                });
        }
        public CoctailDTO Get(int id)
        {
            var coctail = coctailRepository.Get(id);
            return new CoctailDTO()
            {
                Id = coctail.CoctailId,
                Name = coctail.CoctailName,
                GlassName = coctail.GlassName,
                Image = coctail.CoctailImage,
                Favorite = Convert.ToBoolean(coctail.Favorite),
                Type = coctail.CoctailType,
                Ingridients = coctail.Ingridients.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList(),
                Instructions = coctail.Instructions
            };
        }

        public async Task<CoctailDTO> GetAsync(int id) => await Task<CoctailDTO>.Run(() => Get(id));

        public void Delete(CoctailDTO coctailDto)
        {
            var coctailToDelete = coctailRepository.Get(coctailDto.Id);
            coctailRepository.Delete(coctailToDelete);
            coctailRepository.Save();
        }

        public async Task DeleteAsync(CoctailDTO coctailDto) => await Task.Run(() => Delete(coctailDto));
        public void Update(CoctailDTO coctailDto)
        {
            var coctailToUpdate = coctailRepository.Get(coctailDto.Id);

            coctailToUpdate.CoctailName = coctailDto.Name;
            coctailToUpdate.GlassName = coctailDto.GlassName;
            coctailToUpdate.CoctailImage = coctailDto.Image;
            coctailToUpdate.Ingridients = string.Join(",", coctailDto.Ingridients);
            coctailToUpdate.Instructions = coctailDto.Instructions;
            coctailToUpdate.CoctailType = coctailDto.Type;
            coctailToUpdate.Favorite = Convert.ToInt32(coctailDto.Favorite);
            coctailRepository.CreateOrUpdate(coctailToUpdate);
            coctailRepository.Save();
        }

        public async Task UpdateAsync(CoctailDTO filmDto) => await Task.Run(() => Update(filmDto));

        public CoctailDTO Create(CoctailDTO coctailDto)
        {
            var coctail = new DAL.Context.Coctail()
            {
                CoctailName = coctailDto.Name,
                GlassName = coctailDto.GlassName,
                CoctailImage = coctailDto.Image,
                Favorite = Convert.ToInt32(coctailDto.Favorite),
                CoctailType = coctailDto.Type,
                Ingridients = string.Join(",", coctailDto.Ingridients),
                Instructions = coctailDto.Instructions
            };
            coctailRepository.CreateOrUpdate(coctail);
            coctailRepository.Save();
            coctailDto.Id = coctail.CoctailId;
            return coctailDto;
        }
        public async Task<CoctailDTO> CreateAsync(CoctailDTO coctailDto) => await Task<CoctailDTO>.Run(() => Create(coctailDto));
    }
}