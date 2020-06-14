using PostSystem.Business.DTO;
using PostSystem.Data;
using PostSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PostSystem.Business.Services
{
    public class CityService : IService<CityDto>
    {
        public IEnumerable<CityDto> GetAll(int postCode = 0)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {

                IEnumerable<City> cities;
                if (postCode != 0)
                {
                    cities = unitOfWork.CityRepository.GetAll(city => city.City_Post_Code == postCode);
                }
                else cities = unitOfWork.CityRepository.GetAll();


                return cities.Select(city => GenerateDto(city));
            }
        }

        public CityDto GetById(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var city = unitOfWork.CityRepository.GetById(id);

                return city == null ? null : GenerateDto(city);
            }
        }

        public bool Create(CityDto cityDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var city = new City();

                BindModel(city, cityDto, true);

                unitOfWork.CityRepository.Create(city);

                return unitOfWork.Save();
            }
        }

        public bool Update(CityDto cityDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var result = unitOfWork.CityRepository.GetById(cityDto.Id);

                if (result == null)
                {
                    return false;
                }

                BindModel(result, cityDto, false);

                unitOfWork.CityRepository.Update(result);

                return unitOfWork.Save();
            }
        }

        public bool Delete(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                City result = unitOfWork.CityRepository.GetById(id);

                if (result == null)
                {
                    return false;
                }

                unitOfWork.CityRepository.Delete(result);

                return unitOfWork.Save();
            }
        }

        static void BindModel(City city, CityDto source, bool isCreated)
        {
            city.City_Name = source.City_Name;
            city.City_Post_Code = source.City_Post_Code;
            city.HasExpressDelivery = source.HasExpressDelivery;
            city.Updated_On = DateTime.Now;
            if(isCreated)
            {
                city.Created_On = DateTime.Now;
            }
        }

        public static CityDto GenerateDto(City source)
        {
            return new CityDto
            {
                Id = source.Id,
                City_Name = source.City_Name,
                City_Post_Code = source.City_Post_Code,
                HasExpressDelivery = source.HasExpressDelivery,
                Created_On = source.Created_On,
                Updated_On = source.Updated_On
            };
        }
    }
}
