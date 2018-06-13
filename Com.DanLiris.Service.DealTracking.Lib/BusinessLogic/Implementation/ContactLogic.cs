using Com.DanLiris.Service.DealTracking.Lib.Models;
using Com.DanLiris.Service.DealTracking.Lib.Utilities;
using Com.DanLiris.Service.DealTracking.Lib.Utilities.BaseClass;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.DealTracking.Lib.BusinessLogic.Implementation
{
    public class ContactLogic : BaseLogic<Contact>
    {
        public ContactLogic(IServiceProvider serviceProvider, DealTrackingDbContext dbContext) : base(serviceProvider, dbContext)
        {
        }

        public override Tuple<List<Contact>, int, Dictionary<string, string>, List<string>> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<Contact> Query = this.DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "Code", "Name"
            };

            Query = QueryHelper<Contact>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<Contact>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "Code", "Name", "Email", "PhoneNumber", "Company", "JobTitle"
            };

            Query = Query
                .Select(field => new Contact
                {
                    Id = field.Id,
                    Code = field.Code,
                    Name = field.Name,
                    Email = field.Email,
                    PhoneNumber = field.PhoneNumber,
                    Company = field.Company,
                    JobTitle = field.JobTitle,
                    LastModifiedUtc = field.LastModifiedUtc
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<Contact>.Order(Query, OrderDictionary);

            List<Contact> Data = Query.Skip((page - 1) * size).Take(size).ToList();
            int TotalData = Query.Count();

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public override Task<Contact> ReadById(long id)
        {
            return DbSet.Where(d => d.Id.Equals(id) && d.IsDeleted.Equals(false))
                .Include(d => d.Company)
                .FirstOrDefaultAsync();
        }
    }
}
