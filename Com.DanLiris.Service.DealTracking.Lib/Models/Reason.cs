using Com.DanLiris.Service.DealTracking.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.DanLiris.Service.DealTracking.Lib.Models
{
    public class Reason : BaseModel
    {
        [MaxLength(255)]
        public string Code { get; set; }
        [MaxLength(255)]
        public string LoseReason { get; set; }
    }
}
