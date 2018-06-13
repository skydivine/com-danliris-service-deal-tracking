using Com.DanLiris.Service.DealTracking.Lib.BusinessLogic.Implementation;
using Com.DanLiris.Service.DealTracking.Lib.BusinessLogic.Interfaces;
using Com.DanLiris.Service.DealTracking.Lib.Models;
using Com.DanLiris.Service.DealTracking.Lib.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Com.DanLiris.Service.DealTracking.Lib.Utilities;
using System.Linq;
using Com.Moonlay.Models;

namespace Com.DanLiris.Service.DealTracking.Lib.BusinessLogic.Facades
{
    public class ActivityFacade : IActivityFacade
    {
        private readonly IServiceProvider serviceProvider;
        private readonly DealTrackingDbContext DbContext;
        private readonly DbSet<Activity> DbSet;
        private IdentityService IdentityService;
        private ActivityLogic ActivityLogic;

        public ActivityFacade(IServiceProvider serviceProvider, DealTrackingDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<Activity>();
            this.serviceProvider = serviceProvider;
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.ActivityLogic = serviceProvider.GetService<ActivityLogic>();
        }

        public async Task<int> Create(Activity model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (this.DbSet.Any(d => d.Code.Equals(model.Code)));

            if (model.Attachments.Count > 0)
            {
                foreach (ActivityAttachment attachment in model.Attachments)
                {
                    EntityExtension.FlagForCreate(attachment, IdentityService.Username, "deal-tracking-service");
                }
            }

            ActivityLogic.Create(model);
            return await DbContext.SaveChangesAsync();
        }

        public Tuple<List<Activity>, int, Dictionary<string, string>, List<string>> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return ActivityLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<Activity> ReadById(long id)
        {
            return await ActivityLogic.ReadById(id);
        }

        public async Task<int> Update(long id, Activity model)
        {
            ActivityLogic.Update(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(long id)
        {
            AzureStorageService azureStorageService = serviceProvider.GetService<AzureStorageService>();
            Activity model = await ActivityLogic.ReadById(id);

            if (model.Attachments != null)
            {
                foreach (ActivityAttachment attachment in model.Attachments)
                {
                    await azureStorageService.DeleteImage("Activity", attachment.FilePath);
                }
            }

            await ActivityLogic.Delete(id);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAttachment(long id)
        {
            ActivityAttachment model = await DbContext.DealTrackingActivityAttachments.FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
            EntityExtension.FlagForDelete(model, IdentityService.Username, "deal-tracking-service", true);
            DbContext.DealTrackingActivityAttachments.Update(model);

            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> CreateAttachment(long activityId, List<ActivityAttachment> attachments)
        {
            foreach(ActivityAttachment attachment in attachments)
            {
                attachment.ActivityId = activityId;
                EntityExtension.FlagForCreate(attachment, IdentityService.Username, "deal-tracking-service");
                DbContext.DealTrackingActivityAttachments.Add(attachment);
            }

            return await DbContext.SaveChangesAsync();
        }
    }
}
