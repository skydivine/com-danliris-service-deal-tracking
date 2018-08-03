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
    public class CompanyLogic : BaseLogic<Company>
    {
        public CompanyLogic(IServiceProvider serviceProvider, DealTrackingDbContext dbContext) : base(serviceProvider, dbContext)
        {
        }

        public override Tuple<List<Company>, int, Dictionary<string, string>, List<string>> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<Company> Query = this.DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "Code", "Name"
            };

            Query = QueryHelper<Company>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<Company>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "Code", "Name", "Website", "Industry", "PhoneNumber", "City"
            };

            Query = Query
                .Select(field => new Company
                {
                    Id = field.Id,
                    Code = field.Code,
                    Name = field.Name,
                    Website = field.Website,
                    Industry = field.Industry,
                    PhoneNumber = field.PhoneNumber,
                    City = field.City,
                    LastModifiedUtc = field.LastModifiedUtc
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<Company>.Order(Query, OrderDictionary);

            List<Company> Data = Query.Skip((page - 1) * size).Take(size).ToList();
            int TotalData = Query.Count();

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }
    }
}
