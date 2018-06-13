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
    public class CompanyFacade : ICompanyFacade
    {
        private readonly DealTrackingDbContext DbContext;
        private readonly DbSet<Company> DbSet;
        private IdentityService IdentityService;
        private CompanyLogic CompanyLogic;

        public CompanyFacade(IServiceProvider serviceProvider, DealTrackingDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<Company>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.CompanyLogic = serviceProvider.GetService<CompanyLogic>();
        }

        public async Task<int> Create(Company model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (this.DbSet.Any(d => d.Code.Equals(model.Code)));
            
            CompanyLogic.Create(model);
            return await DbContext.SaveChangesAsync();
        }

        public Tuple<List<Company>, int, Dictionary<string, string>, List<string>> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return CompanyLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<Company> ReadById(long id)
        {
            return await CompanyLogic.ReadById(id);
        }

        public async Task<int> Update(long id, Company model)
        {
            CompanyLogic.Update(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(long id)
        {
            await CompanyLogic.Delete(id);
            return await DbContext.SaveChangesAsync();
        }
    }
}
