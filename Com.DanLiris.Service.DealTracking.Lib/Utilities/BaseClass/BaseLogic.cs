using Com.DanLiris.Service.DealTracking.Lib.Services;
using Com.DanLiris.Service.DealTracking.Lib.Utilities.BaseInterface;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.DealTracking.Lib.Utilities.BaseClass
{
    public abstract class BaseLogic<TModel> : IBaseLogic<TModel>
       where TModel : BaseModel
    {
        protected DbSet<TModel> DbSet;
        protected IdentityService IdentityService;
  
        public BaseLogic(IServiceProvider serviceProvider, DealTrackingDbContext dbContext)
        {
            this.DbSet = dbContext.Set<TModel>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
        }

        public abstract Tuple<List<TModel>, int, Dictionary<string, string>, List<string>> Read(int page, int size, string order, List<string> select, string keyword, string filter);

        public virtual void Create(TModel model)
        {
            EntityExtension.FlagForCreate(model, IdentityService.Username, "deal-tracking-service");
            DbSet.Add(model);
        }

        public virtual Task<TModel> ReadById(long id)
        {
            return DbSet.FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
        }

        public virtual void Update(long id, TModel model)
        {
            EntityExtension.FlagForUpdate(model, IdentityService.Username, "deal-tracking-service");
            DbSet.Update(model);
        }

        public virtual async Task Delete(long id)
        {
            TModel model = await ReadById(id);
            EntityExtension.FlagForDelete(model, IdentityService.Username, "deal-tracking-service", true);
            DbSet.Update(model);
        }
    }
}
