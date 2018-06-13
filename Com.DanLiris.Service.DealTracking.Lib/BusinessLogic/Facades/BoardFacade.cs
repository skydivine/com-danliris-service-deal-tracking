using Com.DanLiris.Service.DealTracking.Lib.BusinessLogic.Implementation;
using Com.DanLiris.Service.DealTracking.Lib.Models;
using Com.DanLiris.Service.DealTracking.Lib.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Com.DanLiris.Service.DealTracking.Lib.BusinessLogic.Interfaces;
using Com.DanLiris.Service.DealTracking.Lib.Utilities;
using System.Linq;

namespace Com.DanLiris.Service.DealTracking.Lib.BusinessLogic.Facades
{
    public class BoardFacade : IBoardFacade
    {
        private readonly DealTrackingDbContext DbContext;
        private readonly DbSet<Board> DbSet;
        private IdentityService IdentityService;
        private BoardLogic BoardLogic;

        public BoardFacade(IServiceProvider serviceProvider, DealTrackingDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<Board>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.BoardLogic = serviceProvider.GetService<BoardLogic>();
        }

        public async Task<int> Create(Board model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (this.DbSet.Any(d => d.Code.Equals(model.Code)));

            BoardLogic.Create(model);
            return await DbContext.SaveChangesAsync();
        }

        public Tuple<List<Board>, int, Dictionary<string, string>, List<string>> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return BoardLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<Board> ReadById(long id)
        {
            return await BoardLogic.ReadById(id);
        }

        public async Task<int> Update(long id, Board model)
        {
            BoardLogic.Update(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(long id)
        {
            await BoardLogic.Delete(id);
            return await DbContext.SaveChangesAsync();
        }
    }
}
