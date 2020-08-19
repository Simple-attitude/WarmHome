namespace DB.ISerives
{
    public class HouseSearchOptions
    {
        public long CityId { get; set; }//城市  id
        public long TypeId { get; set; }//房源类型，可空
        public long? RegionId { get; set; }//区域，可空
        public int? StartMonthRent { get; set; }//起始月租，可空
        public int? EndMonthRent { get; set; }//结束月租，可空
        public HouseSearchOrderByType OrderByType { get; set; } = HouseSearchOrderByType.MonthRentAsc;//排序            方式
        public string Keywords { get; set; }//搜索关键字，可空
        public int PageSize { get; set; }//每页数据条数
        public int CurrentIndex { get; set; }//当前页码

    }
}