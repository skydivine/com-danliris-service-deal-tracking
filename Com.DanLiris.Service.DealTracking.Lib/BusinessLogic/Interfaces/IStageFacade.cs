using Com.DanLiris.Service.DealTracking.Lib.Models;
using Com.DanLiris.Service.DealTracking.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.DanLiris.Service.DealTracking.Lib.BusinessLogic.Interfaces
{
    public interface IStageFacade : IBaseFacade<Stage>
    {
		Task<Tuple<int, int, int>> UpdateDealStage();
	}
}
