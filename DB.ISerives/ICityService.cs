using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmHome.DTO;

namespace DB.IService
{
    public interface ICityService
    {
        void Deleted(long id);
        long AddCity(string name);
        IEnumerable<CityDTO> GetAll();
        CityDTO GetById(long cityId);
        void Edit(long id, string name);
    }
}
