using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmHome.DTO;

namespace DB.IService
{
   public  interface IIdNameService
    {
        IEnumerable<IdNameDTO> GetAll(string typeName);
        long AddIdName(string typeName, string name);
        IdNameDTO GetById(long id);
    }
}
