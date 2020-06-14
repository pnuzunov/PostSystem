using PostSystem.Business.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostSystem.Business.Services
{
    public interface IService<TDto> where TDto : BaseDto, new()
    {
        TDto GetById(int id);

        bool Create(TDto dto);

        bool Update(TDto dto);

        bool Delete(int id);
    }
}
