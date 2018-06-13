using Com.DanLiris.Service.DealTracking.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.DanLiris.Service.DealTracking.Lib.Models
{
    public class ActivityAttachment : BaseModel
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public virtual long ActivityId { get; set; }
        [ForeignKey("ActivityId")]
        public virtual Activity Activity { get; set; }
    }
}
