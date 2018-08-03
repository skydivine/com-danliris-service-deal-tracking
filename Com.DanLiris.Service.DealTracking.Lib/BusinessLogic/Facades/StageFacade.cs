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

namespace Com.DanLiris.Service.DealTracking.Lib.BusinessLogic.Facades
{
    public class StageFacade : IStageFacade
    {
        private readonly DealTrackingDbContext DbContext;
        private readonly DbSet<Stage> DbSet;
        private IdentityService IdentityService;
        private StageLogic StageLogic;

        public StageFacade(IServiceProvider serviceProvider, DealTrackingDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<Stage>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.StageLogic = serviceProvider.GetService<StageLogic>();
        }

        public async Task<int> Create(Stage model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (this.DbSet.Any(d => d.Code.Equals(model.Code)));

            StageLogic.Create(model);
            return await DbContext.SaveChangesAsync();
        }

        public Tuple<List<Stage>, int, Dictionary<string, string>, List<string>> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return StageLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<Stage> ReadById(long id)
        {
            return await StageLogic.ReadById(id);
        }

        public async Task<int> Update(long id, Stage model)
        {
            StageLogic.Update(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(long id)
        {
            await StageLogic.Delete(id);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateDealsOrderCreate(long id, long dealId)
        {
            StageLogic.UpdateDealsOrderCreate(id, dealId);
            return await DbContext.SaveChangesAsync();
        }
    }
}
