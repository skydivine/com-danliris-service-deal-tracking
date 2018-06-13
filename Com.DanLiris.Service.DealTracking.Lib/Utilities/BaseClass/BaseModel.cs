using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.DanLiris.Service.DealTracking.Lib.Utilities.BaseClass
{
    public abstract class BaseModel : StandardEntity<long>
    {
        public string UId { get; set; }
    }
}
