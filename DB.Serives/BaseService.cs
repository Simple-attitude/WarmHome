using DB.Service.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Service
{
    class BaseService<T> where T:BaseEntity
    {
        private readonly WarmHomeContext db;
        public BaseService(WarmHomeContext db)
        {
            this.db = db;
        }
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns>返回数据集合</returns>
        public IQueryable<T> GetAll()
        {
                return db.Set<T>().Where(e=>e.IsDeleted==false);
        }
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public long GetConunt()
        {
            return GetAll().LongCount();
        }
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="startIndex">从某一条开始获取</param>
        /// <param name="count">获取多少条</param>
        /// <returns></returns>
        public IQueryable<T> GetPageData(int startIndex, int count)
        {
            return GetAll().OrderBy(e => e.CreateDateTime).Skip(startIndex).Take(count);
        }
        /// <summary>
        /// 返回一条数据
        /// </summary>
        /// <param name="id">查找的Id</param>
        /// <returns>返回数据</returns>
        public T GetById(long id)
        {
            return GetAll().Where(e => e.Id == id).SingleOrDefault();
        }
        public void MarkDeleted(long id)
        {
            var data = GetById(id);
            data.IsDeleted = true;
            db.SaveChanges();
        }
    }
}
