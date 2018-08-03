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
    public class ContactFacade : IContactFacade
    {
        private readonly DealTrackingDbContext DbContext;
        private readonly DbSet<Contact> DbSet;
        private IdentityService IdentityService;
        private ContactLogic ContactLogic;

        public ContactFacade(IServiceProvider serviceProvider, DealTrackingDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<Contact>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.ContactLogic = serviceProvider.GetService<ContactLogic>();
        }

        public async Task<int> Create(Contact model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (this.DbSet.Any(d => d.Code.Equals(model.Code)));

            model.Company = null;

            ContactLogic.Create(model);
            return await DbContext.SaveChangesAsync();
        }

        public Tuple<List<Contact>, int, Dictionary<string, string>, List<string>> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return ContactLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<Contact> ReadById(long id)
        {
            return await ContactLogic.ReadById(id);
        }

        public async Task<int> Update(long id, Contact model)
        {
            model.Company = null;
            ContactLogic.Update(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(long id)
        {
            await ContactLogic.Delete(id);
            return await DbContext.SaveChangesAsync();
        }
    }
}
