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
    public class ReasonFacade : IReasonFacade
    {
        private readonly DealTrackingDbContext DbContext;
        private readonly DbSet<Reason> DbSet;
        private IdentityService IdentityService;
        private ReasonLogic ReasonLogic;

        public ReasonFacade(IServiceProvider serviceProvider, DealTrackingDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<Reason>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.ReasonLogic = serviceProvider.GetService<ReasonLogic>();
        }

        public async Task<int> Create(Reason model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (this.DbSet.Any(d => d.Code.Equals(model.Code)));

            ReasonLogic.Create(model);
            return await DbContext.SaveChangesAsync();
        }

        public Tuple<List<Reason>, int, Dictionary<string, string>, List<string>> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return ReasonLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<Reason> ReadById(long id)
        {
            return await ReasonLogic.ReadById(id);
        }

        public async Task<int> Update(long id, Reason model)
        {
            ReasonLogic.Update(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(long id)
        {
            await ReasonLogic.Delete(id);
            return await DbContext.SaveChangesAsync();
        }
    }
}
