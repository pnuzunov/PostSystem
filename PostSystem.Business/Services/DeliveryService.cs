using PostSystem.Business.DTO;
using PostSystem.Data;
using PostSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostSystem.Business.Services
{
    public class DeliveryService : IService<DeliveryDto>
    {
        
        public IEnumerable<DeliveryDto> GetAll(int officeCode = 0)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var deliveries = officeCode == 0 ?
                    unitOfWork.DeliveryRepository.GetAll() :
                    unitOfWork.DeliveryRepository.GetAll(
                        m => m.From_Delivery_Office.Office_Post_Code.ToString().Contains(officeCode.ToString()) ||
                        m.To_Delivery_Office.Office_Post_Code.ToString().Contains(officeCode.ToString()));

                return deliveries.Select(delivery => GenerateDto(delivery)).ToList();
            }
        }
        /*
        public IEnumerable<DeliveryDto> GetAll(string details = null)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var deliveries = details == null ?
                    unitOfWork.DeliveryRepository.GetAll() :
                    unitOfWork.DeliveryRepository.GetAll(m => m.Details.Contains(details));

                return deliveries.Select(delivery => GenerateDto(delivery)).ToList();
            }
        }
        */
        public DeliveryDto GetById(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var delivery = unitOfWork.DeliveryRepository.GetById(id);

                return delivery == null ? null : GenerateDto(delivery);
            }
        }

        public bool Create(DeliveryDto deliveryDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var delivery = new Delivery();

                BindModel(delivery, deliveryDto, true);

                unitOfWork.DeliveryRepository.Create(delivery);

                return unitOfWork.Save();
            }
        }

        public bool Update(DeliveryDto deliveryDto)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var result = unitOfWork.DeliveryRepository.GetById(deliveryDto.Id);

                if (result == null)
                {
                    return false;
                }

                BindModel(result, deliveryDto, false);

                unitOfWork.DeliveryRepository.Update(result);

                return unitOfWork.Save();
            }
        }

        public bool Delete(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Delivery result = unitOfWork.DeliveryRepository.GetById(id);

                if (result == null)
                {
                    return false;
                }

                unitOfWork.DeliveryRepository.Delete(result);

                return unitOfWork.Save();
            }
        }
        static void BindModel(Delivery delivery, DeliveryDto source, bool isCreated)
        {
            delivery.Details = source.Details;
            delivery.Express_Delivery = source.Express_Delivery;
            delivery.Tax = source.Tax;
            delivery.Mail_Id = source.Mail_Id;
            delivery.From_Office_Id = source.From_Office_Id;
            delivery.To_Office_Id = source.To_Office_Id;
            delivery.Updated_On = DateTime.Now;
            if (isCreated)
                delivery.Created_On = DateTime.Now;
        }

        public static DeliveryDto GenerateDto(Delivery source)
        {
            return new DeliveryDto
            {
                Id = source.Id,

                Details = source.Details,
                Express_Delivery = source.Express_Delivery,
                Tax = source.Tax,
                Delivery_Mail = MailService.GenerateDto(source.Delivery_Mail),
                From_Delivery_Office = PostOfficeService.GenerateDto(source.From_Delivery_Office),
                To_Delivery_Office = PostOfficeService.GenerateDto(source.To_Delivery_Office),
                Created_On = source.Created_On,
                Updated_On = source.Updated_On
            };
        }

    }
}
