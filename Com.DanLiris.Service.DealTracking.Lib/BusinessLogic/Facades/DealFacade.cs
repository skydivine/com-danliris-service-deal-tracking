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
using Com.DanLiris.Service.DealTracking.Lib.ViewModels;

namespace Com.DanLiris.Service.DealTracking.Lib.BusinessLogic.Facades
{
    public class DealFacade : IDealFacade
    {
        private readonly DealTrackingDbContext DbContext;
        private readonly DbSet<Deal> DbSet;
        private IdentityService IdentityService;
        private DealLogic DealLogic;
        private ActivityLogic ActivityLogic;
        private StageFacade StageFacade;
        private StageLogic StageLogic;

        public DealFacade(IServiceProvider serviceProvider, DealTrackingDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<Deal>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.DealLogic = serviceProvider.GetService<DealLogic>();
            this.ActivityLogic = serviceProvider.GetService<ActivityLogic>();
            this.StageFacade = serviceProvider.GetService<StageFacade>();
            this.StageLogic = serviceProvider.GetService<StageLogic>();
        }

        public async Task<int> Create(Deal model)
        {
            int Count = 0;

            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    do
                    {
                        model.Code = CodeGenerator.Generate();
                    }
                    while (this.DbSet.Any(d => d.Code.Equals(model.Code)));
                    DealLogic.Create(model);

                    Activity activity = new Activity() { DealId = model.Id, DealCode = model.Code, DealName = model.Name, Type = "ADD" };

                    do
                    {
                        activity.Code = CodeGenerator.Generate();
                    }
                    while (this.DbContext.DealTrackingActivities.Any(d => d.Code.Equals(activity.Code)));
                    ActivityLogic.Create(activity);

                    Count += await DbContext.SaveChangesAsync();
                    Count += await StageFacade.UpdateDealsOrderCreate(model.StageId, model.Id);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }

                transaction.Commit();
            }

            return Count;
        }

        public Tuple<List<Deal>, int, Dictionary<string, string>, List<string>> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            return DealLogic.Read(page, size, order, select, keyword, filter);
        }

        public async Task<Deal> ReadById(long id)
        {
            return await DealLogic.ReadById(id);
        }

        public async Task<int> Update(long id, Deal model)
        {
            DealLogic.Update(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(long id)
        {
            long stageId = this.DbSet.Single(p => p.Id == id).StageId;

            HashSet<long> activitiesId = new HashSet<long>(this.DbContext.DealTrackingActivities
                   .Where(p => p.DealId.Equals(id))
                   .Select(p => p.Id));

            List<Task> tasks = new List<Task>();

            foreach (long activityId in activitiesId)
            {
                tasks.Add(ActivityLogic.Delete(activityId));
            }

            await Task.WhenAll();
            await DealLogic.Delete(id);

            StageLogic.UpdateDealsOrderDelete(stageId, id);

            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> MoveActivity(MoveActivityViewModel viewModel)
        {
            int Count = 0;

            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    switch (viewModel.Type)
                    {
                        case "Order":
                            StageLogic.UpdateDealsOrder(viewModel.SourceStageId, viewModel.SourceDealsOrder);
                            Count = await DbContext.SaveChangesAsync();
                            break;
                        case "Move":
                            StageLogic.UpdateDealsOrder(viewModel.SourceStageId, viewModel.SourceDealsOrder);
                            StageLogic.UpdateDealsOrder(viewModel.TargetStageId, viewModel.TargetDealsOrder);

                            Deal model = await ReadById(viewModel.DealId);
                            model.StageId = viewModel.TargetStageId;
                            await this.Update(viewModel.DealId, model);

                            Activity activity = new Activity()
                            {
                                DealId = model.Id,
                                DealCode = model.Code,
                                DealName = model.Name,
                                Type = "MOVE",
                                StageFromId = viewModel.SourceStageId,
                                StageFromName = viewModel.SourceStageName,
                                StageToId = viewModel.TargetStageId,
                                StageToName = viewModel.TargetStageName
                            };

                            do
                            {
                                activity.Code = CodeGenerator.Generate();
                            }
                            while (this.DbContext.DealTrackingActivities.Any(d => d.Code.Equals(activity.Code)));
                            ActivityLogic.Create(activity);

                            Count = await DbContext.SaveChangesAsync();
                            break;
                    }
                }
                catch (DbUpdateConcurrencyException e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }

                transaction.Commit();
            }

            return Count;
        }
    }
}
