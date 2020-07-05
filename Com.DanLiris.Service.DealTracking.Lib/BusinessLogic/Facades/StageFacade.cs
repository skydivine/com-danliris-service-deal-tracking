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
using Newtonsoft.Json;

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

		public async Task<Tuple<int, int, int>> UpdateDealStage()
		{
			int result = 0;
			int countstring = 0;
			int countint = 0;
			DbContext.Database.SetCommandTimeout(300);
			using (var transaction = DbContext.Database.BeginTransaction())
			{

				try
				{
					var data = DbSet.IgnoreQueryFilters().Where(s => s.CreatedAgent == "manager");
					foreach (var item in data)
					{
						List<string> dealId = item.DealsOrder.Replace("[", "").Replace("]", "").Split(",").Select(d => d.Trim()).Where(e => !string.IsNullOrEmpty(e)).ToList();
						countstring += dealId.Count;
						var dealData = DbContext.DealTrackingDeals.IgnoreQueryFilters().Where(s => dealId.Contains(s.UId));
						countint += dealData.Count();
						if (dealId.Count != dealData.Count())
						{
							throw new Exception("err");
						}
						var newDealId = JsonConvert.SerializeObject(dealData.Select(s => s.Id));
						item.DealsOrder = newDealId;

						foreach (var detail in dealData)
						{
							detail.StageId = item.Id;
						}
						result += await DbContext.SaveChangesAsync();
					}
					transaction.Commit();
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					throw ex;
				}

			}
			return new Tuple<int, int, int>(result, countstring, countint);
		}

		public async Task<Tuple<int, int, int>> UpdateDealOrderStage()
		{
			int result = 0;
			int countstring = 0;
			int countint = 0;
			DbContext.Database.SetCommandTimeout(300);
			using (var transaction = DbContext.Database.BeginTransaction())
			{

				try
				{
					var data = DbSet.Include(s => s.Deals);
					foreach (var item in data)
					{
						List<string> dealId = item.DealsOrder != null ? item.DealsOrder.Replace("[", "").Replace("]", "").Split(",").Select(d => d.Trim()).Where(e => !string.IsNullOrEmpty(e)).ToList() : new List<string>();
						countstring += dealId.Count;

						countint += item.Deals.Count;

						var newDealId = JsonConvert.SerializeObject(item.Deals.Select(s => s.Id));
						item.DealsOrder = newDealId;

						result += await DbContext.SaveChangesAsync();
					}
					transaction.Commit();
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					throw ex;
				}

			}
			return new Tuple<int, int, int>(result, countstring, countint);
		}

		public async Task<int> UpdateStageActivity()
		{
			int result = 0;
			DbContext.Database.SetCommandTimeout(300);
			using (var transaction = DbContext.Database.BeginTransaction())
			{
				try
				{
					int index = 0;
					var data = DbContext.DealTrackingActivities.Include(s => s.Deal).ThenInclude(d => d.Stage).Where(s => s.Type == "MOVE");
					foreach (var item in data)
					{
						var stageFrom = DbContext.DealTrackingStages.AsNoTracking().FirstOrDefault(s => s.BoardId == item.Deal.Stage.BoardId && s.Name.ToLower() == item.StageFromName);
						index++;

						if (stageFrom != null)
						{
							item.StageFromId = stageFrom.Id;
						}
						else
						{
							throw new Exception("err");
						}

						var stageTo = DbContext.DealTrackingStages.AsNoTracking().FirstOrDefault(s => s.BoardId == item.Deal.Stage.BoardId && s.Name.ToLower() == item.StageToName);

						if (stageTo != null)
						{
							item.StageToId = stageTo.Id;
						}
						else
						{
							throw new Exception("err");
						}
						
						if(index % 1000 == 0)
						{
							result += await DbContext.SaveChangesAsync();
						}
					}
					result += await DbContext.SaveChangesAsync();
					transaction.Commit();
				}
				catch (Exception e)
				{
					transaction.Rollback();
					throw e;
				}
			}
			return result;
		}
	}
}
