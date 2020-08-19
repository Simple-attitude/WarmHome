using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmHome.DTO;

namespace DB.ISerives
{
    public interface ICommunityService
    {
        IEnumerable<CommunityDTO> GetByRegionId(long regionId);
        IEnumerable<CommunityDTO> GetAll();
        CommunityDTO GetById(long id);
        void AddCommunity(CommunityDTO community);
        void UpdateCommunity(CommunityDTO community);
        void Deleted(long id);
    }
}
