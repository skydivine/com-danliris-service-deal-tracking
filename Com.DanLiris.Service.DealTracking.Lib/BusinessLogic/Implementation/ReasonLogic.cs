using Com.DanLiris.Service.DealTracking.Lib.Models;
using Com.DanLiris.Service.DealTracking.Lib.Utilities;
using Com.DanLiris.Service.DealTracking.Lib.Utilities.BaseClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.DanLiris.Service.DealTracking.Lib.BusinessLogic.Implementation
{
    public class ReasonLogic : BaseLogic<Reason>
    {
        public ReasonLogic(IServiceProvider serviceProvider, DealTrackingDbContext dbContext) : base(serviceProvider, dbContext)
        {
        }

        public override Tuple<List<Reason>, int, Dictionary<string, string>, List<string>> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<Reason> Query = this.DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "Code", "LoseReason"
            };

            Query = QueryHelper<Reason>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<Reason>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "Code", "LoseReason"
            };

            Query = Query
                .Select(field => new Reason
                {
                    Id = field.Id,
                    Code = field.Code,
                    LoseReason = field.LoseReason,
                    LastModifiedUtc = field.LastModifiedUtc
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<Reason>.Order(Query, OrderDictionary);

            List<Reason> Data = Query.Skip((page - 1) * size).Take(size).ToList();
            int TotalData = Query.Count();

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }
    }
}
