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
    public class ActivityLogic : BaseLogic<Activity>
    {
        public ActivityLogic(IServiceProvider serviceProvider, DealTrackingDbContext dbContext) : base(serviceProvider, dbContext)
        {
        }

        public override Tuple<List<Activity>, int, Dictionary<string, string>, List<string>> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<Activity> Query = this.DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                "Code"
            };

            Query = QueryHelper<Activity>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<Activity>.Filter(Query, FilterDictionary);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "Code", "Type", "CreatedUtc", "CreatedBy", "TaskTitle", "Status", "Notes", "AssignedTo", "DueDate", "StageFromId", "StageFromName", "StageToId", "StageToName", "Attachments"
            };

            Query = Query
                .Select(field => new Activity
                {
                    Id = field.Id,
                    Code = field.Code,
                    Type = field.Type,
                    CreatedUtc = field.CreatedUtc,
                    CreatedBy = field.CreatedBy,
                    TaskTitle = field.TaskTitle,
                    Status = field.Status,
                    Notes = field.Notes,
                    AssignedTo = field.AssignedTo,
                    DueDate = field.DueDate,
                    StageFromId = field.StageFromId,
                    StageFromName = field.StageFromName,
                    StageToId = field.StageToId,
                    StageToName = field.StageToName,
                    Attachments = field.Attachments,
                    LastModifiedUtc = field.LastModifiedUtc
                });

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<Activity>.Order(Query, OrderDictionary);

            List<Activity> Data = Query.Skip((page - 1) * size).Take(size).ToList();
            int TotalData = Query.Count();

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public override Task<Activity> ReadById(long id)
        {
            return DbSet.Where(d => d.Id.Equals(id) && d.IsDeleted.Equals(false))
                .Include(p => p.Attachments)
                .FirstOrDefaultAsync();
        }
    }
}
