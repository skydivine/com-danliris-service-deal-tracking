using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.DealTracking.Lib.Utilities.BaseInterface
{
    public interface IBaseFacade<TModel>
    {
        Tuple<List<TModel>, int, Dictionary<string, string>, List<string>> Read(int Page, int Size, string Order, List<string> Select, string Keyword, string Filter);
        Task<int> Create(TModel model);
        Task<TModel> ReadById(long id);
        Task<int> Update(long id, TModel model);
        Task<int> Delete(long id);
    }
}
