using PostSystem.Business.DTO;
using PostSystem.Data;
using PostSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostSystem.Business.Services
{
    public class MailService : IService<MailItemDto>
    {
        public IEnumerable<MailItemDto> GetAll(decimal weight = 0)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                
                IEnumerable<MailItem> mails;
                if (weight != 0)
                {
                    mails = unitOfWork.MailRepository.GetAll(m => m.Weight == weight);
                }
                else mails = unitOfWork.MailRepository.GetAll();


                return mails.Select(mail => GenerateDto(mail)).ToList();
            }
        }

        public MailItemDto GetById(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var mail = unitOfWork.MailRepository.GetById(id);

                return mail == null ? null : GenerateDto(mail);
            }
        }

        public bool Create(MailItemDto mailDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var mail = new MailItem();

                BindModel(mail, mailDto, true);

                unitOfWork.MailRepository.Create(mail);

                return unitOfWork.Save();
            }
        }

        public bool Update(MailItemDto mailDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var result = unitOfWork.MailRepository.GetById(mailDto.Id);

                if (result == null)
                {
                    return false;
                }

                BindModel(result, mailDto, false);

                unitOfWork.MailRepository.Update(result);

                return unitOfWork.Save();
            }
        }

        public bool Delete(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                MailItem result = unitOfWork.MailRepository.GetById(id);

                if (result == null)
                {
                    return false;
                }

                unitOfWork.MailRepository.Delete(result);

                return unitOfWork.Save();
            }
        }

        static void BindModel(MailItem mail, MailItemDto source, bool isCreated)
        {
            mail.Description = source.Description;
            mail.Height = source.Height;
            mail.Width = source.Width;
            mail.Weight = source.Weight;
            mail.Length = source.Length;
            mail.Updated_On = DateTime.Now;
            if (isCreated)
                mail.Created_On = DateTime.Now;
        }

        public static MailItemDto GenerateDto(MailItem source)
        {
            return new MailItemDto
            {
                Id = source.Id,
                Height = source.Height,
                Length = source.Length,
                Weight = source.Weight,
                Width = source.Width,
                Description = source.Description,
                Created_On = source.Created_On,
                Updated_On = source.Updated_On
            };
        }

    }
}
