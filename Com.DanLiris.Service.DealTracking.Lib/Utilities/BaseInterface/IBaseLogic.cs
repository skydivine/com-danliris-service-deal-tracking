using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.DealTracking.Lib.Utilities.BaseInterface
{
    public interface IBaseLogic<TModel>
    {
        Tuple<List<TModel>, int, Dictionary<string, string>, List<string>> Read(int page, int size, string order, List<string> select, string keyword, string filter);
        void Create(TModel model);
        Task<TModel> ReadById(long id);
        void Update(long id, TModel model);
        Task Delete(long id);
    }
}
