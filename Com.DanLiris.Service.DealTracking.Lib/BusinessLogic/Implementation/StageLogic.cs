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
    public class StageLogic : BaseLogic<Stage>
    {
        public StageLogic(IServiceProvider serviceProvider, DealTrackingDbContext dbContext) : base(serviceProvider, dbContext)
        {
        }

        public override Tuple<List<Stage>, int, Dictionary<string, string>, List<string>> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<Stage> Query = this.DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "Code"
            };

            Query = QueryHelper<Stage>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<Stage>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "Code", "Name", "Deals", "DealsOrder"
            };

            Query = Query
                .Select(field => new Stage
                {
                    Id = field.Id,
                    Code = field.Code,
                    Name = field.Name,
                    BoardId = field.BoardId,
                    Deals = field.Deals,
                    DealsOrder = field.DealsOrder,
                    CreatedUtc = field.CreatedUtc,
                    LastModifiedUtc = field.LastModifiedUtc
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<Stage>.Order(Query, OrderDictionary);

            /*
            List<Stage> Data = Query.Skip((page - 1) * size).Take(size).ToList();
            int TotalData = DbSet.Count();
            */

            List<Stage> Data = Query.ToList();
            int TotalData = Query.Count();

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public void UpdateDealsOrder(long id, string dealsOrder)
        {
            Stage model = this.DbSet.Single(p => p.Id.Equals(id));

            model.DealsOrder = dealsOrder;
            this.Update(id, model);
        }

        public void UpdateDealsOrderCreate(long id, long dealId)
        {
            Stage model = this.DbSet.Single(p => p.Id.Equals(id));

            if (model.DealsOrder != null)
            {
                List<string> dealsOrder = model.DealsOrder.Replace("[", "").Replace("]", "").Split(",").ToList();
                dealsOrder.Add(dealId.ToString());
                model.DealsOrder = string.Concat("[", string.Join(",", dealsOrder), "]");
            }
            else
            {
                model.DealsOrder = $"[{dealId}]";
            }

            this.Update(id, model);
        }

        public void UpdateDealsOrderDelete(long id, long dealId)
        {
            Stage model = this.DbSet.Single(p => p.Id.Equals(id));

            List<string> dealsOrder = model.DealsOrder.Replace("[", "").Replace("]", "").Split(",").ToList();
            string itemToRemove = dealsOrder.Single(p => p == dealId.ToString());
            dealsOrder.Remove(itemToRemove);

            if (dealsOrder.Count > 0)
            {
                model.DealsOrder = string.Concat("[", string.Join(",", dealsOrder), "]");
            }
            else
            {
                model.DealsOrder = null;
            }

            this.Update(id, model);
        }
    }
}
