using System.Collections.Generic;
using WarmHome.DTO;

namespace DB.ISerives
{
    public class HouseSearchResult
    {
        public List<HouseDTO> result { get; set; }//当前页的数据
        public long totalCount { get; set; }//搜索的结果总条数

    }
}