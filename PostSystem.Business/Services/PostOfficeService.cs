using PostSystem.Business.DTO;
using PostSystem.Data;
using PostSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostSystem.Business.Services
{
    public class PostOfficeService : IService<PostOfficeDto>
    {
        public IEnumerable<PostOfficeDto> GetAll(int city = 0)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var postOffices = city == 0 ?
                    unitOfWork.PostOfficeRepository.GetAll() :
                    unitOfWork.PostOfficeRepository.GetAll(p => p.Office_City.Id == city);

                return postOffices.Select(postOffice => GenerateDto(postOffice)).ToList();
            }
        }

        public PostOfficeDto GetById(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var postOffice = unitOfWork.PostOfficeRepository.GetById(id);

                return postOffice == null ? null : GenerateDto(postOffice);
            }
        }

        public bool Create(PostOfficeDto postOfficeDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var postOffice = new PostOffice();
                BindModel(postOffice, postOfficeDto, true);

                unitOfWork.PostOfficeRepository.Create(postOffice);

                return unitOfWork.Save();
            }
        }

        public bool Update(PostOfficeDto postOfficeDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var result = unitOfWork.PostOfficeRepository.GetById(postOfficeDto.Id);

                if (result == null)
                {
                    return false;
                }

                BindModel(result, postOfficeDto, false);

                unitOfWork.PostOfficeRepository.Update(result);

                return unitOfWork.Save();
            }
        }

        public bool Delete(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                PostOffice result = unitOfWork.PostOfficeRepository.GetById(id);

                if (result == null)
                {
                    return false;
                }

                unitOfWork.PostOfficeRepository.Delete(result);

                return unitOfWork.Save();
            }
        }

        static void BindModel(PostOffice office, PostOfficeDto source, bool isCreated)
        {
            office.Address = source.Address;
            office.City_Id = source.City_Id;
            office.Office_Post_Code = source.Office_Post_Code;
            office.Desk_Count = source.Desk_Count;
            office.Updated_On = DateTime.Now;
            if (isCreated)
                office.Created_On = DateTime.Now;
        }

        public static PostOfficeDto GenerateDto(PostOffice source)
        {
            return new PostOfficeDto
            {
                Id = source.Id,
                Address = source.Address,
                Office_City = CityService.GenerateDto(source.Office_City),
                Office_Post_Code = source.Office_Post_Code,
                Desk_Count = source.Desk_Count,
                Created_On = source.Created_On,
                Updated_On = source.Updated_On
            };
        }

    }
}
