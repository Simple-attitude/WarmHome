using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmHome.DTO;

namespace DB.ISerives
{
    public interface IRegionService
    {
        RegionDTO GetById(long id);
        IEnumerable<RegionDTO> GetCityAll(long cityId);
        IEnumerable<RegionDTO> GetAll();
        void AddRegion(long cityId, string name);
        void UpdateRegion(long id,long cityId, string name);
        void Deleted(long id);
    }
}
