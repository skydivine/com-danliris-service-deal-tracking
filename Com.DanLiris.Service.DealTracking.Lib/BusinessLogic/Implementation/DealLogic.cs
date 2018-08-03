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
    public class DealLogic : BaseLogic<Deal>
    {
        public DealLogic(IServiceProvider serviceProvider, DealTrackingDbContext dbContext) : base(serviceProvider, dbContext)
        {
        }

        public override Tuple<List<Deal>, int, Dictionary<string, string>, List<string>> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<Deal> Query = this.DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "Code", "Name"
            };

            Query = QueryHelper<Deal>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<Deal>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "Code", "Name", "Amount", "Uom", "Quantity", "CloseDate"
            };

            Query = Query
                .Select(field => new Deal
                {
                    Id = field.Id,
                    Code = field.Code,
                    Name = field.Name,
                    Amount = field.Amount,
                    UomUnit = field.UomUnit,
                    Quantity = field.Quantity,
                    CloseDate = field.CloseDate,
                    LastModifiedUtc = field.LastModifiedUtc
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<Deal>.Order(Query, OrderDictionary);

            /*
            List<Deal> Data = Query.Skip((page - 1) * size).Take(size).ToList();
            int TotalData = DbSet.Count();
            */

            List<Deal> Data = Query.ToList();
            int TotalData = Query.Count();

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }
    }
}
