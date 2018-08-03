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
    public class BoardLogic : BaseLogic<Board>
    {
        public BoardLogic(IServiceProvider serviceProvider, DealTrackingDbContext dbContext) : base(serviceProvider, dbContext)
        {
        }

        public override Tuple<List<Board>, int, Dictionary<string, string>, List<string>> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<Board> Query = this.DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "Code"
            };

            Query = QueryHelper<Board>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<Board>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "Code", "Title"
            };

            Query = Query
                .Select(field => new Board
                {
                    Id = field.Id,
                    Code = field.Code,
                    Title = field.Title,
                    LastModifiedUtc = field.LastModifiedUtc
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<Board>.Order(Query, OrderDictionary);

            /*
            List<Board> Data = Query.Skip((page - 1) * size).Take(size).ToList();
            int TotalData = DbSet.Count();
            */

            List<Board> Data = Query.ToList();
            int TotalData = Query.Count();

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }
    }
}
