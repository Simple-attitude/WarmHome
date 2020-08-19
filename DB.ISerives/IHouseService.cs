using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmHome.DTO;

namespace DB.ISerives
{
    public interface IHouseService
    {
        HouseDTO GetById(long id);
        IEnumerable<HouseDTO> GetAll(long cityId, long typeId);
        long GetTotalCout(long cityId, long typeId);
        //分页获取 typeId这种房源类别下   cityId这个城市中房源
        IEnumerable<HouseDTO> GetPagedData(long cityId, long typeId, int pageSize, int currentIndex);
        //新增房源，返回房源  id
        long AddNew(HouseAddNewDTO house);
        //更新房源，房源的附件先删除再新增
        void Update(HouseDTO house);
        //软删除
        void MarkDeleted(long id);
        //得到房源的图片
        IEnumerable<HousePicDTO> GetPics(long houseId);
        //添加房源图片
        long AddNewHousePic(HousePicDTO housePic);
        //软删除房源图片
        void DeleteHousePic(long housePicId);
        //搜索，返回值包含：总条数和 HouseDTO[]两个属性
        HouseSearchResult Search(HouseSearchOptions options);
        int GetCount(long cityId, DateTime startDateTime, DateTime endDateTime);

    }
}
